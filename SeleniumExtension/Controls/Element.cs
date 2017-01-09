using OpenQA.Selenium;
using SeleniumExtension.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumExtension.Controls
{
    class Element
    {

        internal static bool Dispaly(By by)
        {

            IWebDriver driver = SDriver.Browser;
            try
            {
                IWebElement ellemnt = driver.FindElement(by);
                if (ellemnt.Displayed)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;

            }
        }
    }
}
