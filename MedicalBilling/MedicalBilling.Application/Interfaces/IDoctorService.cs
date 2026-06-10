using MedicalBilling.Application.DTOs;

namespace MedicalBilling.Application.Interfaces;

public interface IDoctorService
{
    Task<IEnumerable<DoctorReadDto>> GetAllAsync();
    Task<DoctorReadDto?> GetByIdAsync(int id);
    Task<DoctorReadDto> CreateAsync(DoctorCreateDto dto);
    Task<DoctorVisitStatsDto> CountDoctorVisitsAsync(int doctorId);
}
