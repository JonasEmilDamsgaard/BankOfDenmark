using BankApp.Database;
using Data.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace BankApp.ViewModels
{
    public class CustomerInfoViewModel : BindableBase, INavigationAware
    {
      private readonly IRegionManager regionManager;
      private readonly IUnitOfWork unitOfWork;
      private IRegionNavigationJournal journal;
      private Customer selectedCustomer;
      //private bool isChanges;
      private Customer originalCustomer;

      public CustomerInfoViewModel(IRegionManager regionManager, IUnitOfWork unitOfWork)
      {
          this.regionManager = regionManager;
          this.unitOfWork = unitOfWork;

          DelegateCommand okCommand = new DelegateCommand(OnOkCommand);
          OkCommand = okCommand;

          DelegateCommand cancelCommand = new DelegateCommand(OnGoBack);
          CancelCommand = cancelCommand;
      }
      public DelegateCommand OkCommand { get; }
      public DelegateCommand CancelCommand { get; }

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

      private void OnOkCommand()
      {
          //isChanges = true;

          originalCustomer.FullName = SelectedCustomer.FullName;
          originalCustomer.Account = SelectedCustomer.Account;
          originalCustomer.City = SelectedCustomer.City;
          originalCustomer.PhoneNumber = SelectedCustomer.PhoneNumber;
          originalCustomer.PostalCode = SelectedCustomer.PostalCode;
          originalCustomer.SocialSecurityNumber = SelectedCustomer.SocialSecurityNumber;
          originalCustomer.StreetName = SelectedCustomer.StreetName;
          originalCustomer.StreetNumber = SelectedCustomer.StreetNumber;

          unitOfWork.Complete();
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

          originalCustomer = unitOfWork.Customers.GetById(id);
          SelectedCustomer = originalCustomer.Clone() as Customer;

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
