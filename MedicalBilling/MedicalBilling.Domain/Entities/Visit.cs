namespace MedicalBilling.Domain.Entities;

public class Visit
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public DateTime VisitDate { get; set; }
    public decimal Fee { get; set; }
    public Patient Patient { get; set; } = null!;
    public Doctor Doctor { get; set; } = null!;
}
