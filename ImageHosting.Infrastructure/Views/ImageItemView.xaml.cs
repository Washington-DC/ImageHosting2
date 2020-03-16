using ImageHosting.Infrastructure.ViewModels;
using System.Windows.Controls;

namespace ImageHosting.Infrastructure.Views
{
    /// <summary>
    /// Interaction logic for ImageItemView
    /// </summary>
    public partial class ImageItemView : UserControl
    {
        public ImageItemView()
        {
            InitializeComponent();
        }

        public ImageItemViewModel ViewModel
        {
            set
            {
                this.DataContext = value;
            }
        }

    }
}
