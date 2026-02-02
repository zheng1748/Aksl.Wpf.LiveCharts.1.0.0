using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;

using Unity;

using Aksl.Modules.LiveCharts.Axes.ViewModels;
using Aksl.Modules.LiveCharts.Axes.Views;

//install-package LiveChartsCore.SkiaSharpView.WPF -Version 2.0.0-beta.90
//install-package LiveCharts.Core -Version 2.0.0-beta.90

namespace Aksl.Modules.LiveCharts.Axes
{
    public class AxesModule : IModule
    {
        #region Members
        private readonly IUnityContainer _container;
        #endregion

        #region Constructors
        public AxesModule(IUnityContainer container)
        {
            this._container = container;
        }
        #endregion

        #region IModule
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
           
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
         
        }
        #endregion
    }
}
