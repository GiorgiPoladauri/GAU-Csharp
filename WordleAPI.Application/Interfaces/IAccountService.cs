using WordleAPI.Application.DTOs;

namespace WordleAPI.Application.Interfaces;

public interface IAccountService
{
    Task<AuthResponse> RegisterAsync(RegisterRequest request);
    Task<AuthResponse> LoginAsync(LoginRequest request);
}