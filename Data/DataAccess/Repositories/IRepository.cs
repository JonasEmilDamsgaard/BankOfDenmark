﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Data.DataAccess.Repositories
{
    public interface IRepository<T> where T : class
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Remove(T entity);
    }
}
