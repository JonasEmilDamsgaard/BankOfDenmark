using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Data.DataAccess;
using Data.EF.DataAccess;
using Data.Models;

namespace BankApp.Services
{
    public class AccountService
    {
        private readonly IUnitOfWork unitOfWork;

        public AccountService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Customer Deposit(Customer selectedCustomer, decimal amount)
        {
            var accountBalance = unitOfWork.Customers.GetById(selectedCustomer.Id).Account.Balance += amount;
            unitOfWork.Complete();

            return selectedCustomer;
        }

        public Customer Withdraw(Customer selectedCustomer, decimal amount)
        {
            var accountBalance = unitOfWork.Customers.GetById(selectedCustomer.Id).Account.Balance -= amount;
            unitOfWork.Complete();

            return selectedCustomer;

            //if (selectedCustomer.Account.Balance - amount < 0)
            //{
            //    MessageBox.Show($"Not possible to withdraw the amount of { amount} from the account", "Error");
            //    return selectedCustomer;
            //}


            //var c = unitOfWork.Customers.GetById(selectedCustomer.Id);
            //c.Account.Balance -= amount;
            //unitOfWork.Complete();
            //return c;
        }
    }
}
