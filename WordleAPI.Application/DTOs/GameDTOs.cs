using WordleAPI.Domain.Enums;

namespace WordleAPI.Application.DTOs;

public class StartGameResponse
{
    public Guid GameId { get; set; }
    public DateTime StartDate { get; set; }
    public int MaxAttempts { get; set; }
    public int WordLength { get; set; }
    public string Message { get; set; } = string.Empty;
}

public class GuessRequest
{
    public Guid GameId { get; set; }
    public string Word { get; set; } = string.Empty;
}

public class LetterResult
{
    public char Letter { get; set; }
    public int Position { get; set; }
    public LetterStatus Status { get; set; }
    public string StatusText => Status.ToString().ToLower();
}

public class GuessResponse
{
    public Guid GameId { get; set; }
    public string Word { get; set; } = string.Empty;
    public int GuessNumber { get; set; }
    public int AttemptsRemaining { get; set; }
    public List<LetterResult> LetterResults { get; set; } = new();
    public GameStatus GameStatus { get; set; }
    public string GameStatusText => GameStatus.ToString();
    public bool IsCorrect { get; set; }
    public string Message { get; set; } = string.Empty;
}
