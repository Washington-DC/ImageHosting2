using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageHosting.Infrastructure.Utils
{
    public partial class GlobalUtil
    {
        public static string CurrentDirectory = Environment.CurrentDirectory;

        public static string GetCacheDirectory()
        {
            string cache = CurrentDirectory + "\\cache";
            if (!File.Exists(cache))
            {
                Directory.CreateDirectory(cache);
            }
            return cache;
        }

        public static string GetConfigDirectory()
        {
            string config = CurrentDirectory + "\\config";
            if (!File.Exists(config))
            {
                Directory.CreateDirectory(config);
            }
            return config;
        }

        public static string GetCacheConfigPath()
        {
            string ConfigPath = GetConfigDirectory();
            return ConfigPath + "\\Cache.conf";
        }

        public static string GetGlobalConfigPath()
        {
            string ConfigPath = GetConfigDirectory();
            return ConfigPath + "\\ImageHosting.conf";
        }
    }
}
