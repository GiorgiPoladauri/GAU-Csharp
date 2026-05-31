using WordleAPI.Application.DTOs;

namespace WordleAPI.Application.Interfaces;

public interface IGameService
{
    Task<StartGameResponse> StartGameAsync(Guid userId);
    Task<GuessResponse> MakeGuessAsync(GuessRequest request, Guid userId);
    Task<IEnumerable<GameSummaryDto>> GetUserGamesAsync(Guid userId);
    Task<GameDetailDto> GetGameByIdAsync(Guid gameId, Guid userId);
}