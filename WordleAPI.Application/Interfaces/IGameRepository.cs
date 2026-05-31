using WordleAPI.Domain.Entities;

namespace WordleAPI.Application.Interfaces;

public interface IGameRepository
{
    Task<Game> CreateAsync(Game game);
    Task<Game?> GetByIdAsync(Guid id);
    Task<Game?> GetByIdWithGuessesAsync(Guid id);
    Task<IEnumerable<Game>> GetByUserIdAsync(Guid userId);
    Task<Game> UpdateAsync(Game game);
}