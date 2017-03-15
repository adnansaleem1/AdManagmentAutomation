using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumExtension.Driver;
using SeleniumExtension.Utilties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumExtension.Controls
{
    public class Element
    {

        public static bool Dispaly(By by)
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
        public static bool Dispaly(IWebElement by)
        {

            IWebDriver driver = SDriver.Browser;
            try
            {
                IWebElement ellemnt = by;
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
        public static void ScrolTo(By by)
        {

            try
            {
                IWebDriver driver = SDriver.Browser;
                IWebElement element = driver.FindElement(by);
                //  ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(" + x + "," + y + ");");

                Actions actions = new Actions(driver);
                actions.MoveToElement(element);
                //actions.click();
                actions.Perform();
                Wait.Second(1);
            }
            catch (Exception)
            {

                //throw;
            }
        }
        public static void ScrolTo(IWebElement ele)
        {

            try
            {
                IWebDriver driver = SDriver.Browser;
                //IWebElement element = driver.FindElement(by);
                //  ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(" + x + "," + y + ");");

                Actions actions = new Actions(driver);
                actions.MoveToElement(ele);
                //actions.click();
                actions.Perform();
                Wait.Second(1);
            }
            catch (Exception)
            {

                //throw;
            }
        }

        public static IWebElement GetByValueFromList(IList<IWebElement> eList, string p)
        {
            return eList.FirstOrDefault(e => e.GetAttribute("Value") == p);
        }

        public static void syncCheckBox(bool p, IWebElement IncludeSubCatInput)
        {
            if (IncludeSubCatInput.Selected != p) {
                IncludeSubCatInput.Click();
            }
        }

        internal static By Dispaly(IList<By> elelist)
        {
            IWebDriver driver = SDriver.Browser;
            foreach (var item in elelist)
            {
                try
                {
                    IWebElement ellemnt = driver.FindElement(item);
                    if (ellemnt.Displayed)
                    {
                        return item;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                   // return null;

                } 
            }
            return null;
        }


        internal static IWebElement GetParent(IWebElement CategoryField)
        {
          return  CategoryField.FindElement(By.XPath(".."));
        }

        internal static IWebElement FindByTagAndTextInContainer(IWebElement dialog,string Text,string Tag)
        {
          return  dialog.FindElements(By.TagName(Tag)).First(e => e.Text == Text);
        }


        public static void MoveTo(IWebElement Ele)
        {
            IWebDriver driver = SDriver.Browser;
            Actions builder = new Actions(driver);
            builder.MoveToElement(Ele).Perform();
        }
    }
}
