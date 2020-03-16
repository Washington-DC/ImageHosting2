using ImageHosting.Infrastructure.Interface;
using ImageHosting.Infrastructure.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageHosting.Module.ImageServer.SMMS
{
    class ImageServer : IImageServer
    {
        private string _Domain = @"https://sm.ms/api/v2";
        private string _Resource = @"/upload";
        //private Logger logger = LogManager.GetCurrentClassLogger();
        private string _Message = string.Empty;
        private string _Name = "SMMS";

        public bool SetConfig(object obj)
        {
            return true;
        }

        public string ServerName()
        {
            return _Name;
        }

        bool UploadImageProcess(byte[] data, string strFileName, out string ImageURL)
        {
            try
            {
                var client = new RestClient(_Domain);
                var request = new RestRequest(_Resource, Method.POST);
                request.AddFileBytes("smfile", data, strFileName);
                //var response = await client.ExecuteAsync(request);
                var response = client.Execute(request);
                var content = response.Content;
                var jDoc = (JObject)JsonConvert.DeserializeObject(response.Content);

                var result = jDoc.GetValue("success").ToString();
                if (!result.ToLower().Equals("true"))
                {
                    var state = jDoc.GetValue("code").ToString();
                    if (state.Equals("image_repeated"))
                    {
                        ImageURL = jDoc.GetValue("images").ToString();
                        return true;
                    }
                    _Message = jDoc.GetValue("msg").ToString();
                    //logger.Error(content);
                    ImageURL = "";
                    return false;
                }
                JObject jData = jDoc.GetValue("data").ToObject<JObject>();
                ImageURL = jData.GetValue("url").ToString();
                //logger.Info(ImageURL);

                return true;
            }
            catch (Exception ex)
            {
                //logger.Error(ex, "SMMS 上传失败:" + strFileName);
                ImageURL = "";
                _Message = ex.Message;
                return false;
            }
        }

        public bool UploadImage(string path, out string ImageURL)
        {
            return UploadImageProcess(GlobalUtil.GetFileBinary(path), Path.GetFileName(path), out ImageURL);
        }

        public void InitConfig()
        {

        }

        public string GetErrorMessage()
        {
            return _Message;
        }

        public bool UploadImage(byte[] data, string strFileName, out string ImageURL)
        {
            return UploadImageProcess(data, strFileName, out ImageURL);
        }
    }
}
