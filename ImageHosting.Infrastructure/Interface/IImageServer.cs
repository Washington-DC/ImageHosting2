using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageHosting.Infrastructure.Interface
{
    public interface IImageServer
    {
        string ServerName();
        bool UploadImage(string path, out string ImageURL);
        bool UploadImage(byte[] data, string strFileName, out string ImageURL);
        bool SetConfig(object obj);
        void InitConfig();
        string GetErrorMessage();
    }
}
