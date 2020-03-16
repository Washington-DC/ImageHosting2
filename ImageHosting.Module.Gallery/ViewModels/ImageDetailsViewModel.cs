using CommonServiceLocator;
using ImageHosting.Infrastructure;
using ImageHosting.Infrastructure.Events;
using ImageHosting.Infrastructure.ViewModels;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ImageHosting.Module.Gallery.ViewModels
{
    public class ImageDetailsViewModel : BindableBase
    {
        public string ImagePath { 
            get
            {
                if(ImageItem != null)
                {
                    return ImageItem.ImageFullPath;
                }
                else
                {
                    return string.Empty;
                }
            } 
        }
        private IEventAggregator _eventAggregator = null;
        private ImageItemViewModel ImageItem = null;
        public ImageDetailsViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<ShowImageEvent>().Unsubscribe(ShowImageItem);
            _eventAggregator.GetEvent<ShowImageEvent>().Subscribe(ShowImageItem);
        }

        private void ShowImageItem(ImageItemViewModel obj)
        {
            ImageItem = obj;
            RaisePropertyChanged(nameof(ImagePath));
            IRegionManager regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            regionManager.RequestNavigate(RegionToken.MainContentRegionName, new Uri("ImageDetailsView", UriKind.RelativeOrAbsolute));
        }
    }
}
