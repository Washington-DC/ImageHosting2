using CommonServiceLocator;
using ImageHosting.Infrastructure;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ImageHosting.Module.ImageServer.SMMS
{
    /// <summary>
    /// SMMSViewItem.xaml 的交互逻辑
    /// </summary>
    public partial class SMMSViewItem : TreeViewItem
    {
        public SMMSViewItem()
        {
            InitializeComponent();
        }

        private void TreeViewItem_Selected(object sender, RoutedEventArgs e)
        {
            IRegionManager regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            //regionManager.RequestNavigate(RegionToken.MainContentRegionName, new Uri("SettingsView", UriKind.Relative));
            regionManager.RequestNavigate(RegionToken.MainContentRegionName, "ImageHosting.Module.ImageServer.SMMS.Views.SettingView");
        }
    }
}
