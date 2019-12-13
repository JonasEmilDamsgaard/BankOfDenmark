using System;
using System.Collections.Generic;
using BankApp.Annotations;
using Data.DataAccess;
using Data.EF.DataAccess;
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

        public IEnumerable<Customer> GetAllCustomers => unitOfWork.Customers.GetAll();

        public Customer GetSelectedCustomer(int id)
        {
            return unitOfWork.Customers.GetById(id);
        }

        public IEnumerable<Customer> GetTopCustomers(int numberOfCustomers)
        {
            return unitOfWork.Customers.GetTopCustomers(numberOfCustomers);
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

        public void EditCustomer(Customer selectedCustomer)
        {
            var c = unitOfWork.Customers.GetById(selectedCustomer.Id);
            c.FullName = selectedCustomer.FullName;
            c.PhoneNumber = selectedCustomer.PhoneNumber;
            c.PostalCode = selectedCustomer.PostalCode;
            c.City = selectedCustomer.City;
            c.SocialSecurityNumber = selectedCustomer.SocialSecurityNumber;
            c.StreetName = selectedCustomer.StreetName;
            c.StreetNumber = selectedCustomer.StreetNumber;
            unitOfWork.Complete();
        }

        public IEnumerable<Customer> FilterCustomers(string filter)
        {
            return unitOfWork.Customers.Find(c => c.FullName.ToLower().Contains(filter.ToLower()));
        }
    }
}
