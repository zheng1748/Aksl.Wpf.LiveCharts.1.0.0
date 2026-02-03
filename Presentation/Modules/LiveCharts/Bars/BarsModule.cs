using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Unity;

//install-package LiveChartsCore.SkiaSharpView.WPF -Version 2.0.0-beta.90

namespace Aksl.Modules.LiveCharts.Bars
{
    public class BarsModule : IModule
    {
        #region Members
        private readonly IUnityContainer _container;
        #endregion

        #region Constructors
        public BarsModule(IUnityContainer container)
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
