using ImageHosting.Infrastructure.Events;
using ImageHosting.Infrastructure.Utils;
using ImageHosting.Infrastructure.ViewModels;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageHosting.Module.Gallery.ViewModels
{
    public class GalleryViewModel : BindableBase
    {
        //private string _message;
        //public string Message
        //{
        //    get { return _message; }
        //    set { SetProperty(ref _message, value); }
        //}

        private IEventAggregator _eventAggregator = null;
        public ObservableCollection<ImageItemViewModel> ImageItemGallaryData { get; private set; } = null;

        //  private Logger logger = LogManager.GetCurrentClassLogger();
        public GalleryViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<UploadImageEvent>().Unsubscribe(NewImageItem);
            _eventAggregator.GetEvent<UploadImageEvent>().Subscribe(NewImageItem);

            string cachePath = GlobalUtil.GetCacheConfigPath();
            if (File.Exists(cachePath))
            {
                try
                {
                    string jsontext = GlobalUtil.GetFileText(cachePath);
                    ImageItemGallaryData = JsonConvert.DeserializeObject<ObservableCollection<ImageItemViewModel>>(jsontext);
                }
                catch (Exception ex)
                {
                   // logger.Error(ex);
                }
            }
            else
            {
               // Directory.CreateDirectory(cachePath);
            }
            if(ImageItemGallaryData == null)
                ImageItemGallaryData = new ObservableCollection<ImageItemViewModel>();
        }

        private void NewImageItem(ImageItemViewModel obj)
        {
            ImageItemGallaryData.Insert(0, obj);
            UpdateCacheConfig();
        }


        private void UpdateCacheConfig()
        {
            string cachePath = GlobalUtil.GetCacheConfigPath();
            string data = JsonConvert.SerializeObject(ImageItemGallaryData, Formatting.Indented);

            using (FileStream fs = new FileStream(cachePath, FileMode.Create))
            {
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(data);
                sw.Flush();
                sw.Close();
            }
        }
    }
}
