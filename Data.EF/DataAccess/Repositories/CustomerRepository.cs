using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Data.DataAccess.Repositories;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace Data.EF.DataAccess.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DbContext context;

        public CustomerRepository(DatabaseContext context)
        {
            this.context = context;
        }
        public Customer GetById(int id)
        {
            return GetAll().Single(c => c.Id == id);
        }

        public IEnumerable<Customer> GetAll()
        {
            return context.Set<Customer>().Include(c => c.Account).ToList();
        }

        public IEnumerable<Customer> Find(Expression<Func<Customer, bool>> filter)
        {
            return GetAll().Where(filter.Compile());
        }

        public void Add(Customer entity)
        {
            context.Set<Customer>().Add(entity ?? throw new ArgumentNullException(nameof(entity)));
        }

        public void Remove(Customer entity)
        {
            context.Set<Customer>().Remove(entity ?? throw new ArgumentNullException(nameof(entity)));
        }

        public IEnumerable<Customer> GetTopCustomers(int numberOfCustomers)
        {
            if (numberOfCustomers > context.Set<Customer>().ToList().Count)
            {
                return new List<Customer>();
            }

            return context.Set<Customer>().OrderByDescending(x => x.Account.Balance).Take(numberOfCustomers).ToList();
        }
    }
}