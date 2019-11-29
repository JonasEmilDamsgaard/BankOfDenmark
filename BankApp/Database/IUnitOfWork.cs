using System;

namespace BankApp.Database
{
  public interface IUnitOfWork : IDisposable
  {
    int Complete();

    ICustomerRepository Customers { get; }
  }
}