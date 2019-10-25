using BankApp.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace BankApp.ViewModels
{
    public class CustomerViewModel : BindableBase
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
        }

        public BankManager Model { get; set; }
        public DelegateCommand AddCustomerCommand { get; }
        public DelegateCommand DeleteCustomerCommand { get; }
        public DelegateCommand ShowAccountCommand { get; }

        private void ShowAccountView()
        {
            if (Model.SelectedCustomer != null)
            {
                NavigationParameters parameter = new NavigationParameters {{"selectedCustomer", Model.SelectedCustomer}};
                regionManager.RequestNavigate("MainRegion", nameof(AccountViewModel), parameter);
            }
        }
    }
}
