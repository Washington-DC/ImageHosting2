using ImageHosting.Views;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;
using ImageHosting.Module.Upload;
using ImageHosting.Module.ImageServerManage;
using ImageHosting.Module.ImageServer.SMMS;
using ImageHosting.Module.Gallery;

namespace ImageHosting
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

        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<UploadModule>();
            moduleCatalog.AddModule<GalleryModule>();
            moduleCatalog.AddModule<ImageServerManageModule>();
            moduleCatalog.AddModule<SMMSModule>();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new DirectoryModuleCatalog() { ModulePath = @".\Modules" };
        }
    }
}
