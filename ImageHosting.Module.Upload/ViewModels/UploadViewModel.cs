using ImageHosting.Infrastructure.Events;
using ImageHosting.Infrastructure.ViewModels;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace ImageHosting.Module.Upload.ViewModels
{
    public class UploadViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator = null;
        public DelegateCommand UploadImageCommand { get; private set; } = null;
        public DelegateCommand UploadFromClipBoard { get; private set; } = null;
        public DelegateCommand<DragEventArgs> DragImageCommand { get; private set; } = null;

        public UploadViewModel(IEventAggregator eventAggregator)
        {
            UploadImageCommand = new DelegateCommand(UploadImageCommandExcute);
            UploadFromClipBoard = new DelegateCommand(UploadFromClipBoardExcute);
            DragImageCommand = new DelegateCommand<DragEventArgs>(DragImageCommandExcute);

            _eventAggregator = eventAggregator;
        }

        private void UploadFromClipBoardExcute()
        {
            // MessageBox.Show("UploadFromClipBoardExcute", "OK", MessageBoxButton.OK);
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent(typeof(Bitmap)))
            {
                BitmapSource bs = Clipboard.GetImage();

                using (MemoryStream ms = new MemoryStream())
                {
                    BitmapEncoder enc = new BmpBitmapEncoder();
                    enc.Frames.Add(BitmapFrame.Create(bs));
                    enc.Save(ms);
                    Image image = Image.FromStream(ms);

                    byte[] buffer = new byte[ms.Length];
                    ms.Seek(0, SeekOrigin.Begin);
                    ms.Read(buffer, 0, buffer.Length);

                    ImageItemViewModel item = new ImageItemViewModel();
                    item.Info = new ImageInfo();
                    bool bRes = UploadFactory.Instance.UploadImage(buffer, ".bmp", item);

                    if (bRes)
                    {
                        Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                        {
                            _eventAggregator.GetEvent<UploadImageEvent>().Publish(item);
                        }));
                    }
                    else
                    {

                    }
                }

                //Bitmap b = new Bitmap(bs.PixelWidth,bs.PixelHeight,(System.Drawing.Imaging.PixelFormat.)bs.Format);
                //System.Drawing.Image image = (Image)Clipboard.GetImage();

                ////var image =as Image; 
                //using (var ms = new MemoryStream())
                //{
                //    image.Save(ms, image.RawFormat);

                //    byte[] buffer = new byte[ms.Length];
                //    ms.Seek(0, SeekOrigin.Begin);
                //    ms.Read(buffer, 0, buffer.Length);

                //    ImageItemViewModel item = new ImageItemViewModel();
                //   // item.Info = new ImageInfo();
                //    bool bRes = UploadFactory.Instance.UploadImage(buffer, "." + image.RawFormat.ToString(), item);

                //    if (bRes)
                //    {
                //        Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                //        {
                //            _eventAggregator.GetEvent<UploadImageEvent>().Publish(item);
                //        }));
                //    }
                //    else
                //    {

                //    }
                //}
            }
        }

        private void DragImageCommandExcute(DragEventArgs obj)
        {
            string filePath = ((System.Array)obj.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            //  MessageBox.Show(fileName, "OK", MessageBoxButton.OK);

            if (IsImage(filePath))
            {
                UploadImage(filePath);
            }
        }

        private void UploadImageCommandExcute()
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Title = "选择图片",
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif",
                FilterIndex = 1,
                FileName = string.Empty,
                RestoreDirectory = true,
                Multiselect = true
            };

            if (ofd.ShowDialog() == true)
            {
                var text = ofd.FileNames;
                var task = Task.Factory.StartNew(() =>
                {
                    foreach (var item in text)
                    {
                        UploadImage(item);
                    }
                });
            }
        }

        private void UploadImage(string FilePath)
        {
            ImageItemViewModel item = new ImageItemViewModel();
            item.Info = new ImageInfo();
            bool bRes = UploadFactory.Instance.UploadImage(FilePath, item);

            if (bRes)
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                {
                    _eventAggregator.GetEvent<UploadImageEvent>().Publish(item);
                }));
            }
            else
            {

            }
        }


        private bool IsImage(string path)
        {
            try
            {
                Image img = Image.FromFile(path);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
