using WordleAPI.Application.DTOs;
using WordleAPI.Application.Interfaces;
using WordleAPI.Domain.Entities;

namespace WordleAPI.Application.Services;

public class StatisticService : IStatisticService
{
    private readonly IStatisticRepository _statisticRepository;
    private readonly IUserRepository _userRepository;

    public StatisticService(IStatisticRepository statisticRepository, IUserRepository userRepository)
    {
        _statisticRepository = statisticRepository;
        _userRepository = userRepository;
    }

    public async Task<StatisticResponse> GetStatisticsAsync(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId)
            ?? throw new KeyNotFoundException("User not found.");

        var stat = await _statisticRepository.GetByUserIdAsync(userId)
            ?? new Statistic { UserId = userId };

        return new StatisticResponse
        {
            UserId = userId,
            Email = user.Email,
            GamesPlayed = stat.GamesPlayed,
            Wins = stat.Wins,
            CurrentStreak = stat.CurrentStreak,
            MaxStreak = stat.MaxStreak,
            TotalPoints = stat.TotalPoints
        };
    }

    public async Task UpdateStatisticsAsync(Guid userId, bool isWin, int attemptsUsed)
    {
        var stat = await _statisticRepository.GetByUserIdAsync(userId);
        if (stat == null)
        {
            stat = new Statistic { UserId = userId };
            stat = await _statisticRepository.CreateAsync(stat);
        }

        stat.GamesPlayed++;

        if (isWin)
        {
            stat.Wins++;
            stat.CurrentStreak++;
            if (stat.CurrentStreak > stat.MaxStreak)
                stat.MaxStreak = stat.CurrentStreak;

            // Points: fewer attempts = more points
            // 6 attempts = 10 pts, 5 = 20, 4 = 30, 3 = 50, 2 = 75, 1 = 100
            stat.TotalPoints += attemptsUsed switch
            {
                1 => 100,
                2 => 75,
                3 => 50,
                4 => 30,
                5 => 20,
                _ => 10
            };
        }
        else
        {
            stat.CurrentStreak = 0;
        }

        await _statisticRepository.UpdateAsync(stat);
    }
}