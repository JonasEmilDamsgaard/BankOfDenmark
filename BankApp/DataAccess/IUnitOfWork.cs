using System;
using BankApp.DataAccess.Repositories;

namespace BankApp.DataAccess
{
  public interface IUnitOfWork : IDisposable
  {
      ICustomerRepository Customers { get; }
      IAccountRepository Accounts { get; }
      int Complete();
    }
}