using BankApp.Services;
using Data.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace BankApp.ViewModels
{
    public class AccountViewModel : BindableBase, INavigationAware
    {
        private readonly CustomerService customerService;
        private readonly AccountService accountService;
        private IRegionNavigationJournal journal;
        private Customer selectedCustomer;
        private double amount;

        public AccountViewModel(CustomerService customerService, AccountService accountService)
        {
            this.customerService = customerService;
            this.accountService = accountService;

            WithDrawCommand = new DelegateCommand(OnWithdraw);
            DepositCommand = new DelegateCommand(OnDeposit);
            GoBackCommand = new DelegateCommand(OnGoBack);
        }

        public DelegateCommand WithDrawCommand { get; set; }
        public DelegateCommand DepositCommand { get; set; }
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

        public double Amount
        {
            get => amount;
            set
            {
                if (amount == value) return;
                amount = value;
                RaisePropertyChanged();
            }
        }

        private void OnDeposit()
        {
            SelectedCustomer = accountService.Deposit(SelectedCustomer, Amount);
            RaisePropertyChanged(nameof(SelectedCustomer));
            Amount = 0;
        }

        private void OnWithdraw()
        {
            SelectedCustomer = accountService.Withdraw(SelectedCustomer, Amount);
            RaisePropertyChanged(nameof(SelectedCustomer));
            Amount = 0;
        }

        private void OnGoBack()
        {
            if (journal.CanGoBack)
                journal.GoBack();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            NavigationParameters parameter = navigationContext.Parameters;
            var id = parameter.GetValue<int>("selectedCustomer");
            SelectedCustomer = customerService.GetSelectedCustomer(id);

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
