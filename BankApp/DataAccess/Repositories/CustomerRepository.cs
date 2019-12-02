using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Data.EF;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BankApp.DataAccess.Repositories
{
    class CustomerRepository : ICustomerRepository
    {
        private readonly DatabaseContext context;
        private readonly IQueryable<Customer> specificEntities;

        public CustomerRepository(DatabaseContext context)
        {
            this.context = context;
            specificEntities = context.Set<Customer>().Include(c => c.Account);
        }

        public Customer GetById(int id)
        {
            return specificEntities.Single(c => c.Id == id);
        }

        public IEnumerable<Customer> GetAll()
        {
            return specificEntities.AsEnumerable();
        }

        public IEnumerable<Customer> Find(Expression<Func<Customer, bool>> filter)
        {
            return GetAll().Where(filter.Compile());
        }

        public void Add(Customer entity)
        {
            context.Customers.Add(entity ?? throw new ArgumentNullException(nameof(entity)));
        }

        public void Remove(Customer entity)
        {
            context.Customers.Remove(entity ?? throw new ArgumentNullException(nameof(entity)));
        }

        public IEnumerable<Customer> GetMostValuedCustomers(int numberOfCustomers)
        {
            if (numberOfCustomers > context.Customers.ToList().Count)
            {
                throw new IndexOutOfRangeException();
            }

            return context.Customers.OrderByDescending(x => x.Account.Balance).Take(numberOfCustomers).ToList();
        }
    }
}