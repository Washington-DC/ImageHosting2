using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ImageHosting.Module.Upload.ViewModels
{
    public class UploadViewModel : BindableBase
    {
        public DelegateCommand UploadImageCommand { get; private set; } = null;
        public DelegateCommand<DragEventArgs> DragImageCommand { get; private set; } = null;

        public UploadViewModel()
        {
            UploadImageCommand = new DelegateCommand(UploadImageCommandExcute);
            DragImageCommand = new DelegateCommand<DragEventArgs>(DragImageCommandExcute);
        }

        private void DragImageCommandExcute(DragEventArgs obj)
        {
            string fileName = ((System.Array)obj.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            MessageBox.Show(fileName, "OK", MessageBoxButton.OK);
        }


        private void UploadImageCommandExcute()
        {
            MessageBox.Show("UploadImageCommandExcute", "OK", MessageBoxButton.OK);

        }
    }
}
