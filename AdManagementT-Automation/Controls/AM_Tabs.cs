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
using SeleniumExtension.Controls;

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
            else if (MainTab == AM_MainTab.Inventory && (this.GetActiveMainTab() != AM_MainTab.Inventory || this.GetSubTabInventory() != AM_Sub_Inventory.Inventory))
            {

                driver.FindElement(By.LinkText("Inventory")).Click();
                Wait.AM_Loaging_ShowAndHide();
            }
            else if (MainTab == AM_MainTab.Proposals)
            {
                driver.FindElement(By.LinkText("Proposals")).Click();
                Wait.Second(1);
            }
            else if (MainTab == AM_MainTab.Insertion_Orders&&this.GetActiveMainTab()!=AM_MainTab.Insertion_Orders)
            {
                driver.FindElement(By.LinkText("Insertion Orders")).Click();
                Wait.AM_Loaging_ShowAndHide();
            }
            else if (MainTab == AM_MainTab.Reports)
            {
                driver.FindElement(By.LinkText("Reports")).Click();
                Wait.Second(1);
            }
        }


        internal void Switch(AM_Sub_Admin aM_Sub_Admin)
        {

            if (this.GetActiveMainTab() != AM_MainTab.Admin) {
                this.Switch(AM_MainTab.Admin);
            }
            if (aM_Sub_Admin == AM_Sub_Admin.Pages && this.GetActiveAdminTab() != AM_Sub_Admin.Pages)
            {
                driver.FindElement(By.LinkText("Pages")).Click();
              //  Wait.UntilDisply(By.Id("drpApplication"));
                Wait.AM_Loaging_ShowAndHide();
            }
            if (aM_Sub_Admin == AM_Sub_Admin.Simulate_Search) {
                driver.FindElement(By.LinkText("Simulate Search")).Click();
                Wait.AM_Loaging_ShowAndHide();
            }
            if (aM_Sub_Admin == AM_Sub_Admin.AddGroups)
            {
                driver.FindElement(By.LinkText("Ad Groups")).Click();
                Wait.MLSeconds(1000);
            }

        }

        private AM_Sub_Admin GetActiveAdminTab()
        {
            string Active = driver.FindElements(By.CssSelector("li[data-ng-repeat='menu in selectedMenu.menus']")).First(e => e.GetAttribute("class").Contains("active")).Text;
            if (Active == "Pages")
            {
                return AM_Sub_Admin.Pages;
            }
            else if (Active == "Rate Cards") {

                return AM_Sub_Admin.Rate_Cards;
            }
            else if (Active == "Simulate Search")
            {

                return AM_Sub_Admin.Simulate_Search;
            }

            else if (Active == "Redis")
            {

                return AM_Sub_Admin.Redis;
            }

            else if (Active == "Audit")
            {

                return AM_Sub_Admin.Audit;
            }

            else if (Active == "Renewal")
            {

                return AM_Sub_Admin.Renewal;
            }

            else if (Active == "Ad Groups")
            {

                return AM_Sub_Admin.AddGroups;
            }
            throw new Exception("Unable to identify selected Admin tab.");
        }
        internal void Switch(AM_Sub_Insertion_Orders aM_Sub_Order)
        {

            if (this.GetActiveMainTab() != AM_MainTab.Insertion_Orders)
            {
                this.Switch(AM_MainTab.Insertion_Orders);
            }
            if (aM_Sub_Order == AM_Sub_Insertion_Orders.Edit_Order)
            {
                driver.FindElement(By.LinkText("Edit Order")).Click();
                //  Wait.UntilDisply(By.Id("drpApplication"));
                Wait.AM_Loaging_ShowAndHide();
            }
            if (aM_Sub_Order == AM_Sub_Insertion_Orders.All_Order) {
                Wait.IfLoadingIsStillVisible();
                driver.FindElement(By.LinkText("All Orders")).Click();
                Wait.AM_Loaging_ShowAndHide();
            }
        }
        internal AM_MainTab GetActiveMainTab() {
            string Active = driver.FindElements(By.CssSelector("li[ng-repeat='menu in mainmenu']")).First(e => e.GetAttribute("class").Contains("active")).Text;
            if (Active == "Admin") {
                return AM_MainTab.Admin;
            }
            else if (Active == "Inventory") {
                return AM_MainTab.Inventory;
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
            else if (Active == "Advertisements") {
                return AM_MainTab.Advertisments;
            }
            throw new Exception("Unable to identify selected main tab.");
           
        }
        public AM_Sub_Inventory GetSubTabInventory(){
            string Active = driver.FindElements(By.CssSelector("li[data-ng-repeat='menu in selectedMenu.menus']")).First(e => e.GetAttribute("class").Contains("active")).Text;
            if (Active == "Inventory")
            {
                return AM_Sub_Inventory.Inventory;
            }
            else if (Active == "Wait List")
            {
                return AM_Sub_Inventory.Wait_List;
            }
            throw new Exception("Unable to identify selected Inventory tab.");
        }
        internal void Switch(AM_Sub_Inventory aM_Sub_Inventory)
        {
            this.Switch(AM_MainTab.Inventory);
            if (aM_Sub_Inventory == AM_Sub_Inventory.Wait_List) {
                driver.FindElement(By.LinkText("Wait List")).Click();
                Wait.AM_Loaging_ShowAndHide();
            }
        }

        internal void Switch(AM_Sub_Proposlas aM_Sub_Proposlas)
        {
            this.Switch(AM_MainTab.Proposals);
            if (aM_Sub_Proposlas == AM_Sub_Proposlas.Create_Proposal) {
                driver.FindElement(By.LinkText("Create Proposal")).Click();
                Wait.Second(1);
                //Wait.AM_Loaging_ShowAndHide();
            }

            if (aM_Sub_Proposlas == AM_Sub_Proposlas.All_Proposal&&!Element.HasClass(Element.GetPerent(driver.FindElement(By.LinkText("All Proposals"))),"active"))
            {
                driver.FindElement(By.LinkText("All Proposals")).Click();
                Wait.AM_Loaging_ShowAndHide();
                Wait.Second(1);
                //Wait.AM_Loaging_ShowAndHide();
            }
        }
    }
}
