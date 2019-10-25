using BankApp.Models;
using BankApp.Views;
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

            DelegateCommand withDrawCommand = new DelegateCommand(Withdraw);
            WithDrawCommand = withDrawCommand;

            DelegateCommand depositCommand = new DelegateCommand(Deposit);
            DepositCommand = depositCommand;

            DelegateCommand editCustomerCommand = new DelegateCommand(ShowCustomerInfoView);
            EditCustomerCommand = editCustomerCommand;

            DelegateCommand goBackCommand = new DelegateCommand(GoBackView);
            GoBackCommand = goBackCommand;
        }

        public DelegateCommand WithDrawCommand { get; set; }
        public DelegateCommand DepositCommand { get; set; }
        public DelegateCommand EditCustomerCommand { get; }
        public DelegateCommand GoBackCommand { get; }


        private void Withdraw()
        {
            Model.Account.Withdraw();
        }

        private void Deposit()
        {
            Model.Account.Deposit();
        }

        public Customer Model
        {
            get => selectedCustomer;
            set
            {
                if( selectedCustomer == value) return;
                selectedCustomer = value;
                RaisePropertyChanged();
            }
        }

        private void ShowCustomerInfoView()
        {
            if (Model != null)
            {
                NavigationParameters parameter = new NavigationParameters { { "selectedCustomer", Model } };
                regionManager.RequestNavigate("MainRegion", nameof(CustomerInfoViewModel), parameter);
            }
        }

        private void GoBackView()
        {
            //regionManager.RequestNavigate("MainRegion", nameof(CustomerView));

            if (journal.CanGoBack)
                journal.GoBack();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            NavigationParameters parameter = navigationContext.Parameters;
            Model = parameter.GetValue<Customer>("selectedCustomer");

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
