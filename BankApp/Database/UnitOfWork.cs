using System;
using System.Collections.Generic;
using System.Text;
using Data.EF;

namespace BankApp.Database
{
  public class UnitOfWork : IUnitOfWork
  {
    private readonly CustomerContext context;

    public UnitOfWork(CustomerContext context)
    {
      this.context = context;
      Customers = new CustomerRepository(context);
    }

    public ICustomerRepository Customers { get; private set; }
    //public IAccountRepository Authors { get; private set; }

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
