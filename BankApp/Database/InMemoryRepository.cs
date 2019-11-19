using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Data;

namespace BankApp.Database
{
  class InMemoryRepository<T> : IRepository<T> where T : IEntity
  {
    private readonly List<T> elements;

    public InMemoryRepository(params T[] customers)
    {
      elements = new List<T>(customers);
    }

    public T GetById(int id) => elements.Single(p => p.Id == id);

    public IQueryable<T> GetAll() => elements
      .AsQueryable();

    public IQueryable<T> GetAll(Expression<Func<T, bool>> filter) => elements
      .AsQueryable()
      .Where(filter);

    public void Add(T element)
    {
      if (element == null)
      {
        throw new ArgumentNullException(nameof(element));
      }

      int existingIndex = elements.FindIndex(p => p.Id == element.Id);
      if (existingIndex >= 0)
      {
        elements[existingIndex] = element;
      }
      else
      {
        elements.Add(element);
      }
    }

    public void Remove(T element)
    {
      elements.Remove(element);
    }
  }
}
