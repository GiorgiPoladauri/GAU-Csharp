using MedicalBilling.Application.DTOs;

namespace MedicalBilling.Application.Interfaces;

public interface IPatientService
{
    Task<IEnumerable<PatientReadDto>> GetAllAsync();
    Task<PatientReadDto?> GetByIdAsync(int id);
    Task<PatientReadDto> CreateAsync(PatientCreateDto dto);
    Task<PatientReadDto> UpdateAsync(int id, PatientUpdateDto dto);
    Task DeleteAsync(int id);
    Task<BillingSummaryDto> CalculateTotalBillingForPatientAsync(int patientId);
}
