using AddManagmentData.Model;
using AdManagementT_Automation.Base;
using AdManagementT_Automation.Ref;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using SeleniumExtension.Controls;
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

        public AdvertisementPage Navigate() {
            PagesRepo.AddTabs.Switch(AM_MainTab.Advertisments);
            return this;
        }

        public AdvertisementPage FillSearchParamitters(AdvertisementSearchModel Data)
        {
            MemberId.SendKeys(Data.MemberID==null?"":Data.MemberID);
            this.SelectFromMultipleControl(Data.Positions, PositionControl);
            this.TagBasedInput(Data.SearchTerms, SearchTermsControl);
            Element.syncCheckBox(Data.IncludeSubCat, IncludeSubCatInput);
            this.SelectFromMultipleControl(Data.SalesReps, SalesRepsControl);
            this.SelectFromMultipleControl(Data.Statuses, AdStatusControl);
            Select.ByText( YearDD,Data.Year.ToString());
            Select.ByText(MonthDD, Data.Month.ToString());
            this.SelectFromMultipleControl(Data.Rates, RateControl);
            FilterTextfield.SendKeys(Data.SearchField==null?"":Data.SearchField);
            Searchbtn.Click();
            Wait.AM_Loaging_ShowAndHide();
            return this;
        }



        private void SelectFromMultipleControl(IList<string> list, IWebElement Control)
        {
            try
            {

                if (list.Count > 0)
                {
                    Element.ScrolTo(Control);
                    Control.FindElement(By.ClassName("dropdown-toggle")).Click();
                    Wait.MLSeconds(200);
                    IWebElement ListItemele;
                    foreach (var item in list)
                    {
                        ListItemele = Control.FindElement(By.LinkText(item.ToString()));
                        Element.ScrolTo(ListItemele);
                        ListItemele.Click();
                        Wait.MLSeconds(100);
                    }
                    Control.FindElement(By.XPath("..")).Click();
                }
            }
            catch (Exception)
            {

                //throw;
            }
        }

        private void TagBasedInput(IList<string> list, IWebElement Control)
        {
            Element.ScrolTo(Control);
            foreach (var item in list) {
                Control.FindElement(By.TagName("input")).SendKeys(item);
                Wait.UntilDisply(By.ClassName("autocomplete"));
                Control.FindElement(By.TagName("input")).SendKeys(Keys.Enter);
                Wait.MLSeconds(200);
            }

        }

        private void SelectFromMultipleControl(IList<int> list, IWebElement Control)
        {
            try
            {

                if (list.Count > 0)
                {
                    Element.ScrolTo(Control);
                    Control.FindElement(By.ClassName("dropdown-toggle")).Click();
                    Wait.MLSeconds(200);
                    IWebElement ListItemele;
                    foreach (var item in list)
                    {
                       ListItemele= Control.FindElement(By.LinkText(item.ToString()));
                       Element.ScrolTo(ListItemele);
                       ListItemele.Click();
                       Wait.MLSeconds(100);
                    }
                    Control.FindElement(By.XPath("..")).Click();
                }
            }
            catch (Exception)
            {
                
                //throw;
            }
            
        }




    }
}
