using MedicalBilling.Application.Interfaces;
using MedicalBilling.Infrastructure.Data;
using MedicalBilling.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicalBilling.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly HealthDbContext _context;
    public UserRepository(HealthDbContext context) => _context = context;

    public async Task<(string Username, string PasswordHash, string Role, int? DoctorId)?> GetUserAsync(string username)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        if (user == null) return null;
        return (user.Username, user.PasswordHash, user.Role, user.DoctorId);
    }

    public async Task AddUserAsync(string username, string passwordHash, string role, int? doctorId)
    {
        _context.Users.Add(new AppUser
        {
            Username = username,
            PasswordHash = passwordHash,
            Role = role,
            DoctorId = doctorId
        });
        await _context.SaveChangesAsync();
    }

    public async Task<bool> UserExistsAsync(string username) =>
        await _context.Users.AnyAsync(u => u.Username == username);
}
