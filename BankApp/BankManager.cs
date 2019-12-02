using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using BankApp.DataAccess;
using BankApp.DataAccess.Repositories;
using Data.Annotations;
using Data.EF;
using Data.Models;

namespace BankApp
{
    public class BankManager : INotifyPropertyChanged
    {
        private Customer selectedCustomer;
        //private ObservableCollection<Customer> filteredCustomers = new ObservableCollection<Customer>();
        //private string filter = "";
        //private bool toggle;

        public BankManager()
        {
            //IRepository<Customer> repository = new CustomerRepository(new CustomerContext());
            //Customers = new ObservableCollection<Customer>(repository.GetAll());
            //UpdateView();

            //IRepository<Customer> customerRepository = new CustomerRepository(new DatabaseContext());
            //Customers = new ObservableCollection<Customer>(customerRepository.GetAll());

            //IRepository<Account> accountRepository = new AccountRepository(new DatabaseContext());
            //Accounts = new List<Account>(accountRepository.GetAll());
        }
        public Customer SelectedCustomer
        {
            get => selectedCustomer;
            set
            {
                if (Equals(value, selectedCustomer)) return;
                selectedCustomer = value;
                OnPropertyChanged();
            }
        }
        //public ObservableCollection<Customer> Customers { get; set; }
        //public List<Account> Accounts { get; set; }
        

        //public string Filter
        //{
        //    get => filter;
        //    set
        //    {
        //        if (Equals(value, filter)) return;
        //        filter = value;
        //    }
        //}

        //private void UpdateView()
        //{
        //  FilteredCustomers = new ObservableCollection<Customer>(); // Clears the filtered customer list

        //  if (Filter == String.Empty)
        //  {
        //    FilteredCustomers = Customers;
        //  }
        //  else
        //  {
        //    foreach (Customer customer in Customers)
        //    {
        //      if (customer.FullName.ToLower().Contains(Filter.ToLower()))
        //        FilteredCustomers.Add(customer);
        //    }
        //  }

        //  SelectedCustomer = FilteredCustomers.LastOrDefault();
        //}


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //public void SortCustomers()
        //{
        //    if (toggle)
        //        FilteredCustomers = new ObservableCollection<Customer>(FilteredCustomers.OrderByDescending(c => c));
        //    else
        //        FilteredCustomers = new ObservableCollection<Customer>(FilteredCustomers.OrderBy(c => c));

        //    toggle = !toggle;
        //}
    }
}
