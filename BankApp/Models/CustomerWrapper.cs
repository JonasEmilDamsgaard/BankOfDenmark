using System;
using System.Collections.Generic;
using System.Text;
using Data.Models;
using Prism.Mvvm;

namespace BankApp.Models
{
    public class CustomerWrapper : BindableBase
    {
        private readonly Customer customer;

        public CustomerWrapper(Customer customer)
        {
            this.customer = customer;
        }

        public string SocialSecurityNumber
        {
            get => customer.SocialSecurityNumber;
            set
            {
                if (customer.SocialSecurityNumber == value) return;
                customer.SocialSecurityNumber = value;
                RaisePropertyChanged();
            }
        }

        public string FullName
        {
            get => customer.FullName;
            set
            {
                if (customer.FullName == value) return;
                customer.FullName = value;
                RaisePropertyChanged();
            }
        }

        public string StreetName
        {
            get => customer.StreetName;
            set
            {
                if (customer.StreetName == value) return;
                customer.StreetName = value;
                RaisePropertyChanged();
            }
        }

        public int StreetNumber
        {
            get => customer.StreetNumber;
            set
            {
                if (customer.StreetNumber == value) return;
                customer.StreetNumber = value;
                RaisePropertyChanged();
            }
        }

        public int PostalCode
        {
            get => customer.PostalCode;
            set
            {
                if (customer.PostalCode == value) return;
                customer.PostalCode = value;
                RaisePropertyChanged();
            }
        }

        public string City
        {
            get => customer.City;
            set
            {
                if (customer.City == value) return;
                customer.City = value;
                RaisePropertyChanged();
            }
        }

        public int PhoneNumber
        {
            get => customer.PhoneNumber;
            set
            {
                if (customer.PhoneNumber == value) return;
                customer.PhoneNumber = value;
                RaisePropertyChanged();
            }
        }
    }
}
