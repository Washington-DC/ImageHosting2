using CommonServiceLocator;
using ImageHosting.Infrastructure.Events;
using ImageHosting.Infrastructure.Utils;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ImageHosting.Infrastructure.ViewModels
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ImageInfo : BindableBase
    {
        [JsonProperty]
        public string Filename { get; set; }      //文件名

        [JsonProperty]
        public string DisplayName { get; set; }      //显示的原始文件名

        [JsonProperty]
        public string Extname { get; set; }       //扩展名

        [JsonProperty]
        public int Width { get; set; }

        [JsonProperty]
        public int Height { get; set; }

        [JsonProperty]
        public string ImageUrl { get; set; }     //链接

        [JsonProperty]
        public string GUID { get; set; }

        [JsonProperty]
        public string ImageServer { get; set; }    //服务器类型

        [JsonProperty]
        public long UploadTime { get; set; }     //上传时间

        [JsonProperty]
        public bool Success { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class ImageItemViewModel : BindableBase
    {
        [JsonProperty]
        public ImageInfo Info { get; set; }

        private string _ImageFullPath = string.Empty;
        //private Logger logger = LogManager.GetCurrentClassLogger();
        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(_ImageFullPath))
                    _ImageFullPath = GlobalUtil.GetCacheDirectory() + "\\" + Info.Filename;


                Debug.WriteLine("------------------>" + _ImageFullPath);

                if (!File.Exists(_ImageFullPath) && !string.IsNullOrEmpty(Info.ImageUrl))
                {
                    //ThreadPool.QueueUserWorkItem(item => {
                    //    DownloadImage((item as PictureItemViewModel).ImageFullPath);
                    //},this);
                    Task.Factory.StartNew(() => { DownloadImage(_ImageFullPath); });
                }
                return _ImageFullPath;
            }
        }

        public ImageItemViewModel()
        {
            // Info = new ImageInfo();
            CopyImageCommand = new DelegateCommand(CopyImageUrl);
            DeleteImageCommand = new DelegateCommand(DeleteImage);
            ClickSelectedCommand = new DelegateCommand(SelectItem);
            ShowImageCommand = new DelegateCommand(ShowImage);
        }

        private void ShowImage()
        {
            IEventAggregator _eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            _eventAggregator.GetEvent<ShowImageEvent>().Publish(this);
        }

        private void SelectItem()
        {
            IsSelected = !IsSelected;
        }

        private void DeleteImage()
        {
            IEventAggregator _eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            //_eventAggregator.GetEvent<DeleteImageEvent>().Publish(this);
        }

        private void CopyImageUrl()
        {
            Clipboard.SetDataObject(Info.ImageUrl);
        }

        private void DownloadImage(string path)
        {
            try
            {
                GlobalUtil.DownloadImage(Info.ImageUrl, path);
                RaisePropertyChanged();
            }
            catch (Exception ex)
            {
                //logger.Error(ex, "下载图片错误");
            }
        }

        private bool isSelected = false;
        public bool IsSelected { get => isSelected; set { SetProperty(ref isSelected, value); } }

        public DelegateCommand CopyImageCommand { get; private set; }
        public DelegateCommand DeleteImageCommand { get; private set; }

        public DelegateCommand ClickSelectedCommand { get; private set; }
        public DelegateCommand ShowImageCommand { get; private set; }
    }
}
