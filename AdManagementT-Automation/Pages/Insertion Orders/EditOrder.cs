using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using SeleniumExtension.Driver;
using SeleniumExtension.Utilties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumExtension.Controls;

namespace AdManagementT_Automation.Pages.Insertion_Orders
{
    public class EditOrderPage
    {
        IWebDriver driver = SDriver.Browser;
        [FindsBy(How = How.XPath, Using = "/html/body/div[1]/div[3]/form/div/div[9]/div[2]/div/div/button[2]")]
        private IWebElement AddOrderLineBtn { get; set; }

        [FindsBy(How = How.LinkText, Using = "PFP")]
        private IWebElement PFP { get; set; }

        [FindsBy(How = How.LinkText, Using = "Banner")]
        private IWebElement Banner { get; set; }

        public void AddNewOrderLine(string lineType)
        {
            Element.ScrolTo(AddOrderLineBtn);
            AddOrderLineBtn.Click();
            Wait.MLSeconds(500);
            if (lineType == "PFP")
            {
                PFP.Click();
                Wait.UntilDisply(By.Id("drpApplication"));
            }
            else if (lineType == "Banner")
            {
                Banner.Click();
                Wait.UntilDisply(By.Id("drpApplication"));
            }
        }
    }
}
