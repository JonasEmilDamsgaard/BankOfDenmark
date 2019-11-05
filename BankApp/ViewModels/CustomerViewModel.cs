using System.Diagnostics;
using System.Linq;
using BankApp.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace BankApp.ViewModels
{
    public class CustomerViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager regionManager;

        public CustomerViewModel(IRegionManager regionManager, BankManager bankManager)
        {
            this.regionManager = regionManager;
            Model = bankManager;

            DelegateCommand addCustomerCommand = new DelegateCommand(Model.AddCustomer);
            AddCustomerCommand = addCustomerCommand;

            DelegateCommand deleteCustomerCommand = new DelegateCommand(Model.DeleteCustomer);
            DeleteCustomerCommand = deleteCustomerCommand;

            DelegateCommand showAccountCommand = new DelegateCommand(ShowAccountView);
            ShowAccountCommand = showAccountCommand;

            DelegateCommand showCustomerInfoCommand = new DelegateCommand(ShowCustomerInfoView);
            ShowCustomerInfoCommand = showCustomerInfoCommand;
        }

        public BankManager Model { get; set; }
        public DelegateCommand AddCustomerCommand { get; }
        public DelegateCommand DeleteCustomerCommand { get; }
        public DelegateCommand ShowAccountCommand { get; }
        public DelegateCommand ShowCustomerInfoCommand { get; }

        private void ShowAccountView()
        {
            if (Model.SelectedCustomer != null)
            {
                NavigationParameters parameter = new NavigationParameters { { "selectedCustomer", Model.SelectedCustomer } };
                regionManager.RequestNavigate("MainRegion", nameof(AccountViewModel), parameter);
            }
        }

        private void ShowCustomerInfoView()
        {
            if (Model.SelectedCustomer != null)
            {
                NavigationParameters parameter = new NavigationParameters { { "selectedCustomer", Model.SelectedCustomer } };
                regionManager.RequestNavigate("MainRegion", nameof(CustomerInfoViewModel), parameter);
            }
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            NavigationParameters parameter = navigationContext.Parameters;

            if (parameter.ContainsKey("editedCustomer"))
            {
                var updatedCustomer = parameter.GetValue<Customer>("editedCustomer");

                //var updatedCustomer = parameter["editedCustomer"] as Customer;
                if (updatedCustomer != null)
                {
                    //Update model
                    var customer = Model.Customers.FirstOrDefault(c => c.Account == updatedCustomer.Account);
                    //customer.FullName = updateddata.FullName;
                    Model.FilteredCustomers.Remove(customer);
                    Model.FilteredCustomers.Add( updatedCustomer );
                }
            }

            //if (parameter.ContainsKey("selectedCustomer"))
            //    Model.SelectedCustomer = parameter.GetValue<Customer>("selectedCustomer");
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
