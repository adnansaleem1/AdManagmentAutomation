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


        #region Order Line Test Cases

        [Test, Order(0)]
        public void LogInToAddManagement()
        {
            LoginPageManager = PagesRepo.LoginP;
            LoginPageManager
                .Navigate()
                .LoginGivenUser(AddUserData.AddUser1)
                .GoToAppCatagory("ESP");
        }
        [Test, Order(1)]
        public void AddPFPOrderLine_SaveAndCopy()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.OrderId);
            PagesRepo.EditOrder.AddNewOrderLine("PFP");
            PagesRepo.EditOrderLine.
                FillOrderLine(OrderLineData.SaveAndCopy).
                SaveAndCopy().VerifySaveAndCopy();
        }
        [Test, Order(2)]
        public void AddPFPOrderLine_SaveAndAdd()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.OrderId);
            PagesRepo.EditOrder.AddNewOrderLine("PFP");
            PagesRepo.EditOrderLine.
            FillOrderLine(OrderLineData.SaveAndAdd)
            .VerfiyMultipleProducts(3);
        }
         [Test, Order(3)]
        public void AddPFPOrderLine_Add3ProdcutsManually()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.OrderId);
            PagesRepo.EditOrder.AddNewOrderLine("PFP");
            PagesRepo.EditOrderLine.
            FillOrderLine(OrderLineData.MultipleProductInManualSelection)
            .VerfiyMultipleProducts(3);
        } 
        #endregion

        #region Setup Section
        [OneTimeSetUp]
        public void OnTimeSetUp()
        {
            SDriver.StartBrowser(BrowserTypes.Chrome);
            driver = SDriver.Browser;
            driver.Manage().Window.Maximize();
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
