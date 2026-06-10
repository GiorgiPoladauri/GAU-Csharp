using MedicalBilling.Application.DTOs;
using MedicalBilling.Application.Interfaces;
using MedicalBilling.Domain.Entities;
using MedicalBilling.Domain.Interfaces;

namespace MedicalBilling.Application.Services;

public class DoctorService : IDoctorService
{
    private readonly IDoctorRepository _doctors;
    private readonly IVisitRepository _visits;

    public DoctorService(IDoctorRepository doctors, IVisitRepository visits)
    {
        _doctors = doctors;
        _visits = visits;
    }

    public async Task<IEnumerable<DoctorReadDto>> GetAllAsync()
    {
        var all = await _doctors.GetAllAsync();
        return all.Select(Map);
    }

    public async Task<DoctorReadDto?> GetByIdAsync(int id)
    {
        var d = await _doctors.GetByIdAsync(id);
        return d == null ? null : Map(d);
    }

    public async Task<DoctorReadDto> CreateAsync(DoctorCreateDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.FullName))
            throw new ArgumentException("FullName is required.");
        if (string.IsNullOrWhiteSpace(dto.Specialization))
            throw new ArgumentException("Specialization is required.");

        var entity = new Doctor
        {
            FullName = dto.FullName.Trim(),
            Specialization = dto.Specialization.Trim()
        };
        var created = await _doctors.AddAsync(entity);
        return Map(created);
    }

    // Named exactly as task requires: CountDoctorVisits
    public async Task<DoctorVisitStatsDto> CountDoctorVisitsAsync(int doctorId)
    {
        var doctor = await _doctors.GetByIdAsync(doctorId)
            ?? throw new KeyNotFoundException($"Doctor {doctorId} not found.");

        int total = await _visits.CountDoctorVisitsAsync(doctorId);

        return new DoctorVisitStatsDto
        {
            DoctorId = doctor.Id,
            DoctorFullName = doctor.FullName,
            Specialization = doctor.Specialization,
            TotalVisitsByDoctor = total
        };
    }

    private static DoctorReadDto Map(Doctor d) => new()
    {
        Id = d.Id,
        FullName = d.FullName,
        Specialization = d.Specialization
    };
}
