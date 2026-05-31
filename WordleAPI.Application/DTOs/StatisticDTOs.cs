namespace WordleAPI.Application.DTOs;

public class StatisticResponse
{
    public Guid UserId { get; set; }
    public string Email { get; set; } = string.Empty;
    public int GamesPlayed { get; set; }
    public int Wins { get; set; }
    public double WinRate => GamesPlayed == 0 ? 0 : Math.Round((double)Wins / GamesPlayed * 100, 1);
    public int CurrentStreak { get; set; }
    public int MaxStreak { get; set; }
    public int TotalPoints { get; set; }
}