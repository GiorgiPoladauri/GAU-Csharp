using Microsoft.EntityFrameworkCore;
using WordleAPI.Domain.Entities;

namespace WordleAPI.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Game> Games => Set<Game>();
    public DbSet<Guess> Guesses => Set<Guess>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Statistic> Statistics => Set<Statistic>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.HasIndex(u => u.Email).IsUnique();
            entity.Property(u => u.Email).IsRequired().HasMaxLength(256);
            entity.Property(u => u.PasswordHash).IsRequired();
        });

        modelBuilder.Entity<Statistic>(entity =>
        {
            entity.HasKey(s => s.Id);
            entity.HasOne(s => s.User)
                  .WithOne(u => u.Statistic)
                  .HasForeignKey<Statistic>(s => s.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(g => g.Id);
            entity.Property(g => g.TargetWord).IsRequired().HasMaxLength(5);
            entity.Property(g => g.StartDate).IsRequired();
            entity.Property(g => g.Attempts).HasDefaultValue(0);
            entity.Property(g => g.IsWin).HasDefaultValue(false);
            entity.Property(g => g.Status).HasConversion<string>().HasMaxLength(20);

            entity.HasOne(g => g.User)
                  .WithMany(u => u.Games)
                  .HasForeignKey(g => g.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(g => g.Guesses)
                  .WithOne(guess => guess.Game)
                  .HasForeignKey(guess => guess.GameId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Guess>(entity =>
        {
            entity.HasKey(g => g.Id);
            entity.Property(g => g.Word).IsRequired().HasMaxLength(5);
            entity.Property(g => g.GuessNumber).IsRequired();
            entity.Property(g => g.GuessResult).IsRequired().HasMaxLength(2000);
        });
    }
}