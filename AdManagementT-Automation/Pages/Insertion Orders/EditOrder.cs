using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using SeleniumExtension.Driver;
using SeleniumExtension.Utilties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumExtension.Controls;
using AddManagmentData.Model;
using SeleniumExtension.Ref;

namespace AdManagementT_Automation.Pages.Insertion_Orders
{
    public class EditOrderPage
    {
        IWebDriver driver = SDriver.Browser;

        [FindsBy(How = How.XPath, Using = "/html/body/div[1]/div[3]/form/div/div[9]/div[2]/div/div/button[2]")]
        private IWebElement AddOrderLineBtn { get; set; }

        [FindsBy(How = How.LinkText, Using = "PFP")]
        private IWebElement PFP { get; set; }

        [FindsBy(How = How.LinkText, Using = "Banner")]
        private IWebElement Banner { get; set; }

        [FindsBy(How = How.CssSelector, Using = "table[data-ng-table='tableOrderLinesParams']")]
        private IWebElement OrderLineGrid { get; set; }

        [FindsBy(How = How.CssSelector, Using = "div[data-ng-show='isEditMode && order.OrderStatus.Name == \\'Active\\'']")]
        private IWebElement ExportBtn { get; set; }

        [FindsBy(How = How.ClassName, Using = "results-count")]
        private IWebElement ResultCountDiv { get; set; }

        [FindsBy(How = How.CssSelector, Using = "div[data-ng-modal='TotalCost']")]
        private IWebElement ActiveTotal { get; set; }

        [FindsBy(How = How.ClassName, Using = "results-bar")]
        private IWebElement ResultBar { get; set; }
        
        #region Ad Group Elements

        [FindsBy(How = How.CssSelector, Using = "button[data-ng-click='showGroups = !showGroups; showNotes = false']")]
        private IWebElement AdGroupBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = "button[data-ng-click='addGroup()']")]
        private IWebElement CreateAddGroupBtn { get; set; }

        [FindsBy(How = How.Id, Using = "drpApplication")]
        private IWebElement AdGroupForm_ApplicationDD { get; set; }

        [FindsBy(How = How.Id, Using = "drpAdType")]
        private IWebElement AdGroupForm_AdTypeDD { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[data-ng-model='adGroup.Name']")]
        private IWebElement AdGroupForm_GroupNameField { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[data-ng-model='adGroup.Impressions']")]
        private IWebElement AdGroupForm_ImpressionsField { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[data-ng-model='adGroup.TargetAmount']")]
        private IWebElement AdGroupForm_BudgetField { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[data-ng-model='adGroup.EndDate']")]
        private IWebElement AdGroupForm_EndDateField { get; set; }

        [FindsBy(How = How.Id, Using = "adgroups")]
        private IWebElement AdGroupPanel { get; set; }

        [FindsBy(How = How.CssSelector, Using = "button[data-ng-click='saveGroup(frmAdGroup, adGroup)']")]
        private IWebElement AdGroupForm_SaveBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = "button[data-ng-click='cancelGroup()']")]
        private IWebElement AdGroupForm_CancelBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = "ng-form[name='groupForm']")]
        private IWebElement AddGroupForm { get; set; }
        #endregion
        #region Note Elemets

        [FindsBy(How = How.CssSelector, Using = "button[data-ng-click='showNotes = !showNotes; showGroups = false']")]
        private IWebElement OpenNoteBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = "button[data-ng-disabled='model.Content == null || model.Content.length == 0']")]
        private IWebElement AddNoteBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = "textarea[data-ng-model='model.Content']")]
        private IWebElement AddNoteText { get; set; }

        [FindsBy(How = How.CssSelector, Using = "ng-form[name='noteForm']")]
        private IWebElement NoteForm { get; set; }
        #endregion
        #region Note Panel Functions

        public bool DeleteNote(string data)
        {
            try
            {
                this.OpenNotePanel();
                IWebElement aNote;
                IList<IWebElement> AddGroupList = NoteForm.FindElements(By.ClassName("box"));
                // AdGroup=AddGroupList.FirstOrDefault(e => e.FindElement(By.CssSelector("div[style='word-wrap:break-word;font-weight:bold']")).Text.Contains(data.GroupName));
                aNote = AddGroupList.FirstOrDefault(e => e.Text.Contains(data));

                if (aNote != null)
                {
                    Element.MoveTo(aNote);
                    aNote.FindElement(By.CssSelector("button[data-ng-click='deleteNote(note)']")).Click();
                    string result = Wait.UntilToastMessageShow();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CreateNewNote(string Note)
        {
            //this.OpenAdGroupPanel();
            AddNoteText.SendKeys(Note);
            AddNoteBtn.Click();
            this.SaveNote(Note);
            return true;
        }

        private void SaveNote(string Note)
        {
            this.AddNoteBtn.Click();
            string Result = Wait.UntilToastMessageShow();
            if (Result == "Saved successfully")
            {
                //AdGroupForm_CancelBtn.Click();
                Wait.Second(1);
                Logger.Log(LogingType.TestCasePass, "Save Note Save Successfully");

            }
            else
            {

                AdGroupForm_CancelBtn.Click();
                Wait.Second(1);
                Logger.Log(LogingType.TextCaseFail, "Save Note Save Failed - " + Result);
                throw new Exception(Result);
            }
        }

        private void OpenNotePanel()
        {

            //if (!CreateAddGroupBtn.Enabled)
            //  {
            OpenNoteBtn.Click();
            Wait.UntilDisply(NoteForm);
            // }
        }

        #endregion
        #region Ad Group Functions
        public bool DeleteAddGroup(AdGroupModel data)
        {
            try
            {
                this.OpenAdGroupPanel();
                IWebElement AdGroup;
                IList<IWebElement> AddGroupList = AddGroupForm.FindElements(By.ClassName("box"));
                // AdGroup=AddGroupList.FirstOrDefault(e => e.FindElement(By.CssSelector("div[style='word-wrap:break-word;font-weight:bold']")).Text.Contains(data.GroupName));
                AdGroup = AddGroupList.FirstOrDefault(e => e.Text.Contains(data.GroupName));

                if (AdGroup != null)
                {
                    Element.MoveTo(AdGroup);
                    AdGroup.FindElement(By.CssSelector("button[data-ng-click='deleteGroup(adgroup)']")).Click();
                    string result = Wait.UntilToastMessageShow();


                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void OpenAdGroupPanel()
        {

            //if (!CreateAddGroupBtn.Enabled)
            //  {
            AdGroupBtn.Click();
            Wait.UntilDisply(AdGroupPanel);
            // }
        }

        public bool CreateNewAddGroup(AdGroupModel data)
        {
            //this.OpenAdGroupPanel();
            CreateAddGroupBtn.Click();
            Wait.UntilDisply(AdGroupForm_AdTypeDD);
            this.FillAddGroupData(data);
            this.SaveAddGroup(data);
            return true;
        }

        private void SaveAddGroup(AdGroupModel data)
        {
            this.AdGroupForm_SaveBtn.Click();
            string Result = Wait.UntilToastMessageShow();
            if (Result == "Saved successfully")
            {
                //AdGroupForm_CancelBtn.Click();
                Wait.Second(1);
                Logger.Log(LogingType.TestCasePass, "Ad Group Save Successfully");

            }
            else
            {

                AdGroupForm_CancelBtn.Click();
                Wait.Second(1);
                Logger.Log(LogingType.TextCaseFail, "Ad Group Save Failed - " + Result);
                throw new Exception(Result);
            }
        }

        private void FillAddGroupData(AdGroupModel data)
        {
            if (!String.IsNullOrEmpty(data.ProductGroup))
            {
                Select.ByText(AdGroupForm_ApplicationDD, data.ProductGroup);
                Wait.Second(2);
            }
            if (!String.IsNullOrEmpty(data.AddType))
            {
                Select.ByText(AdGroupForm_AdTypeDD, data.AddType);
                Wait.MLSeconds(200);
            }
            if (!String.IsNullOrEmpty(data.GroupName))
            {
                this.AdGroupForm_GroupNameField.Clear();
                this.AdGroupForm_GroupNameField.SendKeys(data.GroupName);
                Wait.MLSeconds(200);
            }
            if (data.Impression != null)
            {
                this.AdGroupForm_ImpressionsField.Clear();
                this.AdGroupForm_ImpressionsField.SendKeys(data.Impression.ToString());
                Wait.MLSeconds(200);
            }
            if (data.Budget != null)
            {
                this.AdGroupForm_BudgetField.Clear();
                this.AdGroupForm_BudgetField.SendKeys(data.Budget.ToString());
                Wait.MLSeconds(200);
            }
            if (data.EndDate != null)
            {
                this.AdGroupForm_EndDateField.Clear();
                this.AdGroupForm_EndDateField.SendKeys(data.EndDate.ToString());
                Wait.MLSeconds(200);
            }

        }
        #endregion

        public void ExportToExcel() { 
        ExportBtn.FindElement(By.TagName("button")).Click();
        Wait.MLSeconds(200);
        FileHandler.BerforeDownLoadNotification();
        ExportBtn.FindElement(By.LinkText("Excel")).Click();
        Wait.UntilDownloading();
        bool Result=FileHandler.CheckIfExcelFileContainRecordsLib(FileHandler.FindExcelFilePathForReport());
        if (Result)
        {
            Logger.Log(LogingType.Error, "Excel Report verified successfully.");
        }
        else {
            Logger.Log(LogingType.Error, "Excel Report does't have any Records.");
            //this.CancelnewPage();
            throw new Exception();
        }
        }
        public void ExportToPDF() {
            ExportBtn.FindElement(By.TagName("button")).Click();
            Wait.MLSeconds(200);
            FileHandler.BerforeDownLoadNotification();
            ExportBtn.FindElement(By.LinkText("PDF")).Click();
            Wait.UntilDownloading();
            bool Result = FileHandler.CheckIfPDFFileContainRecords(FileHandler.FindPDFFilePathForReport());
            if (Result)
            {
                Logger.Log(LogingType.Error, "Excel Report verified successfully.");
            }
            else
            {
                Logger.Log(LogingType.Error, "Excel Report does't have any Records.");
                //this.CancelnewPage();
                throw new Exception();
            }
        }


        public void AddNewOrderLine(string lineType)
        {
            Element.ScrolTo(AddOrderLineBtn);
            AddOrderLineBtn.Click();
            Wait.MLSeconds(500);
            if (lineType == "PFP")
            {
                PFP.Click();
                Wait.UntilDisply(By.Id("drpApplication"));
            }
            else if (lineType == "Banner")
            {
                Banner.Click();
                Wait.UntilDisply(By.Id("drpApplication"));
            }
        }

        public IWebElement SearchOrderLine(string SearchTerm = "", string ProdInfo = "")
        {
            if (SearchTerm == "" && ProdInfo == "")
            {
                return null;
            }
            Wait.UntilDisply(OrderLineGrid);
            IWebElement FilterRow = OrderLineGrid.FindElement(By.TagName("thead")).FindElements(By.TagName("tr"))[1];

            if (SearchTerm != "" && SearchTerm != null)
            {

                IWebElement Searchinput = FilterRow.FindElements(By.TagName("th"))[6].FindElement(By.TagName("input"));
                Element.ScrolTo(Searchinput);
                Searchinput.Clear();
                Searchinput.SendKeys(SearchTerm);
            }
            if (ProdInfo != "" && ProdInfo != null)
            {

                IWebElement productInfoinput = FilterRow.FindElements(By.TagName("th"))[5].FindElement(By.TagName("input"));
                productInfoinput.Clear();
                productInfoinput.SendKeys(ProdInfo);
            }
            Wait.AM_Loaging_ShowAndHide();
            try
            {
                var row = OrderLineGrid.FindElement(By.TagName("tbody")).FindElements(By.TagName("tr"))[0];
                Element.ScrolTo(row);
                return row;

            }
            catch (Exception)
            {

                return null;
            }
        }
        public bool DeleteRow(IWebElement Row)
        {
            Row.FindElement(By.LinkText("Delete")).Click();
            Modal.CommonclickYes();
            // Wait.UntilLoading();
            var result = this.verfiyDelete();
            Wait.Second(1);
            //Wait.UntilClickAble(AddOrderLineBtn);
            Wait.AM_Loaging_ShowAndHide();
            return result;
        }
        private bool verfiyDelete()
        {
            string result = Wait.UntilToastMessageShow();

            if (result == "Deleted successfully")
            {

                return true;
            }
            else
            {
                return false;
            }
        }

        internal void DeleteResultBannerIfAlreadyExists(OrderLineModel orderLineModel)
        {
            var oldOrderLine = this.SearchOrderLine(orderLineModel.SearchTerm, orderLineModel.ProductInformation);
            if (oldOrderLine != null)
            {
                this.DeleteRow(oldOrderLine);
            }
        }

        internal int GetAdCount()
        {
            string count=ResultCountDiv.FindElement(By.TagName("span")).Text;
            if (count == "") {
                return 0;
            }else{
            return int.Parse(count);
            }
        }

        internal void OpenOrderLine(OrderLineModel data)
        {
            var oldOrderLine = this.SearchOrderLine(data.SearchTerm, data.ProductInformation);
            if (oldOrderLine != null)
            {
                oldOrderLine.Click();
                Wait.AM_Loaging_ShowAndHide();
            }
            else {
                throw new Exception("No Add Found Related to Given Information.");
            }
        }

        internal double GetActiveCost()
        {
            String Active = ResultBar.FindElements(By.ClassName("results-group"))[0].Text;
            Active = Active.Trim();
            if (Active == "")
            {
                return 0;
            }
            else
            {
                Active = Active.Replace("Active:", "");
                Active = Active.Trim();
                return Double.Parse(Active, System.Globalization.NumberStyles.Currency);
            }
        }

        internal double GetTotalCost()
        {
            string Total = ActiveTotal.Text;
            Total = Total.Trim();
            if (Total == "")
            {
                return 0;
            }
            else
            {
                return Double.Parse(Total, System.Globalization.NumberStyles.Currency);
            }
        }
    }
}
