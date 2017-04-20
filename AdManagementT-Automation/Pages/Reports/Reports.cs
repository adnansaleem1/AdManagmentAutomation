using AdManagementT_Automation.Base;
using AdManagementT_Automation.Ref;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using SeleniumExtension.Controls;
using SeleniumExtension.Driver;
using SeleniumExtension.Ref;
using SeleniumExtension.Utilties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdManagementT_Automation.Pages.Reports
{
   public class Reports
    {
        IWebDriver driver = SDriver.Browser;

        [FindsBy(How = How.CssSelector, Using = "select[data-ng-model='Report.Code']")]
        private IWebElement ReportTypeDD { get; set; }

       [FindsBy(How = How.CssSelector, Using = "div[data-ng-show='ShowReportBody']")]
        private IWebElement ExcelOnly { get; set; }
       

        [FindsBy(How = How.Id, Using = "drpApplication")]
        private IWebElement Productgroup { get; set; }

        [FindsBy(How = How.Id, Using = "drpAdType")]
        private IWebElement AdType { get; set; }

        [FindsBy(How = How.CssSelector, Using = "button[data-ng-disabled='!AllowExport']")]
        private IWebElement ExportBtn { get; set; }

          [FindsBy(How = How.CssSelector, Using = "input[data-ng-model='filterModel.MemberId']")]
        private IWebElement MemberIDDD { get; set; }
       

        [FindsBy(How = How.CssSelector, Using = "button[data-ng-click='ViewReport()']")]
        private IWebElement RunReportBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = "tr[data-ng-repeat='row in $data']")]
        private IList<IWebElement> DropRepoductResult { get; set; }


        [FindsBy(How = How.CssSelector, Using = "tr[data-ng-repeat='inventory in $data']")]
        private IList<IWebElement> InventoryHistoryRecords { get; set; }
       

        [FindsBy(How = How.CssSelector, Using = "tags-input[data-ng-model='filterModel.SearchTerm']")]
        private IWebElement searchTermsControl { get; set; }
       


        public Reports DropProductReport(string ProductG, string Adtype)
        {
            Select.ByText(ReportTypeDD, "Dropped Product");
            Wait.UntilDisply(Productgroup);
            Select.ByText(Productgroup, ProductG);
            Select.ByText(AdType, Adtype);
            Wait.MLSeconds(200);
            Wait.UntilClickAble(RunReportBtn);
            RunReportBtn.Click();
            Wait.AM_Loaging_ShowAndHide();
            this.verifyGridResult();
            return this;
        }

        private void verifyGridResult()
        {
            if (DropRepoductResult.Count>0)
            {
                Logger.Log(LogingType.TestCasePass, "Report Contain Result Records");

            }
            else
            {
                
                Logger.Log(LogingType.TextCaseFail, "Report Contains No Result");
                throw new Exception("Report Contains No Result");
            }
        }
         private void verifyGridResult_InventoryHistory()
        {
            if (InventoryHistoryRecords.Count>0)
            {
                Logger.Log(LogingType.TestCasePass, "Report Contain Result Records");

            }
            else
            {
                
                Logger.Log(LogingType.TextCaseFail, "Report Contains No Result");
                throw new Exception("Report Contains No Result");
            }
        }

       
        public void Export(string type) {
            if (type.ToLower() == "pdf") {
                ExportBtn.Click();
                Wait.MLSeconds(100);
                FileHandler.BerforeDownLoadNotification();
                driver.FindElement(By.LinkText("PDF")).Click();
                Wait.UntilDownloading();
                FileHandler.CheckIfPDFFileContainRecords(FileHandler.FindPDFFilePathForReport());
            }
            else if (type.ToLower() == "excel") {
                try
                {
                    ExportBtn.Click();
                   
                }
                catch (Exception)
                {

                    ExcelOnly.FindElement(By.TagName("button")).Click();
                }
                FileHandler.BerforeDownLoadNotification();
                Wait.MLSeconds(100);
                driver.FindElement(By.LinkText("Excel")).Click();
                Wait.UntilDownloading();
                Wait.MLSeconds(200);
                FileHandler.CheckIfExcelFileContainRecords(FileHandler.FindExcelFilePathForReport());
            }
        
        }



        public Reports Navigate()
        {
            PagesRepo.AddTabs.Switch(AM_MainTab.Reports);
            return this;
        }

        internal Reports OrderBySearchTerms(string ProductG, string Adtype,string term)
        {
            Select.ByText(ReportTypeDD, "Orders By Search Term");
            Wait.UntilDisply(Productgroup);
            Wait.MLSeconds(500);
            Select.ByText(Productgroup, ProductG);
            Wait.MLSeconds(300);
            Select.ByText(AdType, Adtype);
            Select.ClearTagBasedInput(searchTermsControl);
            Select.TagBasedInput(term, searchTermsControl);
            RunReportBtn.Click();
            Wait.AM_Loaging_ShowAndHide();
            this.verifyGridResult();
            return this;
        }

        internal Reports AdsBelowRateCard()
        {
            Select.ByText(ReportTypeDD, "Ads Below Rate Card");
            Wait.Second(1);
            RunReportBtn.Click();
            Wait.AM_Loaging_ShowAndHide();
            this.verifyGridResult();
            return this;
        }

        internal Reports DefaultProductReport(string MemberId, string ProductGroup)
        {
            Select.ByText(ReportTypeDD, "Default Product Report");
            Wait.Second(1);
            Select.ByText(Productgroup, ProductGroup);
            Select.FromList(MemberId, MemberIDDD);
            Wait.MLSeconds(100);
            RunReportBtn.Click();
            Wait.AM_Loaging_ShowAndHide();
            this.verifyGridResult();
            return this;
        }

        internal Reports InventoryHistoryReport(string ProductGroup,string Searchterms="")
        {
            Select.ByText(ReportTypeDD, "Inventory History Report");
            Wait.Second(1);
            Select.ByText(Productgroup, ProductGroup);
            //Select.ByText(AdType, Adtype);
            if (!string.IsNullOrEmpty(Searchterms)) {
                Select.TagBasedInput(Searchterms, searchTermsControl);            
            }
            RunReportBtn.Click();
            Wait.AM_Loaging_ShowAndHide();
            this.verifyGridResult_InventoryHistory();
            return this;
        }
    }
}
