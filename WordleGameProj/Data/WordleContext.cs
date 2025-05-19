// Data/WordleContext.cs
using System.Data.Entity;
using WordleGameProj.Models;

namespace WordleGameProj.Data
{
    public class WordleContext : DbContext
    {
        public WordleContext() : base("name=WordleDb") { }

        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
    }
}
