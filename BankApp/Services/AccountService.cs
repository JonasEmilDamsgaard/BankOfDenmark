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

        public Customer Deposit(Customer selectedCustomer, double amount)
        {
            selectedCustomer.Account.Balance = unitOfWork.Customers.GetById(selectedCustomer.Id).Account.Balance += amount;
            unitOfWork.Complete();

            return selectedCustomer;
        }

        public Customer Withdraw(Customer selectedCustomer, double amount)
        {
            selectedCustomer.Account.Balance = unitOfWork.Customers.GetById(selectedCustomer.Id).Account.Balance -= amount;
            unitOfWork.Complete();

            return selectedCustomer;
        }
    }
}
