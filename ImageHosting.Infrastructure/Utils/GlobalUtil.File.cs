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
        public static byte[] GetFileBinary(string path)
        {
            byte[] array = null;
            try
            {
                FileStream fs = new FileStream(path, FileMode.Open);
                long size = fs.Length;
                array = new byte[size];
                fs.Read(array, 0, array.Length);
                fs.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }

            return array;
        }

        public static string GetFileText(string path)
        {
            string text = string.Empty;
            try
            {
                StreamReader sr = new StreamReader(path, Encoding.Default);
                text = sr.ReadToEnd();
                sr.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
            return text;
        }
    }
}
