using System.Data.Entity.Migrations;

namespace GradeManagerProj.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<GradeManagerProj.Data.ExamContext>
    {
        public Configuration()
        {
            // Switch to 'true' if you want EF to apply schema changes automatically at runtime:
            AutomaticMigrationsEnabled = false;

            // If you do enable AutomaticMigrationsEnabled = true and want to allow data-loss:
            // AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(GradeManagerProj.Data.ExamContext context)
        {
            // This method runs after migrating to the latest version.
            // You can add initial “seed” data here if you like. For example:
            //
            // context.Teachers.AddOrUpdate(t => t.FullName,
            //     new Models.Teachers { FullName = "Default Teacher" }
            // );
            //
            // context.SaveChanges();
        }
    }
}
