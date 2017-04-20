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

namespace AdManagementT_Automation.Pages.Proposals
{
    public class AllProposalsPage
    {
        [FindsBy(How = How.CssSelector, Using = "tr[data-ng-repeat='proposal in $data']")]
        private IList<IWebElement> TableRecords { get; set; }

         [FindsBy(How = How.ClassName, Using = "ng-table-filters")]
        private IWebElement FilterRow { get; set; }

         


        internal AllProposalsPage Navigate()
        {
            PagesRepo.AddTabs.Switch(AM_Sub_Proposlas.All_Proposal);
            return this;
        }

        private void OpenLatestProposal() {
            TableRecords[0].Click();
            Wait.AM_Loaging_ShowAndHide();
        }


        internal void OpenProposal(PropsalModel Data)
        {
            FilterProposal(Data);
        }

        private void FilterProposal(PropsalModel Data)
        {
            IList<IWebElement> list = FilterRow.FindElements(By.TagName("input"));
            Wait.UntilDisply(FilterRow);
            Element.ClearAllFields(FilterRow.FindElements(By.TagName("input")));
            list[0].SendKeys(Data.MemberId);
            Wait.AM_Loaging_ShowAndHide();
            this.OpenLatestProposal();
        }
    }
}
