using System.Collections.Generic;
using Data.Models;

namespace BankApp.DataAccess.Repositories
{
    public interface IAccountRepository : IRepository<Account>
    {
        IEnumerable<Account> GetMostValuedAccounts(int numberOfAccounts);
    }
}
