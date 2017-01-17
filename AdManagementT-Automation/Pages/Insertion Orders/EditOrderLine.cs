﻿using AddManagmentData.Model;
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

namespace AdManagementT_Automation.Pages.Insertion_Orders
{
    public class EditOrderLinePage
    {
        #region Elements
        [FindsBy(How = How.Id, Using = "drpApplication")]
        private IWebElement ProductGroupDD { get; set; }

        [FindsBy(How = How.Id, Using = "drpAdType")]
        private IWebElement AddTypeDD { get; set; }

        [FindsBy(How = How.Id, Using = "drpPosition")]
        private IWebElement PositionDD { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[data-ng-model='orderLine.AdPage.Page.DisplayName']")]
        private IWebElement SearchTermField { get; set; }

        [FindsBy(How = How.Id, Using = "drpDeliveryPref")]
        private IWebElement DeliveryPrefDD { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[data-ng-model='orderLine.ProductInfo']")]
        private IWebElement ProductInfoField { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[data-ng-model='orderLine.Impressions']")]
        private IWebElement ImpressionsField { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[data-ng-model='orderLine.TargetAmount']")]
        private IWebElement CostField { get; set; }


        [FindsBy(How = How.CssSelector, Using = "input[data-ng-model='orderLine.AllowRateOverwrite']")]
        private IWebElement RateCheckBox { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[data-ng-model='orderLine.Rate']")]
        private IWebElement RateField { get; set; }


        [FindsBy(How = How.CssSelector, Using = "input[data-ng-model='orderLine.IsGeoTargeting']")]
        private IWebElement GeoTaggingEnable { get; set; }

        [FindsBy(How = How.CssSelector, Using = "tags-input[data-ng-model='orderLine.GeoTargetCountry']")]
        private IWebElement GeoCountryParent { get; set; }

        [FindsBy(How = How.CssSelector, Using = "tags-input[data-ng-model='orderLine.GeoTargetState']")]
        private IWebElement GeoTargetStatesParent { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[data-ng-model='IsKeywordTarget']")]
        private IWebElement KeyWordCheckBox { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[data-ng-model='IsCategoryTarget']")]
        private IWebElement IsCatagoryTargetCheckBox { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[data-ng-model='orderLine.AllowSubstitutions']")]
        private IList<IWebElement> AllowSubstitutionsRadios { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[data-ng-model='orderLine.DisplayMultipleAds']")]
        private IList<IWebElement> DisplayMultipleAdsRadios { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[data-ng-model='AllowSearchLeadingText']")]
        private IList<IWebElement> AllowSearchLeadingTextRadios { get; set; }

        [FindsBy(How = How.Id, Using = "drpWeight")]
        private IWebElement PerorityDD { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[data-ng-model='orderLine.ImpressionsPerDay']")]
        private IWebElement ImpressionsPerDayField { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[data-ng-model='orderLine.AutoProductSelection']")]
        private IList<IWebElement> AutoProductSelectionRadio { get; set; }

        [FindsBy(How = How.Id, Using = "drpStatus")]
        private IWebElement StatusDD { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[data-ng-model='orderLine.Group.Id']")]
        private IWebElement groupNameDD { get; set; }

        [FindsBy(How = How.CssSelector, Using = "li[data-ng-click='addProduct()']")]
        private IWebElement AddProductBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[name='searchText']")]
        private IWebElement ProductSearchField { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id=\"productSearchForm\"]/div[2]/div/div/form/div/div/button")]
        private IWebElement ProductSearchByDropDown { get; set; }

        [FindsBy(How = How.LinkText, Using = "Product Id")]
        private IWebElement BYProductIdMenuItem { get; set; }

        [FindsBy(How = How.LinkText, Using = "Keywords")]
        private IWebElement ByKeyWordsMenuItem { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id=\"productSearchForm\"]/div[2]/div/div/form/div/span/button")]
        private IWebElement ProductSearchBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = "div[data-ng-show='orderId']")]
        private IWebElement OrderLineSaveBtn { get; set; }

        [FindsBy(How = How.LinkText, Using = "Save and Add")]
        private IWebElement SaveAndAddMenuItem { get; set; }

        [FindsBy(How = How.LinkText, Using = "Save and Copy")]
        private IWebElement SaveAndCopyMenuItem { get; set; }

        [FindsBy(How = How.CssSelector, Using = "button[data-ng-click='backToOrderDetail(orderId)']")]
        private IWebElement CloseBtn { get; set; }

        [FindsBy(How = How.ClassName, Using = "product-results")]
        private IWebElement ProductsearchResult { get; set; }

        [FindsBy(How = How.CssSelector, Using = "button[data-dismiss='modal']")]
        private IWebElement ProductsearchModelCancelbtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = "ul[move-in-progress='moveInProgress']")]
        private IWebElement SearchTermsListParent { get; set; } 
        
        #endregion
        

        public EditOrderLinePage FillOrderLine(OrderLineModel OrderLineData)
        {
            Wait.UntilDisply(PositionDD);
            Select.ByText(ProductGroupDD, OrderLineData.ProductGroup);
            Wait.MLSeconds(200);
            Select.ByText(AddTypeDD, OrderLineData.AddType);
            Wait.MLSeconds(200);
            Select.ByText(PositionDD, OrderLineData.Position.ToString());
            Wait.MLSeconds(200);
            SelectSearchTerms(OrderLineData.SearchTerm);
            ProductInfoField.Click();
            Wait.MLSeconds(200);
            Select.ByText(DeliveryPrefDD, OrderLineData.DeliveryPreferences);
            Wait.MLSeconds(100);
            ProductInfoField.Clear();
            ProductInfoField.SendKeys(OrderLineData.ProductInformation);
            Wait.MLSeconds(100);
            ImpressionsField.Clear();
            ImpressionsField.SendKeys(OrderLineData.Impressions.ToString());
            Wait.MLSeconds(100);
            if (OrderLineData.Cost != null && OrderLineData.Cost != 0.0)
            {
                CostField.Clear();
                CostField.SendKeys(OrderLineData.Cost.ToString());
                Wait.MLSeconds(100);
            }
            else
            {
                ProductInfoField.Click();
                Wait.MLSeconds(200);
            }
            if (OrderLineData.RateEnable)
            {
                RateCheckBox.Click();
                Wait.MLSeconds(200);
                RateField.Clear();
                RateField.SendKeys(OrderLineData.Rate.ToString());
            }
            if (OrderLineData.GeoTargetEnable)
            {
                GeoTaggingEnable.Click();
                Wait.MLSeconds(200);
                GeoCountryParent.FindElement(By.LinkText("input")).SendKeys(OrderLineData.Countries);
                GeoTargetStatesParent.FindElement(By.TagName("input")).SendKeys(OrderLineData.States);
            }
            if (!OrderLineData.KeyWordsEnable)
            {
                KeyWordCheckBox.Click();
                Wait.MLSeconds(100);
            }
            if (!OrderLineData.CatogoriesEnable)
            {
                IsCatagoryTargetCheckBox.Click();
                Wait.MLSeconds(100);
            }
            if (!OrderLineData.SubsitutionsAllow)
            {
                Element.GetByValueFromList(AllowSubstitutionsRadios, "0").Click();
            }
            if (!OrderLineData.DisplayMultipleAddsAllow)
            {
                Element.GetByValueFromList(DisplayMultipleAdsRadios, "0").Click();
            }
            if (!OrderLineData.SearchLeadingTextAllow)
            {
                Element.GetByValueFromList(AllowSearchLeadingTextRadios, "0").Click();
            }
            if (OrderLineData.Priority != null)
            {
                Select.ByText(PerorityDD, OrderLineData.Priority.ToString());
            }
            if (OrderLineData.ImpressionsPerDay != null)
            {
                ImpressionsField.SendKeys(OrderLineData.ImpressionsPerDay.ToString());
            }
            if (!string.IsNullOrEmpty(OrderLineData.AddGroupName))
            {
                Select.ByText(groupNameDD, OrderLineData.AddGroupName);
            }
            if (!OrderLineData.ProductSelectionManual)
            {
                Element.GetByValueFromList(AutoProductSelectionRadio, "1").Click();
            }
            else if (OrderLineData.ProductId_ManualSelection.Count > 0)
            {
                foreach (var item in OrderLineData.ProductId_ManualSelection)
                {
                    AddProductToOrderLineByID(item);
                }
            }

            if (OrderLineData.Status != null) {
                Element.ScrolTo(StatusDD);
                Select.ByText(StatusDD, OrderLineData.Status);
                Wait.MLSeconds(100);
            }
            return this;
        }

        private void SelectSearchTerms(string searchTerms)
        {
            SearchTermField.Clear();
            SearchTermField.SendKeys(searchTerms);
            Wait.MLSeconds(200);
            IWebElement Element = SearchTermsListParent.FindElements(By.TagName("li")).FirstOrDefault(e => e.Text == searchTerms);
            Element.Click();
            Wait.MLSeconds(200);
        }

        private void AddProductToOrderLineByID(string ProductId_ManualSelection)
        {
            Element.ScrolTo(AddProductBtn);
            AddProductBtn.Click();
            Wait.UntilDisply(By.Id("productSearchForm"));
            ProductSearchByDropDown.Click();
            Wait.MLSeconds(100);
            BYProductIdMenuItem.Click();
            ProductSearchField.SendKeys(ProductId_ManualSelection);
            Wait.MLSeconds(200);
            ProductSearchBtn.Click();
            By DispledElement=Wait.UntilDisply(new List<By>() { By.ClassName("toast-message"), By.ClassName("product-results") });

            if (DispledElement.ToString() =="By.ClassName[Contains]: product-results")
            {
                IList<IWebElement> pList = ProductsearchResult.FindElements(By.CssSelector("div[ng-click='addProductSelection(product)']"));
                IWebElement Productele= pList.FirstOrDefault(e => e.Text.Contains(ProductId_ManualSelection));
                Productele.Click();
                Wait.MLSeconds(500);
            }else
            {
                Logger.Log(LogingType.NoResult, string.Format("Serch product by Id : {0}", ProductId_ManualSelection));
            }

            ProductsearchModelCancelbtn.Click();
            Wait.MLSeconds(500);

        }
        public EditOrderLinePage CancelOrderLine()
        {
            CloseBtn.Click();
            return this;

        }
        public EditOrderLinePage SaveAndCopy()
        {


            Element.ScrolTo(OrderLineSaveBtn.FindElement(By.ClassName("dropdown-toggle")));
            Wait.MLSeconds(100);
            OrderLineSaveBtn.FindElement(By.ClassName("dropdown-toggle")).Click();
            Wait.MLSeconds(100);
            SaveAndCopyMenuItem.Click();
            return this;

        }
        public EditOrderLinePage SaveAndAdd()
        {
            Element.ScrolTo(OrderLineSaveBtn.FindElement(By.ClassName("dropdown-toggle")));
            Wait.MLSeconds(100);
            OrderLineSaveBtn.FindElement(By.ClassName("dropdown-toggle")).Click();
            Wait.MLSeconds(100);
            SaveAndAddMenuItem.Click();
            return this;

        }

        internal EditOrderLinePage VerifySaveAndCopy()
        {

            return this;
        }

        internal void VerfiyMultipleProducts(int p)
        {
            throw new NotImplementedException();
        }
    }
}
