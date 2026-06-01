using System.Text.Json;
using WordleAPI.Application.DTOs;
using WordleAPI.Application.Interfaces;
using WordleAPI.Domain.Entities;
using WordleAPI.Domain.Enums;

namespace WordleAPI.Application.Services;

public class GameService : IGameService
{
    private readonly IGameRepository _gameRepository;
    private readonly IGuessRepository _guessRepository;
    private readonly IWordService _wordService;

    public GameService(
        IGameRepository gameRepository,
        IGuessRepository guessRepository,
        IWordService wordService)
    {
        _gameRepository = gameRepository;
        _guessRepository = guessRepository;
        _wordService = wordService;
    }

    public async Task<StartGameResponse> StartGameAsync()
    {
        var targetWord = _wordService.GetRandomWord();

        var game = new Game
        {
            TargetWord = targetWord.ToUpper(),
            StartDate = DateTime.UtcNow,
            Attempts = 0,
            IsWin = false,
            Status = GameStatus.InProgress
        };

        var created = await _gameRepository.CreateAsync(game);

        return new StartGameResponse
        {
            GameId = created.Id,
            StartDate = created.StartDate,
            MaxAttempts = Game.MaxAttempts,
            WordLength = Game.WordLength,
            Message = "Game started! Guess the 5-letter word. You have 6 attempts."
        };
    }

    public async Task<GuessResponse> MakeGuessAsync(GuessRequest request)
    {
        // Validate game exists
        var game = await _gameRepository.GetByIdAsync(request.GameId)
            ?? throw new KeyNotFoundException($"Game with ID {request.GameId} not found.");

        // Validate game is still in progress
        if (game.Status != GameStatus.InProgress)
        {
            throw new InvalidOperationException(
                game.Status == GameStatus.Won
                    ? "This game has already been won."
                    : "This game is over. No more attempts allowed.");
        }

        // Validate word length
        var word = request.Word.Trim().ToUpper();
        if (word.Length != Game.WordLength)
            throw new ArgumentException($"Word must be exactly {Game.WordLength} letters long.");

        // Validate all letters
        if (!word.All(char.IsLetter))
            throw new ArgumentException("Word must contain only letters.");

        // Check attempts
        if (game.Attempts >= Game.MaxAttempts)
            throw new InvalidOperationException("Maximum number of attempts reached.");

        // Evaluate the guess
        var letterResults = EvaluateGuess(word, game.TargetWord);
        var isCorrect = word == game.TargetWord;

        // Persist the guess
        game.Attempts++;
        var guessNumber = game.Attempts;

        var guess = new Guess
        {
            GameId = game.Id,
            Word = word,
            GuessNumber = guessNumber,
            GuessResult = JsonSerializer.Serialize(letterResults.Select(r => new
            {
                letter = r.Letter.ToString(),
                position = r.Position,
                status = r.StatusText
            }))
        };

        await _guessRepository.CreateAsync(guess);

        // Update game state
        if (isCorrect)
        {
            game.IsWin = true;
            game.Status = GameStatus.Won;
            game.EndDate = DateTime.UtcNow;
        }
        else if (game.Attempts >= Game.MaxAttempts)
        {
            game.Status = GameStatus.Lost;
            game.EndDate = DateTime.UtcNow;
        }

        await _gameRepository.UpdateAsync(game);

        var attemptsRemaining = Game.MaxAttempts - game.Attempts;

        string message;
        if (isCorrect)
            message = $"🎉 Correct! You guessed the word in {guessNumber} attempt(s)!";
        else if (game.Status == GameStatus.Lost)
            message = $"Game over! The word was: {game.TargetWord}";
        else
            message = $"Wrong guess. {attemptsRemaining} attempt(s) remaining.";

        return new GuessResponse
        {
            GameId = game.Id,
            Word = word,
            GuessNumber = guessNumber,
            AttemptsRemaining = attemptsRemaining,
            LetterResults = letterResults,
            GameStatus = game.Status,
            IsCorrect = isCorrect,
            Message = message
        };
    }

    /// <summary>
    /// Core Wordle evaluation logic.
    /// Handles duplicate letters correctly:
    /// - Correct positions are marked first.
    /// - Remaining unmatched letters are marked as Present or Absent.
    /// </summary>
    private static List<LetterResult> EvaluateGuess(string guess, string target)
    {
        var results = new LetterResult[Game.WordLength];
        var targetLetterCounts = new Dictionary<char, int>();

        // First pass: mark correct letters and count remaining target letters
        for (int i = 0; i < Game.WordLength; i++)
        {
            if (guess[i] == target[i])
            {
                results[i] = new LetterResult
                {
                    Letter = guess[i],
                    Position = i,
                    Status = LetterStatus.Correct
                };
            }
            else
            {
                // Count target letters not yet matched
                if (!targetLetterCounts.ContainsKey(target[i]))
                    targetLetterCounts[target[i]] = 0;
                targetLetterCounts[target[i]]++;

                results[i] = new LetterResult
                {
                    Letter = guess[i],
                    Position = i,
                    Status = LetterStatus.Absent // default, may be updated
                };
            }
        }

        // Second pass: mark present letters
        for (int i = 0; i < Game.WordLength; i++)
        {
            if (results[i].Status == LetterStatus.Correct)
                continue;

            var letter = guess[i];
            if (targetLetterCounts.TryGetValue(letter, out int count) && count > 0)
            {
                results[i].Status = LetterStatus.Present;
                targetLetterCounts[letter]--;
            }
        }

        return results.ToList();
    }
}
