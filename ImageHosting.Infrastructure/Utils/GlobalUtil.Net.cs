using System;
using System.IO;
using System.Net.Http;

//using NLog;

namespace ImageHosting.Infrastructure.Utils
{
    public partial class GlobalUtil
    {
        public static void DownloadImage(string url,string path)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = client.GetByteArrayAsync(url);
                    response.Wait();
                    using (var fs = new FileStream(path, FileMode.OpenOrCreate))
                    {
                        fs.Write(response.Result, 0, response.Result.Length);
                        fs.Flush();
                        fs.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //LogManager.GetCurrentClassLogger().Error(ex, "DownloadImageAsync错误");
                throw;
            }
        }
    }
}
