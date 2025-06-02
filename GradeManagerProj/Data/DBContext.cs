using GradeManagerProj.Models;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Sql;
namespace GradeManagerProj.Data
{
    public class ExamContext : DbContext
    {
        public ExamContext()
            : base("name=GradeManagerDBConnection")
        {
            Database.SetInitializer<ExamContext>(
                new MigrateDatabaseToLatestVersion<ExamContext, Migrations.Configuration>()
            );
        }

        public DbSet<Students> Students { get; set; }
        public DbSet<Teachers> Teachers { get; set; }
        public DbSet<Subjects> Subjects { get; set; }
        public DbSet<Grades> Grades { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
