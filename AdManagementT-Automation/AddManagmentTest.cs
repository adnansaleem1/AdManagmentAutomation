using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdManagementT_Automation.Pages;
using OpenQA.Selenium;
using NUnit.Framework;
using AdManagementT_Automation.Base;
using AddManagmentData.Model;
using SeleniumExtension.Ref;
using SeleniumExtension.Driver;
using AddManagmentData.Data;

namespace AdManagementT_Automation
{
    [TestFixture]
    public class AddManagmentTest
    {
        private IWebDriver driver = null;
        private LoginPage LoginPageManager = null;



        [Test,Order(0)]
        public void LogInToAddManagement()
        {
            LoginPageManager = PagesRepo.LoginP;
            LoginPageManager
                .Navigate()
                .LoginGivenUser(AddUserData.AddUser1)
                .GoToAppCatagory("ESP");
        }
        [Test, Order(1)]
        public void AddNewOrderLine() { 
        
        }


        #region Setup Section
        [OneTimeSetUp]
        public void OnTimeSetUp()
        {
            SDriver.StartBrowser(BrowserTypes.Chrome);
            driver = SDriver.Browser;
        }
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            SDriver.StopBrowser();
        }
        [SetUp]
        public void SetUpBeforeEveryTest()
        {

        }
        [TearDown]
        public void TearDownAfterEveryTest()
        {
            
        } 
        #endregion
    }
}
