using AdManagementT_Automation.Base;
using AdManagementT_Automation.Ref;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using SeleniumExtension.Controls;
using SeleniumExtension.Utilties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdManagementT_Automation.Pages.Admin
{
    public class AdGroupPage
    {
        [FindsBy(How = How.CssSelector, Using = "input[data-ng-model='filter.Member.Name']")]
        private IWebElement MemberIdField { get; set; }


        [FindsBy(How = How.CssSelector, Using = "div[selected-model='filter.SalesReps']")]
        private IWebElement SalesRepControl { get; set; }

        [FindsBy(How = How.CssSelector, Using = "div[selected-model='filter.Coordinators']")]
        private IWebElement CoordinatorControl { get; set; }

        [FindsBy(How = How.Id, Using = "drpYear")]
        private IWebElement YearDD { get; set; }

        [FindsBy(How = How.CssSelector, Using = "button[ng-click='search()']")]
        private IWebElement SearchBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = "button[ng-click='export()']")]
        private IWebElement ExportBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = "tr[data-ng-repeat='group in $data']")]
        private IList<IWebElement> ResultRows { get; set; }

        [FindsBy(How = How.TagName, Using = "table")]
        private IList<IWebElement> ResultGrid { get; set; }

        public AdGroupPage Navigate() {
            PagesRepo.AddTabs.Switch(AM_Sub_Admin.AddGroups);
            return this;
        }
        public AdGroupPage Search() {
            this.SearchBtn.Click();
            Wait.AM_Loaging_ShowAndHide();
            return this;
        }
        public AdGroupPage Export() {
            Wait.MLSeconds(300);
            Wait.UntilLoading();
            Wait.UntilClickAble(ExportBtn);
            if (ExportBtn.Enabled)
            {
                FileHandler.BerforeDownLoadNotification();
                ExportBtn.Click();
                Wait.UntilDownloading();
                FileHandler.CheckIfExcelFileContainRecords(FileHandler.FindExcelFilePathForReport());

            }
            else {
                Assert.Fail("Export Btn is Disabled.");
            }
            return this; 
        }
        public AdGroupPage ClearAllfilter() {
            MemberIdField.Clear();
            Select.SelectFromMultipleControl(new List<string>() { "Uncheck All" }, SalesRepControl);
            Select.SelectFromMultipleControl(new List<string>() { "Uncheck All" }, CoordinatorControl);
            Select.ByText(YearDD, DateTime.Now.Year.ToString());
            return this;
        }
        public AdGroupPage FillSearchFilters(string memberID,IList<string> SalesRep,IList<string> Coordinator,string year) {
            if (memberID != "") {
                Select.FromList(memberID, MemberIdField);
            }
            if (SalesRep != null)
            {
                Select.SelectFromMultipleControl(SalesRep, SalesRepControl);
            }
            if (Coordinator != null)
            {
                Select.SelectFromMultipleControl(Coordinator,CoordinatorControl);

            } if (year != ""||year!=null) {
                Select.ByText(YearDD, year);
            }
            return this;
        }


    }
}
