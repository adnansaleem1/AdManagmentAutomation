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
        #region Advertisement Test Cases


        [Test, Order(4)]
        public void Advertisement_SearchByMemberID()
        {
            this.LogInToAddManagement();
            var Addpage = PagesRepo.Advertisement;
            Addpage.Navigate()
                .SearchByMemeberID(OrderLineData.OrderId);

        }
        [Test, Order(5)]
        public void Advertisement_SearchByPostition()
        {
            this.LogInToAddManagement();
            var Addpage = PagesRepo.Advertisement;
            Addpage.Navigate()
                .SearchByPosition(OrderLineData.SearchForPosition1.Positions);

        }
        [Test, Order(6)]
        public void Advertisement_SearchByTerms()
        {
            this.LogInToAddManagement();
            var Addpage = PagesRepo.Advertisement;
            Addpage.Navigate()
                .SearchByTerms(OrderLineData.SearchForPosition1.SearchTerms);

        }
        [Test, Order(7)]
        public void Advertisement_SearchByAddStatus()
        {
            this.LogInToAddManagement();
            var Addpage = PagesRepo.Advertisement;
            Addpage.Navigate()
                .SearchByAddStatus(OrderLineData.SearchForPosition1.Statuses);

        }
        [Test, Order(8)]
        public void Advertisement_SearchByRate()
        {
            this.LogInToAddManagement();
            var Addpage = PagesRepo.Advertisement;
            Addpage.Navigate()
                .SearchByRate(OrderLineData.SearchForPosition1.Rates);

        }
        [Test, Order(9)]
        public void Advertisement_FreeFormSearch()
        {
            this.LogInToAddManagement();
            var Addpage = PagesRepo.Advertisement;
            Addpage.Navigate()
                .FreeFormSerch();

        }
        [Test, Order(9)]
        public void Advertisement_ExportAllAds()
        {
            this.LogInToAddManagement();
            var Addpage = PagesRepo.Advertisement;
            Addpage.Navigate()
                .ExportAllAdds();

        }
        [Test, Order(10)]
        public void Advertisement_ChangeStatusOfAdd()
        {
            this.LogInToAddManagement();
            var Addpage = PagesRepo.Advertisement;
            Addpage.Navigate()
                .ChangeAdStatus(OrderLineData.SearchForPosition1);

        }
        #endregion
        #region Inventory Tests
        [Test, Order(11)]
        public void Inventory_SearchWithTermsIncludingSubterms()
        {
            this.LogInToAddManagement();
            var Inventory = PagesRepo.Inventory;
            Inventory.Navigate()
                .FillSearch(InventoryData.ForBags)
                .Search()
                .VerifySearchResultWithSubTerms(InventoryData.ForBags.SearchTerms[0]);
               

        } 
        #endregion
        #region Admin Pages
        [Test, Order(14)]
        public void Pages_AddNewPage()
        {
            var aPages = PagesRepo.adminpages;
            this.LogInToAddManagement();
            aPages.Navigate()
                .FillCratePageForm(PagesData.Page1)
                .SavePage();
                
        }
        [Test, Order(16)]
        public void Pages_AddAdditionalKeyWords()
        {
            var aPages = PagesRepo.adminpages;
            this.LogInToAddManagement();
            aPages.Navigate()
                .SearchPage(PagesData.Page1)
                .AddAddionalKeyWordsinPage(PagesData.Page1);
                
               
        }
        [Test, Order(16)]
        public void Pages_DeletePage()
        {
            var aPages = PagesRepo.adminpages;
            this.LogInToAddManagement();
            aPages.Navigate()
                .DeletePage(PagesData.Page1);
        }
         [Test, Order(17)]
        public void Pages_ActivePage()
        {
            var aPages = PagesRepo.adminpages;
            this.LogInToAddManagement();
            aPages.Navigate()
                .ActivePage(PagesData.Page1);
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
