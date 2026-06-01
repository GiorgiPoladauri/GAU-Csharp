using Microsoft.EntityFrameworkCore;
using WordleAPI.Domain.Entities;

namespace WordleAPI.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Game> Games => Set<Game>();
    public DbSet<Guess> Guesses => Set<Guess>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(g => g.Id);
            entity.Property(g => g.TargetWord)
                  .IsRequired()
                  .HasMaxLength(5);
            entity.Property(g => g.StartDate).IsRequired();
            entity.Property(g => g.Attempts).HasDefaultValue(0);
            entity.Property(g => g.IsWin).HasDefaultValue(false);
            entity.Property(g => g.Status)
                  .HasConversion<string>()
                  .HasMaxLength(20);

            entity.HasMany(g => g.Guesses)
                  .WithOne(guess => guess.Game)
                  .HasForeignKey(guess => guess.GameId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Guess>(entity =>
        {
            entity.HasKey(g => g.Id);
            entity.Property(g => g.Word)
                  .IsRequired()
                  .HasMaxLength(5);
            entity.Property(g => g.GuessNumber).IsRequired();
            entity.Property(g => g.GuessResult)
                  .IsRequired()
                  .HasMaxLength(1000);
        });
    }
}
