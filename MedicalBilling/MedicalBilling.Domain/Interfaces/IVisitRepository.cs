using MedicalBilling.Domain.Entities;

namespace MedicalBilling.Domain.Interfaces;

public interface IVisitRepository : IRepository<Visit>
{
    Task<(IEnumerable<Visit> Items, int TotalCount)> GetPagedAsync(
        int pageNumber, int pageSize,
        int? doctorId, DateTime? visitDateFrom, DateTime? visitDateTo,
        decimal? minFee, decimal? maxFee,
        string? sortBy, string? sortDirection);

    Task<bool> PatientHasVisitOnDateAsync(int patientId, DateTime date, int? excludeVisitId = null);
    Task<decimal> GetTotalBillingForPatientAsync(int patientId);
    Task<int> CountDoctorVisitsAsync(int doctorId);
    Task<int> CountPatientVisitsAsync(int patientId);
}
