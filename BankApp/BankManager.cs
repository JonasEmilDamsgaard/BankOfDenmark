using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Data.Annotations;
using Data.EF;
using Data.Models;

namespace BankApp
{
    public class BankManager : INotifyPropertyChanged
    {
        private Customer selectedCustomer;
        private ObservableCollection<Customer> filteredCustomers = new ObservableCollection<Customer>();
        private string filter = "";
        private bool toggle;

        public BankManager()
        {
            CustomerContext context = new CustomerContext();
            //context.Add(new Customer("Hej"));
            //context.Add(new Customer("Jonas"));
            //context.Add(new Customer("Peter"));
            //context.SaveChanges();

            context.Customers.FirstOrDefault().FullName += "Hej";
            context.SaveChanges();
            Customers = new ObservableCollection<Customer>(context.Customers);
            //Customers.Add(new Customer("Anders"));
            //Customers.Add(new Customer("Jonas"));
            //Customers.Add(new Customer("Peter"));
            UpdateView();
        }

        public Customer SelectedCustomer
        {
            get => selectedCustomer;
            set
            {
                if (Equals(value, selectedCustomer)) return;
                selectedCustomer = value;
            }
        }

        public ObservableCollection<Customer> Customers { get; set; }

        public ObservableCollection<Customer> FilteredCustomers
        {
            get => filteredCustomers;
            set
            {
                if (Equals(value, filteredCustomers)) return;
                filteredCustomers = value;
                OnPropertyChanged();
            }
        }

        public string Filter
        {
            get => filter;
            set
            {
                if (Equals(value, filter)) return;
                filter = value;
                UpdateView();
            }
        }

        private void UpdateView()
        {
            FilteredCustomers = new ObservableCollection<Customer>(); // Clears the filtered customer list

            if (Filter == String.Empty)
            {
                FilteredCustomers = Customers;
            }
            else
            {
                foreach (Customer customer in Customers)
                {
                    if (customer.FullName.ToLower().Contains(Filter.ToLower()))
                        FilteredCustomers.Add(customer);
                }
            }

            SelectedCustomer = FilteredCustomers.LastOrDefault();
        }

        public void AddCustomer()
        {
            Customers.Add(new Customer("New customer"));
            UpdateView();
        }

        public void DeleteCustomer()
        {
            Customers.Remove(SelectedCustomer);
            UpdateView();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SortCustomers()
        {
            if (toggle)
                FilteredCustomers = new ObservableCollection<Customer>(FilteredCustomers.OrderByDescending(c => c));
            else
                FilteredCustomers = new ObservableCollection<Customer>(FilteredCustomers.OrderBy(c => c));

            toggle = !toggle;
        }


    }
}
