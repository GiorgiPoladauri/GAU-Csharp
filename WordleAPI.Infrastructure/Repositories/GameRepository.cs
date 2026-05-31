using Microsoft.EntityFrameworkCore;
using WordleAPI.Application.Interfaces;
using WordleAPI.Domain.Entities;
using WordleAPI.Infrastructure.Data;

namespace WordleAPI.Infrastructure.Repositories;

public class GameRepository : IGameRepository
{
    private readonly ApplicationDbContext _context;

    public GameRepository(ApplicationDbContext context) => _context = context;

    public async Task<Game> CreateAsync(Game game)
    {
        _context.Games.Add(game);
        await _context.SaveChangesAsync();
        return game;
    }

    public async Task<Game?> GetByIdAsync(Guid id) =>
        await _context.Games.FindAsync(id);

    public async Task<Game?> GetByIdWithGuessesAsync(Guid id) =>
        await _context.Games
            .Include(g => g.Guesses)
            .FirstOrDefaultAsync(g => g.Id == id);

    public async Task<IEnumerable<Game>> GetByUserIdAsync(Guid userId) =>
        await _context.Games
            .Where(g => g.UserId == userId)
            .OrderByDescending(g => g.StartDate)
            .ToListAsync();

    public async Task<Game> UpdateAsync(Game game)
    {
        _context.Games.Update(game);
        await _context.SaveChangesAsync();
        return game;
    }
}