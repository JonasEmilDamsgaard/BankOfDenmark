using BankApp.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace BankApp.ViewModels
{
    public class CustomerInfoViewModel : BindableBase, INavigationAware
    {
      private readonly IRegionManager regionManager;
      private IRegionNavigationJournal journal;
      private Customer selectedCustomer;
      private bool isChanges;

      public CustomerInfoViewModel(IRegionManager regionManager)
      {
          this.regionManager = regionManager;

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
          isChanges = true;
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
          SelectedCustomer = (Customer) parameter.GetValue<Customer>("selectedCustomer").Clone();

          journal = navigationContext.NavigationService.Journal;
      }

      public bool IsNavigationTarget(NavigationContext navigationContext)
      {
          return true;
      }

      public void OnNavigatedFrom(NavigationContext navigationContext)
      {
          if (isChanges)
          {
              navigationContext.Parameters.Add("editedCustomer", SelectedCustomer);
              isChanges = false;
          }
      }
    }
}
