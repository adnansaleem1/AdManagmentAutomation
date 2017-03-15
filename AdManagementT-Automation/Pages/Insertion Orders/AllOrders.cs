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


        [FindsBy(How = How.ClassName, Using = "results-bar")]
        private IWebElement ResultBar { get; set; }
        

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

        internal double GetSuspandedAdAmmount()
        {
            string suspanded =ResultBar.FindElements(By.ClassName("results-group"))[3].Text;
            suspanded = suspanded.Trim();
            if ( suspanded == "")
            {
                return 0;
            }
            else {
                suspanded = suspanded.Replace("Suspended :", "");
                suspanded = suspanded.Trim();
                return Double.Parse(suspanded, System.Globalization.NumberStyles.Currency);
            }
        }

        internal double GetCompletedAdAmmount()
        {
            string Complete = ResultBar.FindElements(By.ClassName("results-group"))[2].Text;
            Complete = Complete.Trim();
            if (Complete == "")
            {
                return 0;
            }
            else
            {
                Complete = Complete.Replace("Completed:", "");
                Complete = Complete.Trim();
                return Double.Parse(Complete, System.Globalization.NumberStyles.Currency);
            }
        }

        internal double GetactiveAdAmmount()
        {
            String Active = ResultBar.FindElements(By.ClassName("results-group"))[1].Text;
            Active = Active.Trim();
            if (Active == "")
            {
                return 0;
            }
            else
            {
                Active=Active.Replace("Active:","");
                Active = Active.Trim();
                return Double.Parse(Active, System.Globalization.NumberStyles.Currency);
            }
        }

        internal double GetTotalAdAmmount()
        {
             string Total = ResultBar.FindElements(By.ClassName("results-group"))[0].Text;
             Total = Total.Trim();
             if (Total == "")
            {
                return 0;
            }
            else
            {
                Total = Total.Replace("Total:", "");
                Total = Total.Trim();
                return Double.Parse(Total, System.Globalization.NumberStyles.Currency);
            }
        }

        internal void ClearFilter()
        {
            try
            {
                var Filters = OrderFilterParent.FindElements(By.TagName("input"));
                foreach (var item in Filters) {
                    item.Clear();
                }
                Wait.AM_Loaging_ShowAndHide();
            }
            catch (Exception)
            {

            }
        }
    }
}
