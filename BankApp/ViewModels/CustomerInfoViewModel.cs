using BankApp.Models;
using BankApp.Services;
using Data.DataAccess;
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
      private readonly CustomerService customerService;
      private IRegionNavigationJournal journal;
      private Customer selectedCustomer;
      private Customer originalCustomer;
      private CustomerWrapper customerWrapper;

      public CustomerInfoViewModel(IRegionManager regionManager, IUnitOfWork unitOfWork, CustomerService customerService)
      {
          this.regionManager = regionManager;
          this.unitOfWork = unitOfWork;
          this.customerService = customerService;

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

      public CustomerWrapper CustomerWrapper
      {
          get => customerWrapper;
          set
          {
              if (customerWrapper == value) return;
              customerWrapper = value;
              RaisePropertyChanged();
          }
      }

      private void OnOkCommand()
      {
          originalCustomer = customerService.EditCustomer(SelectedCustomer);
          RaisePropertyChanged(nameof(CustomerWrapper));

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

          CustomerWrapper = new CustomerWrapper(originalCustomer);

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
