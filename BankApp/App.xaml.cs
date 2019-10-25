using System;
using BankApp.Views;
using System.Windows;
using BankApp.Models;
using BankApp.ViewModels;
using Prism.Ioc;
using Prism.Regions;

namespace BankApp
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App
  {
    protected override Window CreateShell()
    {
      return Container.Resolve<MainWindow>();
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
      //var bankManager = BankManagerFactory.Create();
      containerRegistry.RegisterSingleton<BankManager>(); //Local singleton

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
