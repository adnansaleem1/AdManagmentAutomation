using OpenQA.Selenium;
using SeleniumExtension.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumExtension.Utilties
{
    public class DriveURL
    {
        public static string GetLocalPath() {
            IWebDriver driver = SDriver.Browser;
            var uri = new Uri(driver.Url);
            return uri.LocalPath;
        }
    }
}
