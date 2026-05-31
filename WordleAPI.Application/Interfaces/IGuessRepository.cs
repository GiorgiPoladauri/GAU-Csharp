using WordleAPI.Domain.Entities;

namespace WordleAPI.Application.Interfaces;

public interface IGuessRepository
{
    Task<Guess> CreateAsync(Guess guess);
    Task<List<Guess>> GetByGameIdAsync(Guid gameId);
}
