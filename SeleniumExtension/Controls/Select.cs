using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtension.Driver;
using SeleniumExtension.Utilties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumExtension.Controls
{
    public class Select
    {
        public static void ByText(IWebElement ele, string option) {
            if (option!=""&&option!=null)
            {
                IWebDriver driver = SDriver.Browser;
                SelectElement Select = new SelectElement(ele);
                Wait.MLSeconds(100);
                Select.SelectByText(option); 
            }
        }
    }
}
