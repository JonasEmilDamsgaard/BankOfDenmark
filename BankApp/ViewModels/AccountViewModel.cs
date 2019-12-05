using System;
using BankApp.Models;
using BankApp.Services;
using Data.DataAccess;
using Data.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace BankApp.ViewModels
{
    public class AccountViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager regionManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly AccountService accountService;
        private IRegionNavigationJournal journal;
        private Customer selectedCustomer;
        private AccountWrapper accountWrapper;

        public AccountViewModel(IRegionManager regionManager, IUnitOfWork unitOfWork, AccountService accountService)
        {
            this.regionManager = regionManager;
            this.unitOfWork = unitOfWork;
            this.accountService = accountService;

            DelegateCommand withDrawCommand = new DelegateCommand(OnWithdraw);
            WithDrawCommand = withDrawCommand;

            DelegateCommand depositCommand = new DelegateCommand(OnDeposit);
            DepositCommand = depositCommand;

            DelegateCommand goBackCommand = new DelegateCommand(OnGoBack);
            GoBackCommand = goBackCommand;
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

        public AccountWrapper AccountWrapper
        {
            get => accountWrapper;
            set
            {
                if (accountWrapper == value) return;
                accountWrapper = value;
                RaisePropertyChanged();
            }
        }

        private void OnWithdraw()
        {
            accountService.Withdraw(SelectedCustomer.Account);
            RaisePropertyChanged(nameof(AccountWrapper));
        }

        private void OnDeposit()
        {
            accountService.Deposit(SelectedCustomer.Account);
            RaisePropertyChanged(nameof(AccountWrapper));
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

            SelectedCustomer = unitOfWork.Customers.GetById(id);
            AccountWrapper = new AccountWrapper(SelectedCustomer.Account);

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
