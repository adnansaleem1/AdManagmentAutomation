using AddManagmentData.Model;
using AdManagementT_Automation.Base;
using AdManagementT_Automation.Controls;
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

namespace AdManagementT_Automation.Pages.Inventory
{
    public class InventoryPage
    {

        IWebDriver driver = SDriver.Browser;
        [FindsBy(How = How.Id, Using = "drpApplication")]
        private IWebElement Productgroup { get; set; }

        [FindsBy(How = How.Id, Using = "drpAdType")]
        private IWebElement AdType { get; set; }

        [FindsBy(How = How.Id, Using = "drpMonth")]
        private IWebElement Month { get; set; }

        [FindsBy(How = How.Id, Using = "drpYear")]
        private IWebElement Year { get; set; }

        [FindsBy(How = How.CssSelector, Using = "tags-input[data-ng-model='SearchTerm']")]
        private IWebElement searchTermsControl { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[data-ng-model='IncludeSubCategory']")]
        private IWebElement SubCatagoriesCheckbox { get; set; }

        [FindsBy(How = How.CssSelector, Using = "button[data-ng-click='loadGridData(amFormInventory.$valid)']")]
        private IWebElement SearchBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = "button[data-ng-click='createProposal()']")]
        private IWebElement CreateProposalBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = "table[data-ng-table='tableParams']")]
        private IWebElement ResultGrid { get; set; }

        [FindsBy(How = How.Id, Using = "menu1")]
        private IWebElement InventoryTypeMenu { get; set; }

        [FindsBy(How = How.CssSelector, Using = "ul[aria-labelledby='menu1']")]
        private IWebElement InventoryTypeMenuList { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[data-ng-model='proposal.MemberId']")]
        private IWebElement MemberIdField { get; set; }

        [FindsBy(How = How.CssSelector, Using = "select[data-ng-model='selectedShipToContact']")]
        private IWebElement ContactDD { get; set; }

        [FindsBy(How = How.CssSelector, Using = "select[data-ng-model='selectedSalesRep']")]
        private IWebElement SalesRepDD { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[data-ng-model='proposal.ProposalName']")]
        private IWebElement ProposalNameField { get; set; }

        [FindsBy(How = How.CssSelector, Using = "button[data-ng-click='saveProposal(amFormProposalDetail.$valid, \\'save\\');']")]
        private IWebElement SaveProposalBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = "span[ng-bind='$getDisplayText()']")]
        private IList<IWebElement> FilterText { get; set; }
        

        public InventoryPage Navigate()
        {
            PagesRepo.AddTabs.Switch(AM_MainTab.Inventory);
            return this;
        }

        internal InventoryPage FillSearch(AddManagmentData.Model.InventoryModel inventory)
        {
        
            if (string.IsNullOrEmpty(inventory.InventoryType))
            {
                this.SelectInventoryType(inventory.InventoryType);
            }
            if (!string.IsNullOrEmpty(inventory.ProductGroup))
            {
                Select.ByText(Productgroup, inventory.ProductGroup);
            }
            if (!string.IsNullOrEmpty(inventory.AdType))
            {
                Select.ByText(AdType, inventory.AdType);
            }
            if (!string.IsNullOrEmpty(inventory.Month))
            {
                Select.ByText(Month, inventory.Month);
            }
            if (!string.IsNullOrEmpty(inventory.Year.ToString()))
            {
                Select.ByText(Year, inventory.Year.ToString());
            }
            if (inventory.SearchTerms.Count > 0)
            {
                Wait.MLSeconds(200);
                Select.ClearTagBasedInput(searchTermsControl);
                Select.TagBasedInput(inventory.SearchTerms, searchTermsControl);
            }
            Element.syncCheckBox(inventory.IncludeSubCat, SubCatagoriesCheckbox);
            Wait.MLSeconds(100);
            return this;
        }

        internal InventoryPage Search()
        {
            Wait.MLSeconds(200);
            Wait.UntilClickAble(SearchBtn);
            SearchBtn.Click();
            Wait.AM_Loaging_ShowAndHide();
            return this;
        }

        internal void VerifySearchResultWithSubTerms(string p)
        {
            IList<IWebElement> ResultList = this.GetResultRows();
            var Count = ResultList.Count(e => e.FindElement(By.CssSelector("td[data-title-text='Search Term']")).Text.Contains(p + "/"));
            if (Count > 0)
            {
                Logger.Log(LogingType.TestCasePass, "Inventory Search Result contain sub categories.");
            }
            else
            {
                Logger.Log(LogingType.TextCaseFail, "Inventory Search Result does not contain sub categories.");
            }

        }

        internal IWebElement GetResultFromSearch(string p)
        {
            IList<IWebElement> ResultList = this.GetResultRows();
            var Result = ResultList.FirstOrDefault(e => e.FindElement(By.CssSelector("td[data-title-text='Search Term']")).Text.Contains(p + "/") || e.FindElement(By.CssSelector("td[data-title-text='Search Term']")).Text.Contains(p));
            if (Result == null)
            {
                throw new Exception("No Result Found With given term.");
            }
            return Result;
        }
        private IList<IWebElement> GetResultRows()
        {
            return ResultGrid.FindElements(By.CssSelector("tr[data-ng-repeat='inventory in $data']"));
        }

        private void SelectInventoryType(string p)
        {
            InventoryTypeMenu.Click();
            Wait.MLSeconds(100);
            InventoryTypeMenuList.FindElement(By.LinkText(p)).Click();
            Wait.MLSeconds(300);
        }

        internal void CreateProposal(PropsalModel Data)
        {
            IWebElement Result = this.GetResultFromSearch(Data.SearchTerms[0]);
            Result.FindElement(By.CssSelector("input[checklist-value='inventory']")).Click();
            Wait.MLSeconds(100);
            CreateProposalBtn.Click();
            Wait.UntilDisply(MemberIdField);
            this.FillProposal(Data);
            this.SaveProposalBtn.Click();
            IList<string> Resultbanner = Wait.UntilToastMessageShow();
            if (Resultbanner.Any(e => e == "Saved successfully"))
            {
                Logger.Log(LogingType.TestCasePass, "New Proposal Save Sucessfully");

            }
            else
            {
                Logger.Log(LogingType.TextCaseFail, "New Proposal Unable to save");

            }
        }
        private void FillProposal(PropsalModel Data)
        {

            Select.FromList(Data.MemberId, MemberIdField);
            Wait.Second(2);
            Select.ByText(ContactDD, Data.Contact);
            Select.ByText(SalesRepDD, Data.SalesRep);
            ProposalNameField.Clear();
            ProposalNameField.SendKeys(Data.ProposalName);
            Wait.MLSeconds(100);
        }
        

        internal int GetPositionInventory(int pos, string Data)
        {
            int basepos = 5;
            IWebElement ResultRow = this.GetResultFromSearch(Data);
            if (ResultRow != null)
            {
                return int.Parse(ResultRow.FindElements(By.TagName("td"))[basepos + pos].Text.Trim());
            }
            else
            {

                throw new Exception("Unable to found Serach term");
            }
        }

        internal InventoryPage ChangeInventoryType(string p)
        {
            driver.FindElement(By.Id("menu1")).Click();
            Wait.MLSeconds(300);
            driver.FindElement(By.LinkText(p)).Click();
            Wait.AM_Loaging_ShowAndHide();
            return this;
        }

        internal void VerifySearchResultWithoutSubTerms(string p)
        {
            IList<IWebElement> ResultList = this.GetResultRows();
            var Count = ResultList.Count(e => e.FindElement(By.CssSelector("td[data-title-text='Search Term']")).Text.Contains(p + "/"));
            if (Count > 0)
            {
                Logger.Log(LogingType.TextCaseFail, "Inventory Search Result contain sub categories.");
                throw new Exception("Inventory Search Result contain sub categories.");
            }
            else
            {
                Logger.Log(LogingType.TestCasePass, "Inventory Search Result does not contain sub categories.");
            }

        }




        internal string ChangeSortOrder(int ColNo)
        {
            var Cols = ResultGrid.FindElement(By.TagName("thead")).FindElements(By.TagName("tr"))[0].FindElements(By.TagName("th"));
            Cols[ColNo].Click();
            Wait.AM_Loaging_ShowAndHide();
            return this.GetSortOnCol(ColNo);
        }

        internal string GetSortOnCol(int ColNo)
        {

            var Cols = ResultGrid.FindElement(By.TagName("thead")).FindElements(By.TagName("tr"))[0].FindElements(By.TagName("th"));
            return Cols[ColNo].GetAttribute("class").Split(' ').FirstOrDefault(e => e.Contains("sort-"));
        }

        internal void VerfiySearchRetain(InventoryModel Data)
        {

            if (Data.SearchTerms[0].ToLower() != FilterText[0].Text.ToLower()) {
                throw new Exception("System was unable to retain filter information");
            }
        }

        internal void VerfiySearchNotRetain(InventoryModel Data)
        {
            if (FilterText.Count>0)
            {
                throw new Exception("System was retaining filter information.");
            }
        }
    }
}
