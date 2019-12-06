using System;

namespace Data.Models
{
    public class Customer : IEntity, ICloneable, IComparable
    {
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
        public string SocialSecurityNumber { get; set; }
        public string FullName { get; set; }
        public string StreetName { get; set; }
        public int StreetNumber { get; set; }
        public int PostalCode { get; set; }
        public string City { get; set; }
        public int PhoneNumber { get; set; }
        public Account Account { get; set; }

        public object Clone()
        {
            return MemberwiseClone(); // Shallow clone
        }

        public int CompareTo(object obj)
        {
            Customer customer = (Customer)obj;
            return string.CompareOrdinal(FullName, customer.FullName);
        }
    }
}
