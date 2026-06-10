using MedicalBilling.Application.Common;
using MedicalBilling.Application.DTOs;

namespace MedicalBilling.Application.Interfaces;

public interface IVisitService
{
    Task<PagedResult<VisitReadDto>> GetVisitsAsync(VisitQueryParams query, int? currentDoctorId = null);
    Task<VisitReadDto?> GetVisitByIdAsync(int id, int? currentDoctorId = null);
    Task<VisitReadDto> CreateVisitAsync(VisitCreateDto dto);
    Task<VisitReadDto> UpdateVisitAsync(int id, VisitUpdateDto dto);
    Task DeleteVisitAsync(int id);
}
