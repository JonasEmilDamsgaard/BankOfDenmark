using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.EF
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
            base.Database.EnsureCreated();
        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=db.sqlite;");
        }
    }
}