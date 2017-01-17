using AdManagementT_Automation.Ref;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumExtension.Driver;
using SeleniumExtension.Utilties;

namespace AdManagementT_Automation.Controls
{
    public class AM_Tabs
    {

        IWebDriver driver = SDriver.Browser;
        [FindsBy(How = How.XPath, Using = "/html/body/div[1]/div[2]/div[1]/div[2]")]
        public IWebElement MainTabParent { get; set; }

        [FindsBy(How = How.XPath, Using = "/html/body/div[1]/div[2]/div[2]")]
        public IWebElement ChildTabTabParent { get; set; }

        public void Switch(AM_MainTab MainTab) {
            if (MainTab == AM_MainTab.Admin) {

            }
            else if (MainTab == AM_MainTab.Advertisments) {
                driver.FindElement(By.LinkText("Advertisements")).Click();
                Wait.UntilDisply(By.Id("drpYear"));
            }
            else if (MainTab == AM_MainTab.Invetory)
            {
                driver.FindElement(By.LinkText("Insertion Orders")).Click();
                Wait.AM_Loaging_ShowAndHide();
            }
            else if (MainTab == AM_MainTab.Proposals)
            {

            }
            else if (MainTab == AM_MainTab.Insertion_Orders)
            {

            }
            else if (MainTab == AM_MainTab.Reports)
            {

            }
        }
    }
}
