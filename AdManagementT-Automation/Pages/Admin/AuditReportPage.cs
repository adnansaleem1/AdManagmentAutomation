using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdManagementT_Automation.Pages.Admin
{
    class AuditReportPage
    {
        [FindsBy(How = How.Id, Using = "drpprofile")]
        private IWebElement ProfileDD { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[data-ng-model='filterModel.ToDate']")]
        private IWebElement ToDateField { get; set; }



    }
}
