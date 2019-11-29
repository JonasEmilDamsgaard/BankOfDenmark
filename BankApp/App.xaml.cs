using BankApp.Views;
using System.Windows;
using BankApp.Database;
using BankApp.ViewModels;
using Data.EF;
using Data.Models;
using Prism.Ioc;
using Prism.Regions;
using Prism.Unity;

namespace BankApp
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : PrismApplication
  {
    protected override Window CreateShell()
    {
      return Container.Resolve<MainWindow>();
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
      //var bankManager = BankManagerFactory.Create();
      containerRegistry.RegisterSingleton<BankManager>(); //Local singleton
      containerRegistry.Register(typeof(CustomerContext));
      containerRegistry.Register(typeof(ICustomerRepository), typeof(CustomerRepository));
      containerRegistry.Register(typeof(IUnitOfWork), typeof(UnitOfWork));

      containerRegistry.RegisterForNavigation<CustomerView>(nameof(CustomerViewModel));
      containerRegistry.RegisterForNavigation<CustomerInfoView>(nameof(CustomerInfoViewModel));
      containerRegistry.RegisterForNavigation<AccountView>(nameof(AccountViewModel));
    }

    protected override void OnInitialized()
    {
      base.OnInitialized();

      Container.Resolve<IRegionManager>().RequestNavigate("MainRegion", nameof(CustomerViewModel));
    }
  }
}
