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
        public static void ByText(IWebElement ele, string option)
        {
            if (option != "" && option != null)
            {
                IWebDriver driver = SDriver.Browser;
                SelectElement Select = new SelectElement(ele);
                Wait.MLSeconds(1000);
                //Wait.UntilDisply(option);
                Select.SelectByText(option);
            }
        }
        public static void SelectFromMultipleControl(IList<int> list, IWebElement Control)
        {
            try
            {

                if (list.Count > 0)
                {
                    Element.ScrolTo(Control);
                    Control.FindElement(By.ClassName("dropdown-toggle")).Click();
                    Wait.MLSeconds(200);
                    IWebElement ListItemele;
                    foreach (var item in list)
                    {
                        ListItemele = Control.FindElement(By.LinkText(item.ToString()));
                        Element.ScrolTo(ListItemele);
                        ListItemele.Click();
                        Wait.MLSeconds(100);
                    }
                    Control.FindElement(By.XPath("..")).Click();
                }
            }
            catch (Exception)
            {

                //throw;
            }

        }
        public static void SelectFromMultipleControl(IList<string> list, IWebElement Control)
        {
            try
            {

                if (list.Count > 0)
                {
                    Element.ScrolTo(Control);
                    Control.FindElement(By.ClassName("dropdown-toggle")).Click();
                    Wait.MLSeconds(200);
                    IWebElement ListItemele;
                    foreach (var item in list)
                    {
                        ListItemele = Control.FindElement(By.LinkText(item.ToString()));
                        Element.ScrolTo(ListItemele);
                        ListItemele.Click();
                        Wait.MLSeconds(100);
                    }
                    Control.FindElement(By.XPath("..")).Click();
                }
            }
            catch (Exception)
            {

                //throw;
            }
        }

        public static void FromList(string p, IWebElement CategoryField)
        {
            IWebElement GroupControl = Element.GetParent(CategoryField);
            IWebElement TypeAHeadControl = GroupControl.FindElement(By.TagName("ul"));
           // IList<IWebElement> TypeAHeadList = TypeAHeadControl.FindElements(By.TagName("li"));
            CategoryField.Clear();
            CategoryField.SendKeys(p);
            CategoryField.SendKeys(Keys.Enter);
            Wait.UntilDisply(TypeAHeadControl);
            IList<IWebElement> TypeAHeadList = TypeAHeadControl.FindElements(By.TagName("li"));
            IWebElement MatchedItem = TypeAHeadList.FirstOrDefault(e => e.Text.ToLower() == p.ToLower());
            if (MatchedItem == null)
            {
                throw new Exception("Unable to Find given text");
            }
            else {
                MatchedItem.Click();
                Wait.MLSeconds(200);
            }
        }


        public static void ByFreeKeyWords(IWebElement KeyWordsControl, IList<string> list)
        {
            IWebElement InputBox= KeyWordsControl.FindElement(By.TagName("input"));
            foreach (var i in list) {
                InputBox.Clear();
                InputBox.SendKeys(i);
                InputBox.SendKeys(Keys.Enter);
                Wait.MLSeconds(100);
            }
        }

        public static void TagBasedInput(IList<string> list, IWebElement Control)
        {
            Element.ScrolTo(Control);
            foreach (var item in list)
            {
                Control.FindElement(By.TagName("input")).SendKeys(item);
                Wait.UntilDisply(By.ClassName("autocomplete"));
                Control.FindElement(By.TagName("input")).SendKeys(Keys.Enter);
                Wait.MLSeconds(200);
            }

        }
    }
}
