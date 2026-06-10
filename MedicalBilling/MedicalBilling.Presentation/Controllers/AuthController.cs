using MedicalBilling.Application.DTOs;
using MedicalBilling.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MedicalBilling.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _users;
    private readonly ITokenService _tokens;

    public AuthController(IUserRepository users, ITokenService tokens)
    {
        _users = users;
        _tokens = tokens;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var user = await _users.GetUserAsync(dto.Username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Value.PasswordHash))
            return Unauthorized(new { message = "Invalid username or password." });

        var token = _tokens.GenerateToken(user.Value.Username, user.Value.Role, user.Value.DoctorId);
        return Ok(new TokenResponseDto
        {
            Token = token,
            Username = user.Value.Username,
            Role = user.Value.Role,
            ExpiresAt = DateTime.UtcNow.AddMinutes(60)
        });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        if (await _users.UserExistsAsync(dto.Username))
            return BadRequest(new { message = $"Username '{dto.Username}' is already taken." });

        if (dto.Role != "admin" && dto.Role != "doctor")
            return BadRequest(new { message = "Role must be 'admin' or 'doctor'." });

        if (dto.Role == "doctor" && !dto.DoctorId.HasValue)
            return BadRequest(new { message = "DoctorId is required for doctor role." });

        await _users.AddUserAsync(dto.Username, BCrypt.Net.BCrypt.HashPassword(dto.Password), dto.Role, dto.DoctorId);
        return StatusCode(201, new { message = $"User '{dto.Username}' created with role '{dto.Role}'." });
    }
}
