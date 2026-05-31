using Microsoft.EntityFrameworkCore;
using WordleAPI.Application.Interfaces;
using WordleAPI.Domain.Entities;
using WordleAPI.Infrastructure.Data;

namespace WordleAPI.Infrastructure.Repositories;

public class StatisticRepository : IStatisticRepository
{
    private readonly ApplicationDbContext _context;

    public StatisticRepository(ApplicationDbContext context) => _context = context;

    public async Task<Statistic?> GetByUserIdAsync(Guid userId) =>
        await _context.Statistics.FirstOrDefaultAsync(s => s.UserId == userId);

    public async Task<Statistic> CreateAsync(Statistic statistic)
    {
        _context.Statistics.Add(statistic);
        await _context.SaveChangesAsync();
        return statistic;
    }

    public async Task<Statistic> UpdateAsync(Statistic statistic)
    {
        _context.Statistics.Update(statistic);
        await _context.SaveChangesAsync();
        return statistic;
    }
}