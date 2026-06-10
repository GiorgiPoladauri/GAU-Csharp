using MedicalBilling.Application.Common;
using MedicalBilling.Application.DTOs;
using MedicalBilling.Application.Interfaces;
using MedicalBilling.Domain.Entities;
using MedicalBilling.Domain.Interfaces;

namespace MedicalBilling.Application.Services;

public class VisitService : IVisitService
{
    private readonly IVisitRepository _visits;
    private readonly IPatientRepository _patients;
    private readonly IDoctorRepository _doctors;

    public VisitService(IVisitRepository visits, IPatientRepository patients, IDoctorRepository doctors)
    {
        _visits = visits;
        _patients = patients;
        _doctors = doctors;
    }

    public async Task<PagedResult<VisitReadDto>> GetVisitsAsync(VisitQueryParams query, int? currentDoctorId = null)
    {
        // doctors can only see their own visits
        if (currentDoctorId.HasValue)
            query.DoctorId = currentDoctorId.Value;

        var (items, totalCount) = await _visits.GetPagedAsync(
            query.PageNumber, query.PageSize,
            query.DoctorId, query.VisitDateFrom, query.VisitDateTo,
            query.MinFee, query.MaxFee,
            query.SortBy, query.SortDirection);

        int totalPages = (int)Math.Ceiling(totalCount / (double)query.PageSize);

        return new PagedResult<VisitReadDto>
        {
            Items = items.Select(Map),
            TotalCount = totalCount,
            TotalPages = totalPages,
            CurrentPage = query.PageNumber,
            PageSize = query.PageSize
        };
    }

    public async Task<VisitReadDto?> GetVisitByIdAsync(int id, int? currentDoctorId = null)
    {
        var visit = await _visits.GetByIdAsync(id);
        if (visit == null) return null;

        if (currentDoctorId.HasValue && visit.DoctorId != currentDoctorId.Value)
            throw new UnauthorizedAccessException("You can only view your own visits.");

        return Map(visit);
    }

    public async Task<VisitReadDto> CreateVisitAsync(VisitCreateDto dto)
    {
        // Validation in service layer
        if (dto.Fee <= 0)
            throw new ArgumentException("Visit fee must be greater than 0.");
        if (dto.Fee >= 1000)
            throw new ArgumentException("Visit fee must be less than 1000.");

        var patient = await _patients.GetByIdAsync(dto.PatientId)
            ?? throw new KeyNotFoundException($"Patient {dto.PatientId} not found.");

        var doctor = await _doctors.GetByIdAsync(dto.DoctorId)
            ?? throw new KeyNotFoundException($"Doctor {dto.DoctorId} not found.");

        // Doctor must have specialization
        if (string.IsNullOrWhiteSpace(doctor.Specialization))
            throw new InvalidOperationException("Doctor must have a specialization before being assigned a visit.");

        // ValidateVisitDate — one visit per patient per day
        await ValidateVisitDateAsync(dto.PatientId, dto.VisitDate, null);

        var visit = new Visit
        {
            PatientId = dto.PatientId,
            DoctorId = dto.DoctorId,
            VisitDate = dto.VisitDate.Date,
            Fee = dto.Fee
        };

        var created = await _visits.AddAsync(visit);
        var full = await _visits.GetByIdAsync(created.Id);
        return Map(full!);
    }

    public async Task<VisitReadDto> UpdateVisitAsync(int id, VisitUpdateDto dto)
    {
        var visit = await _visits.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Visit {id} not found.");

        if (dto.Fee <= 0)
            throw new ArgumentException("Visit fee must be greater than 0.");
        if (dto.Fee >= 1000)
            throw new ArgumentException("Visit fee must be less than 1000.");

        if (!await _patients.ExistsAsync(dto.PatientId))
            throw new KeyNotFoundException($"Patient {dto.PatientId} not found.");

        var doctor = await _doctors.GetByIdAsync(dto.DoctorId)
            ?? throw new KeyNotFoundException($"Doctor {dto.DoctorId} not found.");

        if (string.IsNullOrWhiteSpace(doctor.Specialization))
            throw new InvalidOperationException("Doctor must have a specialization before being assigned a visit.");

        await ValidateVisitDateAsync(dto.PatientId, dto.VisitDate, id);

        visit.PatientId = dto.PatientId;
        visit.DoctorId = dto.DoctorId;
        visit.VisitDate = dto.VisitDate.Date;
        visit.Fee = dto.Fee;

        await _visits.UpdateAsync(visit);
        var full = await _visits.GetByIdAsync(id);
        return Map(full!);
    }

    public async Task DeleteVisitAsync(int id)
    {
        if (!await _visits.ExistsAsync(id))
            throw new KeyNotFoundException($"Visit {id} not found.");
        await _visits.DeleteAsync(id);
    }

    // Named exactly as task requires
    private async Task ValidateVisitDateAsync(int patientId, DateTime date, int? excludeId)
    {
        if (await _visits.PatientHasVisitOnDateAsync(patientId, date.Date, excludeId))
            throw new InvalidOperationException(
                $"Patient already has a visit on {date:yyyy-MM-dd}. Only one visit per day is allowed.");
    }

    private static VisitReadDto Map(Visit v) => new()
    {
        Id = v.Id,
        PatientId = v.PatientId,
        PatientFullName = v.Patient?.FullName ?? string.Empty,
        DoctorId = v.DoctorId,
        DoctorFullName = v.Doctor?.FullName ?? string.Empty,
        DoctorSpecialization = v.Doctor?.Specialization ?? string.Empty,
        VisitDate = v.VisitDate,
        Fee = v.Fee
    };
}
