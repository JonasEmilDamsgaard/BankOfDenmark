using System.Collections.Generic;
using Data.Models;

namespace BankApp.DataAccess.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        IEnumerable<Customer> GetMostValuedCustomers(int numberOfCustomers);
    }
}