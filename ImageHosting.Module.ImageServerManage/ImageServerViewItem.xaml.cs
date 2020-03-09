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

namespace ImageHosting.Module.ImageServerManage
{
    /// <summary>
    /// ImageServerViewItem.xaml 的交互逻辑
    /// </summary>
    public partial class ImageServerViewItem : TreeViewItem
    {
        public ImageServerViewItem()
        {
            InitializeComponent();
        }

        private void TreeViewItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem treeviewItem = sender as TreeViewItem;
            if (treeviewItem != null)
            {
                if (treeviewItem.IsSelected)
                {
                    treeviewItem.IsExpanded = !treeviewItem.IsExpanded;
                    treeviewItem.IsSelected = false;
                }
            }
        }

        private void TreeViewItem_Selected(object sender, RoutedEventArgs e)
        {

        }
    }
}
