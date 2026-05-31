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
    private readonly IStatisticService _statisticService;

    public GameService(
        IGameRepository gameRepository,
        IGuessRepository guessRepository,
        IWordService wordService,
        IStatisticService statisticService)
    {
        _gameRepository = gameRepository;
        _guessRepository = guessRepository;
        _wordService = wordService;
        _statisticService = statisticService;
    }

    public async Task<StartGameResponse> StartGameAsync(Guid userId)
    {
        var targetWord = _wordService.GetRandomWord();

        var game = new Game
        {
            UserId = userId,
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

    public async Task<GuessResponse> MakeGuessAsync(GuessRequest request, Guid userId)
    {
        var game = await _gameRepository.GetByIdAsync(request.GameId)
            ?? throw new KeyNotFoundException($"Game with ID {request.GameId} not found.");

        if (game.UserId != userId)
            throw new UnauthorizedAccessException("You do not have access to this game.");

        if (game.Status != GameStatus.InProgress)
            throw new InvalidOperationException(
                game.Status == GameStatus.Won
                    ? "This game has already been won."
                    : "This game is over. No more attempts allowed.");

        var word = request.Word.Trim().ToUpper();
        if (word.Length != Game.WordLength)
            throw new ArgumentException($"Word must be exactly {Game.WordLength} letters long.");

        if (!word.All(char.IsLetter))
            throw new ArgumentException("Word must contain only letters.");

        if (game.Attempts >= Game.MaxAttempts)
            throw new InvalidOperationException("Maximum number of attempts reached.");

        var letterResults = EvaluateGuess(word, game.TargetWord);
        var isCorrect = word == game.TargetWord;

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

        // Update statistics when game ends
        if (game.Status != GameStatus.InProgress)
            await _statisticService.UpdateStatisticsAsync(userId, isWin: isCorrect, attemptsUsed: guessNumber);

        var attemptsRemaining = Game.MaxAttempts - game.Attempts;

        string message = isCorrect
            ? $"🎉 Correct! You guessed the word in {guessNumber} attempt(s)!"
            : game.Status == GameStatus.Lost
                ? $"Game over! The word was: {game.TargetWord}"
                : $"Wrong guess. {attemptsRemaining} attempt(s) remaining.";

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

    public async Task<IEnumerable<GameSummaryDto>> GetUserGamesAsync(Guid userId)
    {
        var games = await _gameRepository.GetByUserIdAsync(userId);
        return games.Select(g => new GameSummaryDto
        {
            Id = g.Id,
            StartDate = g.StartDate,
            EndDate = g.EndDate,
            Attempts = g.Attempts,
            IsWin = g.IsWin,
            Status = g.Status.ToString()
        });
    }

    public async Task<GameDetailDto> GetGameByIdAsync(Guid gameId, Guid userId)
    {
        var game = await _gameRepository.GetByIdWithGuessesAsync(gameId)
            ?? throw new KeyNotFoundException($"Game {gameId} not found.");

        if (game.UserId != userId)
            throw new UnauthorizedAccessException("You do not have access to this game.");

        return new GameDetailDto
        {
            Id = game.Id,
            StartDate = game.StartDate,
            EndDate = game.EndDate,
            Attempts = game.Attempts,
            IsWin = game.IsWin,
            Status = game.Status.ToString(),
            Guesses = game.Guesses
                .OrderBy(g => g.GuessNumber)
                .Select(g => new GuessSummaryDto
                {
                    GuessNumber = g.GuessNumber,
                    Word = g.Word,
                    GuessResult = g.GuessResult
                }).ToList()
        };
    }

    private static List<LetterResult> EvaluateGuess(string guess, string target)
    {
        var results = new LetterResult[Game.WordLength];
        var targetLetterCounts = new Dictionary<char, int>();

        for (int i = 0; i < Game.WordLength; i++)
        {
            if (guess[i] == target[i])
            {
                results[i] = new LetterResult { Letter = guess[i], Position = i, Status = LetterStatus.Correct };
            }
            else
            {
                if (!targetLetterCounts.ContainsKey(target[i]))
                    targetLetterCounts[target[i]] = 0;
                targetLetterCounts[target[i]]++;
                results[i] = new LetterResult { Letter = guess[i], Position = i, Status = LetterStatus.Absent };
            }
        }

        for (int i = 0; i < Game.WordLength; i++)
        {
            if (results[i].Status == LetterStatus.Correct) continue;
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