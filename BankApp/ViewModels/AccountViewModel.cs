using Data.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace BankApp.ViewModels
{
    public class AccountViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager regionManager;
        private IRegionNavigationJournal journal;
        private Customer selectedCustomer;

        public AccountViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;

            DelegateCommand withDrawCommand = new DelegateCommand(OnWithdraw);
            WithDrawCommand = withDrawCommand;

            DelegateCommand depositCommand = new DelegateCommand(OnDeposit);
            DepositCommand = depositCommand;

            DelegateCommand editCustomerCommand = new DelegateCommand(OnShowCustomerInfoView);
            EditCustomerCommand = editCustomerCommand;

            DelegateCommand goBackCommand = new DelegateCommand(OnGoBack);
            GoBackCommand = goBackCommand;
        }

        public DelegateCommand WithDrawCommand { get; set; }
        public DelegateCommand DepositCommand { get; set; }
        public DelegateCommand EditCustomerCommand { get; }
        public DelegateCommand GoBackCommand { get; }

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

        private void OnWithdraw()
        {
            SelectedCustomer.Account.Withdraw();
        }

        private void OnDeposit()
        {
            SelectedCustomer.Account.Deposit();
        }


        private void OnShowCustomerInfoView()
        {
            if (SelectedCustomer != null)
            {
                NavigationParameters parameter = new NavigationParameters { { "selectedCustomer", SelectedCustomer } };
                regionManager.RequestNavigate("MainRegion", nameof(CustomerInfoViewModel), parameter);
            }
        }

        private void OnGoBack()
        {
            if (journal.CanGoBack)
                journal.GoBack();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            NavigationParameters parameter = navigationContext.Parameters;
            SelectedCustomer = parameter.GetValue<Customer>("selectedCustomer");

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
