using System;
using System.Collections.Generic;
using System.Text;
using BankApp.DataAccess;
using Data.Models;

namespace BankApp.Services
{
    public class CustomerService
    {
        private readonly IUnitOfWork unitOfWork;

        public CustomerService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Customer AddCustomer()
        {
            var customer = new Customer("New customer");
            unitOfWork.Customers.Add(customer);
            unitOfWork.Complete();

            return customer;
        }

        public void DeleteCustomer(Customer selectedCustomer)
        {
            unitOfWork.Customers.Remove(selectedCustomer);
            unitOfWork.Complete();
        }
    }
}
