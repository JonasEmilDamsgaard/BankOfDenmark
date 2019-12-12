using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Data.DataAccess.Repositories;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.EF.DataAccess.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerContext context;

        public CustomerRepository(CustomerContext context)
        {
            this.context = context;
        }
        public Customer GetById(int id) => GetAll().Single(c => c.Id == id);

        public IEnumerable<Customer> GetAll() => context.Customers.Include(c => c.Account);

        public IEnumerable<Customer> Find(Expression<Func<Customer, bool>> filter) => GetAll().Where(filter.Compile());

        public void Add(Customer entity)
        {
            context.Customers.Add(entity ?? throw new ArgumentNullException(nameof(entity)));
        }

        public void Remove(Customer entity)
        {
            context.Customers.Remove(entity ?? throw new ArgumentNullException(nameof(entity)));
        }

        public IEnumerable<Customer> GetTopCustomers(int numberOfCustomers)
        {
            if (numberOfCustomers > context.Customers.ToList().Count)
            {
                return new List<Customer>();
            }

            return context.Customers.OrderByDescending(x => x.Account.Balance).Take(numberOfCustomers).ToList();
        }
    }
}