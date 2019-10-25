using System;
using System.Diagnostics;
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

      public CustomerInfoViewModel(IRegionManager regionManager)
      {
          this.regionManager = regionManager;

          DelegateCommand goBackCommand = new DelegateCommand(GoBackView);
          GoBackCommand = goBackCommand;
      }

      public Customer Model
      {
          get => selectedCustomer;
          set
          {
              if (selectedCustomer == value) return;
              selectedCustomer = value;
              RaisePropertyChanged();
          }
      }

      public DelegateCommand GoBackCommand { get; }

      private void GoBackView()
      {
          if (journal.CanGoBack)
              journal.GoBack();
      }

      public void OnNavigatedTo(NavigationContext navigationContext)
      {
          NavigationParameters parameter = navigationContext.Parameters;
          //Model = parameter.GetValue<Customer>("selectedCustomer");
          SetCustomer(parameter.GetValue<Customer>("selectedCustomer"));

          journal = navigationContext.NavigationService.Journal;
      }

      public bool IsNavigationTarget(NavigationContext navigationContext)
      {
        return true;
      }

      public void OnNavigatedFrom(NavigationContext navigationContext)
      {

      }

      private void SetCustomer(Customer customer)
      {
          Customer originalCustomer = customer;
          Model = (Customer) customer.Clone();

      }
    }


}
