using System;
using System.Collections.Generic;
using System.Text;
using Data.Models;
using Prism.Mvvm;

namespace BankApp.Models
{
    public class AccountWrapper : BindableBase
    {
        private readonly Account account;

        public AccountWrapper(Account account)
        {
            this.account = account;
        }

        public decimal Balance
        {
            get => account.Balance;
            set
            {
                if (account.Balance == value) return;
                account.Balance = value;
                RaisePropertyChanged();
            }
        }

        public State State
        {
            get => account.State;
            set
            {
                if (account.State == value) return;
                account.State = value;
                RaisePropertyChanged();
            }
        }

        public decimal Amount
        {
            get => account.Amount;
            set
            {
                if (account.Amount == value) return;
                account.Amount = value;
                RaisePropertyChanged();
            }
        }
    }
}
