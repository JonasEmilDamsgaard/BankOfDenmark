using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.EF
{
  public class CustomerContext : DbContext
  {
    public CustomerContext()
    {
      //base.Database.EnsureDeleted();
      base.Database.EnsureCreated();
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Account> Accounts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlite("Data Source=db.sqlite;");
    }
  }
}