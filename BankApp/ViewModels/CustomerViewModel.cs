using System.Collections;
using System.Collections.ObjectModel;
using Data.Models;
using System.Linq;
using System.Windows.Data;
using BankApp.DataAccess;
using BankApp.DataAccess.Repositories;
using BankApp.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace BankApp.ViewModels
{
    public class CustomerViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager regionManager;
        private readonly ICustomerRepository customerRepository;
        private readonly CustomerService customerService;
        private Customer selectedCustomer;
        private bool toggle;
        private string filter;

        public CustomerViewModel(IRegionManager regionManager, ICustomerRepository customerRepository, CustomerService customerService)
        {
            this.regionManager = regionManager;
            this.customerRepository = customerRepository;
            this.customerService = customerService;

            Customers = new ObservableCollection<Customer>();

            DelegateCommand addCustomerCommand = new DelegateCommand(AddCustomer);
            AddCustomerCommand = addCustomerCommand;

            DelegateCommand deleteCustomerCommand = new DelegateCommand(DeleteCustomer);
            DeleteCustomerCommand = deleteCustomerCommand;

            DelegateCommand sortCustomersCommand = new DelegateCommand(SortCustomers);
            SortCustomersCommand = sortCustomersCommand;

            DelegateCommand showAccountCommand = new DelegateCommand(OnShowAccountView);
            ShowAccountCommand = showAccountCommand;

            DelegateCommand showCustomerInfoCommand = new DelegateCommand(OnShowCustomerInfoView);
            ShowCustomerInfoCommand = showCustomerInfoCommand;
        }

        public DelegateCommand AddCustomerCommand { get; }
        public DelegateCommand DeleteCustomerCommand { get; }
        public DelegateCommand SortCustomersCommand { get; }
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
                FilterCustomers();
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

        private void FilterCustomers()
        {
            Customers.Clear();
            Customers.AddRange(customerRepository.Find(c => c.FullName.ToLower().Contains(Filter.ToLower())));
        }

        private void OnShowAccountView()
        {
            if (SelectedCustomer != null)
            {
                NavigationParameters parameter = new NavigationParameters{{ "selectedCustomer", SelectedCustomer.Id }};
                regionManager.RequestNavigate("MainRegion", nameof(AccountViewModel), parameter);
            }
        }

        private void OnShowCustomerInfoView()
        {
            if (SelectedCustomer != null)
            {
                NavigationParameters parameter = new NavigationParameters{{ "selectedCustomer", SelectedCustomer.Id }};
                regionManager.RequestNavigate("MainRegion", nameof(CustomerInfoViewModel), parameter);
            }
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Customers.Clear();
            Customers.AddRange(customerRepository.GetAll());
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
