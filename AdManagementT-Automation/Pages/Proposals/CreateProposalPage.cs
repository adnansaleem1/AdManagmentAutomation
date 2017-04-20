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
   public class CreateProposalPage
    {
        IWebDriver driver = SDriver.Browser;

        [FindsBy(How = How.CssSelector, Using = "input[data-ng-model='proposal.MemberId']")]
        private IWebElement MemberIdField { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[data-ng-model='proposal.CompanyInfo.Name']")]
        private IWebElement CompanyNameField { get; set; }

        [FindsBy(How = How.CssSelector, Using = "select[data-ng-model='selectedShipToContact']")]
        private IWebElement ContactDD { get; set; }

        [FindsBy(How = How.Id, Using = "drpSalesRep")]
        private IWebElement SalesRepDD { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[data-ng-model='proposal.ProposalDate']")]
        private IWebElement ProposalDateField { get; set; }
       
        [FindsBy(How = How.CssSelector, Using = "input[data-ng-model='proposal.ProposalName']")]
        private IWebElement PerposalNameField { get; set; }


        [FindsBy(How = How.CssSelector, Using= "button[data-ng-click='backToProposals(//'/am/proposals//')']")]
        private IWebElement CancelBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[data-ng-model='proposal.ProposalName']")]
        private IWebElement ProposalNameField { get; set; }

        [FindsBy(How = How.CssSelector, Using = "button[data-ng-click='createProposal()']")]
        private IWebElement CreateProposalBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = "button[data-ng-click='saveProposal(amFormProposalDetail.$valid, \\'save\\');']")]
        private IWebElement SaveProposalBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = "button[data-ng-click='searchInventory(amFormInventory.$valid)']")]
        private IWebElement SearchBtn { get; set; }

        [FindsBy(How = How.Id, Using = "drpApplication")]
        private IWebElement Productgroup { get; set; }

        [FindsBy(How = How.Id, Using = "drpAdType")]
        private IWebElement AdType { get; set; }

        [FindsBy(How = How.CssSelector, Using = "tags-input[data-ng-model='SearchTerm']")]
        private IWebElement searchTermsControl { get; set; }

        [FindsBy(How = How.CssSelector, Using = "tr[data-ng-repeat='inventory in $data']")]
        private IList<IWebElement> TableRecords { get; set; }

         [FindsBy(How = How.CssSelector, Using = "button[data-ng-click='addToProposal()']")]
        private IWebElement AdToProposalBtn { get; set; }
         [FindsBy(How = How.CssSelector, Using = "div[data-ng-show='isEditMode']")]
        private IWebElement ExportControl { get; set; }
       
       [FindsBy(How = How.CssSelector, Using = "button[data-ng-click='backToProposals(\\'/am/proposals\\')']")]
        private IWebElement CancelCreateProposal { get; set; }
       

        [FindsBy(How = How.ClassName, Using = "modal-dialog")]
        private IWebElement TermsModal { get; set; }
           [FindsBy(How = How.ClassName, Using = "dirtycheck-dialog")]
        private IWebElement Dirtycheck { get; set; }
       

        internal void CreateProposal(PropsalModel Data)
        {

            Wait.UntilDisply(MemberIdField);
            this.AddSearchTerm(Data);
            this.FillProposal(Data);
            this.SaveProposalBtn.Click();
            this.VerifySave();
        }
        public void VerifySave() {
            IList<string> Resultbanner = Wait.UntilToastMessageShow();
            if (Resultbanner.Any(e => e == "Saved successfully"))
            {
                Logger.Log(LogingType.TestCasePass, "New Proposal Save Sucessfully");

            }
            else
            {
                Logger.Log(LogingType.TextCaseFail, "New Proposal Unable to save");
                throw new Exception("New Proposal Unable to save");

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
        private void OpenTermDialog(string type) {
            Wait.UntilClickAble(driver.FindElements(By.ClassName("dropdown-toggle"))[1]);
            driver.FindElements(By.ClassName("dropdown-toggle"))[1].Click();
            Wait.MLSeconds(100);
            driver.FindElement(By.LinkText(type)).Click();
            Wait.UntilDisply(SearchBtn);
        }
        private void AddSearchTerm(PropsalModel Data)
        {
            this.OpenTermDialog(Data.InventoryType);
            this.FillSerachTerms(Data);
            SearchBtn.Click();
            Wait.UntilLoading(TableRecords);
            Wait.Second(1);
            TermsModal.FindElements(By.CssSelector("tr[data-ng-repeat='inventory in $data']"))[0].FindElement(By.TagName("input")).Click();
            Wait.MLSeconds(100);
            AdToProposalBtn.Click();
        }

        private void FillSerachTerms(PropsalModel Data)
        {
            Select.ByText(Productgroup, Data.ProductGroup);
            Wait.MLSeconds(300);
            Select.ByText(AdType, Data.AdType);
            if (Data.SearchTerms.Count > 0)
            {
                Select.TagBasedInputAboslute(Data.SearchTerms, searchTermsControl);
            }
           // Element.syncCheckBox(inventory.IncludeSubCat, SubCatagoriesCheckbox);
            Wait.MLSeconds(100);
        }


        internal CreateProposalPage Navigate()
        {
            PagesRepo.AddTabs.Switch(AM_Sub_Proposlas.Create_Proposal);
            return this;
        }

        internal void CreateProposalSaveAndCopy(PropsalModel Data)
        {
            Wait.UntilDisply(MemberIdField);
            this.AddSearchTerm(Data);
            this.FillProposal(Data);

            Element.GetPerent(this.SaveProposalBtn).FindElement(By.CssSelector("button[data-toggle='dropdown']")).Click();
            Wait.Second(1);
            driver.FindElement(By.LinkText("Save and Copy")).Click();
            this.VerifySave_AndCopy(Data);
            this.CancelProposal();
        }

        private void CancelProposal()
        {
            CancelCreateProposal.Click();
            Wait.MLSeconds(200);
            Modal.ClickYes(Dirtycheck);
            Wait.AM_Loaging_ShowAndHide();
        }

        private void VerifySave_AndCopy(PropsalModel Data)
        {
              IList<string> Resultbanner = Wait.UntilToastMessageShow();
              if (Resultbanner.Any(e => e == "Saved and copied successfully"))
            {
                Logger.Log(LogingType.TestCasePass, "New Proposal Save Sucessfully");

            }
            else
            {
                Logger.Log(LogingType.TextCaseFail, "New Proposal Unable to save");
                throw new Exception("New Proposal Unable to save");

            }
        }
        internal void CreateProposalSaveAndAdd(PropsalModel Data)
        {
            Wait.UntilDisply(MemberIdField);
            this.AddSearchTerm(Data);
            this.FillProposal(Data);

            Element.GetPerent(this.SaveProposalBtn).FindElement(By.CssSelector("button[data-toggle='dropdown']")).Click();
            Wait.Second(1);
            driver.FindElement(By.LinkText("Save and Add")).Click();
            this.VerifySave_AndAdd(Data);
        }

        private void VerifySave_AndAdd(PropsalModel Data)
        {
            IList<string> Resultbanner = Wait.UntilToastMessageShow();
            if (Resultbanner.Any(e => e == "Saved and new added successfully"))
            {
                Logger.Log(LogingType.TestCasePass, "New Proposal Save Sucessfully");

            }
            else
            {
                Logger.Log(LogingType.TextCaseFail, "New Proposal Unable to save");
                throw new Exception("New Proposal Unable to save");

            }
        }

        internal void UpdateSearchTerms(PropsalModel Data)
        {
            Wait.UntilDisply(MemberIdField);
            this.AddSearchTerm(Data);
            this.SaveProposalBtn.Click();
            this.VarifyUpdate();
        }

        private void VarifyUpdate()
        {
            IList<string> Resultbanner = Wait.UntilToastMessageShow();
            if (Resultbanner.Any(e => e == "Updated successfully"))
            {
                Logger.Log(LogingType.TestCasePass, " Proposal Updated Sucessfully");

            }
            else
            {
                Logger.Log(LogingType.TextCaseFail, "Proposal Unable to Update");
                throw new Exception("Proposal Unable to Update");

            }
        }

        internal void Export(string p)
        {
            if (p.ToLower() == "pdf") {
                ExportControl.FindElement(By.TagName("button")).Click();
                ExportControl.FindElement(By.LinkText("PDF")).Click();
                FileHandler.BerforeDownLoadNotification();
                Wait.UntilDownloading();
                FileHandler.CheckIfPDFFileContainRecords(FileHandler.FindPDFFilePathForReport());
            }
            else if (p.ToLower() == "excel") {
                ExportControl.FindElement(By.TagName("button")).Click();
                ExportControl.FindElement(By.LinkText("Excel")).Click();
                FileHandler.BerforeDownLoadNotification();
                Wait.UntilDownloading();
                FileHandler.CheckIfExcelFileContainRecords(FileHandler.FindExcelFilePathForReport());
            }
        }
    }
}
