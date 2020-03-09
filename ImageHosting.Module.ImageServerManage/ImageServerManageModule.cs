using ImageHosting.Infrastructure;
using ImageHosting.Module.ImageServerManage;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace ImageHosting.Module.ImageServerManage
{
    public class ImageServerManageModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(RegionToken.NavigationBarRegionName, typeof(ImageServerViewItem));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}