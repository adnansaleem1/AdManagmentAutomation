using AdManagementT_Automation.Base;
using AdManagementT_Automation.Controls;
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

namespace AdManagementT_Automation.Pages.Inventory
{
    public class InventoryPage
    {
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
        
        
        public InventoryPage Navigate() {
            PagesRepo.AddTabs.Switch(AM_MainTab.Invetory);
            return this;
        }



        internal InventoryPage FillSearch(AddManagmentData.Model.InventoryModel inventory)
        {
            if (string.IsNullOrEmpty(inventory.InventoryType))
            {
                this.SelectInventoryType(inventory.InventoryType);
            }
            if (!string.IsNullOrEmpty(inventory.ProductGroup)) {
                Select.ByText(Productgroup, inventory.ProductGroup);
            }
            if (!string.IsNullOrEmpty(inventory.AdType)) {
                Select.ByText(AdType, inventory.AdType);
            }
            if (!string.IsNullOrEmpty(inventory.Month))
            {
                Select.ByText(Month, inventory.Month);
            }
            if (!string.IsNullOrEmpty(inventory.Year.ToString()))
            {
                Select.ByText(Year, inventory.AdType.ToString());
            }
            if (inventory.SearchTerms.Count>0)
            {
                Select.SelectFromMultipleControl(inventory.SearchTerms,searchTermsControl);
            }
            Element.syncCheckBox(inventory.IncludeSubCat, SubCatagoriesCheckbox);
            Wait.MLSeconds(100);
            return this;
        }

        internal InventoryPage Search() {

            SearchBtn.Click();
            Wait.AM_Loaging_ShowAndHide();
            return this;        
        }

        internal void VerifySearchResultWithSubTerms(string p) {
           IList<IWebElement> ResultList=  this.GetResultRows();
           var Count = ResultList.Count(e => e.FindElement(By.CssSelector("td[data-title-text='Search Term']")).Text.Contains(p + "/"));
           if (Count > 0)
           {
               Logger.Log(LogingType.TestCasePass, "Inventory Search Result contain sub categories.");
           }
           else {
               Logger.Log(LogingType.TextCaseFail, "Inventory Search Result does not contain sub categories.");                     
           }

        }
        private IList<IWebElement> GetResultRows() {
            return ResultGrid.FindElements(By.CssSelector("tr[data-ng-repeat='inventory in $data']"));
        }

        private void SelectInventoryType(string p)
        {
            InventoryTypeMenu.Click();
            Wait.MLSeconds(100);
            InventoryTypeMenuList.FindElement(By.LinkText(p)).Click();
            Wait.MLSeconds(300);
        }
    }
}
