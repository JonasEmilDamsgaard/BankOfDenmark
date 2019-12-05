using System;
using System.Collections.Generic;
using System.Text;
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

        public decimal Deposit(Customer selectedCustomer, decimal amount)
        {
            var accountBalance = unitOfWork.Customers.GetById(selectedCustomer.Id).Account.Balance += amount;
            unitOfWork.Complete();

            return accountBalance;
        }

        public decimal Withdraw(Customer selectedCustomer, decimal amount)
        {
            var accountBalance = unitOfWork.Customers.GetById(selectedCustomer.Id).Account.Balance -= amount;
            unitOfWork.Complete();

            return accountBalance;
        }
    }
}
