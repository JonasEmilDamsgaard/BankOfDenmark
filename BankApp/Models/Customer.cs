using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BankApp.Annotations;

namespace BankApp.Models
{
    public class Customer : INotifyPropertyChanged, ICloneable
    {
        private Guid id;
        private string socialSecurityNumber;
        private string fullName;
        private string streetName;
        private int streetNumber;
        private int postalCode;
        private string city;
        private int phoneNumber;
        private Account account;

        public Customer(string fullName)
        {
            FullName = fullName;
            Account = new Account();
            Id = Guid.NewGuid();
        }

        public Guid Id
        {
            get => id;
            private set
            {
                if (Equals(value, id)) return;
                id = value;
                OnPropertyChanged();
            }
        }

        public string SocialSecurityNumber
        {
            get => socialSecurityNumber;
            set
            {
                if (Equals(value, socialSecurityNumber)) return;
                socialSecurityNumber = value;
                OnPropertyChanged();
            }
        }

        public string FullName
        {
            get => fullName;
            set
            {
                if (Equals(value, fullName)) return;
                fullName = value;
                OnPropertyChanged();
            }
        }

        public string StreetName
        {
            get => streetName;
            set
            {
                if (Equals(value, streetName)) return;
                if (value.Any(char.IsDigit)) return;
                streetName = value;
                OnPropertyChanged();
            }
        }

        public int StreetNumber
        {
            get => streetNumber;
            set
            {
                if (Equals(value, streetNumber)) return;
                streetNumber = value;
                OnPropertyChanged();
            }
        }

        public int PostalCode
        {
            get => postalCode;
            set
            {
                if (Equals(value, postalCode)) return;
                postalCode = value;
                OnPropertyChanged();
            }
        }

        public string City
        {
            get => city;
            set
            {
                if (Equals(value, city)) return;
                city = value;
                OnPropertyChanged();
            }
        }

        public int PhoneNumber
        {
            get => phoneNumber;
            set
            {
                if (Equals(value, phoneNumber)) return;
                phoneNumber = value;
                OnPropertyChanged();
            }
        }

        public Account Account
        {
            get => account;
            set
            {
                if (Equals(value, account)) return;
                account = value;
                OnPropertyChanged();
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone(); // Shallow clone
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
