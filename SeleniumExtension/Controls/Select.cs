using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtension.Driver;
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
            IWebDriver driver = SDriver.Browser;
            SelectElement Select = new SelectElement(ele);
            Select.SelectByText(option);
        }
    }
}
