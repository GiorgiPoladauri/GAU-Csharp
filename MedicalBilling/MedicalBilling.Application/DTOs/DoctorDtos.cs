namespace MedicalBilling.Application.DTOs;

public class DoctorCreateDto
{
    public string FullName { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
}

public class DoctorReadDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
}

public class DoctorVisitStatsDto
{
    public int DoctorId { get; set; }
    public string DoctorFullName { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
    public int TotalVisitsByDoctor { get; set; }
}
