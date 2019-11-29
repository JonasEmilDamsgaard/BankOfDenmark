using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.EF;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Database
{
  public interface ICustomerRepository : IRepository<Customer>
  {

  }

  class CustomerRepository : ICustomerRepository
  {
    private readonly CustomerContext context;

    public CustomerRepository(CustomerContext context)
    {
      this.context = context;
    }

    public Customer GetById(int id)
    {
      return GetAll().Single(c => c.Id == id);
    }

    public IQueryable<Customer> GetAll()
    {
      return context.Customers.Include(c => c.Account);
    }

    public IQueryable<Customer> GetAll(Expression<Func<Customer, bool>> filter)
    {
      return GetAll().Where(filter);
    }

    public void Add(Customer customer)
    {
      context.Add(customer);
      context.SaveChanges();
    }

    public void Remove(Customer customer)
    {
      context.Remove(customer);
      context.SaveChanges();
    }
  }

  class Repository<T> : IRepository<T> where T : class, IEntity
  {
    private readonly DbContext context;

    public Repository(DbContext context)
    {
      this.context = context;
    }

    public T GetById(int id) => context.Set<T>()
      .Single(p => p.Id == id);

    public IQueryable<T> GetAll() => context.Set<T>();

    public IQueryable<T> GetAll(Expression<Func<T, bool>> filter) => context.Set<T>()
      .Where(filter);

    public void Add(T t)
    {
      context.Set<T>().Add(t ?? throw new ArgumentNullException(nameof(t)));
      context.SaveChanges();
    }

    public void Remove(T t)
    {
      context.Set<T>().Remove(t ?? throw new ArgumentNullException(nameof(t)));
      context.SaveChanges();
    }
  }
}
