﻿using System.Collections.Generic;
using Data.Models;

namespace Data.DataAccess.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        IEnumerable<Customer> GetTopCustomers(int numberOfCustomers);
    }
}