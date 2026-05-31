namespace WordleAPI.Domain.Entities;

public class Statistic
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public int GamesPlayed { get; set; } = 0;
    public int Wins { get; set; } = 0;
    public int CurrentStreak { get; set; } = 0;
    public int MaxStreak { get; set; } = 0;
    public int TotalPoints { get; set; } = 0;

    public User User { get; set; } = null!;
}