using BankApp.Services;
using Data.DataAccess;
using Data.EF.DataAccess;
using Data.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace BankApp.ViewModels
{
    public class CustomerInfoViewModel : BindableBase, INavigationAware
    {
      private readonly IRegionManager regionManager;
      private readonly UnitOfWork unitOfWork;
      private readonly CustomerService customerService;
      private IRegionNavigationJournal journal;
      private Customer selectedCustomer;
      private Customer cloneCustomer;

      public CustomerInfoViewModel(IRegionManager regionManager, UnitOfWork unitOfWork, CustomerService customerService)
      {
          this.regionManager = regionManager;
          this.unitOfWork = unitOfWork;
          this.customerService = customerService;

          DelegateCommand okCommand = new DelegateCommand(OnOkCommand);
          OkCommand = okCommand;

          DelegateCommand cancelCommand = new DelegateCommand(OnCancelCommand);
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

      public Customer CloneCustomer
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
          SelectedCustomer = customerService.EditCustomer(CloneCustomer);
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

          SelectedCustomer = unitOfWork.Customers.GetById(id);
          CloneCustomer = SelectedCustomer.Clone() as Customer;

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
