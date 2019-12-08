using BankApp.Services;
using Data.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace BankApp.ViewModels
{
    public class CustomerInfoViewModel : BindableBase, INavigationAware
    {
        private readonly CustomerService customerService;
        private IRegionNavigationJournal journal;
        private Customer cloneCustomer;

        public CustomerInfoViewModel(CustomerService customerService)
        {
            this.customerService = customerService;

            OkCommand = new DelegateCommand(OnOkCommand);
            CancelCommand = new DelegateCommand(OnCancelCommand);
        }
        public DelegateCommand OkCommand { get; }
        public DelegateCommand CancelCommand { get; }

        public Customer SelectedCustomer
        {
            get => cloneCustomer;
            set
            {
                if (cloneCustomer == value) return;
                cloneCustomer = value;
                RaisePropertyChanged();
            }
        }

        private void OnOkCommand()
        {
            customerService.EditCustomer(SelectedCustomer);
            OnGoBack();
        }

        private void OnCancelCommand()
        {
            OnGoBack();
        }

        private void OnGoBack()
        {
            if (journal.CanGoBack)
            {
                journal.GoBack();
            }
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            NavigationParameters parameter = navigationContext.Parameters;
            var id = parameter.GetValue<int>("selectedCustomer");

            var selectedCustomer = customerService.GetSelectedCustomer(id);
            SelectedCustomer = selectedCustomer.Clone() as Customer;

            journal = navigationContext.NavigationService.Journal;
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
