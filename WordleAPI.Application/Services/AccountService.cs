using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WordleAPI.Application.DTOs;
using WordleAPI.Application.Interfaces;
using WordleAPI.Domain.Entities;

namespace WordleAPI.Application.Services;

public class AccountService : IAccountService
{
    private readonly IUserRepository _userRepository;
    private readonly IStatisticRepository _statisticRepository;
    private readonly IConfiguration _configuration;

    public AccountService(
        IUserRepository userRepository,
        IStatisticRepository statisticRepository,
        IConfiguration configuration)
    {
        _userRepository = userRepository;
        _statisticRepository = statisticRepository;
        _configuration = configuration;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        var existing = await _userRepository.GetByEmailAsync(request.Email);
        if (existing != null)
            throw new InvalidOperationException("A user with this email already exists.");

        var user = new User
        {
            Email = request.Email.ToLower().Trim(),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
        };

        var created = await _userRepository.CreateAsync(user);

        // Create empty statistics for this user
        await _statisticRepository.CreateAsync(new Statistic { UserId = created.Id });

        return GenerateToken(created);
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email.ToLower().Trim())
            ?? throw new UnauthorizedAccessException("Invalid email or password.");

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid email or password.");

        return GenerateToken(user);
    }

    private AuthResponse GenerateToken(User user)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiresAt = DateTime.UtcNow.AddHours(
            double.Parse(jwtSettings["ExpiresInHours"] ?? "24"));

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: expiresAt,
            signingCredentials: credentials);

        return new AuthResponse
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Email = user.Email,
            UserId = user.Id,
            ExpiresAt = expiresAt
        };
    }
}