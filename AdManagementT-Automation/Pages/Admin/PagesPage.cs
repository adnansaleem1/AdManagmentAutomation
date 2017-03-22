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

            if (msg.Any(e=>e== "Saved successfully"))
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
            IWebElement PageNameInput = FilterControlList[1].FindElement(By.TagName("input"));
            PageNameInput.Clear();
            PageNameInput.SendKeys(pageModel.GenratedPageName);
            Wait.AM_Loaging_ShowAndHide();

            return this;
        }

        internal void AddAddionalKeyWordsinPage(PageModel pageModel)
        {
            IWebElement ResultRow = this.GetMatchedRowFromFilteredData(pageModel);
            ResultRow.Click();
            Wait.UntilDisply(SubCategorySelect);
            Select.ByFreeKeyWords(KeyWordsControl, pageModel.AdditionalKeyWords);
            this.SavePage();
           // this.VerifySavePage();
            this.VerifyAddionalKeyWords(pageModel);

        }

        private void VerifyAddionalKeyWords(PageModel pageModel)
        {
            this.SearchPage(pageModel);
            IWebElement ResultRow = this.GetMatchedRowFromFilteredData(pageModel);
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
            return ResultRow.FindElements(By.TagName("td"))[3].Text.Split(',');

        }
        public void DeletePage(PageModel mypage)
        {
            this.SearchPage(mypage);
            IWebElement ResultRow = this.GetMatchedRowFromFilteredData(mypage);
            ResultRow.FindElement(By.LinkText("Delete")).Click();
            Wait.MLSeconds(100);
            Alert.ClickOK();

            Wait.AM_Loaging_ShowAndHide();
            this.VerifyDeletePage(mypage);
        }

        private void VerifyDeletePage(PageModel mypage)
        {
            this.SearchPage(mypage);
            try
            {

                IWebElement Row = this.GetMatchedRowFromFilteredData(mypage);
                Logger.Log(LogingType.TextCaseFail, "Unable To Delete Product");
                throw new Exception();
            }
            catch (Exception ex)
            {
                Logger.Log(LogingType.TextCaseFail, "Product Deleted Successfully");
            }

        }

        private IWebElement GetMatchedRowFromFilteredData(PageModel pageModel)
        {
            IList<IWebElement> list = this.GetResultSetRows();
            return list.First(e => e.FindElements(By.TagName("td"))[1].Text == pageModel.GenratedPageName.ToString());
        }

        private IList<IWebElement> GetResultSetRows()
        {
            return this.PagesResultGrid.FindElement(By.CssSelector("tr[ng-show='show_filter']")).FindElements(By.TagName("th"));
        }

        private IList<IWebElement> GetFilterControlList()
        {

            return this.PagesResultGrid.FindElement(By.TagName("tbody")).FindElements(By.CssSelector("tr[data-ng-repeat='page in $data']"));
        }

        internal void ActivePage(PageModel pageModel)
        {
            this.ChageStatusFilter("Inactive");
            this.SearchPage(pageModel);
            IWebElement Row = this.GetMatchedRowFromFilteredData(pageModel);
            Row.FindElement(By.LinkText("Make Active")).Click();
            Wait.MLSeconds(100);
            Alert.ClickOK();
            Wait.AM_Loaging_ShowAndHide();
            this.VerifyActivePage(pageModel);
        }

        private void VerifyActivePage(PageModel pageModel)
        {
            this.ChageStatusFilter("Active");
            this.SearchPage(pageModel);
            try
            {
                this.GetMatchedRowFromFilteredData(pageModel);

            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void ChageStatusFilter(string b)
        {
            IList<IWebElement> filterList = this.GetFilterControlList();
            IWebElement SelectEle = filterList[6].FindElement(By.TagName("select"));
            Select.ByText(SelectEle, b);
        }
    }
}
