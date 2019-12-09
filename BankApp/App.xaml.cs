using BankApp.Views;
using System.Windows;
using BankApp.Services;
using BankApp.ViewModels;
using Data.DataAccess;
using Data.DataAccess.Repositories;
using Data.EF;
using Data.EF.DataAccess;
using Data.EF.DataAccess.Repositories;
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
            containerRegistry.RegisterSingleton(typeof(CustomerContext));
            containerRegistry.RegisterSingleton(typeof(ICustomerRepository), typeof(CustomerRepository));
            containerRegistry.RegisterSingleton(typeof(IUnitOfWork), typeof(UnitOfWork));
            containerRegistry.RegisterSingleton(typeof(CustomerService));
            containerRegistry.RegisterSingleton(typeof(AccountService));

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
