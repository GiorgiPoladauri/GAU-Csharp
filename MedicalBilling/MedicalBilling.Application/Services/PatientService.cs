using MedicalBilling.Application.DTOs;
using MedicalBilling.Application.Interfaces;
using MedicalBilling.Domain.Entities;
using MedicalBilling.Domain.Interfaces;

namespace MedicalBilling.Application.Services;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patients;
    private readonly IVisitRepository _visits;

    public PatientService(IPatientRepository patients, IVisitRepository visits)
    {
        _patients = patients;
        _visits = visits;
    }

    public async Task<IEnumerable<PatientReadDto>> GetAllAsync()
    {
        var all = await _patients.GetAllAsync();
        return all.Select(Map);
    }

    public async Task<PatientReadDto?> GetByIdAsync(int id)
    {
        var p = await _patients.GetByIdAsync(id);
        return p == null ? null : Map(p);
    }

    public async Task<PatientReadDto> CreateAsync(PatientCreateDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.FullName))
            throw new ArgumentException("FullName is required.");
        if (dto.BirthDate > DateTime.Today)
            throw new ArgumentException("BirthDate cannot be in the future.");

        var entity = new Patient { FullName = dto.FullName.Trim(), BirthDate = dto.BirthDate.Date };
        var created = await _patients.AddAsync(entity);
        return Map(created);
    }

    public async Task<PatientReadDto> UpdateAsync(int id, PatientUpdateDto dto)
    {
        var patient = await _patients.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Patient {id} not found.");

        if (string.IsNullOrWhiteSpace(dto.FullName))
            throw new ArgumentException("FullName is required.");
        if (dto.BirthDate > DateTime.Today)
            throw new ArgumentException("BirthDate cannot be in the future.");

        patient.FullName = dto.FullName.Trim();
        patient.BirthDate = dto.BirthDate.Date;
        var updated = await _patients.UpdateAsync(patient);
        return Map(updated);
    }

    public async Task DeleteAsync(int id)
    {
        if (!await _patients.ExistsAsync(id))
            throw new KeyNotFoundException($"Patient {id} not found.");
        await _patients.DeleteAsync(id);
    }

    // Named exactly as task requires: CalculateTotalBillingForPatient
    public async Task<BillingSummaryDto> CalculateTotalBillingForPatientAsync(int patientId)
    {
        var patient = await _patients.GetByIdAsync(patientId)
            ?? throw new KeyNotFoundException($"Patient {patientId} not found.");

        var totalPaid = await _visits.GetTotalBillingForPatientAsync(patientId);
        var totalVisits = await _visits.CountPatientVisitsAsync(patientId);

        return new BillingSummaryDto
        {
            PatientId = patient.Id,
            PatientFullName = patient.FullName,
            TotalPaid = totalPaid,
            TotalVisits = totalVisits
        };
    }

    private static PatientReadDto Map(Patient p) => new()
    {
        Id = p.Id,
        FullName = p.FullName,
        BirthDate = p.BirthDate
    };
}
