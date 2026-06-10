using MedicalBilling.Domain.Entities;
using MedicalBilling.Domain.Interfaces;
using MedicalBilling.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MedicalBilling.Infrastructure.Repositories;

public class VisitRepository : Repository<Visit>, IVisitRepository
{
    public VisitRepository(HealthDbContext context) : base(context) { }

    public override async Task<Visit?> GetByIdAsync(int id) =>
        await _context.Visits
            .Include(v => v.Patient)
            .Include(v => v.Doctor)
            .FirstOrDefaultAsync(v => v.Id == id);

    public async Task<(IEnumerable<Visit> Items, int TotalCount)> GetPagedAsync(
        int pageNumber, int pageSize,
        int? doctorId, DateTime? visitDateFrom, DateTime? visitDateTo,
        decimal? minFee, decimal? maxFee,
        string? sortBy, string? sortDirection)
    {
        var query = _context.Visits
            .Include(v => v.Patient)
            .Include(v => v.Doctor)
            .AsQueryable();

        if (doctorId.HasValue)
            query = query.Where(v => v.DoctorId == doctorId.Value);
        if (visitDateFrom.HasValue)
            query = query.Where(v => v.VisitDate >= visitDateFrom.Value.Date);
        if (visitDateTo.HasValue)
            query = query.Where(v => v.VisitDate <= visitDateTo.Value.Date);
        if (minFee.HasValue)
            query = query.Where(v => v.Fee >= minFee.Value);
        if (maxFee.HasValue)
            query = query.Where(v => v.Fee <= maxFee.Value);

        bool asc = !string.Equals(sortDirection, "desc", StringComparison.OrdinalIgnoreCase);
        query = sortBy?.ToLower() switch
        {
            "fee"       => asc ? query.OrderBy(v => v.Fee)       : query.OrderByDescending(v => v.Fee),
            "visitdate" => asc ? query.OrderBy(v => v.VisitDate) : query.OrderByDescending(v => v.VisitDate),
            _           => query.OrderByDescending(v => v.VisitDate)
        };

        int totalCount = await query.CountAsync();
        var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        return (items, totalCount);
    }

    public async Task<bool> PatientHasVisitOnDateAsync(int patientId, DateTime date, int? excludeVisitId = null)
    {
        var q = _context.Visits.Where(v => v.PatientId == patientId && v.VisitDate == date.Date);
        if (excludeVisitId.HasValue) q = q.Where(v => v.Id != excludeVisitId.Value);
        return await q.AnyAsync();
    }

    public async Task<decimal> GetTotalBillingForPatientAsync(int patientId) =>
        await _context.Visits
            .Where(v => v.PatientId == patientId)
            .SumAsync(v => (decimal?)v.Fee) ?? 0m;

    public async Task<int> CountDoctorVisitsAsync(int doctorId) =>
        await _context.Visits.CountAsync(v => v.DoctorId == doctorId);

    public async Task<int> CountPatientVisitsAsync(int patientId) =>
    await _context.Visits.CountAsync(v => v.PatientId == patientId);
}
