using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using CommonServiceLocator;
using ImageHosting.Infrastructure;
using ImageHosting.Infrastructure.Interface;
using ImageHosting.Infrastructure.Utils;
using ImageHosting.Infrastructure.ViewModels;
using Prism.Regions;

namespace ImageHosting.Module.Upload
{
    public class UploadFactory
    {
        private UploadFactory() { }

        public static UploadFactory Instance = new UploadFactory();

        private string DefaultServer
        {
            get { return GlobalConfig.Instance.DefaultServer; }
            set { GlobalConfig.Instance.DefaultServer = value; }
        }

        public bool UploadImage(string path, ImageItemViewModel item)
        {
            try
            {
                if (string.IsNullOrEmpty(DefaultServer))
                {
                    DefaultServer = "SMMS";
                    GlobalConfig.Instance.SaveSettings();
                }
                string AssemblyName = "ImageHosting.Module.ImageServer." + DefaultServer;

                IImageServer server = (IImageServer)Assembly.Load(AssemblyName).CreateInstance(AssemblyName + ".ImageServer");
                var config = GlobalConfig.Instance.GetImageServerConfig(DefaultServer);
                bool bRes = server.SetConfig(config);

                if (!bRes)
                {
                    var result = MessageBox.Show("确定?", "此图床服务需要必要的设置，是否跳转到设置界面?", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        IRegionManager regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
                        regionManager.RequestNavigate(RegionToken.NavigationBarRegionName, AssemblyName + ".Views." + DefaultServer);
                    }
                    return false;
                }

                item.Info.DisplayName = Path.GetFileName(path);
                item.Info.Extname = Path.GetExtension(path);

                string ImageURL = string.Empty;
                bRes = server.UploadImage(path, out ImageURL);

                if (bRes && !string.IsNullOrEmpty(ImageURL))
                {
                    item.Info.GUID = Guid.NewGuid().ToString();
                    item.Info.UploadTime = DateTime.Now.ToFileTimeUtc();
                    item.Info.ImageServer = server.ServerName();
                    item.Info.ImageUrl = ImageURL;
                    item.Info.Filename = item.Info.GUID + Path.GetExtension(path);

                    string dst = GlobalUtil.GetCacheDirectory() + "\\" + item.Info.Filename;
                    File.Copy(path, dst, true);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }



        public bool UploadImage(byte[] data, string fmt, ImageItemViewModel item)
        {
            try
            {
                if (string.IsNullOrEmpty(DefaultServer))
                {
                    DefaultServer = "SMMS";
                    GlobalConfig.Instance.SaveSettings();
                }
                string AssemblyName = "ImageHosting.Module.ImageServer." + DefaultServer;

                IImageServer server = (IImageServer)Assembly.Load(AssemblyName).CreateInstance(AssemblyName + ".ImageServer");
                var config = GlobalConfig.Instance.GetImageServerConfig(DefaultServer);
                bool bRes = server.SetConfig(config);

                if (!bRes)
                {
                    var result = MessageBox.Show("确定?", "此图床服务需要必要的设置，是否跳转到设置界面?", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        IRegionManager regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
                        regionManager.RequestNavigate(RegionToken.NavigationBarRegionName, AssemblyName + ".Views." + DefaultServer);
                    }
                    return false;
                }

                item.Info.DisplayName = "Clipboard_" +  Guid.NewGuid().ToString() + fmt;
                item.Info.Extname = fmt;
                string ImageURL = string.Empty;

                bRes = server.UploadImage(data, item.Info.DisplayName, out ImageURL);

                if (bRes && !string.IsNullOrEmpty(ImageURL))
                {
                    item.Info.GUID = Guid.NewGuid().ToString();
                    item.Info.UploadTime = DateTime.Now.ToFileTimeUtc();
                    item.Info.ImageServer = server.ServerName();
                    item.Info.ImageUrl = ImageURL;
                    item.Info.Filename = item.Info.GUID + item.Info.Extname;

                    string dst = GlobalUtil.GetCacheDirectory() + "\\" + item.Info.Filename;
                    // File.Copy(path, dst, true);
                    using (var file = new  FileStream(dst, FileMode.Create, FileAccess.Write))
                    {
                        file.Write(data,0,data.Length);
                        file.Flush();
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        //public bool UploadImage(List<string> path)
        //{
        //    try
        //    {
        //        string DefaultType = GlobalConfig.Instance.DefaultType;
        //        string AssemblyName = "ImageHosting.Module.ImageServer." + DefaultType;

        //        IImageServer server = (IImageServer)Assembly.Load(AssemblyName).CreateInstance(AssemblyName + ".ImageServer");
        //        server.GetConfig(GlobalConfig.Instance.GetHostingConfig(DefaultType));
        //        return server.UploadImage(path);
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex.Message);
        //        return false;
        //    }
        //}
    }
}
