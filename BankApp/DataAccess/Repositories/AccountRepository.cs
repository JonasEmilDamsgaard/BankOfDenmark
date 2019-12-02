using System;
using System.Collections.Generic;
using System.Linq;
using Data.EF;
using Data.Models;

namespace BankApp.DataAccess.Repositories
{
    class AccountRepository : Repository<Account>, IAccountRepository
    {
        private readonly DatabaseContext context;

        public AccountRepository(DatabaseContext context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<Account> GetMostValuedAccounts(int numberOfAccounts)
        {
            if (numberOfAccounts> context.Accounts.ToList().Count)
            {
                throw new IndexOutOfRangeException();
            }

            return context.Accounts.OrderByDescending(x => x.Balance).Take(numberOfAccounts).ToList();
        }
    }
}
