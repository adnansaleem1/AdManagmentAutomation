using AddManagmentData.Data;
using AdManagementT_Automation.Base;
using AdManagementT_Automation.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtension.Controls;
using SeleniumExtension.Driver;
using SeleniumExtension.Ref;
using SeleniumExtension.Utilties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdManagementT_Automation
{
    [TestFixture]
    class Sales_User_TestCases
    {
        private IWebDriver driver = null;
        private LoginPage LoginPageManager = null;


        #region Inventory Tests
        [Test, Order(1)]
        public void Inventory_SearchWithTermsIncludingSubterms()
        {
            this.LogInToAddManagement();
            var Inventory = PagesRepo.Inventory;
            Inventory.Navigate()
                .FillSearch(InventoryData.ForBags)
                .Search()
                .VerifySearchResultWithSubTerms(InventoryData.ForBags.SearchTerms[0]);
        }
        [Test, Order(2)]
        public void Inventory_SearchWithOutTermsIncludingSubterms()
        {
            this.LogInToAddManagement();
            var Inventory = PagesRepo.Inventory;
            var Data = InventoryData.ForBags;
            Data.IncludeSubCat = false;
            Inventory.Navigate()
                .FillSearch(Data)
                .Search()
                .VerifySearchResultWithoutSubTerms(Data.SearchTerms[0]);
        }
        [Test, Order(3)]
        public void ESP_PFP_ProposalInventoryscreen()
        {
            this.LogInToAddManagement();
            var Inventory = PagesRepo.Inventory;
            var Data = InventoryData.CreateProposal;
            Inventory.Navigate()
                .FillSearch(Data)
                .Search()
                .CreateProposal(Data);

        }
        [Test, Order(4)]
        public void ESP_Websites_PFP_ProposalInventoryscreen()
        {
            this.LogInToAddManagement();
            var Inventory = PagesRepo.Inventory;
            var Data = InventoryData.CreateProposal;
            Data.ProductGroup = "ESP Websites";
            Inventory.Navigate()
                .FillSearch(Data)
                .Search()
                .CreateProposal(Data);

        }
        [Test, Order(5)]
        public void Banner_ESP_ProposalInventoryscreen()
        {
            this.LogInToAddManagement();
            var Inventory = PagesRepo.Inventory;
            var Data = InventoryData.CreateProposal;
            Data.ProductGroup = "ESP";
            Data.AdType = "Banner";
            //Inventory.ChangeInventoryType("Banners");
            Inventory.Navigate().ChangeInventoryType("Banners")
                .FillSearch(Data)
                .Search()
                .CreateProposal(Data);

        }
        #endregion
        #region Wait List Region
        [Test, Order(6)]
        public void ESP_PFP_ItemToWaitList()
        {
            this.LogInToAddManagement();
            var WaitList = PagesRepo.Waitlistpage;
            var Data = WaitListData.EspPfp;
            WaitList.Navigate().
                OpenNewWaitListPage().
                FillNewWaitList(Data).
                VerifySaveNewAdd();
        }
        [Test, Order(7)]
        public void MobileBanner_ItemToWaitList()
        {
            this.LogInToAddManagement();
            var WaitList = PagesRepo.Waitlistpage;
            var Data = WaitListData.EspPfp;
            var AllOrders = PagesRepo.AllOrders;
            var EditOrder = PagesRepo.EditOrder;
            var data = OrderLineData.ResultBanner;
            Data.ProductGroup = "ESP Mobile";
            Data.AddType = "Results Banner";
            Data.Position = null;
            Data.Cost = null;
            
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.OrderId);
            data.Status = null;
            data.DeliveryPreferences = null;
            data.AddType = "Results Banner";
            data.ProductGroup = "ESP Mobile";
            data.Position = null;
            data.Priority = null;
            data.ProductInformation = data.SearchTerm = Data.SearchTerms;
            EditOrder.DeleteResultBannerIfAlreadyExists(data);
            PagesRepo.EditOrder.AddNewOrderLine("Banner");
            PagesRepo.EditOrderLine.
            FillOrderLine_Admin(data).Save();
            //Data.defaultProduct = "";
            WaitList.Navigate().
                OpenNewWaitListPage().
                FillNewWaitList(Data).
                VerifySaveNewAdd();
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.OrderId);
            EditOrder.DeleteResultBannerIfAlreadyExists(data);
        }
        [Test, Order(8)]
        public void Edit_WaitList()
        {
            this.LogInToAddManagement();
            var WaitList = PagesRepo.Waitlistpage;
            var Data = WaitListData.EspPfp;
            Data.defaultProduct = Data.defaultProduct + "Test 1";
            //Data.defaultProduct = "";
            WaitList.Navigate().EditWaitList(Data);
        }
        #endregion
        #region CreateProposal Page

        [Test, Order(9)]
        public void CreateProposal_ESP_PFP_Proposal()
        {
            this.LogInToAddManagement();
            var ProposalPage = PagesRepo.CreatePropsal;
            var Data = InventoryData.CreateProposal;
            ProposalPage.Navigate()
                .CreateProposal(Data);

        }
        [Test, Order(10)]
        public void ESP_Websites_PFP_Proposal()
        {
            this.LogInToAddManagement();
            var ProposalPage = PagesRepo.CreatePropsal;
            var Data = InventoryData.CreateProposal;
            Data.ProductGroup = "ESP Websites";
            ProposalPage.Navigate()
                .CreateProposal(Data);

        }
        [Test, Order(11)]
        public void ESP_Banner_PFP_Proposal()
        {
            this.LogInToAddManagement();
            var ProposalPage = PagesRepo.CreatePropsal;
            var Data = InventoryData.CreateProposal;
            Data.InventoryType = "Banners";
            Data.ProductGroup = "ESP";
            Data.AdType = "Banner";
            ProposalPage.Navigate()
                .CreateProposal(Data);

        }
        [Test, Order(12)]
        public void Mobile_PFP_Proposal()
        {
            this.LogInToAddManagement();
            var ProposalPage = PagesRepo.CreatePropsal;
            var Data = InventoryData.CreateProposal;
            Data.SearchTerms = new List<string>() { "MUGS & STEINS/Mugs & Steins-Ceramic" };
            Data.ProductGroup = "ESP Mobile";
            ProposalPage.Navigate()
                .CreateProposal(Data);

        }
        [Test, Order(13)]
        public void Mobile_Banner_Proposal()
        {
            this.LogInToAddManagement();
            var ProposalPage = PagesRepo.CreatePropsal;
            var Data = InventoryData.CreateProposal;
            Data.SearchTerms[0] = "MUGS & STEINS";
            Data.InventoryType = "Banners";
            Data.ProductGroup = "ESP Mobile";
            Data.AdType = "Banner";
            ProposalPage.Navigate()
                .CreateProposal(Data);

        }
        [Test, Order(14)]
        public void CreateProposal_SaveAndCopy()
        {
            this.LogInToAddManagement();
            var ProposalPage = PagesRepo.CreatePropsal;
            var Data = InventoryData.CreateProposal;
            ProposalPage.Navigate()
                .CreateProposalSaveAndCopy(Data);

        }
        [Test, Order(15)]
        public void CreateProposal_EditProposal()
        {
            this.LogInToAddManagement();
            var ProposalPage = PagesRepo.CreatePropsal;
            var allPro = PagesRepo.AllProposalPage;

            var Data = InventoryData.CreateProposal;
            ProposalPage.Navigate()
                .CreateProposal(Data);
            allPro.Navigate().OpenProposal(Data);
            Data.AdType = "PFP";
            Data.ProductGroup = "ESP";
            ProposalPage.UpdateSearchTerms(Data);
        }
        [Test, Order(16)]
        public void CreateProposal_ExportToPdf()
        {
            this.LogInToAddManagement();
            var ProposalPage = PagesRepo.CreatePropsal;
            var allPro = PagesRepo.AllProposalPage;

            var Data = InventoryData.CreateProposal;
            ProposalPage.Navigate()
                .CreateProposal(Data);
            allPro.Navigate().OpenProposal(Data);
            ProposalPage.Export("pdf");
        }
        #endregion
        #region Gernal
        [Test, Order(0)]
        public void LogInToAddManagement()
        {
            var LoginPageManager = PagesRepo.LoginP;
            if (!LoginPageManager.LoginVerification(AddUserData.SalesUser))
            {
                LoginPageManager
            .Navigate()
            .LoginGivenUser(AddUserData.SalesUser)
            .GoToAppCatagory("ESP");
            }
        }
        [Test, Order(17)]
        public void AdvertisementTabShouldNotbeThere()
        {
            this.LogInToAddManagement();
            var adver = PagesRepo.Advertisement;
            adver.VerifyTabShouldNotBeThere();
        }

        [Test, Order(18)]
        public void AdminTabShouldNotbeThere()
        {
            this.LogInToAddManagement();
            var admin = PagesRepo.adminpages;
            admin.VerifyAdminTabNotThere();
        }
        [Test, Order(41)]
        public void RetensionOfFilterAndSearch()
        {
            this.LogInToAddManagement();
            var Inventory = PagesRepo.Inventory;
            var AllOrders = PagesRepo.AllOrders;
            var Data = InventoryData.ForBags;
            Inventory.Navigate()
                .FillSearch(Data)
                .Search();
            var SearchOrder_SearchTerms_AfterChange = Inventory.ChangeSortOrder(1);
            AllOrders.Navigate();
            Inventory.Navigate().VerfiySearchRetain(Data);
            if (SearchOrder_SearchTerms_AfterChange != Inventory.GetSortOnCol(1))
            {
                throw new Exception("System was unable to retain sort information");
            }
        }
        [Test, Order(42)]
        public void ClearFilterAfterLogout()
        {
            this.LogInToAddManagement();
            LoginPageManager = PagesRepo.LoginP;
            var Inventory = PagesRepo.Inventory;
            var AllOrders = PagesRepo.AllOrders;
            var Data = InventoryData.ForBags;
            Inventory.Navigate()
                .FillSearch(Data)
                .Search();
            var SearchOrder_SearchTerms_AfterChange = Inventory.ChangeSortOrder(1);
            LoginPageManager.LogOut();
            this.LogInToAddManagement();
            Inventory.Navigate().VerfiySearchNotRetain(Data);
        }


        #endregion
        #region Reports
        [Test, Order(19)]
        public void DropedProducts_ESP_PFP()
        {
            this.LogInToAddManagement();
            var repo = PagesRepo.Reports;
            repo.Navigate().DropProductReport("ESP", "PFP").Export("PDF");

        }
        [Test, Order(20)]
        public void OrderBySearchTerms_ESP_Banner()
        {
            this.LogInToAddManagement();
            var repo = PagesRepo.Reports;
            repo.Navigate().OrderBySearchTerms("ESP", "Banner", "BAGS").Export("Excel");

        }
        [Test, Order(21)]
        public void OrderBySearchTerms_ESP_PFP()
        {
            this.LogInToAddManagement();
            var repo = PagesRepo.Reports;
            repo.Navigate().OrderBySearchTerms("ESP", "PFP", "BAGS").Export("Excel");

        }
        [Test, Order(22)]
        public void AdsBelowRateCard()
        {
            this.LogInToAddManagement();
            var repo = PagesRepo.Reports;
            repo.Navigate().AdsBelowRateCard().Export("Excel");

        }
        [Test, Order(23)]
        public void DefaultProductReport()
        {
            var oerderId = OrderLineData.ActiveOrderId;
            this.LogInToAddManagement();
            var repo = PagesRepo.Reports;
            repo.Navigate().DefaultProductReport(oerderId, "All").Export("Excel");

        }
        [Test, Order(24)]
        public void InventoryHistoryReport_ESP()
        {
            //var oerderId = OrderLineData.ActiveOrderId;
            this.LogInToAddManagement();
            var repo = PagesRepo.Reports;
            repo.Navigate().InventoryHistoryReport("ESP").Export("Excel");
        }
        [Test, Order(25)]
        public void InventoryHistoryReport_ESP_Subterms()
        {
            //var oerderId = OrderLineData.ActiveOrderId;
            this.LogInToAddManagement();
            var repo = PagesRepo.Reports;
            repo.Navigate().InventoryHistoryReport("ESP", "BAGS").Export("pdf");
        }

        #endregion
        #region Orders

        [Test, Order(26)]
        public void RenewalButtonShouldNotBeThere()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var data = OrderData.Note;
            AllOrders.Navigate();
            AllOrders.RenewalButtonShouldNotBeThere();
        }
        [Test, Order(27)]
        public void AddNote()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var editOrder = PagesRepo.EditOrder;
            var data = OrderData.Note;
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.OrderId);
            editOrder.DeleteNote(data);
            editOrder.CreateNewNote(data);
        }
        //[Test, Order(28)]
        //public void ExportToPDF()
        //{
        //    this.LogInToAddManagement();
        //    var AllOrders = PagesRepo.AllOrders;
        //    var editOrder = PagesRepo.EditOrder;
        //    AllOrders.Navigate()
        //    .SelectGivenOrderByID(OrderLineData.OrderId);
        //    editOrder.ExportToPDF();
        //}
        #endregion
        #region Order Line Tests
        [Test, Order(29)]
        public void ESP_PFP_OrderLine()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.OrderId);
            PagesRepo.EditOrder.AddNewOrderLine("PFP");
            PagesRepo.EditOrderLine.
                FillOrderLine_Sales(OrderLineData.SaveAndCopy).
                Save();
        }
        [Test, Order(30)]
        public void ESP_Website()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var EditOrder = PagesRepo.EditOrder;
            var data = OrderLineData.ESP_Website;
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.OrderId);
            EditOrder.DeleteResultBannerIfAlreadyExists(data);
            data.DeliveryPreferences = "Standard";
            PagesRepo.EditOrder.AddNewOrderLine("PFP");
            PagesRepo.EditOrderLine.
            FillOrderLine_Sales(data).Save();
        }

        [Test, Order(31)]
        public void MobilePFP()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var EditOrder = PagesRepo.EditOrder;
            var data = OrderLineData.Mobile_PFP;
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.OrderId);
            EditOrder.DeleteResultBannerIfAlreadyExists(data);
            PagesRepo.EditOrder.AddNewOrderLine("PFP");
            PagesRepo.EditOrderLine.
            FillOrderLine_Sales(data).Save();
        }
        [Test, Order(32)]
        public void Homepage_Banner_withStandard()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var EditOrder = PagesRepo.EditOrder;
            var data = OrderLineData.ResultBanner;
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.OrderId);
            data.DeliveryPreferences = "Standard";
            data.AddType = "Homepage Banner";
            data.Position = null;
            data.SearchTerm = "";
            EditOrder.DeleteResultBannerIfAlreadyExists(data);
            PagesRepo.EditOrder.AddNewOrderLine("Banner");
            PagesRepo.EditOrderLine.
            FillOrderLine_Sales(data).Save();
        }
        [Test, Order(33)]
        public void Homepage_Tile_withStandard()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var EditOrder = PagesRepo.EditOrder;
            var data = OrderLineData.ResultBanner;
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.OrderId);
            data.DeliveryPreferences = "Standard";
            data.AddType = "Homepage Tile";
            data.Position = null;
            data.SearchTerm = "";
            EditOrder.DeleteResultBannerIfAlreadyExists(data);
            PagesRepo.EditOrder.AddNewOrderLine("Banner");
            PagesRepo.EditOrderLine.
            FillOrderLine_Sales(data).Save();
        }
        [Test, Order(34)]
        public void ResultBanner()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var EditOrder = PagesRepo.EditOrder;
            var Data = OrderLineData.ResultBanner;
            Data.DeliveryPreferences = "Standard";
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.OrderId);
            EditOrder.DeleteResultBannerIfAlreadyExists(Data);
            PagesRepo.EditOrder.AddNewOrderLine("Banner");
            PagesRepo.EditOrderLine.
            FillOrderLine_Sales(Data).Save();
        }
        [Test, Order(35)]
        public void ResultTile()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var EditOrder = PagesRepo.EditOrder;
            var Data = OrderLineData.ResultTile;
            //Data.DeliveryPreferences = "Standard";
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.OrderId);
            EditOrder.DeleteResultBannerIfAlreadyExists(Data);
            PagesRepo.EditOrder.AddNewOrderLine("Banner");
            PagesRepo.EditOrderLine.
            FillOrderLine_Sales(Data).Save();
        }
        [Test, Order(36)]
        public void ResultTower()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var EditOrder = PagesRepo.EditOrder;
            var Data = OrderLineData.ResultTower;
            Data.DeliveryPreferences = "Standard";
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.ParmanentActiveOrderId);
            EditOrder.DeleteResultBannerIfAlreadyExists(Data);
            PagesRepo.EditOrder.AddNewOrderLine("Banner");
            PagesRepo.EditOrderLine.
            FillOrderLine_Sales(Data).Save();
        }
        [Test, Order(37)]
        public void ESP_Mobile_HomePage_Tile()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var EditOrder = PagesRepo.EditOrder;
            var data = OrderLineData.ResultBanner;
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.OrderId);
            data.DeliveryPreferences = null;
            data.SearchTerm = "";
            data.AddType = "Homepage Tile";
            data.ProductGroup = "ESP Mobile";
            data.Position = null;
            data.SearchTerm = "";
            EditOrder.DeleteResultBannerIfAlreadyExists(data);
            PagesRepo.EditOrder.AddNewOrderLine("Banner");
            PagesRepo.EditOrderLine.
            FillOrderLine_Sales(data).Save();
        }
        [Test, Order(38)]
        public void ESP_Mobile_ResultsBanner()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var EditOrder = PagesRepo.EditOrder;
            var data = OrderLineData.ResultBanner;
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.OrderId);
            data.DeliveryPreferences = null;
            //data.SearchTerm = "";
            data.AddType = "Results Banner";
            data.ProductGroup = "ESP Mobile";
            data.Position = null;
            //data.SearchTerm = "";
            EditOrder.DeleteResultBannerIfAlreadyExists(data);
            PagesRepo.EditOrder.AddNewOrderLine("Banner");
            PagesRepo.EditOrderLine.
            FillOrderLine_Sales(data).Save();
        }
        [Test, Order(39)]
        public void ESP_Mobile_LoginBanner()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var EditOrder = PagesRepo.EditOrder;
            var data = OrderLineData.ResultBanner;
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.OrderId);
            data.DeliveryPreferences = null;
            //data.SearchTerm = "";
            data.AddType = "Login Banner";
            data.ProductGroup = "ESP Mobile";
            data.Position = null;
            data.SearchTerm = "";
            EditOrder.DeleteResultBannerIfAlreadyExists(data);
            PagesRepo.EditOrder.AddNewOrderLine("Banner");
            PagesRepo.EditOrderLine.
            FillOrderLine_Sales(data).Save();
        }
        [Test, Order(40)]
        public void OrderLineDetail_Next_Previous()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var EditOrder = PagesRepo.EditOrder;
            var orderLine = PagesRepo.EditOrderLine;
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.OrderId);
            EditOrder.OpenFirstOrderLine();
            orderLine.Next();
            orderLine.Previous();
            orderLine.GoBackOrderLineByLink();

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
            try
            {

                Wait.UntilLoading();
                Modal.Close();
                try
                {
                    Modal.DirtyclickYes();
                }
                catch (Exception)
                {
                }
                Wait.UntilLoading();
                Modal.dirtCheckClose();
                Element.CheckIfBreakBetweenForm();
                Wait.UntilLoading();
            }
            catch (Exception)
            {
            }
        }
        #endregion


    }
}
