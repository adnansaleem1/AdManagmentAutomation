using OpenQA.Selenium;
using SeleniumExtension.Driver;
using SeleniumExtension.Utilties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumExtension.Controls
{
    public class Modal
    {
        public static void ClickYes(IWebElement dialog) {
            Element.FindByTagAndTextInContainer(dialog,"Yes","button").Click();
        }
        public static bool CommonclickYes() {
            Wait.UntilDisply(By.ClassName("bootstrap-dialog-footer-buttons"));
            var dialog = SDriver.Browser.FindElement(By.ClassName("bootstrap-dialog-footer-buttons"));
            Element.FindByTagAndTextInContainer(dialog, "Yes", "button").Click();
            return true;
        }
    }
}
