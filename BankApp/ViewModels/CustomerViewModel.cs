using System.Collections.ObjectModel;
using Data.Models;
using System.Linq;
using BankApp.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace BankApp.ViewModels
{
    public class CustomerViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager regionManager;
        private readonly CustomerService customerService;
        private Customer selectedCustomer;
        private bool toggle;
        private string filter;
        private double totalCustomerDeposits;
        private double totalCustomerWithdraws;

        public CustomerViewModel(IRegionManager regionManager, CustomerService customerService)
        {
            this.regionManager = regionManager;
            this.customerService = customerService;

            AddCustomerCommand = new DelegateCommand(AddCustomer);
            DeleteCustomerCommand = new DelegateCommand(DeleteCustomer);
            SortCustomersCommand = new DelegateCommand(SortCustomers);
            FindTopCustomerCommand = new DelegateCommand(FindTopCustomer);
            ShowAccountCommand = new DelegateCommand(OnShowAccountView);
            ShowCustomerInfoCommand = new DelegateCommand(OnShowCustomerInfoView);

            Customers = new ObservableCollection<Customer>();
        }

        public DelegateCommand AddCustomerCommand { get; }
        public DelegateCommand DeleteCustomerCommand { get; }
        public DelegateCommand SortCustomersCommand { get; }
        public DelegateCommand FindTopCustomerCommand { get; }
        public DelegateCommand ShowAccountCommand { get; }
        public DelegateCommand ShowCustomerInfoCommand { get; }
        public ObservableCollection<Customer> Customers { get; set; }

        public Customer SelectedCustomer
        {
            get => selectedCustomer;
            set
            {
                if (selectedCustomer == value) return;

                selectedCustomer = value;
                RaisePropertyChanged();
            }
        }

        public string Filter
        {
            get => filter;
            set
            {
                if (filter == value) return;

                filter = value;
                RaisePropertyChanged();

                FilterCustomers();
            }
        }

        public double TotalCustomerDeposits
        {
            get => totalCustomerDeposits;
            set
            {
                if (totalCustomerDeposits == value) return;

                totalCustomerDeposits = value;
                RaisePropertyChanged();
            }
        }

        public double TotalCustomerWithdraws
        {
          get => totalCustomerWithdraws;
          set
          {
            if (totalCustomerWithdraws == value) return;

            totalCustomerWithdraws = value;
            RaisePropertyChanged();
          }
        }

        private void AddCustomer()
        {
            var newCustomer = customerService.AddCustomer();
            Customers.Add(newCustomer);

            SelectedCustomer = newCustomer;
        }

        private void DeleteCustomer()
        {
            customerService.DeleteCustomer(SelectedCustomer);
            Customers.Remove(SelectedCustomer);

            SelectedCustomer = Customers.LastOrDefault();
            CalculateStatistics();
    }

        private void FilterCustomers()
        {
          Customers.Clear();
          Customers.AddRange(customerService.FilterCustomers(Filter));
          SelectedCustomer = Customers.LastOrDefault();
        }

        private void SortCustomers()
        {
            var customers = Customers.ToList();

            Customers.Clear();
            Customers.AddRange(toggle
                ? customers.OrderByDescending(c => c.FullName)
                : customers.OrderBy(c => c.FullName));

            toggle = !toggle;
        }

        private void CalculateStatistics()
        {
            double deposits = 0;
            double withdraws = 0;

            foreach (var customer in Customers)
            {
              if (customer.Account.Balance > 0)
                deposits += customer.Account.Balance;
              else
              {
                withdraws += customer.Account.Balance;
              }
            }

            TotalCustomerDeposits = deposits;
            TotalCustomerWithdraws = withdraws;
        }

        private void FindTopCustomer()
        {
            var topCustomer = customerService.GetTopCustomers(1).FirstOrDefault();

            if (topCustomer != null)
                SelectedCustomer = topCustomer;
        }

        private void OnShowAccountView()
        {
            if (SelectedCustomer == null) return;

            NavigationParameters parameter = new NavigationParameters{{ "selectedCustomer", SelectedCustomer.Id }};
            regionManager.RequestNavigate("MainRegion", nameof(AccountViewModel), parameter);
        }

        private void OnShowCustomerInfoView()
        {
            if (SelectedCustomer == null) return;

            NavigationParameters parameter = new NavigationParameters{{ "selectedCustomer", SelectedCustomer.Id }};
            regionManager.RequestNavigate("MainRegion", nameof(CustomerInfoViewModel), parameter);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Customers.Clear();
            Customers.AddRange(customerService.GetAllCustomers);

            CalculateStatistics();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}
