using WordleAPI.Domain.Enums;

namespace WordleAPI.Domain.Entities;

public class Game
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public string TargetWord { get; set; } = string.Empty;
    public DateTime StartDate { get; set; } = DateTime.UtcNow;
    public DateTime? EndDate { get; set; }
    public int Attempts { get; set; } = 0;
    public bool IsWin { get; set; } = false;
    public GameStatus Status { get; set; } = GameStatus.InProgress;

    public User User { get; set; } = null!;
    public ICollection<Guess> Guesses { get; set; } = new List<Guess>();

    public const int MaxAttempts = 6;
    public const int WordLength = 5;
}