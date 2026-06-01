using WordleAPI.Domain.Entities;

namespace WordleAPI.Application.Interfaces;

public interface IGameRepository
{
    Task<Game> CreateAsync(Game game);
    Task<Game?> GetByIdAsync(Guid id);
    Task<Game> UpdateAsync(Game game);
}
