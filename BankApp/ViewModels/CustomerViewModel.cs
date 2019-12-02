using System.Collections.ObjectModel;
using Data.Models;
using System.Linq;
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

        public CustomerViewModel(IRegionManager regionManager, ICustomerRepository customerRepository, CustomerService customerService)
        {
            this.regionManager = regionManager;
            this.customerRepository = customerRepository;
            this.customerService = customerService;

            DelegateCommand addCustomerCommand = new DelegateCommand(AddCustomer);
            AddCustomerCommand = addCustomerCommand;

            DelegateCommand deleteCustomerCommand = new DelegateCommand(() => customerService.DeleteCustomer(SelectedCustomer));
            DeleteCustomerCommand = deleteCustomerCommand;

            DelegateCommand sortCustomersCommand = new DelegateCommand(CustomerSorting);
            SortCustomersCommand = sortCustomersCommand;

            DelegateCommand showAccountCommand = new DelegateCommand(OnShowAccountView);
            ShowAccountCommand = showAccountCommand;

            DelegateCommand showCustomerInfoCommand = new DelegateCommand(OnShowCustomerInfoView);
            ShowCustomerInfoCommand = showCustomerInfoCommand;

            Customers = new ObservableCollection<Customer>();
        }

        private void AddCustomer()
        {
            var newCustomer = customerService.AddCustomer();
            Customers.Add(newCustomer);

            SelectedCustomer = newCustomer;
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
                OnPropertyChanged();
            }
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

        private void CustomerSorting()
        {

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
