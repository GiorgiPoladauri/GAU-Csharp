using WordleAPI.Application.DTOs;

namespace WordleAPI.Application.Interfaces;

public interface IStatisticService
{
    Task<StatisticResponse> GetStatisticsAsync(Guid userId);
    Task UpdateStatisticsAsync(Guid userId, bool isWin, int attemptsUsed);
}