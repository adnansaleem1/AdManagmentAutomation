using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumExtension.Ref
{
   public class Config
    {
        public static string DefaultFileDownloadPath =Path.GetFullPath(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory,"..\\..\\"))+"Downloads";

        private static string UniqueId = new Random().Next(1, 100000).ToString();
        public static string GetUniqueId() {
            return Config.UniqueId;
        }
    }
}
