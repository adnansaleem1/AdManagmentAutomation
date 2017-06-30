using AddManagmentData.Model;
using AdManagementT_Automation.Base;
using AdManagementT_Automation.Ref;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using SeleniumExtension.Controls;
using SeleniumExtension.Ref;
using SeleniumExtension.Utilties;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        [FindsBy(How = How.CssSelector, Using = "input[data-ng-model='orderLine.EndDate']")]
        private IWebElement EndDate { get; set; }

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

        [FindsBy(How = How.CssSelector, Using = "select[data-ng-model='orderLine.Group.Id']")]
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

        [FindsBy(How = How.LinkText, Using = "Next Order Line")]
        private IWebElement NextOrderLine { get; set; }

        [FindsBy(How = How.LinkText, Using = "Previous Order Line")]
        private IWebElement PreviousOrderLine { get; set; }

        [FindsBy(How = How.LinkText, Using = "View All Order Lines")]
        private IWebElement ViewAllOrderLines { get; set; }

        [FindsBy(How = How.CssSelector, Using = "li[data-ng-repeat='product in orderLine.Products track by product.Id']")]
        private IList<IWebElement> AutoSelectionList { get; set; }

        #endregion


        public EditOrderLinePage  FillOrderLine_Admin(OrderLineModel OrderLineData)
        {
            Wait.UntilDisply(PositionDD); 
            if (!string.IsNullOrEmpty(OrderLineData.AddGroupName))
            {
                Select.ByText(ProductGroupDD, "ESP Mobile");
                Wait.MLSeconds(200);
                Select.ByText(AddTypeDD, "PFP");
                Wait.MLSeconds(1000);
            }
            Select.ByText(ProductGroupDD, OrderLineData.ProductGroup);
            Wait.MLSeconds(200);
            Select.ByText(AddTypeDD, OrderLineData.AddType);
            Wait.MLSeconds(1000);

            if (OrderLineData.Position != null && OrderLineData.Position != 0)
            {
                Select.ByText(PositionDD, OrderLineData.Position.ToString());
                Wait.MLSeconds(200);
            }
            if (OrderLineData.SearchTerm != null && OrderLineData.SearchTerm != "")
            {
                SelectSearchTerms(OrderLineData.SearchTerm);
            }
            if (OrderLineData.EndDate != null)
            {
                EndDate.SendKeys(OrderLineData.EndDate.Value.ToString("M/d/yyyy"));
            }
            ProductInfoField.Click();
            Wait.MLSeconds(200);
            Select.ByText(DeliveryPrefDD, OrderLineData.DeliveryPreferences);
            Wait.MLSeconds(100);
            ProductInfoField.Clear();
            ProductInfoField.SendKeys(OrderLineData.ProductInformation);
            Wait.MLSeconds(100);
            if (OrderLineData.Impressions != null)
            {
                ImpressionsField.Clear();
                ImpressionsField.SendKeys(OrderLineData.Impressions.ToString());
                Wait.MLSeconds(100);
            }
            if (OrderLineData.Cost != null && OrderLineData.Cost != 0.0)
            {
                CostField.Clear();
                CostField.SendKeys(OrderLineData.Cost.ToString());
                Wait.MLSeconds(100);
            }
            else
            {
                Wait.MLSeconds(500);
                try
                {
                    ProductInfoField.Click();
                }
                catch (Exception)
                {
                }
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
                try
                {
                    Select.ByText(groupNameDD, OrderLineData.AddGroupName);
                }
                catch (Exception ex)
                {
                    Logger.Log(LogingType.TextCaseFail, ex.ToString());
                    this.CloseBtn.Click();
                    Wait.MLSeconds(500);
                    Modal.DirtyclickYes();
                    Wait.MLSeconds(100);
                    throw new Exception(ex.Message);
                }
            }
            if (OrderLineData.ProductSelectionManual == false && OrderLineData.ProductSelectionManual != null)
            {
                Element.ScrolTo(StatusDD);
                Element.GetByValueFromList(AutoProductSelectionRadio, "1").Click();
                Wait.AM_Loaging_ShowAndHide();
            }
            else if (OrderLineData.ProductId_ManualSelection != null && OrderLineData.ProductId_ManualSelection.Count > 0)
            {
                foreach (var item in OrderLineData.ProductId_ManualSelection)
                {
                    AddProductToOrderLineByID(item);
                }
            }

            if (OrderLineData.Status != null)
            {
                Element.ScrolTo(StatusDD);
                Select.ByText(StatusDD, OrderLineData.Status);
                Wait.MLSeconds(100);
            }
            return this;
        }
        public EditOrderLinePage FillOrderLine_Sales(OrderLineModel OrderLineData)
        {
            Wait.UntilDisply(PositionDD);
            Select.ByText(ProductGroupDD, OrderLineData.ProductGroup);
            Wait.MLSeconds(200);
            Select.ByText(AddTypeDD, OrderLineData.AddType);
            Wait.MLSeconds(1000);
            if (OrderLineData.Position != null && OrderLineData.Position != 0)
            {
                Select.ByText(PositionDD, OrderLineData.Position.ToString());
                Wait.MLSeconds(200);
            }
            if (OrderLineData.SearchTerm != null && OrderLineData.SearchTerm != "")
            {
                SelectSearchTerms(OrderLineData.SearchTerm);
            }
            if (OrderLineData.EndDate != null)
            {
                EndDate.SendKeys(OrderLineData.EndDate.Value.ToString("M/d/yyyy"));
            }
            ProductInfoField.Click();
            Wait.MLSeconds(200);
            Select.ByText(DeliveryPrefDD, OrderLineData.DeliveryPreferences);
            Wait.MLSeconds(100);
            ProductInfoField.Clear();
            ProductInfoField.SendKeys(OrderLineData.ProductInformation);
            Wait.MLSeconds(100);
            if (OrderLineData.Impressions != null)
            {
                ImpressionsField.Clear();
                ImpressionsField.SendKeys(OrderLineData.Impressions.ToString());
                Wait.MLSeconds(100);
            }
            if (OrderLineData.Cost != null && OrderLineData.Cost != 0.0)
            {
                CostField.Clear();
                CostField.SendKeys(OrderLineData.Cost.ToString());
                Wait.MLSeconds(100);
            }
            else
            {
                Wait.MLSeconds(300);
                ProductInfoField.Click();
                Wait.MLSeconds(200);
            }
            //if (OrderLineData.RateEnable)
            //{
            //    RateCheckBox.Click();
            //    Wait.MLSeconds(200);
            //    RateField.Clear();
            //    RateField.SendKeys(OrderLineData.Rate.ToString());
            //}
            //if (OrderLineData.GeoTargetEnable)
            //{
            //    GeoTaggingEnable.Click();
            //    Wait.MLSeconds(200);
            //    GeoCountryParent.FindElement(By.LinkText("input")).SendKeys(OrderLineData.Countries);
            //    GeoTargetStatesParent.FindElement(By.TagName("input")).SendKeys(OrderLineData.States);
            //}
            //if (!OrderLineData.KeyWordsEnable)
            //{
            //    KeyWordCheckBox.Click();
            //    Wait.MLSeconds(100);
            //}
            //if (!OrderLineData.CatogoriesEnable)
            //{
            //    IsCatagoryTargetCheckBox.Click();
            //    Wait.MLSeconds(100);
            //}
            //if (!OrderLineData.SubsitutionsAllow)
            //{
            //    Element.GetByValueFromList(AllowSubstitutionsRadios, "0").Click();
            //}
            //if (!OrderLineData.DisplayMultipleAddsAllow)
            //{
            //    Element.GetByValueFromList(DisplayMultipleAdsRadios, "0").Click();
            //}
            //if (!OrderLineData.SearchLeadingTextAllow)
            //{
            //    Element.GetByValueFromList(AllowSearchLeadingTextRadios, "0").Click();
            //}
            //if (OrderLineData.Priority != null)
            //{
            //    Select.ByText(PerorityDD, OrderLineData.Priority.ToString());
            //}
            //if (OrderLineData.ImpressionsPerDay != null)
            //{
            //    ImpressionsField.SendKeys(OrderLineData.ImpressionsPerDay.ToString());
            //}
            //if (!string.IsNullOrEmpty(OrderLineData.AddGroupName))
            //{
            //    Select.ByText(groupNameDD, OrderLineData.AddGroupName);
            //}
            //if (OrderLineData.ProductSelectionManual == false && OrderLineData.ProductSelectionManual != null)
            //{
            //    Element.GetByValueFromList(AutoProductSelectionRadio, "1").Click();
            //}
            //else if (OrderLineData.ProductId_ManualSelection != null && OrderLineData.ProductId_ManualSelection.Count > 0)
            //{
            //    foreach (var item in OrderLineData.ProductId_ManualSelection)
            //    {
            //        AddProductToOrderLineByID(item);
            //    }
            //}

            //if (OrderLineData.Status != null)
            //{
            //    Element.ScrolTo(StatusDD);
            //    Select.ByText(StatusDD, OrderLineData.Status);
            //    Wait.MLSeconds(100);
            //}
            return this;
        }

        private void SelectSearchTerms(string searchTerms)
        {
            SearchTermField.Clear();
            SearchTermField.SendKeys(searchTerms);
            Wait.UntilDisply(SearchTermsListParent);
            Wait.MLSeconds(100);
            IWebElement Element = SearchTermsListParent.FindElements(By.TagName("li")).FirstOrDefault(e => e.Text == searchTerms);
            Element.Click();
            Wait.MLSeconds(200);
        }

        private void AddProductToOrderLineByID(string ProductId_ManualSelection)
        {
            Element.ScrolTo(AddProductBtn);
            AddProductBtn.Click();
            Wait.UntilAllToastMessageHide();
            Wait.UntilDisply(By.Id("productSearchForm"));
            ProductSearchByDropDown.Click();
            Wait.MLSeconds(200);
            BYProductIdMenuItem.Click();
            ProductSearchField.SendKeys(ProductId_ManualSelection);
            Wait.MLSeconds(200);
            ProductSearchBtn.Click();
            By DispledElement = Wait.UntilDisply(new List<By>() { By.ClassName("toast-message"), By.ClassName("product-results") });
            Wait.MLSeconds(500);
            if (DispledElement.ToString() == "By.ClassName[Contains]: product-results")
            {
                IList<IWebElement> pList = ProductsearchResult.FindElements(By.CssSelector("div[ng-click='addProductSelection(product)']"));
                IWebElement Productele = pList.FirstOrDefault(e => e.Text.Contains(ProductId_ManualSelection));
                Productele.Click();
                Wait.MLSeconds(500);
            }
            else
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
        public EditOrderLinePage Save()
        {
            
            Wait.MLSeconds(200);
            Wait.UntilClickAble(OrderLineSaveBtn);
            Wait.MLSeconds(200);
            Wait.UntilLoading();
            Wait.MLSeconds(800);
            Element.ScrolTo(OrderLineSaveBtn);
            OrderLineSaveBtn.Click();
            this.verfiySave();
            return this;
        }
        //private void verfiySaveWithException()
        //{
        //    IList<string> result = Wait.UntilToastMessageShow();

        //    if (result.Any(e => e == "Saved successfully" || e == "Saved and added successfully" || e == "Saved and copied successfully"))
        //    {
        //        Logger.Log(LogingType.TestCasePass, "Product Save Successfully");

        //    }
        //    else if (result.Any(e => e == "Updated successfully"))
        //    {

        //        Logger.Log(LogingType.TestCasePass, "Product Updated Successfully");

        //    }
        //    else if (result.Any(e => e == "Save error" || e == "Cannot save orderline as Active or Ordered because Order is Completed." || e.Contains("Another supplier has Standard ad beginning") || e.Contains("Another supplier has Standard ad beginning")))
        //    {
        //        this.CloseBtn.Click();
        //        Wait.MLSeconds(500);
        //        Modal.DirtyclickYes();
        //        Logger.Log(LogingType.TextCaseFail, result.ToString());
        //        throw new Exception(String.Join(",",result.ToArray()));

        //    }
        //    else if (result.Any(e => e.Contains("End Date is set to today for Completed status") || e.Contains("Product is not relevant to the ad") || e.Contains("Remaining inventory not available for this search term")))
        //    {
        //        //  Logger.Log(LogingType.TextCaseFail, result[0]);
        //        //  throw new Exception(result.ToString());
        //        this.verfiySave();
        //    }
        //    else if (result.Count == 0)
        //    {
        //        this.Save();
        //        //this.verfiySave(true);

        //    }
        //    else
        //    {
        //        Logger.Log(LogingType.TextCaseFail, result.ToString());
        //        throw new Exception(result.ToString());
        //        this.CloseBtn.Click();
        //        Wait.MLSeconds(500);
        //        Modal.DirtyclickYes();
        //    }
        //}
        private void verfiySave()
        {
            IList<string> result = Wait.UntilToastMessageShow();

            if (result.Any(e => e == "Saved successfully" || e == "Saved and added successfully" || e == "Saved and copied successfully"))
            {
                Logger.Log(LogingType.TestCasePass, "Product Save Successfully");

            }
            else if (result.Any(e => e == "Updated successfully"))
            {

                Logger.Log(LogingType.TestCasePass, "Product Updated Successfully");

            }
            else if (result.Any(e => e == "Save error" || e == "Cannot save orderline as Active or Ordered because Order is Completed." || e.Contains("Another supplier has Standard ad beginning") || e.Contains("Another supplier has Standard ad beginning")))
            {
                Logger.Log(LogingType.TextCaseFail, result.ToString());
                this.CloseBtn.Click();
                Wait.MLSeconds(500);
                Modal.DirtyclickYes();
                Wait.MLSeconds(100);
                throw new Exception(String.Join(",", result.ToArray()));
            }
            else if (result.Any(e => e.Contains("End Date is set to today for Completed status") || e.Contains("Product is not relevant to the ad") || e.Contains("Remaining inventory not available for this search term")))
            {
                //  Logger.Log(LogingType.TextCaseFail, result[0]);
                //  throw new Exception(result.ToString());
                this.verfiySave();
            }
            else
            {
                Logger.Log(LogingType.TextCaseFail, result.ToString());
                Wait.UntilLoading();
                Element.ScrolTo(this.CloseBtn);
                this.CloseBtn.Click();
                Wait.MLSeconds(500);
                Modal.DirtyclickYes();
                throw new Exception(String.Join(",", result.ToArray()));
            }
        }
        internal EditOrderLinePage VerifySaveAndCopy()
        {
            this.verfiySave();
            return this;

        }
        internal EditOrderLinePage VerifySaveAndAdd()
        {

            this.verfiySave();
            return this;
            Wait.UntilLoading();
            Element.ScrolToTop();
        }
        internal void VerfiyMultipleProducts(int p)
        {
            throw new NotImplementedException();
        }
        internal void GoBackOrder()
        {
            Wait.MLSeconds(500);
            Wait.UntilLoading();
            PagesRepo.AddTabs.Switch(AM_Sub_Insertion_Orders.Edit_Order);
        }

        internal void Update(OrderLineModel data)
        {
            if (data.Status != null)
            {
                Element.ScrolTo(StatusDD);
                Select.ByText(StatusDD, data.Status);
                Wait.MLSeconds(100);
            }
            this.Save();
        }

        internal void Next()
        {
            NextOrderLine.Click();
            Wait.AM_Loaging_ShowAndHide();
        }

        internal void Previous()
        {
            PreviousOrderLine.Click();
            Wait.AM_Loaging_ShowAndHide();
        }

        internal void GoBackOrderLineByLink()
        {
            ViewAllOrderLines.Click();
            Wait.Second(1);
            this.AcceptConfirmIFExists();
            Wait.AM_Loaging_ShowAndHide();
        }

        private void AcceptConfirmIFExists()
        {
           if(Element.Dispaly(By.ClassName("dirtycheck-dialog")))
               Modal.DirtyclickYes();
        }

        internal EditOrderLinePage VerifyAutoSelect()
        {

            if (AutoSelectionList == null || AutoSelectionList.Count <= 0)
            {
                this.discardChanges();
                Assert.Fail("Auto Selection List was Empty.");
            }
            discardChanges();

            return this;
        }

        private void discardChanges()
        {
            this.CloseBtn.Click();
            Wait.MLSeconds(500);
            Modal.DirtyclickYes();
            Wait.AM_Loaging_ShowAndHide();
        }
    }
}
