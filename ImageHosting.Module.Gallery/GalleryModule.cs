using ImageHosting.Infrastructure;
using ImageHosting.Module.Gallery.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace ImageHosting.Module.Gallery
{
    public class GalleryModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(RegionToken.NavigationBarRegionName, typeof(GalleryViewItem));
            regionManager.RegisterViewWithRegion(RegionToken.MainContentRegionName, typeof(GalleryView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
          
        }
    }
}