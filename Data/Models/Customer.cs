using System;

namespace Data.Models
{
    public class Customer : ICloneable, IComparable
    {
        public Customer(string fullName)
        {
            FullName = fullName;
            Account = new Account();
        }

        public int Id { get; set; }
        public string SocialSecurityNumber { get; set; }
        public string FullName { get; set; }
        public string StreetName { get; set; }
        public int StreetNumber { get; set; }
        public int PostalCode { get; set; }
        public string City { get; set; }
        public int PhoneNumber { get; set; }
        public Account Account { get; set; }

        public Object Clone()
        {
            return MemberwiseClone();
        }

        public int CompareTo(object obj)
        {
            Customer customer = (Customer)obj;
            return string.CompareOrdinal(FullName, customer.FullName);
        }
    }
}
