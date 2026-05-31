using Microsoft.EntityFrameworkCore;
using WordleAPI.Application.Interfaces;
using WordleAPI.Domain.Entities;
using WordleAPI.Infrastructure.Data;

namespace WordleAPI.Infrastructure.Repositories;

public class GuessRepository : IGuessRepository
{
    private readonly ApplicationDbContext _context;

    public GuessRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guess> CreateAsync(Guess guess)
    {
        _context.Guesses.Add(guess);
        await _context.SaveChangesAsync();
        return guess;
    }

    public async Task<List<Guess>> GetByGameIdAsync(Guid gameId)
    {
        return await _context.Guesses
            .Where(g => g.GameId == gameId)
            .OrderBy(g => g.GuessNumber)
            .ToListAsync();
    }
}
