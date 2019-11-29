using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Data.Annotations;

namespace Data.Models
{
    public class Customer : INotifyPropertyChanged, ICloneable, IComparable, IEntity
    {
        private string socialSecurityNumber;
        private string fullName;
        private string streetName;
        private int streetNumber;
        private int postalCode;
        private string city;
        private int phoneNumber;
        private Account account;

        public Customer() // Needed for serialization
        {
        }

        public Customer(string fullName)
        {
            FullName = fullName;
            Account = new Account();
            Guid = Guid.NewGuid();
        }

        public int Id { get; set; }
        public Guid Guid { get; }

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

        public int CompareTo(object obj)
        {
            Customer customer = (Customer) obj;
            return string.CompareOrdinal(FullName, customer.FullName);
        }
    }
}
