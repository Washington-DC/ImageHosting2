using ImageHosting.Infrastructure;
using ImageHosting.Upload.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace ImageHosting.Upload
{
    public class UploadModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(RegionToken.NavigationBarRegionName, typeof(UploadViewItem));
            regionManager.RegisterViewWithRegion(RegionToken.MainContentRegionName, typeof(UploadView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}