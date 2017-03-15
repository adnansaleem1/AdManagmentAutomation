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
        #region Orders Test cases

        [Test, Order(1)]
        public void CreateOrder()
        {

        }
        [Test, Order(2)]
        public void CreateAddGroup_ESP_PFP()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var editOrder = PagesRepo.EditOrder;
            var data = OrderData.AdGroup;
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.OrderId);
            editOrder.DeleteAddGroup(data);
            editOrder.CreateNewAddGroup(data);
        }
        [Test, Order(3)]
        public void CreateAddGroup_ESP_Website()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var editOrder = PagesRepo.EditOrder;
            var data = OrderData.AdGroup;
            data.ProductGroup = "ESP Websites";
            data.AddType = "PFP";
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.OrderId);
            editOrder.DeleteAddGroup(data);
            editOrder.CreateNewAddGroup(data);
        }
        [Test, Order(4)]
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
        [Test, Order(4)]
        public void ExportToExcel()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var editOrder = PagesRepo.EditOrder;
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.OrderId);
            editOrder.ExportToExcel();
        }
        [Test, Order(5)]
        public void ExportToPDF()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var editOrder = PagesRepo.EditOrder;
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.OrderId);
            editOrder.ExportToPDF();
        }
        [Test, Order(6)]
        public void AddAnAddVerifyTotalUpdate()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var EditOrder = PagesRepo.EditOrder;
            var OrderLine = PagesRepo.EditOrderLine;
            var data = OrderLineData.ESP_Website;
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.OrderId);
            EditOrder.DeleteResultBannerIfAlreadyExists(data);
            int PreAdCount = EditOrder.GetAdCount();
            EditOrder.AddNewOrderLine("PFP");
            OrderLine.
            FillOrderLine(data).Save();
            OrderLine.GoBackToOrderLine();
            if (PreAdCount + 1 != EditOrder.GetAdCount())
            {
                throw new Exception("Add Count Verified.");
            }
        }
        [Test, Order(7)]
        public void ChangeTheStatusOfAd()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var EditOrder = PagesRepo.EditOrder;
            var OrderLine = PagesRepo.EditOrderLine;
            var data = OrderLineData.ESP_Website;
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.OrderId);
            EditOrder.DeleteResultBannerIfAlreadyExists(data);
            int PreAdCount = EditOrder.GetAdCount();
            EditOrder.AddNewOrderLine("PFP");
            OrderLine.
            FillOrderLine(data).Save();
            data.Status = "Completed";
            OrderLine.GoBackToOrderLine();
            EditOrder.OpenOrderLine(data);
            OrderLine.Update(data);
            OrderLine.GoBackToOrderLine();

        }
        [Test, Order(8)]
        public void OrderSummary_ActiveTotal() {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var EditOrder = PagesRepo.EditOrder;
            var OrderLine = PagesRepo.EditOrderLine;
            var data = OrderLineData.ESP_Website;
            AllOrders.ClearFilter();
            Double Active = AllOrders.GetactiveAdAmmount();
            Double Total = AllOrders.GetTotalAdAmmount();
            Double Suspanded = AllOrders.GetSuspandedAdAmmount();
            Double Completed = AllOrders.GetCompletedAdAmmount();
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.ActiveOrderId);
            Double AdTotal=EditOrder.GetTotalCost();
            Double AdActive = EditOrder.GetActiveCost();

        }
        #endregion
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
        [Test, Order(4)]
        public void ResultBanner_WithButOut()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var EditOrder = PagesRepo.EditOrder;
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.OrderId);
            EditOrder.DeleteResultBannerIfAlreadyExists(OrderLineData.ResultBanner);
            PagesRepo.EditOrder.AddNewOrderLine("Banner");
            PagesRepo.EditOrderLine.
            FillOrderLine(OrderLineData.ResultBanner).Save();
        }
        [Test, Order(5)]
        public void ResultTile_WithRemnant()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var EditOrder = PagesRepo.EditOrder;
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.OrderId);
            EditOrder.DeleteResultBannerIfAlreadyExists(OrderLineData.ResultTile);
            PagesRepo.EditOrder.AddNewOrderLine("Banner");
            PagesRepo.EditOrderLine.
            FillOrderLine(OrderLineData.ResultTile).Save();
        }
        [Test, Order(6)]
        public void ResultTower_WithBuyRemaining()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var EditOrder = PagesRepo.EditOrder;
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.OrderId);
            EditOrder.DeleteResultBannerIfAlreadyExists(OrderLineData.ResultTower);
            PagesRepo.EditOrder.AddNewOrderLine("Banner");
            PagesRepo.EditOrderLine.
            FillOrderLine(OrderLineData.ResultTower).Save();
        }
        [Test, Order(7)]
        public void ESP_PFP_withButOut()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var EditOrder = PagesRepo.EditOrder;
            var data = OrderLineData.ESP_PFP;
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.OrderId);
            EditOrder.DeleteResultBannerIfAlreadyExists(data);
            data.DeliveryPreferences = "Buy Out";
            PagesRepo.EditOrder.AddNewOrderLine("PFP");
            PagesRepo.EditOrderLine.
            FillOrderLine(data).Save();
        }
        [Test, Order(8)]
        public void ESP_PFP_withRemnant()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var EditOrder = PagesRepo.EditOrder;
            var data = OrderLineData.ESP_PFP;

            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.OrderId);
            EditOrder.DeleteResultBannerIfAlreadyExists(data);
            data.DeliveryPreferences = "Buy Remnant";
            PagesRepo.EditOrder.AddNewOrderLine("PFP");
            PagesRepo.EditOrderLine.
            FillOrderLine(data).Save();
        }
        [Test, Order(9)]
        public void ESP_PFP_withRemaining()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var EditOrder = PagesRepo.EditOrder;
            var data = OrderLineData.ESP_PFP;
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.OrderId);
            EditOrder.DeleteResultBannerIfAlreadyExists(data);
            data.DeliveryPreferences = "Buy Remaining";
            PagesRepo.EditOrder.AddNewOrderLine("PFP");
            PagesRepo.EditOrderLine.
            FillOrderLine(data).Save();
        }
        [Test, Order(10)]
        public void ESP_Website_withBuyOut()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var EditOrder = PagesRepo.EditOrder;
            var data = OrderLineData.ESP_Website;
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.OrderId);
            EditOrder.DeleteResultBannerIfAlreadyExists(data);
            data.DeliveryPreferences = "Buy Out";
            PagesRepo.EditOrder.AddNewOrderLine("PFP");
            PagesRepo.EditOrderLine.
            FillOrderLine(data).Save();
        }
        [Test, Order(11)]
        public void ESP_Website_withRemnant()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var EditOrder = PagesRepo.EditOrder;
            var data = OrderLineData.ESP_Website;
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.OrderId);
            EditOrder.DeleteResultBannerIfAlreadyExists(data);
            data.DeliveryPreferences = "Buy Remnant";
            PagesRepo.EditOrder.AddNewOrderLine("PFP");
            PagesRepo.EditOrderLine.
            FillOrderLine(data).Save();
        }
        [Test, Order(12)]
        public void ESP_Website_withRemaining()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var EditOrder = PagesRepo.EditOrder;
            var data = OrderLineData.ESP_Website;
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.OrderId);
            EditOrder.DeleteResultBannerIfAlreadyExists(data);
            data.DeliveryPreferences = "Buy Remaining";
            PagesRepo.EditOrder.AddNewOrderLine("PFP");
            PagesRepo.EditOrderLine.
            FillOrderLine(data).Save();
        }
        [Test, Order(13)]
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
            FillOrderLine(data).Save();
        }
        [Test, Order(14)]
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
            FillOrderLine(data).Save();
        }
        [Test, Order(15)]
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
            FillOrderLine(data).Save();
        }
        [Test, Order(16)]
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
            FillOrderLine(data).Save();
        }
        [Test, Order(17)]
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
            FillOrderLine(data).Save();
        }
        [Test, Order(18)]
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
            FillOrderLine(data).Save();
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
        [Test, Order(12)]
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
