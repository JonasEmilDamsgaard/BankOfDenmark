using System.Collections.Generic;
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

        public IEnumerable<Customer> Customers => unitOfWork.Customers.GetAll();

        public Customer SelectedCustomer(int id)
        {
            return unitOfWork.Customers.GetById(id);
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

        public Customer EditCustomer(Customer selectedCustomer)
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

            return c;
        }

        public IEnumerable<Customer> FilterCustomers(string filter)
        {
            return unitOfWork.Customers.Find(c => c.FullName.ToLower().Contains(filter.ToLower()));
        }
    }
}
