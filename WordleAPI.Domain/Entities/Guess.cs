namespace WordleAPI.Domain.Entities;

public class Guess
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid GameId { get; set; }
    public string Word { get; set; } = string.Empty;
    public int GuessNumber { get; set; }
    public string GuessResult { get; set; } = string.Empty; // JSON serialized letter results

    public Game Game { get; set; } = null!;
}
