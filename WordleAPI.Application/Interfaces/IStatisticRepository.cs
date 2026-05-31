using WordleAPI.Domain.Entities;

namespace WordleAPI.Application.Interfaces;

public interface IStatisticRepository
{
    Task<Statistic?> GetByUserIdAsync(Guid userId);
    Task<Statistic> CreateAsync(Statistic statistic);
    Task<Statistic> UpdateAsync(Statistic statistic);
}