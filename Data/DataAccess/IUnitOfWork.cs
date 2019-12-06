using System;
using Data.DataAccess.Repositories;

namespace Data.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository Customers { get; }
        int Complete();
    }
}