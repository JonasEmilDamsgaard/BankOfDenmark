using System.Collections.ObjectModel;
using Data.Models;
using System.Linq;
using BankApp.Database;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace BankApp.ViewModels
{
    public class CustomerViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager regionManager;
        private readonly IUnitOfWork unitOfWork;

        public CustomerViewModel(IRegionManager regionManager, BankManager bankManager, IUnitOfWork unitOfWork)
        {
            this.regionManager = regionManager;
            this.unitOfWork = unitOfWork;
            Model = bankManager;

            DelegateCommand addCustomerCommand = new DelegateCommand(Model.AddCustomer);
            AddCustomerCommand = addCustomerCommand;

            DelegateCommand deleteCustomerCommand = new DelegateCommand(Model.DeleteCustomer);
            DeleteCustomerCommand = deleteCustomerCommand;

            DelegateCommand sortCustomersCommand = new DelegateCommand(Model.SortCustomers);
            SortCustomersCommand = sortCustomersCommand;

            DelegateCommand showAccountCommand = new DelegateCommand(OnShowAccountView);
            ShowAccountCommand = showAccountCommand;

            DelegateCommand showCustomerInfoCommand = new DelegateCommand(OnShowCustomerInfoView);
            ShowCustomerInfoCommand = showCustomerInfoCommand;
        }

        public BankManager Model { get; set; }
        public DelegateCommand AddCustomerCommand { get; }
        public DelegateCommand DeleteCustomerCommand { get; }
        public DelegateCommand SortCustomersCommand { get; }
        public DelegateCommand ShowAccountCommand { get; }
        public DelegateCommand ShowCustomerInfoCommand { get; }

        private void OnShowAccountView()
        {
            if (Model.SelectedCustomer != null)
            {
                NavigationParameters parameter = new NavigationParameters{{ "selectedCustomer", Model.SelectedCustomer.Id }};
                regionManager.RequestNavigate("MainRegion", nameof(AccountViewModel), parameter);
            }
        }

        private void OnShowCustomerInfoView()
        {
            if (Model.SelectedCustomer != null)
            {
                NavigationParameters parameter = new NavigationParameters{{ "selectedCustomer", Model.SelectedCustomer.Id }};
                regionManager.RequestNavigate("MainRegion", nameof(CustomerInfoViewModel), parameter);
            }
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            //NavigationParameters parameter = navigationContext.Parameters;

            //if (parameter.ContainsKey("editedCustomer"))
            //{
                //Customer updatedCustomer = parameter.GetValue<Customer>("editedCustomer");
                //var updatedCustomer = parameter["editedCustomer"] as Customer;

                //if (updatedCustomer != null)
                //{
                //    var customer = Model.Customers.FirstOrDefault(c => c.Id == updatedCustomer.Id);
                //    Model.FilteredCustomers.Remove(customer);
                //    Model.FilteredCustomers.Add(updatedCustomer);
                //}
            //}

            Model.Customers.Clear();
            Model.Customers.AddRange(unitOfWork.Customers.GetAll());
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
