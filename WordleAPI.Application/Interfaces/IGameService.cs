using WordleAPI.Application.DTOs;

namespace WordleAPI.Application.Interfaces;

public interface IGameService
{
    Task<StartGameResponse> StartGameAsync();
    Task<GuessResponse> MakeGuessAsync(GuessRequest request);
}
