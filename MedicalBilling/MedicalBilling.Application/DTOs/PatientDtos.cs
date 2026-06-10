namespace MedicalBilling.Application.DTOs;

public class PatientCreateDto
{
    public string FullName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
}

public class PatientUpdateDto
{
    public string FullName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
}

public class PatientReadDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
}

public class BillingSummaryDto
{
    public int PatientId { get; set; }
    public string PatientFullName { get; set; } = string.Empty;
    public decimal TotalPaid { get; set; }
    public int TotalVisits { get; set; }
}
