using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using ImageHosting.Infrastructure.Utils;

using Newtonsoft.Json;

//using NLog;

namespace ImageHosting.Infrastructure
{
    [JsonObject(MemberSerialization.OptIn)]
    class ImageServerSettings
    {
        [JsonProperty]
        public string DefaultServer { get; set; }
        [JsonProperty]
        public Dictionary<string, object> ImageServers { get; set; }
    }
   
    public class GlobalConfig
    {
        private readonly string _configFilePath = GlobalUtil.GetGlobalConfigPath();

        public static GlobalConfig Instance = new GlobalConfig();
        //private Logger logger = LogManager.GetCurrentClassLogger();
        private ImageServerSettings _ImageServerSettings { get; set; }

        public string DefaultServer
        {
            get { return _ImageServerSettings.DefaultServer; }
            set { _ImageServerSettings.DefaultServer = value; }
        }

        private GlobalConfig()
        {
            try
            {
                if (File.Exists(_configFilePath))
                {
                    StreamReader sr = new StreamReader(_configFilePath, Encoding.Default);
                    string jsontext = sr.ReadToEnd();
                    sr.Close();

                    _ImageServerSettings = JsonConvert.DeserializeObject<ImageServerSettings>(jsontext);
                }
                else
                    _ImageServerSettings = new ImageServerSettings();

                if (_ImageServerSettings.ImageServers == null)
                    _ImageServerSettings.ImageServers = new Dictionary<string, object>();
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex.Message);
                //throw;
                //logger.Error(ex);
            }
        }

        public object GetImageServerConfig(string type)
        {
            if (_ImageServerSettings.ImageServers.ContainsKey(type))
                return _ImageServerSettings.ImageServers[type];
            return null;
        }

        public void AddImageServerConfig(string type,object token)
        {
            if (_ImageServerSettings.ImageServers.ContainsKey(type))
                _ImageServerSettings.ImageServers[type] = token;
            else
                _ImageServerSettings.ImageServers.Add(type, token);
        }

        public void SaveSettings()
        {
            try
            {
                string data = JsonConvert.SerializeObject(_ImageServerSettings, Formatting.Indented);
                using (FileStream fs = new FileStream(_configFilePath, FileMode.Create))
                {
                    StreamWriter sw = new StreamWriter(fs);
                    sw.Write(data);
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                //logger.Error(ex);
            }
        }
    }
}
