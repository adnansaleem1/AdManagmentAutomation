using AddManagmentData.Model;
using AdManagementT_Automation.Base;
using AdManagementT_Automation.Ref;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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

namespace AdManagementT_Automation.Pages.Adevertisements
{
    public class AdvertisementPage
    {

        #region Elemets
        IWebDriver driver = SDriver.Browser;

        [FindsBy(How = How.CssSelector, Using = "input[data-ng-model='filterMember.Name']")]
        private IWebElement MemberId { get; set; }

        [FindsBy(How = How.CssSelector, Using = "div[selected-model='filterPositionSelectedValues']")]
        private IWebElement PositionControl { get; set; }

        [FindsBy(How = How.CssSelector, Using = "tags-input[data-ng-model='filterSearchTermSelectedValues']")]
        private IWebElement SearchTermsControl { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[data-ng-model='IncludeSubcategory']")]
        private IWebElement IncludeSubCatInput { get; set; }

        [FindsBy(How = How.CssSelector, Using = "div[selected-model='filterSalesSelectedValues']")]
        private IWebElement SalesRepsControl { get; set; }

        [FindsBy(How = How.CssSelector, Using = "div[selected-model='filterStatusSelectedValues']")]
        private IWebElement AdStatusControl { get; set; }

        [FindsBy(How = How.Id, Using = "drpYear")]
        private IWebElement YearDD { get; set; }

        [FindsBy(How = How.Id, Using = "drpMonth")]
        private IWebElement MonthDD { get; set; }

        [FindsBy(How = How.CssSelector, Using = "div[selected-model='filterRateSelectedValues']")]
        private IWebElement RateControl { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[ng-model='filterText']")]
        private IWebElement FilterTextfield { get; set; }

        [FindsBy(How = How.CssSelector, Using = "button[data-ng-click='searchOnText();']")]
        private IWebElement Searchbtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = "table[ng-table='tableAdListParams']")]
        private IWebElement ResultGrid { get; set; }
        
        [FindsBy(How = How.CssSelector, Using = "button[data-ng-click='exportAds()']")]
        private IWebElement DownloadAdsBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = "button[class='btn btn-default dropdown-toggle ng-binding']")]
        private IWebElement ChangeStatusDropDown { get; set; }

        [FindsBy(How = How.CssSelector, Using = "div[selected-model='filterGroupSelectedValues']")]
        private IWebElement ProductGroupControl { get; set; }

        [FindsBy(How = How.CssSelector, Using = "div[selected-model='filterAdTypeSelectedValues']")]
        private IWebElement AdTypeControl { get; set; }


        #endregion



        #region Utilities


        private void TagBasedInput(IList<string> list, IWebElement Control)
        {
            if (list!=null&&list.Count>0)
            {
                Element.ScrolTo(Control);
                foreach (var item in list)
                {
                    Control.FindElement(By.TagName("input")).SendKeys(item);
                    Wait.UntilDisply(By.ClassName("autocomplete"));
                    Control.FindElement(By.TagName("input")).SendKeys(Keys.Enter);
                    Wait.MLSeconds(200);
                } 
            }

        }

      
        private string FindMemberIdFromResultRow(IWebElement webElement)
        {
            return webElement.FindElement(By.CssSelector("span[ng-bind-html='orderLine.AsiNumber | highlight:filterText']")).Text;
        }
        private IList<IWebElement> GetRowsOfGrid()
        {
            IList<IWebElement> Rows = ResultGrid.FindElements(By.CssSelector("tr[ng-repeat='orderLine in $data']"));
            return Rows;
        }
        private int FindPositionFromResultRow(IWebElement webElement)
        {
            return Convert.ToInt16(webElement.FindElement(By.CssSelector("div[ng-bind-html='orderLine.Position.toString() | highlight:filterText']")).Text);
        }
        private string FindSearchTermsFromResultRow(IWebElement ele)
        {
            return ele.FindElement(By.CssSelector("div[ng-bind-html='orderLine.SearchTermPage | highlight:filterText']")).Text;
        }
        private string FindStatusFromResultRow(IWebElement ele)
        {
            return ele.FindElement(By.CssSelector("span[ng-bind-html='orderLine.Status | highlight:filterText']")).Text;
        }
        private AdvertisementPage Search()
        {
            Wait.MLSeconds(500);
            Searchbtn.Click();
            Wait.AM_Loaging_ShowAndHide();
            return this;
        }
        public AdvertisementPage Navigate()
        {
            PagesRepo.AddTabs.Switch(AM_MainTab.Advertisments);
            Wait.UntilDisply(MemberId);
            return this;
        }
        private void FillEMemberID(string Data)
        {
            if (Data != null)
            {
                Select.FromList(Data, MemberId);
                Wait.IfLoadingIsStillVisible();
            }
           // MemberId.SendKeys(Data == null ? "" : Data);
        }

        public AdvertisementPage FillSearchParamitters(AdvertisementSearchModel Data)
        {
            this.ClearFilter();
            FillEMemberID(Data.MemberID);
            Select.SelectFromMultipleControl(Data.Positions, PositionControl);
            this.TagBasedInput(Data.SearchTerms, SearchTermsControl);
            Element.syncCheckBox(Data.IncludeSubCat, IncludeSubCatInput);
            Select.SelectFromMultipleControl(Data.SalesReps, SalesRepsControl);
            Select.SelectFromMultipleControl(Data.Statuses, AdStatusControl);
            Select.ByText(YearDD, Data.Year.ToString());
            Select.ByText(MonthDD, Data.Month.ToString());
            Select.SelectFromMultipleControl(Data.Rates, RateControl);
            FilterTextfield.SendKeys(Data.SearchField == null ? "" : Data.SearchField);
            Select.SelectFromMultipleControl(Data.ProductGroup, ProductGroupControl);
            Select.SelectFromMultipleControl(Data.AddType, AdTypeControl);
            this.Search();
            return this;
        }

        private string FindAddIDFromResultRow(IWebElement ResultRow)
        {
            return ResultRow.FindElement(By.CssSelector("div[ng-bind-html='orderLine.Id.toString() | highlight:filterText']")).Text;
        }
        private void SelectRow(IWebElement ResultRow)
        {

            ResultRow.FindElement(By.CssSelector("input[ng-click='loginfo(orderLine)']")).Click();
            Wait.MLSeconds(200);
        }

        private void ChangeStatusofSelectedAds(string p)
        {
            ChangeStatusDropDown.Click();
            Wait.MLSeconds(200);
            driver.FindElement(By.LinkText(p)).Click();
            this.VerifyChangeStatus();
            //drive
        }

        private void VerifyChangeStatus()
        {
            IList<string> result = Wait.UntilToastMessageShow();

            if (result.Any(e => e.Contains("Status change failed for ids")))
            {
                Logger.Log(LogingType.TestCasePass, "Fail to change Status of Ad");
                Assert.Fail("Fail to change Status of Ad");
            }
            else 
            {

                Logger.Log(LogingType.TestCasePass, "Ad Status Changed Successfully");

            }  
        }

        public void ClearFilter() {
            this.MemberId.Clear();
            Wait.MLSeconds(100);
            Select.SelectFromMultipleControl(new List<string>() { "Uncheck All" }, PositionControl);
            Select.ClearTagBasedInput(SearchTermsControl);
            Select.SelectFromMultipleControl(new List<string>() { "Uncheck All" }, SalesRepsControl);
            Select.SelectFromMultipleControl(new List<string>() { "Uncheck All" }, AdStatusControl);
            Select.SelectFromMultipleControl(new List<string>() { "Uncheck All" }, RateControl);
            Select.SelectFromMultipleControl(new List<string>() { "Uncheck All" }, AdTypeControl);
            Select.SelectFromMultipleControl(new List<string>() { "Uncheck All" }, ProductGroupControl);
            
            Wait.MLSeconds(200);
        }
        #endregion


        #region Search Methods


        internal AdvertisementPage SearchByMemeberID(string p)
        {
            this.FillEMemberID(p);
            this.Search();
            this.VerifyMemberIdSerch(p);
            return this;
        }
        internal AdvertisementPage SearchByPosition(IList<int> pList)
        {
            Select.SelectFromMultipleControl(pList, PositionControl);
            this.Search();
            this.verifySearchByPostion(pList);
            return this;
        }
        internal void SearchByTerms(IList<string> list)
        {
            this.TagBasedInput(list, SearchTermsControl);
            this.Search();
            this.VerifySearchTerms(list);
        }
        internal void SearchByAddStatus(IList<string> list)
        {
            Select.SelectFromMultipleControl(list, AdStatusControl);
            this.Search();
            this.VerifyStatuses(list);
        }
        internal void SearchByRate(IList<string> list)
        {
            Select.SelectFromMultipleControl(list, RateControl);
            this.Search();
            this.verifyIFResultExists();
        }
        internal void FreeFormSerch()
        {
            this.Search();
            this.verifyIFResultExists();
        }
        
        #endregion


        #region Verifications
        private void VerifyMemberIdSerch(string p)
        {
            IList<IWebElement> Rows = GetRowsOfGrid();
            IWebElement Element = Rows.FirstOrDefault(e => this.FindMemberIdFromResultRow(e) != p);
            if (Element == default(IWebElement))
            {
                Logger.Log(LogingType.TestCasePass, "Search With Member Id Result Verified");
            }
            else
            {
                Logger.Log(LogingType.TextCaseFail, "Search With Member Id Result Not Verified");
                throw new Exception();
            }
            //  this.FindMemberIdFromResultRow(Rows[0]);

        }


        private void verifySearchByPostion(IList<int> pList)
        {
            IList<IWebElement> Rows = GetRowsOfGrid();

            IWebElement Element = Rows.FirstOrDefault(e => pList.Any(x => x != this.FindPositionFromResultRow(e)));
            if (Element == default(IWebElement))
            {
                Logger.Log(LogingType.TestCasePass, "Search By Position Result Verified");
            }
            else
            {
                Logger.Log(LogingType.TextCaseFail, "Search By Position Result Not Verified");
                throw new Exception();
            }
        }

        private void VerifySearchTerms(IList<string> list)
        {
            IList<IWebElement> Rows = GetRowsOfGrid();

            IWebElement Element = Rows.FirstOrDefault(e => list.Any(x => x != this.FindSearchTermsFromResultRow(e)));
            if (Element == default(IWebElement))
            {
                Logger.Log(LogingType.TestCasePass, "Search By Terms Result Verified");
            }
            else
            {
                Logger.Log(LogingType.TextCaseFail, "Search By Terms Result Not Verified");
                throw new Exception();
            }
        }

        private void VerifyStatuses(IList<string> list)
        {
            IList<IWebElement> Rows = GetRowsOfGrid();

            IWebElement Element = Rows.FirstOrDefault(e => list.Any(x => x != this.FindStatusFromResultRow(e)));
            if (Element == default(IWebElement))
            {
                Logger.Log(LogingType.TestCasePass, "Search By Status Result Verified");
            }
            else
            {
                Logger.Log(LogingType.TextCaseFail, "Search By Status Result Not Verified");
                throw new Exception();
            }
        }
        private void verifyIFResultExists()
        {
            IList<IWebElement> Rows = GetRowsOfGrid();

            if (Rows.Count > 0)
            {
                Logger.Log(LogingType.TestCasePass, "Search Result Exists Verified");
            }
            else
            {
                Logger.Log(LogingType.TextCaseFail, "Search Result Not Exists");
                throw new Exception();
            }
        }

        public void VerifyTabShouldNotBeThere() {
            Element.NotExist(By.LinkText("Advertisements"));
        }
        #endregion


        internal void ExportAllAdds()
        {
            this.Search();
            FileHandler.BerforeDownLoadNotification();
            DownloadAdsBtn.Click();
            Wait.UntilDownloading();
            FileHandler.CheckIfExcelFileContainRecords(FileHandler.FindExcelFilePathForReport());
            
        }

        internal void ChangeAdStatus(AdvertisementSearchModel advertisementSearchModel)
        {
            string AddID, Status;
            this.FillSearchParamitters(advertisementSearchModel);
            this.Search();
            if (this.GetRowsOfGrid().Count > 0)
            {
                IWebElement ResultRow = this.GetRowsOfGrid()[0];
                Status = this.FindStatusFromResultRow(ResultRow);
                AddID = this.FindAddIDFromResultRow(ResultRow);
                this.SelectRow(ResultRow);
                this.ChangeStatusofSelectedAds("Work In Progress");
            }
            else {
                this.ClearFilter();
                advertisementSearchModel.Statuses[0] = "Work In Progress";
                this.FillSearchParamitters(advertisementSearchModel);
                this.Search();
                IWebElement ResultRow = this.GetRowsOfGrid()[0];
                Status = this.FindStatusFromResultRow(ResultRow);
                AddID = this.FindAddIDFromResultRow(ResultRow);
                this.SelectRow(ResultRow);
                this.ChangeStatusofSelectedAds("Active");
            }
           
        }






        internal void VerifydisableCheckBox()
        {
            IList<IWebElement> Rows = GetRowsOfGrid();
            if (Rows.Count > 0 && !Rows[0].FindElement(By.TagName("input")).Enabled)
            {
               
            }
            else {
                Assert.Fail("Check Boxes are Active in Result"); 
            }
        }
    }
}
