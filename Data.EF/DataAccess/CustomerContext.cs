using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.EF.DataAccess
{
    public class CustomerContext : DbContext
    {
        public CustomerContext()
        {
            //base.Database.EnsureDeleted();
            base.Database.EnsureCreated();
        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./Database.db");
        }
    }
}