using AddManagmentData.Model;
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

namespace AdManagementT_Automation.Pages.Inventory
{
    public class WaitListPage
    {
        public string Email = "asaleem@asicentral.com";
        IWebDriver driver = SDriver.Browser;

        [FindsBy(How = How.CssSelector, Using = "input[data-ng-model='waitList.MemberId']")]
        private IWebElement form_MemberIdField { get; set; }

        [FindsBy(How = How.Id, Using = "drpApplication")]
        private IWebElement form_ProductgroupDD { get; set; }

        [FindsBy(How = How.CssSelector, Using = "table[data-ng-table='tableParams']")]
        private IWebElement DataGrid { get; set; }

        [FindsBy(How = How.Id, Using = "drpAdType")]
        private IWebElement form_AdTypeDD { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[data-ng-model='waitList.Page.DisplayName']")]
        private IWebElement form_SearchTermsField { get; set; }

        [FindsBy(How = How.Id, Using = "drpPosition")]
        private IWebElement form_PositionDD { get; set; }

        [FindsBy(How = How.Id, Using = "drpDeliveryPref")]
        private IWebElement form_DeliveryPrefDD { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[data-ng-model='waitList.Cost']")]
        private IWebElement form_CostField { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[data-ng-model='waitList.Product']")]
        private IWebElement form_DefaultProduct { get; set; }

        [FindsBy(How = How.Id, Using = "drpSalesRep")]
        private IWebElement form_SaledRepDD { get; set; }

        [FindsBy(How = How.Id, Using = "drpCoordinator")]
        private IWebElement form_coordinatorDD { get; set; }

        [FindsBy(How = How.CssSelector, Using = "button[data-ng-click='createWaitList()']")]
        private IWebElement AddnewWaitListBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = "button[data-ng-click='saveWaitList(frmWaitList)']")]
        private IWebElement SavenewWaitListBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = "ul[move-in-progress='moveInProgress']")]
        private IWebElement SearchTermsListParent { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[data-ng-model='Notification.SalesRepEmail']")]
        private IWebElement Notification_EmailField { get; set; }

        [FindsBy(How = How.CssSelector, Using = "button[data-ng-click='sendNotification()']")]
        private IWebElement Notification_SendBtn { get; set; }
        
        [FindsBy(How = How.CssSelector, Using = "button[data-ng-click='cancelWaitList()']")]
        private IWebElement CancelWaitListBtn { get; set; }

            [FindsBy(How = How.ClassName, Using = "dirtycheck-dialog")]
        private IWebElement dirtyCheckDialog { get; set; }
        

        public WaitListPage OpenNewWaitListPage()
        {
            AddnewWaitListBtn.Click();
            Wait.UntilDisply(form_AdTypeDD);
            return this;
        }
        public WaitListPage FillNewWaitList(WaitListModel Data)
        {
            if (Data.MemberId != "")
            {
                Select.FromList(Data.MemberId, form_MemberIdField);
                Wait.UntilLoading();
                Wait.MLSeconds(1000);
            } if (!string.IsNullOrEmpty(Data.ProductGroup))
            {
                Select.ByText(form_ProductgroupDD, Data.ProductGroup);
            } if (!string.IsNullOrEmpty(Data.AddType))
            {
                Select.ByText(form_AdTypeDD, Data.AddType);
            }
            if (!string.IsNullOrEmpty(Data.SearchTerms))
            {
                Select.FromList(Data.SearchTerms, form_SearchTermsField);
            } if (Data.Position > 0)
            {
                Select.ByText(form_PositionDD, Data.Position.ToString());
            } if (!string.IsNullOrEmpty(Data.DeliveryPrefrnces))
            {
                Select.ByText(form_DeliveryPrefDD, Data.DeliveryPrefrnces);
            } if (Data.Cost > 0)
            {
                form_CostField.Clear();
                form_CostField.SendKeys(Data.Cost.ToString());
            } if (!string.IsNullOrEmpty(Data.defaultProduct))
            {
                Element.ScrolTo(form_DefaultProduct);
                form_DefaultProduct.Clear();
                form_DefaultProduct.SendKeys(Data.defaultProduct);
            } if (!string.IsNullOrEmpty(Data.SalesRep))
            {
                Select.ByText(form_SaledRepDD, Data.SalesRep);
            } if (!string.IsNullOrEmpty(Data.Coordinator))
            {
                Select.ByText(form_coordinatorDD, Data.Coordinator);
            }
            Wait.MLSeconds(200);
            if (!string.IsNullOrEmpty(Data.AddType))
            {
                Select.ByText(form_AdTypeDD, Data.AddType);
            }
            return this;
        }
        private void SelectSearchTerms(string searchTerms)
        {
            form_SearchTermsField.Clear();
            form_SearchTermsField.SendKeys(searchTerms);
            Wait.UntilDisply(SearchTermsListParent);
            Wait.MLSeconds(100);
            IWebElement Element = SearchTermsListParent.FindElements(By.TagName("li")).FirstOrDefault(e => e.Text == searchTerms);
            Element.Click();
            Wait.MLSeconds(200);
        }
        public void VerifySaveNewAdd()
        {
            SavenewWaitListBtn.Click();
            IList<string> result = Wait.UntilToastMessageShow();

            if (result.Any(e => e == "Saved successfully" || e == "Saved and added successfully"))
            {
                Logger.Log(LogingType.TestCasePass, "Wait list Save Successfully");

            }
            else
            {
                this.CancelWaitList();
                Logger.Log(LogingType.TextCaseFail, result.ToString());
                throw new Exception(result.ToString());
            }
        }

        private void CancelWaitList()
        {
            CancelWaitListBtn.Click();
            Wait.MLSeconds(200);
            Modal.ClickYes(dirtyCheckDialog);
            Wait.MLSeconds(1000);
        }
        private void filterWaitList(WaitListModel Data)
        {
            IList<IWebElement> FilterList = DataGrid.FindElement(By.ClassName("ng-table-filters")).FindElements(By.TagName("input"));
            FilterList[0].Clear();
            FilterList[0].SendKeys(Data.MemberId);
            //if (Data.defaultProduct != null) {
            //    FilterList[8].Clear();
            //    FilterList[8].SendKeys(Data.defaultProduct);
            //}
            Wait.AM_Loaging_ShowAndHide();
        }
        public void EditWaitList(WaitListModel Data)
        {
            this.filterWaitList(Data);
            IWebElement aElement = this.GetRowFromFilter();
            aElement.Click();
           // Wait.UntilDisply(form_ProductgroupDD);
            Wait.AM_Loaging_ShowAndHide();
            if (!string.IsNullOrEmpty(Data.defaultProduct))
            {
                Element.ScrolTo(form_DefaultProduct);
                form_DefaultProduct.Clear();
                form_DefaultProduct.SendKeys(Data.defaultProduct);
            }
            SavenewWaitListBtn.Click();
            this.VerifyUpdate();
        }
        public void VerifyUpdate()
        {
            //SavenewWaitListBtn.Click();
            IList<string> result = Wait.UntilToastMessageShow();

            if (result.Any(e => e == "Saved successfully" || e == "Updated successfully"))
            {
                Logger.Log(LogingType.TestCasePass, "Wait list Save Successfully");

            }
            else
            {
                Logger.Log(LogingType.TextCaseFail, result.ToString());
                throw new Exception(result.ToString());
            }
        }
        private IWebElement GetRowFromFilter()
        {
            IList<IWebElement> Rows = DataGrid.FindElements(By.CssSelector("tr[data-ng-repeat='row in $data']"));
            if (Rows.Count > 0)
            {
                return Rows[0];
            }
            else
            {
                throw new Exception("No Result found in Wait List Grid.");
            }

        }
        internal WaitListPage Navigate()
        {
            PagesRepo.AddTabs.Switch(AM_Sub_Inventory.Wait_List);
            return this;
        }

        public void Notify(WaitListModel Data) {
            this.filterWaitList(Data);
            IWebElement aElement = this.GetRowFromFilter();
            aElement.FindElement(By.LinkText("Notify")).Click();
            Wait.UntilDisply(Notification_SendBtn);
            Notification_EmailField.Clear();
            Notification_EmailField.SendKeys(this.Email);
            Notification_SendBtn.Click();
            IList<string> result = Wait.UntilToastMessageShow();

            if (result.Any(e => e == "Message sent successfully"))
            {
                Logger.Log(LogingType.TestCasePass, "Wait list Notify Save Successfully");

            }
            else
            {
                Logger.Log(LogingType.TextCaseFail, result.ToString());
                throw new Exception(result.ToString());
            }

        }
    }
}
