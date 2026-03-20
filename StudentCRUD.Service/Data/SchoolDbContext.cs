using System.Data.Entity;
using StudentCRUD.Service.Models;

namespace StudentCRUD.Service.Data
{
    // DbContext = ბაზასთან კავშირის კლასი
    // ის Entity Framework-ის გული არის
    public class SchoolDbContext : DbContext
    {
        // base("SchoolDB") = App.config-ში connectionString-ის სახელი
        public SchoolDbContext() : base("SchoolDB") { }

        // DbSet<Student> = Students ცხრილი ბაზაში
        // ამ property-ით LINQ-ს ვწერთ ცხრილზე
        public DbSet<Student> Students { get; set; }
    }
}
