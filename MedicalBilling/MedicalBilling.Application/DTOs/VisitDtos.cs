namespace MedicalBilling.Application.DTOs;

public class VisitCreateDto
{
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public DateTime VisitDate { get; set; }
    public decimal Fee { get; set; }
}

public class VisitUpdateDto
{
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public DateTime VisitDate { get; set; }
    public decimal Fee { get; set; }
}

public class VisitReadDto
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public string PatientFullName { get; set; } = string.Empty;
    public int DoctorId { get; set; }
    public string DoctorFullName { get; set; } = string.Empty;
    public string DoctorSpecialization { get; set; } = string.Empty;
    public DateTime VisitDate { get; set; }
    public decimal Fee { get; set; }
}
