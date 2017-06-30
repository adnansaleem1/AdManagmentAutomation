using AdManagementT_Automation.Base;
using AdManagementT_Automation.Controls;
using AdManagementT_Automation.Ref;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using SeleniumExtension.Controls;
using SeleniumExtension.Driver;
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
        IWebDriver driver = SDriver.Browser;
        [FindsBy(How = How.CssSelector, Using = "tr[ng-show='show_filter']")]
        private IWebElement OrderFilterParent { get; set; }

        [FindsBy(How = How.CssSelector, Using = "button[data-ng-click='::manageRenewal()']")]
        private IWebElement RenewalBtn { get; set; }


        [FindsBy(How = How.CssSelector, Using = "button[class='btn btn-default dropdown-toggle ng-binding']")]
        private IWebElement StatusChangeBtn { get; set; }
       


        [FindsBy(How = How.TagName, Using = "tbody")]
        private IWebElement OrderResultGridParent { get; set; }

        [FindsBy(How = How.Id, Using = "drpYear")]
        private IWebElement FilterYearDD { get; set; }

        [FindsBy(How = How.Id, Using = "div[data-ng-modal='TotalCost']")]
        private IWebElement TotalAmmount { get; set; }
        

        [FindsBy(How = How.ClassName, Using = "results-bar")]
        private IWebElement ResultBar { get; set; }


        public AllOrdersPage Navigate()
        {
            PagesRepo.AddTabs.Switch(AM_Sub_Insertion_Orders.All_Order);
            return this;
        }
        public AllOrdersPage SelectGivenOrderByID(string id)
        {
            var Order = this.ClearFilter().SearchOrder(id);
            if (Order == null)
            {
                throw new Exception("No Order Find With given Id.");
            }
            Wait.IfLoadingIsStillVisible();
            Order.Click();
            Wait.AM_Loaging_ShowAndHide();

            return this;
        }
        private IWebElement SearchOrder(string Id = "", string CompanyName = "")
        {
            var Filter = OrderFilterParent.FindElements(By.TagName("input"));
            bool Filtered=false;
            if (Id == "" && CompanyName == "")
            {
                throw new Exception("Atlest one Paramitter must be Given to Search Order");
            }
            if (Id != "" && Filter[0].Text != Id)
            {
                Filtered = true;
                Filter[0].Clear();
                Filter[0].SendKeys(Id);
            }
            if (CompanyName != "" && Filter[1].Text!=CompanyName)
            {
                Filtered = true;
                Filter[1].Clear();
                Filter[1].SendKeys(CompanyName);
            }
            if (Filtered) {
                Wait.AM_Loaging_ShowAndHide_WithWait(2);
            }
            var ResultList = OrderResultGridParent.FindElements(By.TagName("tr"));

            //for (var count = 0; count < ResultList.Count - 1; count++) { 
            //if(ResultList[count].FindElements(By.TagName("td")))
            //}
            var ResultOrder = ResultList.FirstOrDefault(e => e.FindElements(By.TagName("td"))[1].Text == Id || e.FindElements(By.TagName("td"))[2].Text == CompanyName);
            return ResultOrder;
        }

        internal double GetSuspandedAdAmmount()
        {
            string suspanded = ResultBar.FindElements(By.ClassName("results-group"))[3].Text;
            suspanded = suspanded.Trim();
            if (suspanded == "")
            {
                return 0;
            }
            else
            {
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
                Active = Active.Replace("Active:", "");
                Active = Active.Trim();
                return Double.Parse(Active, System.Globalization.NumberStyles.Currency);
            }
        }

        internal double GetTotalAdAmmount()
        {
            String Active = ResultBar.FindElements(By.ClassName("results-group"))[0].Text;
            Active = Active.Trim();
            if (Active == "")
            {
                return 0;
            }
            else
            {
                Active = Active.Replace("Total:", "");
                Active = Active.Trim();
                return Double.Parse(Active, System.Globalization.NumberStyles.Currency);
            }

        }

        internal double GetactiveAdAmmount_OrderLine()
        {
            String Active = ResultBar.FindElements(By.ClassName("results-group"))[0].Text;
            Active = Active.Trim();
            if (Active == "")
            {
                return 0;
            }
            else
            {
                Active = Active.Replace("Active:", "");
                Active = Active.Trim();
                return Double.Parse(Active, System.Globalization.NumberStyles.Currency);
            }
        }

        internal double GetTotalAdAmmount_OrderLine()
        {
            string Total = TotalAmmount.Text;
            Total = Total.Trim();
            if (Total == "")
            {
                return 0;
            }
            else
            {
                Total = Total.Trim();
                return Double.Parse(Total, System.Globalization.NumberStyles.Currency);
            }
        }

        internal AllOrdersPage ClearFilter()
        {
            try
            {
                var Filters = OrderFilterParent.FindElements(By.TagName("input"));
                foreach (var item in Filters)
                {
                    item.Clear();
                }
                Wait.MLSeconds(200);
                Wait.UntilLoading();
            }
            catch (Exception)
            {

            }
            return this;
        }

        internal void RenewalButtonShouldNotBeThere()
        {
            Element.NotExist(RenewalBtn);
        }

        internal void SetFutureYearFilter()
        {
            Select.ByText(FilterYearDD, (DateTime.Now.Year + 1).ToString());
            Wait.AM_Loaging_ShowAndHide();
            if (OrderResultGridParent.FindElements(By.TagName("tr"))[0].FindElement(By.TagName("input")).Enabled)
            {
                Select.ByText(FilterYearDD, DateTime.Now.Year.ToString());
                Wait.AM_Loaging_ShowAndHide();
                throw new Exception("Check box is Enabled for future year.");
            }
            Select.ByText(FilterYearDD, DateTime.Now.Year.ToString());
            Wait.AM_Loaging_ShowAndHide();
        }

        internal AllOrdersPage ChangeStatusOfOrder(string OrderId, string Status)
        {
            this.ClearFilter();
          var OrderRow=this.SearchOrder(OrderId);
          if (!this.VerifyStatus(OrderRow,Status)) {
              OrderRow.FindElement(By.TagName("input")).Click();
              Wait.Second(1);
              StatusChangeBtn.Click();
              Wait.UntilDisply(By.LinkText(Status));
              this.driver.FindElement(By.LinkText(Status)).Click();
              this.VerifyStatusChange();
          }
          return this;
        }

        private void VerifyStatusChange()
        {
            var message=Wait.UntilToastMessageShow();
            if (message.Any(e => e.Contains("Status changed successfully for Order")))
            {

            }
            else {
                Assert.Fail("Unable to Change status of Order");
            }
        }

        private bool VerifyStatus(IWebElement OrderRow,string Status)
        {
            return OrderRow.FindElements(By.TagName("td"))[5].Text.Trim().ToLower() == Status.ToLower();
        }

        internal void Reload()
        {
            this.driver.Navigate().Refresh();
            Wait.AM_Loaging_ShowAndHide();
        }
    }
}
