namespace MedicalBilling.Application.Interfaces;

public interface IUserRepository
{
    Task<(string Username, string PasswordHash, string Role, int? DoctorId)?> GetUserAsync(string username);
    Task AddUserAsync(string username, string passwordHash, string role, int? doctorId);
    Task<bool> UserExistsAsync(string username);
}
