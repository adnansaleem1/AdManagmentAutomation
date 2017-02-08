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
                driver.FindElement(By.LinkText("Admin")).Click();
                Wait.AM_Loaging_ShowAndHide();
            }
            else if (MainTab == AM_MainTab.Advertisments) {
                driver.FindElement(By.LinkText("Advertisements")).Click();
                Wait.UntilDisply(By.Id("drpYear"));
            }
            else if (MainTab == AM_MainTab.Invetory)
            {
                driver.FindElement(By.LinkText("Inventory")).Click();
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

        internal void Switch(AM_Sub_Admin aM_Sub_Admin)
        {

            if (this.GetActiveMainTab() != AM_MainTab.Admin) {
                this.Switch(AM_MainTab.Admin);
            }
            if (aM_Sub_Admin == AM_Sub_Admin.Pages) {
                driver.FindElement(By.LinkText("Pages")).Click();
              //  Wait.UntilDisply(By.Id("drpApplication"));
                Wait.AM_Loaging_ShowAndHide();
            }
        }
        internal AM_MainTab GetActiveMainTab() {
            string Active = driver.FindElements(By.CssSelector("li[ng-repeat='menu in mainmenu']")).First(e => e.GetAttribute("class").Contains("active")).Text;
            if (Active == "Admin") {
                return AM_MainTab.Admin;
            }
            else if (Active == "Invetory") {
                return AM_MainTab.Invetory;
            }
            else if (Active == "Proposals")
            {
                return AM_MainTab.Proposals;
            }
            else if (Active == "Insertion Orders")
            {
                return AM_MainTab.Insertion_Orders;
            }
            else if (Active == "Reports")
            {
                return AM_MainTab.Reports;
            }
            throw new Exception("Unable to identify selected main tab.");
           
        }
    }
}
