using System;
using BankApp.Database;
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
        private IRegionNavigationJournal journal;
        private Customer selectedCustomer;

        public AccountViewModel(IRegionManager regionManager, IUnitOfWork unitOfWork)
        {
            this.regionManager = regionManager;
            this.unitOfWork = unitOfWork;

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

        private void OnWithdraw()
        {
            SelectedCustomer.Account.Withdraw();
        }

        private void OnDeposit()
        {
            SelectedCustomer.Account.Deposit();
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

            journal = navigationContext.NavigationService.Journal;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
          unitOfWork.Complete();
          //unitOfWork.Dispose();
        }
    }
}
