using MedicalBilling.Domain.Entities;
using MedicalBilling.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicalBilling.Infrastructure.Data;

public class HealthDbContext : DbContext
{
    public HealthDbContext(DbContextOptions<HealthDbContext> options) : base(options) { }

    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Doctor> Doctors => Set<Doctor>();
    public DbSet<Visit> Visits => Set<Visit>();
    public DbSet<AppUser> Users => Set<AppUser>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Patient>(e =>
        {
            e.HasKey(p => p.Id);
            e.Property(p => p.FullName).IsRequired().HasMaxLength(200);
            e.Property(p => p.BirthDate).IsRequired();
        });

        modelBuilder.Entity<Doctor>(e =>
        {
            e.HasKey(d => d.Id);
            e.Property(d => d.FullName).IsRequired().HasMaxLength(200);
            e.Property(d => d.Specialization).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<Visit>(e =>
        {
            e.HasKey(v => v.Id);
            e.Property(v => v.VisitDate).IsRequired();
            e.Property(v => v.Fee).IsRequired().HasPrecision(10, 2);

            e.HasOne(v => v.Patient)
             .WithMany(p => p.Visits)
             .HasForeignKey(v => v.PatientId)
             .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(v => v.Doctor)
             .WithMany(d => d.Visits)
             .HasForeignKey(v => v.DoctorId)
             .OnDelete(DeleteBehavior.Restrict);

            // Enforce one visit per patient per day at DB level
            e.HasIndex(v => new { v.PatientId, v.VisitDate }).IsUnique();
        });

        modelBuilder.Entity<AppUser>(e =>
        {
            e.HasKey(u => u.Id);
            e.Property(u => u.Username).IsRequired().HasMaxLength(100);
            e.Property(u => u.PasswordHash).IsRequired();
            e.Property(u => u.Role).IsRequired().HasMaxLength(50);
            e.HasIndex(u => u.Username).IsUnique();
        });
    }
}
