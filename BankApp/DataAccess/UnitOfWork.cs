using BankApp.DataAccess.Repositories;
using Data.EF;

namespace BankApp.DataAccess
{
  public class UnitOfWork : IUnitOfWork
  {
    private readonly DatabaseContext context;

    public UnitOfWork(DatabaseContext context)
    {
      this.context = context;
      Customers = new CustomerRepository(context);
      Accounts = new AccountRepository(context);
    }

    public ICustomerRepository Customers { get; }
    public IAccountRepository Accounts { get; }

    public int Complete()
    {
      return context.SaveChanges();
    }

    public void Dispose()
    {
      context.Dispose();
    }
  }
}
