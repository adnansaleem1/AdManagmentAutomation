using AdManagementT_Automation.Base;
using AdManagementT_Automation.Controls;
using AdManagementT_Automation.Ref;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using SeleniumExtension.Utilties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdManagementT_Automation.Pages.Insertion_Orders
{
    public class AllOrdersPage
    {

        [FindsBy(How = How.CssSelector, Using = "tr[ng-show='show_filter']")]
        private IWebElement OrderFilterParent { get; set; }

        [FindsBy(How = How.TagName, Using = "tbody")]
        private IWebElement OrderResultGridParent { get; set; }

        public AllOrdersPage Navigate()
        {
            PagesRepo.AddTabs.Switch(AM_MainTab.Insertion_Orders);
            return this;
        }
        public AllOrdersPage SelectGivenOrderByID(string id)
        {
            var Order = this.SearchOrder(id);
            if (Order == null) {
                throw new Exception("No Order Find With given Id.");
            }
            Order.Click();
            Wait.AM_Loaging_ShowAndHide();

            return this;
        }
        private IWebElement SearchOrder(string Id = "", string CompanyName = "")
        {
            var Filter = OrderFilterParent.FindElements(By.TagName("input"));
            if (Id == "" && CompanyName == "")
            {
                throw new Exception("Atlest one Paramitter must be Given to Search Order");
            }
            if (Id != "")
            {
                Filter[0].Clear();
                Filter[0].SendKeys(Id);
            }
            if (CompanyName != "")
            {
                Filter[1].Clear();
                Filter[1].SendKeys(CompanyName);
            }
            Wait.Second(1);
            var ResultList = OrderResultGridParent.FindElements(By.TagName("tr"));

            //for (var count = 0; count < ResultList.Count - 1; count++) { 
            //if(ResultList[count].FindElements(By.TagName("td")))
            //}
            var ResultOrder = ResultList.FirstOrDefault(e => e.FindElements(By.TagName("td"))[1].Text == Id || e.FindElements(By.TagName("td"))[2].Text == CompanyName);
            return ResultOrder;
        }
    }
}
