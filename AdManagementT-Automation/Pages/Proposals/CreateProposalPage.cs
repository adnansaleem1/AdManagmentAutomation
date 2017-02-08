using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdManagementT_Automation.Pages.Inventory
{
    class CreateProposalPage
    {
        [FindsBy(How = How.CssSelector, Using = "input[data-ng-model='proposal.MemberId;']")]
        private IWebElement MemberIdField { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[data-ng-model='proposal.CompanyInfo.Name']")]
        private IWebElement CompanyNameField { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[data-ng-model='selectedShipToContact']")]
        private IWebElement ContactDD { get; set; }

        [FindsBy(How = How.CssSelector, Using = "drpSalesRep")]
        private IWebElement SalesRepDD { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[data-ng-model='proposal.ProposalDate']")]
        private IWebElement ProposalDateField { get; set; }
       
        [FindsBy(How = How.CssSelector, Using = "input[data-ng-model='proposal.ProposalName']")]
        private IWebElement PerposalNameField { get; set; }


        [FindsBy(How = How.CssSelector, Using= "button[data-ng-click='backToProposals(//'/am/proposals//')']")]
        private IWebElement CancelBtn { get; set; }


    }
}
