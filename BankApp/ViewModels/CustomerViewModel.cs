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

        public CustomerViewModel(IRegionManager regionManager, CustomerService customerService)
        {
            this.regionManager = regionManager;
            this.customerService = customerService;

            DelegateCommand addCustomerCommand = new DelegateCommand(AddCustomer);
            AddCustomerCommand = addCustomerCommand;

            DelegateCommand deleteCustomerCommand = new DelegateCommand(DeleteCustomer);
            DeleteCustomerCommand = deleteCustomerCommand;

            DelegateCommand sortCustomersCommand = new DelegateCommand(SortCustomers);
            SortCustomersCommand = sortCustomersCommand;

            DelegateCommand findMostValuedCustomerCommand = new DelegateCommand(FindMostValuedCustomer);
            FindMostValuedCustomerCommand = findMostValuedCustomerCommand;

            DelegateCommand showAccountCommand = new DelegateCommand(OnShowAccountView);
            ShowAccountCommand = showAccountCommand;

            DelegateCommand showCustomerInfoCommand = new DelegateCommand(OnShowCustomerInfoView);
            ShowCustomerInfoCommand = showCustomerInfoCommand;

            Customers = new ObservableCollection<Customer>();
        }

        public DelegateCommand AddCustomerCommand { get; }
        public DelegateCommand DeleteCustomerCommand { get; }
        public DelegateCommand SortCustomersCommand { get; }
        public DelegateCommand FindMostValuedCustomerCommand { get; }
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

        private void FilterCustomers()
        {
            Customers.Clear();
            Customers.AddRange(customerService.FilterCustomers(Filter));
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

        private void FindMostValuedCustomer()
        {
            SelectedCustomer = customerService.GetMostValuedCustomers(1).First();
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
