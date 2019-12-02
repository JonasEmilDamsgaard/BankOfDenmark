using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace BankApp.DataAccess.Repositories
{
    class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> entities;

        public Repository(DbContext context)
        {
            entities = context.Set<T>();
        }

        public T GetById(int id)
        {
            return entities.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return entities.ToList();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> filter)
        {
            return entities.Where(filter);
        } 

        public void Add(T entity)
        {
            entities.Add(entity ?? throw new ArgumentNullException(nameof(entity)));
        }

        public void Remove(T entity)
        { 
            entities.Remove(entity ?? throw new ArgumentNullException(nameof(entity)));
        }
    }
}
