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
            Wait.Second(1);
            Element.FindByTagAndTextInContainer(dialog,"Yes","button").Click();
        }
        public static bool CommonclickYes() {
            Wait.UntilDisply(By.ClassName("bootstrap-dialog-footer-buttons"));
            var dialog = SDriver.Browser.FindElement(By.ClassName("bootstrap-dialog-footer-buttons"));
            Element.FindByTagAndTextInContainer(dialog, "Yes", "button").Click();
            return true;
        }

        public static bool DirtyclickYes()
        {
            Wait.UntilDisply(By.ClassName("dirtycheck-dialog"));
            var dialog = SDriver.Browser.FindElement(By.ClassName("dirtycheck-dialog"));
            Element.FindByTagAndTextInContainer(dialog, "Yes", "button").Click();
            return true;
        }

        private static bool IfModalIsThere()
        {
            if (Element.Dispaly(By.ClassName("modal-dialog")) && !Element.Dispaly(By.ClassName("dirtycheck-dialog")))
            {
                return true;
            }
            else {

                return false; 
            }
        }

        public static void Close()
        {
            if (Modal.IfModalIsThere())
            {
                var drive = SDriver.Browser;
                try
                {
                    drive.FindElement(By.ClassName("modal-dialog")).FindElement(By.CssSelector("button[data-ng-click='cancel()']")).Click();
                    Wait.Second(2);
                }
                catch (Exception)
                {
                }

            }

        }

        public static void dirtCheckClose() {
            if (Modal.IfOnForm()) {

                    var drive = SDriver.Browser;
                    try
                    {
                        drive.FindElement(By.XPath("//button[contains(text(),'Cancel')]")).Click();
                        Wait.Second(2);
                        if (Element.Dispaly(By.ClassName("dirtycheck-dialog"))) {
                            Modal.DirtyclickYes();
                        }
                        Wait.Second(2);
                        Wait.IfLoadingIsStillVisible();
                    }
                    catch (Exception)
                    {
                    }

                
            }
        }
 
        private static bool IfOnForm() {
            if (Element.Dispaly((By.XPath("//button[contains(text(),'Cancel')]"))))
            {
                return true;
            }
            else {
                return false;
            }
        }
    }
}
