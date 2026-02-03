using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using Prism.Events;

using Aksl.Toolkit.Dialogs;
using Aksl.Toolkit.Services;

using Aksl.Infrastructure;
using Aksl.Infrastructure.Events;

using Aksl.Modules.Shell;
using Aksl.Modules.Shell.ViewModels;
using Aksl.Modules.Shell.Views;

using Aksl.Modules.HamburgerMenuSideBarTab;
using Aksl.Modules.HamburgerMenuNavigationSideBarTab;
using Aksl.Modules.HamburgerMenuTreeSideBarTab;

using Aksl.Modules.Account;
using Aksl.Modules.Home;
using Aksl.Modules.LiveCharts.Axes;
using Aksl.Modules.LiveCharts.Bars;

namespace Aksl.Wpf.Unity
{
    public partial class App
    {
        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelLocationProvider.Register(typeof(ShellView).ToString(), () => Container.Resolve<ShellViewModel>());
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            #region Initialize
            var services = new ServiceCollection();
            services.AddOptions();

            var serviceProvider = services.BuildServiceProvider();
            containerRegistry.RegisterInstance<IServiceProvider>(serviceProvider);
            #endregion

            containerRegistry.RegisterSingleton(typeof(IDialogService), typeof(DialogService));
            containerRegistry.RegisterSingleton(typeof(IDialogViewService), typeof(DialogViewService));

            containerRegistry.RegisterDialog<ConfirmView, ConfirmViewModel>();

            RegisterMenuFactoryAsync(containerRegistry).GetAwaiter().GetResult();

            RegisterBuildWorkspaceViewEventAsync();
        }

        protected async Task RegisterMenuFactoryAsync(IContainerRegistry containerRegistry)
        {
            try
            {
                MenuService menuService = new(new List<string> {"pack://application:,,,/Aksl.Wpf.LiveCharts;Component/Data/AllMenus.xml",
                                                                "pack://application:,,,/Aksl.Wpf.LiveCharts;Component/Data/Axes.xml",
                                                                "pack://application:,,,/Aksl.Wpf.LiveCharts;Component/Data/Bars.xml",
                                                                });

                await menuService.CreateMenusAsync();

                containerRegistry.RegisterInstance<IMenuService>(menuService);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }

        protected Task RegisterBuildWorkspaceViewEventAsync()
        {
            try
            {
                var eventAggregator = Container.Resolve<IEventAggregator>();

                _ = eventAggregator.GetEvent<OnBuildHamburgerMenuSideBarTabWorkspaceViewEvent>();
                _ = eventAggregator.GetEvent<OnBuildHamburgerMenuNavigationSideBarTabWorkspaceViewEvent>();
                _ = eventAggregator.GetEvent<OnBuildHamburgerMenuTreeSideBarTabWorkspaceViewEvent>();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }

            return Task.CompletedTask;
        }
  

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            _ = moduleCatalog.AddModule(nameof(AccountModule), typeof(AccountModule).AssemblyQualifiedName, InitializationMode.WhenAvailable);

            _ = moduleCatalog.AddModule(nameof(HomeModule), typeof(HomeModule).AssemblyQualifiedName, InitializationMode.WhenAvailable);
            _ = moduleCatalog.AddModule(nameof(AxesModule), typeof(AxesModule).AssemblyQualifiedName, InitializationMode.WhenAvailable);
            _ = moduleCatalog.AddModule(nameof(BarsModule), typeof(BarsModule).AssemblyQualifiedName, InitializationMode.WhenAvailable);

            _ = moduleCatalog.AddModule(typeof(HamburgerMenuSideBarTabModule).Name, typeof(HamburgerMenuSideBarTabModule).AssemblyQualifiedName, InitializationMode.WhenAvailable);
            _ = moduleCatalog.AddModule(typeof(HamburgerMenuNavigationSideBarTabModule).Name, typeof(HamburgerMenuNavigationSideBarTabModule).AssemblyQualifiedName, InitializationMode.WhenAvailable);
            _ = moduleCatalog.AddModule(typeof(HamburgerMenuTreeSideBarTabModule).Name, typeof(HamburgerMenuTreeSideBarTabModule).AssemblyQualifiedName, InitializationMode.WhenAvailable);

            _ = moduleCatalog.AddModule(typeof(ShellModule).Name, typeof(ShellModule).AssemblyQualifiedName, InitializationMode.WhenAvailable, typeof(HamburgerMenuSideBarTabModule).Name);
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<ShellView>();
        }

        protected override  void InitializeShell(Window shell)
        {
            base.InitializeShell(shell);
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
    }
}
