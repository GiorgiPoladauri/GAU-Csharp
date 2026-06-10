using MedicalBilling.Domain.Entities;
using MedicalBilling.Domain.Interfaces;
using MedicalBilling.Infrastructure.Data;

namespace MedicalBilling.Infrastructure.Repositories;

public class PatientRepository : Repository<Patient>, IPatientRepository
{
    public PatientRepository(HealthDbContext context) : base(context) { }
}
