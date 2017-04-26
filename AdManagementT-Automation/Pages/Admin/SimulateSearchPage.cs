using AdManagementT_Automation.Base;
using AdManagementT_Automation.Ref;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using SeleniumExtension.Controls;
using SeleniumExtension.Ref;
using SeleniumExtension.Utilties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdManagementT_Automation.Pages.Admin
{
    public class SimulateSearchPage
    {
        [FindsBy(How = How.CssSelector, Using = "input[data-ng-model='vm.searchModel.KeyWord']")]
        private IWebElement CategoryField { get; set; }


        [FindsBy(How = How.Id, Using = "drpApplication")]
        private IWebElement Productgroup { get; set; }

        [FindsBy(How = How.CssSelector, Using = "button[data-ng-click='vm.getPageData(frmSimulation.$valid)']")]
        private IWebElement SimulateSearch_SearchBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = "button[data-ng-click='vm.searchTestCall(frmtestcall.$valid)']")]
        private IWebElement TestCall_SearchBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[data-ng-model='vm.testCallModel.Page']")]
        private IWebElement TestCall_Page { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[data-ng-model='vm.testCallModel.MemberId']")]
        private IWebElement TestCall_MemberID { get; set; }
       
        [FindsBy(How = How.Id, Using = "drpApp")]
        private IWebElement TestCall_Productgroup { get; set; }

        [FindsBy(How = How.Id, Using = "drpAdType")]
        private IWebElement TestCall_AdType { get; set; }

        [FindsBy(How = How.CssSelector, Using = "div[data-ng-show='vm.ShowPageSearchResult']")]
        private IWebElement SimulateSearch_Result { get; set; }

         [FindsBy(How = How.CssSelector, Using = "table[data-ng-table='vm.tableAdsParams']")]
        private IWebElement TestCall_ResultGrid { get; set; }
        

        public SimulateSearchPage Navigate()
        {
            PagesRepo.AddTabs.Switch(AM_Sub_Admin.Simulate_Search);
            return this;
        }
        public SimulateSearchPage SimulateSearch_Page(string Terms, string productGroup)
        {
            if (!string.IsNullOrEmpty(Terms))
            {
                CategoryField.Clear();
                CategoryField.SendKeys(Terms);
            } if (!string.IsNullOrEmpty(productGroup))
            {
                Select.ByText(Productgroup, productGroup);
            }
            SimulateSearch_SearchBtn.Click();
            Wait.AM_Loaging_ShowAndHide();
            this.VerifySimulateResult(Terms);
            return this;
        }

        private void VerifySimulateResult(string Terms)
        {
            var result = SimulateSearch_Result.FindElements(By.TagName("span"));
            if (result[0].Text.ToLower() != "/" + Terms.ToLower())
            {
                Logger.Log(LogingType.TextCaseFail, "Simulate Search Result Not Varfied");
                throw new Exception("Simulate Search Result Not Varfied");
            }
            else
            {
                Logger.Log(LogingType.TestCasePass, "Simulate Search Result Varfied");
            }
        }
        public string GetPostionData(int position,int Addnumber) {
            IWebElement Body = TestCall_ResultGrid.FindElement(By.TagName("tbody"));
            IList<IWebElement> Tr = Body.FindElements(By.TagName("tr"));
            return Tr[position-1].FindElements(By.TagName("td"))[Addnumber].Text;
        }
        internal void TestCall(string Page, string MemnerID, string productGroup, string adtype)
        {
            if (!string.IsNullOrEmpty(Page)) {
               // TestCall_Page.Clear();
                //TestCall_Page.SendKeys(Page);
                Select.FromList(Page, TestCall_Page);
            }
            if (!string.IsNullOrEmpty(MemnerID))
            {
                TestCall_MemberID.Clear();
                TestCall_MemberID.SendKeys(MemnerID);
            }
            Select.ByText(TestCall_Productgroup, productGroup);
            Select.ByText(TestCall_AdType,adtype);
            Wait.UntilClickAble(TestCall_SearchBtn);
            TestCall_SearchBtn.Click();
            Wait.AM_Loaging_ShowAndHide();
        }
        
    }
}
