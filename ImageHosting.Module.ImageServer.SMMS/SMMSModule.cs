using ImageHosting.Infrastructure;
using ImageHosting.Module.ImageServer.SMMS.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace ImageHosting.Module.ImageServer.SMMS
{
    public class SMMSModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(RegionToken.MainContentRegionName, typeof(SettingView));
            regionManager.RegisterViewWithRegion(RegionToken.ImageServerRegionName, typeof(SMMSViewItem));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
           
        }
    }
}