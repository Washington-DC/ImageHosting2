using ImageHosting.Infrastructure.ViewModels;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageHosting.Infrastructure.Events
{
    public class UploadImageEvent : PubSubEvent<ImageItemViewModel>
    {

    }
}
