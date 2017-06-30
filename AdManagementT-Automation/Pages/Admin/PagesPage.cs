using AddManagmentData.Model.Admin;
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
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdManagementT_Automation.Pages.Admin
{
   public class PagesPage
    {
        #region Create Page Controls
        [FindsBy(How = How.CssSelector, Using = "input[data-ng-model='pageModel.Category']")]
        private IWebElement CategoryField { get; set; }


        [FindsBy(How = How.Id, Using = "drpSubCategory")]
        private IWebElement SubCategorySelect { get; set; }


        [FindsBy(How = How.CssSelector, Using = "tags-input[ng-model='pageModel.SearchPhrase']")]
        private IWebElement KeyWordsControl { get; set; }


        [FindsBy(How = How.CssSelector, Using = "input[data-ng-model='pageModel.Name']")]
        private IWebElement PageField { get; set; }

        [FindsBy(How = How.CssSelector, Using = "button[data-ng-click='onGeneratePage()']")]
        private IWebElement GeneratePageBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = "button[data-ng-click='onSavePage(frmPage)']")]
        private IWebElement SaveBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = "button[data-ng-click='onCancel()']")]
        private IWebElement CancelBtn { get; set; }

        [FindsBy(How = How.ClassName, Using = "bootstrap-dialog-footer-buttons")]
        private IWebElement WarningDialog { get; set; }



        #endregion


        [FindsBy(How = How.CssSelector, Using = "button[data-ng-click='onCreatePage()']")]
        private IWebElement CreatePageBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = "table[data-ng-table='tableParams']")]
        private IWebElement PagesResultGrid { get; set; }

        internal PagesPage Navigate()
        {
            PagesRepo.AddTabs.Switch(AM_Sub_Admin.Pages);
            return this;
        }
        internal PagesPage OpenCreatePagePopup()
        {
            CreatePageBtn.Click();
            Wait.UntilDisply(SubCategorySelect);
            return this;
        }
        internal PagesPage FillCratePageForm(PageModel Page)
        {
            this.OpenCreatePagePopup();
            if (!string.IsNullOrEmpty(Page.Category))
            {
                Select.FromList(Page.Category, CategoryField);
            }
            if (!string.IsNullOrEmpty(Page.SubCategory))
            {
                Select.ByText(SubCategorySelect, Page.SubCategory);
            }
            Select.ByFreeKeyWords(KeyWordsControl, Page.KeyWordList);
            this.GeneratePage();
            Wait.Second(1);
            Page.GenratedPageName = PageField.Text;
            return this;
        }
        internal PagesPage SavePage()
        {
            SaveBtn.Click();
            this.VerifySavePage();
            return this;
        }
        private PagesPage VerifySavePage()
        {
            IList<string> msg = Wait.UntilToastMessageShow();

            if (msg.Any(e => e == "Saved successfully" || e == "Updated successfully"))
            {
                Logger.Log(LogingType.Success, "Page Saved successfully");
                Wait.AM_Loaging_ShowAndHide();
                return this;
            }
            else if (msg.Any(e=>e== "Page already exist. Please generate a valid Page"))
            {
                Logger.Log(LogingType.Error, "Page already exist. Please generate a valid Page");
                this.CancelnewPage();
                throw new Exception();
            }
            else
            {
                Logger.Log(LogingType.Error, "unable to genrate page Error:Unknown");
                this.CancelnewPage();
                throw new Exception();
            }
        }

        private void CancelnewPage()
        {
            CancelBtn.Click();
            Wait.UntilDisply(WarningDialog);
            Modal.ClickYes(WarningDialog);
            Wait.UntilClickAble(CreatePageBtn);
        }

        private void GeneratePage()
        {
            GeneratePageBtn.Click();
            Wait.UntilTextFill(PageField);
        }

        internal PagesPage SearchPage(PageModel pageModel)
        {
            IList<IWebElement> FilterControlList = this.GetFilterControlList();
            if (pageModel.GenratedPageName!=null)
            {
                IWebElement PageNameInput = FilterControlList[1].FindElement(By.TagName("input"));
                PageNameInput.Clear();
                PageNameInput.SendKeys(pageModel.GenratedPageName); 
            }
            if (pageModel.Category != null) {
                IWebElement CatInput = FilterControlList[0].FindElement(By.TagName("input"));
                CatInput.Clear();
                CatInput.SendKeys(pageModel.Category); 
            }
            Wait.AM_Loaging_ShowAndHide();
            Wait.MLSeconds(300);
            return this;
        }

        internal void AddAddionalKeyWordsinPage(PageModel pageModel)
        {
            var jList = this.GetResultSetRows();
            IWebElement ResultRow = jList[0];
            ResultRow.FindElements(By.TagName("td"))[1].Click();
            Wait.UntilDisply(SubCategorySelect);
            Select.ByFreeKeyWords(KeyWordsControl, pageModel.AdditionalKeyWords);
            this.SavePage();
            this.VerifyAddionalKeyWords(pageModel);

        }

        private void VerifyAddionalKeyWords(PageModel pageModel)
        {
            //this.SearchPage(pageModel);
            IWebElement ResultRow = this.GetResultSetRows()[0];//this.GetMatchedRowFromFilteredData(pageModel);
            IList<string> KeyWordsList = this.GetKeyWordsListFromPage(ResultRow);
            var Result = KeyWordsList.Where(e => pageModel.AdditionalKeyWords.Any(x => x == e)).ToList();
            if (Result.Count == pageModel.AdditionalKeyWords.Count)
            {
                Logger.Log(LogingType.TestCasePass, "Keywords Verified in Result");
            }
            else
            {
                Logger.Log(LogingType.TextCaseFail, "Keywords not Verified in Result");
                throw new Exception();
            }
        }

        private IList<string> GetKeyWordsListFromPage(IWebElement ResultRow)
        {
            return ResultRow.FindElements(By.TagName("td"))[2].Text.Split(',');

        }
        public void DeletePage(PageModel mypage)
        {
            //this.SearchPage(mypage);
            IWebElement ResultRow = this.GetResultSetRows()[0];
            ResultRow.FindElement(By.LinkText("Delete")).Click();
            Wait.MLSeconds(100);
            Modal.CommonclickYes();
            this.VerifyDeletePage(mypage);
        }

        private void VerifyDeletePage(PageModel mypage)
        {

            IList<string> msg = Wait.UntilToastMessageShow();

            if (msg.Any(e => e == "Deleted successfully"))
            {
                Logger.Log(LogingType.Success, "Deleted successfully");
                Wait.AM_Loaging_ShowAndHide();
            }
            else
            {
                Logger.Log(LogingType.TextCaseFail, "unable to delete product");
                throw new Exception();
            }
        }

        private IWebElement GetMatchedRowFromFilteredData(PageModel pageModel)
        {
            IList<IWebElement> list = this.GetResultSetRows();
            return list.First(e => e.FindElements(By.TagName("td"))[1].Text == pageModel.GenratedPageName.ToString());
        }

        private IList<IWebElement> GetResultSetRows()
        {
            return this.PagesResultGrid.FindElements(By.CssSelector("tr[data-ng-repeat='page in $data']"));
        }

        private IList<IWebElement> GetFilterControlList()
        {

            return this.PagesResultGrid.FindElement(By.CssSelector("thead")).FindElements(By.TagName("tr"))[1].FindElements(By.TagName("th"));
        }

        internal void ActivePage(PageModel pageModel)
        {
            this.ChageStatusFilter("Inactive");
            IWebElement Row = this.GetResultSetRows()[0];
            Row.FindElement(By.LinkText("Make Active")).Click();
            Wait.MLSeconds(100);
            Modal.CommonclickYes();
            this.VerifyActivePage(pageModel);
        }

        private void VerifyActivePage(PageModel pageModel)
        {

            IList<string> msg = Wait.UntilToastMessageShow();

            if (msg.Any(e => e == "Activated successfully"))
            {
                Logger.Log(LogingType.Success, "Page Active successfully");
                Wait.AM_Loaging_ShowAndHide();
            }
            else {
                Assert.Fail("Unable to activate the Page");
            }
        }

        private void ChageStatusFilter(string b)
        {
            IList<IWebElement> filterList = this.GetFilterControlList();
            IWebElement SelectEle = filterList[6].FindElement(By.TagName("select"));
            Select.ByText(SelectEle, b);
            Wait.AM_Loaging_ShowAndHide();
        }
        public void VerifyAdminTabNotThere()
        {
            Element.NotExist(By.LinkText("Admin"));
        }

        internal PagesPage SortLatestRecords()
        {
            var Row=this.GetSortRow()[5];
            Element.SetSortOrder(Row, SortOrder.Descending);
            return this;
        }

        private IList<IWebElement> GetSortRow()
        {
            return this.PagesResultGrid.FindElement(By.TagName("thead")).FindElements(By.TagName("tr"))[0].FindElements(By.TagName("th"));

        }

        internal void DeleteAddionalKeyWordsinPage(PageModel Data)
        {
            var jList = this.GetResultSetRows();
            IWebElement ResultRow = jList[0];
            ResultRow.FindElements(By.TagName("td"))[1].Click();
            Wait.UntilDisply(SubCategorySelect);
            KeyWordsControl.FindElements(By.TagName("li"))[KeyWordsControl.FindElements(By.TagName("li")).Count-1].FindElement(By.TagName("a")).Click();
            Wait.MLSeconds(1000);
            //Select.ByFreeKeyWords(KeyWordsControl, Data.AdditionalKeyWords);
            this.SavePage();
            //SaveBtn.Click();
            //this.VerifyAddionalKeyWords(Data);
        }

    }
}
