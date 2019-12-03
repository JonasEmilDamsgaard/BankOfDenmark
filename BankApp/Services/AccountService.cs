using System;
using System.Collections.Generic;
using System.Text;
using BankApp.DataAccess;
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

        public void Deposit(Account account)
        {
            account.Balance += account.Amount;
            unitOfWork.Complete();
        }

        public void Withdraw(Account account)
        {
            account.Balance -= account.Amount;
            unitOfWork.Complete();
        }
    }
}
