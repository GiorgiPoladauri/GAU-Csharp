using MedicalBilling.Domain.Entities;
using MedicalBilling.Domain.Interfaces;
using MedicalBilling.Infrastructure.Data;

namespace MedicalBilling.Infrastructure.Repositories;

public class DoctorRepository : Repository<Doctor>, IDoctorRepository
{
    public DoctorRepository(HealthDbContext context) : base(context) { }
}
