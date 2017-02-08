using OpenQA.Selenium;
using SeleniumExtension.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumExtension.Controls
{
    public class Alert
    {
        public static void ClickOK() {
            IWebDriver driver = SDriver.Browser;
            if (Alert.CheckIfAlertPresent())
            {
                driver.SwitchTo().Alert().Accept();
            }
        }

        private static bool CheckIfAlertPresent()
        {
            try
            {
                IWebDriver driver = SDriver.Browser;
                driver.SwitchTo().Alert();
                return true;
            }   // try 
            catch (Exception Ex)
            {
                return false;
            }
        }
    }
}
