using GradeManagerProj.Models;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Sql;
using GradeManagerProj.Migrations;// თუ გჭირდებათ
// დარწმუნდით, რომ "Migrations.Configuration" კლასი ნამდვილად არსებობს და მისთვის სწორი ნეივსპეისია

namespace GradeManagerProj.Data
{
    public class ExamContext : DbContext
    {
        public ExamContext()
            : base("name=GradeManagerDBConnection")
        {
            // აქ აუცილებლად შეავსეთ სახელი <ExamContext> SetInitializer-ში:
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
