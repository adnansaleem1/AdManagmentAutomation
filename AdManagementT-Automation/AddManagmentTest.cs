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
using System.Collections.Generic;

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
            OrderLine.GoBackOrder();
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
            OrderLine.GoBackOrder();
            EditOrder.OpenOrderLine(data);
            OrderLine.Update(data);
            OrderLine.GoBackOrder();

        }
        [Test, Order(8)]
        public void OrderSummary_ActiveTotal()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var EditOrder = PagesRepo.EditOrder;
            var OrderLine = PagesRepo.EditOrderLine;
            var data = OrderLineData.OrderSummary_VerificationData;
            var OrderID = OrderLineData.ActiveOrderId;
            AllOrders.ClearFilter();
            Double Active = AllOrders.GetactiveAdAmmount();
            Double Total = AllOrders.GetTotalAdAmmount();
            Double Suspanded = AllOrders.GetSuspandedAdAmmount();
            Double Completed = AllOrders.GetCompletedAdAmmount();
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderID);
            Double AdTotal = EditOrder.GetTotalCost();
            Double AdActive = EditOrder.GetActiveCost();
            EditOrder.ClearFilter();
            EditOrder.AddNewOrderLine("PFP");
            OrderLine.FillOrderLine(data).Save();
            OrderLine.GoBackOrder();
            Double AdNewTotal = EditOrder.GetTotalCost();
            Double AdNewActive = EditOrder.GetActiveCost();
            EditOrder.GoBackToAllOrders();
            AllOrders.ClearFilter();
            Double NewActive = AllOrders.GetactiveAdAmmount();
            Double NewTotal = AllOrders.GetTotalAdAmmount();
            Double NewSuspanded = AllOrders.GetSuspandedAdAmmount();
            Double NewCompleted = AllOrders.GetCompletedAdAmmount();
            var Cost = data.Cost;
            if (Cost + AdActive != AdNewActive)
            {
                throw new Exception(string.Format("Active Information did't match after add order line --Information: Order ID :{0}  old Active:{1} New Active:{2} Cost:{3} ", OrderID, AdActive, AdNewActive, Cost));
            }
            if (Cost + AdTotal != AdNewTotal)
            {
                throw new Exception(string.Format("Active Information did't match after add order line --Information: Order ID :{0}  old Total:{1} New Total:{2} Cost:{3} ", OrderID, AdTotal, AdNewTotal, Cost));
            }
            if (Cost + Active != NewActive + 1)
            {
                throw new Exception(string.Format("Active Information did't match after add order line(All Orders) --Information: Order ID :{0}  old Active:{1} New Active:{2} Cost:{3} ", OrderID, Active, NewActive, Cost));

            }
            if (Cost + Total != NewTotal + 1)
            {
                throw new Exception(string.Format("Active Information did't match after add order line(All Orders) --Information: Order ID :{0}  old Active:{1} New Active:{2} Cost:{3} ", OrderID, Active, NewActive, Cost));

            }
        }
        [Test, Order(9)]
        public void OrderSummary_Complete()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var EditOrder = PagesRepo.EditOrder;
            var OrderLine = PagesRepo.EditOrderLine;
            var data = OrderLineData.OrderSummary_VerificationData;
            data.Status = "Completed";
            data.ProductId_ManualSelection = null;
            var OrderID = OrderLineData.CompleteOrderId;
            AllOrders.ClearFilter();
            Double Active = AllOrders.GetactiveAdAmmount();
            Double Total = AllOrders.GetTotalAdAmmount();
            Double Suspanded = AllOrders.GetSuspandedAdAmmount();
            Double Completed = AllOrders.GetCompletedAdAmmount();
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderID);
            Double AdTotal = EditOrder.GetTotalCost();
            Double AdActive = EditOrder.GetActiveCost();
            EditOrder.ClearFilter();
            EditOrder.AddNewOrderLine("PFP");
            OrderLine.FillOrderLine(data).Save();
            OrderLine.GoBackOrder();
            Double AdNewTotal = EditOrder.GetTotalCost();
            Double AdNewActive = EditOrder.GetActiveCost();
            EditOrder.GoBackToAllOrders();
            AllOrders.ClearFilter();
            Double NewActive = AllOrders.GetactiveAdAmmount();
            Double NewTotal = AllOrders.GetTotalAdAmmount();
            Double NewSuspanded = AllOrders.GetSuspandedAdAmmount();
            Double NewCompleted = AllOrders.GetCompletedAdAmmount();
            var Cost = data.Cost;
            //if (Cost + AdActive != AdNewActive)
            //{
            //    throw new Exception(string.Format("Active Information did't match after add order line --Information: Order ID :{0}  old Active:{1} New Active:{2} Cost:{3} ", OrderID, AdActive, AdNewActive, Cost));
            //}
            if (Cost + AdTotal != AdNewTotal)
            {
                throw new Exception(string.Format("Active Information did't match after add order line --Information: Order ID :{0}  old Total:{1} New Total:{2} Cost:{3} ", OrderID, AdTotal, AdNewTotal, Cost));
            }
            if (Cost + Completed != NewCompleted)
            {
                throw new Exception(string.Format("Active Information did't match after add order line(All Orders) --Information: Order ID :{0}  old Active:{1} New Active:{2} Cost:{3} ", OrderID, Active, NewActive, Cost));

            }
            if (Cost + Total != NewTotal)
            {
                throw new Exception(string.Format("Active Information did't match after add order line(All Orders) --Information: Order ID :{0}  old Active:{1} New Active:{2} Cost:{3} ", OrderID, Active, NewActive, Cost));

            }
        }
        [Test, Order(9)]
        public void OrderSummary_Suspanded()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var EditOrder = PagesRepo.EditOrder;
            var OrderLine = PagesRepo.EditOrderLine;
            var data = OrderLineData.OrderSummary_VerificationData;
            data.Status = "Completed";
            data.ProductId_ManualSelection = null;
            var OrderID = OrderLineData.SuspandedOrderId;
            AllOrders.ClearFilter();
            Double Active = AllOrders.GetactiveAdAmmount();
            Double Total = AllOrders.GetTotalAdAmmount();
            Double Suspanded = AllOrders.GetSuspandedAdAmmount();
            Double Completed = AllOrders.GetCompletedAdAmmount();
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderID);
            Double AdTotal = EditOrder.GetTotalCost();
            Double AdActive = EditOrder.GetActiveCost();
            EditOrder.ClearFilter();
            EditOrder.AddNewOrderLine("PFP");
            OrderLine.FillOrderLine(data).Save();
            OrderLine.GoBackOrder();
            Double AdNewTotal = EditOrder.GetTotalCost();
            Double AdNewActive = EditOrder.GetActiveCost();
            EditOrder.GoBackToAllOrders();
            AllOrders.ClearFilter();
            Double NewActive = AllOrders.GetactiveAdAmmount();
            Double NewTotal = AllOrders.GetTotalAdAmmount();
            Double NewSuspanded = AllOrders.GetSuspandedAdAmmount();
            Double NewCompleted = AllOrders.GetCompletedAdAmmount();
            var Cost = data.Cost;
            //if (Cost + AdActive != AdNewActive)
            //{
            //    throw new Exception(string.Format("Active Information did't match after add order line --Information: Order ID :{0}  old Active:{1} New Active:{2} Cost:{3} ", OrderID, AdActive, AdNewActive, Cost));
            //}
            if (Cost + AdTotal != AdNewTotal)
            {
                throw new Exception(string.Format("Active Information did't match after add order line --Information: Order ID :{0}  old Total:{1} New Total:{2} Cost:{3} ", OrderID, AdTotal, AdNewTotal, Cost));
            }
            if (Cost + Suspanded != NewSuspanded)
            {
                throw new Exception(string.Format("Active Information did't match after add order line(All Orders) --Information: Order ID :{0}  old Active:{1} New Active:{2} Cost:{3} ", OrderID, Active, NewActive, Cost));

            }
            if (Cost + Total != NewTotal)
            {
                throw new Exception(string.Format("Active Information did't match after add order line(All Orders) --Information: Order ID :{0}  old Active:{1} New Active:{2} Cost:{3} ", OrderID, Active, NewActive, Cost));

            }
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
        [Test, Order(13)]
        public void VerifyPositionUpdateOnBookingAd_ESP_PFP()
        {
            this.LogInToAddManagement();
            var Inventory = PagesRepo.Inventory;
            var Data = InventoryData.PositionUpdate;
            var Allorder = PagesRepo.AllOrders;
            var Editorder = PagesRepo.EditOrder;
            var EditorderLine = PagesRepo.EditOrderLine;
            var PostionHistoyList = new List<int>();
            var PostionNewList = new List<int>();
            var OrderLineposData = OrderLineData.InventoryBookedVerification;
            Inventory.Navigate()
                .FillSearch(Data)
                .Search();
            for (var count = 1; count < 10; count++)
            {
                PostionHistoyList.Add(Inventory.GetPositionInventory(count, Data.SearchTerms[0]));
            }
            int? InventoryBookCount = OrderLineposData.Impressions;
            Allorder.Navigate();
            Allorder.ClearFilter();
            Allorder.SelectGivenOrderByID(OrderLineData.ActiveOrderId);
            Editorder.AddNewOrderLine("PFP");
            for (var count = 1; count < 10; count++)
            {
                OrderLineposData.Position = count;
                EditorderLine.FillOrderLine(OrderLineposData);
                EditorderLine.SaveAndAdd();
                EditorderLine.VerifySaveAndAdd();
            }
            Inventory.Navigate()
            .FillSearch(Data)
            .Search();
            for (var count = 1; count < 10; count++)
            {
                PostionNewList.Add(Inventory.GetPositionInventory(count, Data.SearchTerms[0]));
            }
            for (var count = 0; count < 9; count++)
            {
                if (PostionNewList[count] + InventoryBookCount != PostionHistoyList[count])
                {
                    throw new Exception(string.Format("Unable to verify postion update for booked inventory on position : {0}", count));
                }
            }
            Allorder.Navigate();
            Allorder.SelectGivenOrderByID(OrderLineData.ActiveOrderId);
            Editorder.DeleteAllOrderLine(OrderLineposData);
        }
        [Test, Order(14)]
        public void VerifyPositionUpdateOnBookingAd_ESP_PFP_WebSites()
        {
            this.LogInToAddManagement();
            var Inventory = PagesRepo.Inventory;
            var Data = InventoryData.PositionUpdate;
            var Allorder = PagesRepo.AllOrders;
            var Editorder = PagesRepo.EditOrder;
            var EditorderLine = PagesRepo.EditOrderLine;
            var PostionHistoyList = new List<int>();
            var PostionNewList = new List<int>();
            var OrderLineposData = OrderLineData.InventoryBookedVerification;
            Data.SearchTerms = new List<string>() { "PERFORMANCE APPAREL" };
            Data.ProductGroup = "ESP Websites";
            OrderLineposData.ProductInformation = "PERFORMANCE APPAREL";
            OrderLineposData.SearchTerm = "PERFORMANCE APPAREL";
            OrderLineposData.ProductGroup = "ESP Websites";
            Inventory.Navigate()
                .FillSearch(Data)
                .Search();
            for (var count = 1; count < 5; count++)
            {
                PostionHistoyList.Add(Inventory.GetPositionInventory(count, Data.SearchTerms[0]));
            }
            int? InventoryBookCount = OrderLineposData.Impressions;
            Allorder.Navigate();
            Allorder.ClearFilter();
            Allorder.SelectGivenOrderByID(OrderLineData.ActiveOrderId);
            Editorder.AddNewOrderLine("PFP");
            for (var count = 1; count < 5; count++)
            {
                OrderLineposData.Position = count;
                EditorderLine.FillOrderLine(OrderLineposData);
                EditorderLine.SaveAndAdd();
                EditorderLine.VerifySaveAndAdd();
            }
            Inventory.Navigate()
            .FillSearch(Data)
            .Search();
            for (var count = 1; count < 5; count++)
            {
                PostionNewList.Add(Inventory.GetPositionInventory(count, Data.SearchTerms[0]));
            }
            for (var count = 0; count < 4; count++)
            {
                if (PostionNewList[count] + InventoryBookCount != PostionHistoyList[count])
                {
                    throw new Exception(string.Format("Unable to verify postion update for booked inventory on position : {0}", count));
                }
            }
            Allorder.Navigate();
            Allorder.SelectGivenOrderByID(OrderLineData.ActiveOrderId);
            Editorder.DeleteAllOrderLine(OrderLineposData);
        }
        [Test, Order(16)]
        public void VerifyPositionUpdateOnBookingAd_Result_Banner()
        {
            this.LogInToAddManagement();
            var Inventory = PagesRepo.Inventory;
            var Data = InventoryData.PositionUpdate;
            var Allorder = PagesRepo.AllOrders;
            var Editorder = PagesRepo.EditOrder;
            var EditorderLine = PagesRepo.EditOrderLine;
            var PostionHistoyList = new List<int>();
            var PostionNewList = new List<int>();
            var OrderLineposData = OrderLineData.InventoryBookedVerification;
            Data.SearchTerms = new List<string>() { "RUGS" };
            Data.ProductGroup = "ESP";
            Data.AdType = "Banner";
            OrderLineposData.ProductInformation = "RUGS";
            OrderLineposData.SearchTerm = "RUGS";
            OrderLineposData.ProductGroup = "ESP";
            OrderLineposData.AddType = "Results Banner";
            OrderLineposData.Position = null;
            OrderLineposData.DeliveryPreferences = null;
            OrderLineposData.ProductId_ManualSelection = null;
            Inventory.Navigate()
                .ChangeInventoryType("Banners")
                .FillSearch(Data)
                .Search();
            int ResultBanner = Inventory.GetPositionInventory(1, Data.SearchTerms[0]);
            //int Tile_1 = Inventory.GetPositionInventory(2, Data.SearchTerms[0]);
            //int Tile_2 = Inventory.GetPositionInventory(3, Data.SearchTerms[0]);
            //int Tile_3 = Inventory.GetPositionInventory(4, Data.SearchTerms[0]);
            //int Result_Tower = Inventory.GetPositionInventory(5, Data.SearchTerms[0]);
            int? InventoryBookCount = OrderLineposData.Impressions;
            Allorder.Navigate();
            Allorder.ClearFilter();
            Allorder.SelectGivenOrderByID(OrderLineData.ActiveOrderId);
            Editorder.AddNewOrderLine("Banner");

            //OrderLineposData.Position = count;
            EditorderLine.FillOrderLine(OrderLineposData);
            EditorderLine.SaveAndAdd();
            EditorderLine.VerifySaveAndAdd();
            //OrderLineposData.AddType = "Results Tile";
            //for (var count = 1; count < 4; count++)
            //{
            //    OrderLineposData.Position = count;
            //    EditorderLine.FillOrderLine(OrderLineposData);
            //    EditorderLine.SaveAndAdd();
            //    EditorderLine.VerifySaveAndAdd();
            //}
            //OrderLineposData.AddType = "Results Tower";
            //OrderLineposData.Position = null;

            //EditorderLine.FillOrderLine(OrderLineposData);
            //EditorderLine.SaveAndAdd();
            //EditorderLine.VerifySaveAndAdd();
            Inventory.Navigate()
            .ChangeInventoryType("Banners")
            .FillSearch(Data)
            .Search();
            int NewResultBanner = Inventory.GetPositionInventory(1, Data.SearchTerms[0]);
            //int NewTile_1 = Inventory.GetPositionInventory(2, Data.SearchTerms[0]);
            //int NewTile_2 = Inventory.GetPositionInventory(3, Data.SearchTerms[0]);
            //int NewTile_3 = Inventory.GetPositionInventory(4, Data.SearchTerms[0]);
            //int NewResult_Tower = Inventory.GetPositionInventory(5, Data.SearchTerms[0]);
            if (NewResultBanner + InventoryBookCount != ResultBanner) {
                throw new Exception(string.Format("Unable to verify postion update for booked inventory in Tile,Result Banner,"));

            }
            Allorder.Navigate();
            Allorder.SelectGivenOrderByID(OrderLineData.ActiveOrderId);
            Editorder.DeleteAllOrderLine(OrderLineposData);
        }

        [Test, Order(17)]
        public void VerifyPositionUpdateOnBookingAd_Result_Banner_Tile()
        {
            this.LogInToAddManagement();
            var Inventory = PagesRepo.Inventory;
            var Data = InventoryData.PositionUpdate;
            var Allorder = PagesRepo.AllOrders;
            var Editorder = PagesRepo.EditOrder;
            var EditorderLine = PagesRepo.EditOrderLine;
            var PostionHistoyList = new List<int>();
            var PostionNewList = new List<int>();
            var OrderLineposData = OrderLineData.InventoryBookedVerification;
            Data.SearchTerms = new List<string>() { "RUGS" };
            Data.ProductGroup = "ESP";
            Data.AdType = "Banner";
            OrderLineposData.ProductInformation = "RUGS";
            OrderLineposData.SearchTerm = "RUGS";
            OrderLineposData.ProductGroup = "ESP";
            OrderLineposData.AddType = "Results Banner";
            OrderLineposData.Position = null;
            OrderLineposData.DeliveryPreferences = null;
            OrderLineposData.ProductId_ManualSelection = null;
            Inventory.Navigate()
                .ChangeInventoryType("Banners")
                .FillSearch(Data)
                .Search();
            //int ResultBanner = Inventory.GetPositionInventory(1, Data.SearchTerms[0]);
            int Tile_1 = Inventory.GetPositionInventory(2, Data.SearchTerms[0]);
            int Tile_2 = Inventory.GetPositionInventory(3, Data.SearchTerms[0]);
            int Tile_3 = Inventory.GetPositionInventory(4, Data.SearchTerms[0]);
            //int Result_Tower = Inventory.GetPositionInventory(5, Data.SearchTerms[0]);
            int? InventoryBookCount = OrderLineposData.Impressions;
            Allorder.Navigate();
            Allorder.ClearFilter();
            Allorder.SelectGivenOrderByID(OrderLineData.ActiveOrderId);
            Editorder.AddNewOrderLine("Banner");

            //OrderLineposData.Position = count;
            //EditorderLine.FillOrderLine(OrderLineposData);
            //EditorderLine.SaveAndAdd();
            //EditorderLine.VerifySaveAndAdd();
            OrderLineposData.AddType = "Results Tile";
            for (var count = 1; count < 4; count++)
            {
                OrderLineposData.Position = count;
                EditorderLine.FillOrderLine(OrderLineposData);
                EditorderLine.SaveAndAdd();
                EditorderLine.VerifySaveAndAdd();
            }
            //OrderLineposData.AddType = "Results Tower";
            //OrderLineposData.Position = null;

            //EditorderLine.FillOrderLine(OrderLineposData);
            //EditorderLine.SaveAndAdd();
            //EditorderLine.VerifySaveAndAdd();
            Inventory.Navigate()
            .ChangeInventoryType("Banners")
            .FillSearch(Data)
            .Search();
            //int NewResultBanner = Inventory.GetPositionInventory(1, Data.SearchTerms[0]);
            int NewTile_1 = Inventory.GetPositionInventory(2, Data.SearchTerms[0]);
            int NewTile_2 = Inventory.GetPositionInventory(3, Data.SearchTerms[0]);
            int NewTile_3 = Inventory.GetPositionInventory(4, Data.SearchTerms[0]);
            //int NewResult_Tower = Inventory.GetPositionInventory(5, Data.SearchTerms[0]);
            if (NewTile_1 + InventoryBookCount != Tile_1 || NewTile_2 + InventoryBookCount != Tile_2 || NewTile_3 + InventoryBookCount != Tile_3)
            {
                throw new Exception(string.Format("Unable to verify postion update for booked inventory in Tile,Result Banner,"));

            }
            Allorder.Navigate();
            Allorder.SelectGivenOrderByID(OrderLineData.ActiveOrderId);
            Editorder.DeleteAllOrderLine(OrderLineposData);
        }
        [Test, Order(18)]
        public void VerifyPositionUpdateOnBookingAd_Result_Banner_Tower()
        {
            this.LogInToAddManagement();
            var Inventory = PagesRepo.Inventory;
            var Data = InventoryData.PositionUpdate;
            var Allorder = PagesRepo.AllOrders;
            var Editorder = PagesRepo.EditOrder;
            var EditorderLine = PagesRepo.EditOrderLine;
            var PostionHistoyList = new List<int>();
            var PostionNewList = new List<int>();
            var OrderLineposData = OrderLineData.InventoryBookedVerification;
            Data.SearchTerms = new List<string>() { "RUGS" };
            Data.ProductGroup = "ESP";
            Data.AdType = "Banner";
            OrderLineposData.ProductInformation = "RUGS";
            OrderLineposData.SearchTerm = "RUGS";
            OrderLineposData.ProductGroup = "ESP";
            OrderLineposData.AddType = "Results Banner";
            OrderLineposData.Position = null;
            OrderLineposData.DeliveryPreferences = null;
            OrderLineposData.ProductId_ManualSelection = null;
            Inventory.Navigate()
                .ChangeInventoryType("Banners")
                .FillSearch(Data)
                .Search();
            //int ResultBanner = Inventory.GetPositionInventory(1, Data.SearchTerms[0]);
            //int Tile_1 = Inventory.GetPositionInventory(2, Data.SearchTerms[0]);
            //int Tile_2 = Inventory.GetPositionInventory(3, Data.SearchTerms[0]);
            //int Tile_3 = Inventory.GetPositionInventory(4, Data.SearchTerms[0]);
            int Result_Tower = Inventory.GetPositionInventory(5, Data.SearchTerms[0]);
            int? InventoryBookCount = OrderLineposData.Impressions;
            Allorder.Navigate();
            Allorder.ClearFilter();
            Allorder.SelectGivenOrderByID(OrderLineData.ActiveOrderId);
            Editorder.AddNewOrderLine("Banner");

            //OrderLineposData.Position = count;
            //EditorderLine.FillOrderLine(OrderLineposData);
            //EditorderLine.SaveAndAdd();
            //EditorderLine.VerifySaveAndAdd();
            //OrderLineposData.AddType = "Results Tile";
            //for (var count = 1; count < 4; count++)
            //{
            //    OrderLineposData.Position = count;
            //    EditorderLine.FillOrderLine(OrderLineposData);
            //    EditorderLine.SaveAndAdd();
            //    EditorderLine.VerifySaveAndAdd();
            //}
            OrderLineposData.AddType = "Results Tower";
            OrderLineposData.Position = null;

            EditorderLine.FillOrderLine(OrderLineposData);
            EditorderLine.SaveAndAdd();
            EditorderLine.VerifySaveAndAdd();
            Inventory.Navigate()
            .ChangeInventoryType("Banners")
            .FillSearch(Data)
            .Search();
            //int NewResultBanner = Inventory.GetPositionInventory(1, Data.SearchTerms[0]);
            //int NewTile_1 = Inventory.GetPositionInventory(2, Data.SearchTerms[0]);
            //int NewTile_2 = Inventory.GetPositionInventory(3, Data.SearchTerms[0]);
            //int NewTile_3 = Inventory.GetPositionInventory(4, Data.SearchTerms[0]);
            int NewResult_Tower = Inventory.GetPositionInventory(5, Data.SearchTerms[0]);
            if (NewResult_Tower + InventoryBookCount != Result_Tower)
            {
                throw new Exception(string.Format("Unable to verify postion update for booked inventory in Tile,Result Banner,"));

            }
            Allorder.Navigate();
            Allorder.SelectGivenOrderByID(OrderLineData.ActiveOrderId);
            Editorder.DeleteAllOrderLine(OrderLineposData);
        }
        [Test, Order(19)]
        public void InventoryImpact_Buyout() {
            this.LogInToAddManagement();
            var Inventory = PagesRepo.Inventory;
            var Data = InventoryData.PositionUpdate;
            var Allorder = PagesRepo.AllOrders;
            var Editorder = PagesRepo.EditOrder;
            var EditorderLine = PagesRepo.EditOrderLine;
            var PostionHistoyList = new List<int>();
            var PostionNewList = new List<int>();
            var OrderLineposData = OrderLineData.InventoryBookedVerification;
            Data.SearchTerms = new List<string>() { "RUGS" };
            Data.ProductGroup = "ESP";
            Data.AdType = "Banner";
            OrderLineposData.ProductInformation = "RUGS";
            OrderLineposData.SearchTerm = "RUGS";
            OrderLineposData.ProductGroup = "ESP";
            OrderLineposData.AddType = "Results Banner";
            OrderLineposData.Position = null;
            OrderLineposData.DeliveryPreferences = "Buy Out";
            OrderLineposData.Impressions = null;
            OrderLineposData.ProductId_ManualSelection = null;
            Inventory.Navigate()
                .ChangeInventoryType("Banners")
                .FillSearch(Data)
                .Search();
            int ResultBanner = Inventory.GetPositionInventory(1, Data.SearchTerms[0]);
            int? InventoryBookCount = OrderLineposData.Impressions;
            Allorder.Navigate();
            Allorder.ClearFilter();
            Allorder.SelectGivenOrderByID(OrderLineData.ActiveOrderId);
            Editorder.AddNewOrderLine("Banner");
            EditorderLine.FillOrderLine(OrderLineposData);
            EditorderLine.SaveAndAdd();
            EditorderLine.VerifySaveAndAdd();
            Inventory.Navigate()
            .ChangeInventoryType("Banners")
            .FillSearch(Data)
            .Search();
            int NewResultBanner = Inventory.GetPositionInventory(1, Data.SearchTerms[0]);
            if (NewResultBanner !=0)
            {
                throw new Exception(string.Format("Unable to Buy Out inventory in Result Banner"));

            }
            Allorder.Navigate();
            Allorder.SelectGivenOrderByID(OrderLineData.ActiveOrderId);
            Editorder.DeleteAllOrderLine(OrderLineposData);
        }
        [Test, Order(20)]
        public void InventoryImpact_BuyRemaning()
        {
            this.LogInToAddManagement();
            var Inventory = PagesRepo.Inventory;
            var Data = InventoryData.PositionUpdate;
            var Allorder = PagesRepo.AllOrders;
            var Editorder = PagesRepo.EditOrder;
            var EditorderLine = PagesRepo.EditOrderLine;
            var PostionHistoyList = new List<int>();
            var PostionNewList = new List<int>();
            var OrderLineposData = OrderLineData.InventoryBookedVerification;
            Data.SearchTerms = new List<string>() { "RUGS" };
            Data.ProductGroup = "ESP";
            Data.AdType = "Banner";
            OrderLineposData.ProductInformation = "RUGS";
            OrderLineposData.SearchTerm = "RUGS";
            OrderLineposData.ProductGroup = "ESP";
            OrderLineposData.AddType = "Results Banner";
            OrderLineposData.Position = null;
            OrderLineposData.DeliveryPreferences = "Buy Remaining";
            OrderLineposData.Impressions = null;
            OrderLineposData.ProductId_ManualSelection = null;
            Inventory.Navigate()
                .ChangeInventoryType("Banners")
                .FillSearch(Data)
                .Search();
            int ResultBanner = Inventory.GetPositionInventory(1, Data.SearchTerms[0]);
            int? InventoryBookCount = OrderLineposData.Impressions;
            Allorder.Navigate();
            Allorder.ClearFilter();
            Allorder.SelectGivenOrderByID(OrderLineData.ActiveOrderId);
            Editorder.AddNewOrderLine("Banner");
            EditorderLine.FillOrderLine(OrderLineposData);
            EditorderLine.SaveAndAdd();
            EditorderLine.VerifySaveAndAdd();
            Inventory.Navigate()
            .ChangeInventoryType("Banners")
            .FillSearch(Data)
            .Search();
            int NewResultBanner = Inventory.GetPositionInventory(1, Data.SearchTerms[0]);
            if (NewResultBanner != 0)
            {
                throw new Exception(string.Format("Unable to Buy Out inventory in Result Banner"));

            }
            Allorder.Navigate();
            Allorder.SelectGivenOrderByID(OrderLineData.ActiveOrderId);
            Editorder.DeleteAllOrderLine(OrderLineposData);
        }
        [Test, Order(21)]
        public void InventoryImpact_BuyRemnant()
        {
            this.LogInToAddManagement();
            var Inventory = PagesRepo.Inventory;
            var Data = InventoryData.PositionUpdate;
            var Allorder = PagesRepo.AllOrders;
            var Editorder = PagesRepo.EditOrder;
            var EditorderLine = PagesRepo.EditOrderLine;
            var PostionHistoyList = new List<int>();
            var PostionNewList = new List<int>();
            var OrderLineposData = OrderLineData.InventoryBookedVerification;
            Data.SearchTerms = new List<string>() { "RUGS" };
            Data.ProductGroup = "ESP";
            Data.AdType = "Banner";
            OrderLineposData.ProductInformation = "RUGS";
            OrderLineposData.SearchTerm = "RUGS";
            OrderLineposData.ProductGroup = "ESP";
            OrderLineposData.AddType = "Results Banner";
            OrderLineposData.Position = null;
            OrderLineposData.DeliveryPreferences = "Buy Remnant";
            OrderLineposData.Impressions = null;
            OrderLineposData.ProductId_ManualSelection = null;
            Inventory.Navigate()
                .ChangeInventoryType("Banners")
                .FillSearch(Data)
                .Search();
            int ResultBanner = Inventory.GetPositionInventory(1, Data.SearchTerms[0]);
            int? InventoryBookCount = OrderLineposData.Impressions;
            Allorder.Navigate();
            Allorder.ClearFilter();
            Allorder.SelectGivenOrderByID(OrderLineData.ActiveOrderId);
            Editorder.AddNewOrderLine("Banner");
            EditorderLine.FillOrderLine(OrderLineposData);
            EditorderLine.SaveAndAdd();
            EditorderLine.VerifySaveAndAdd();
            Inventory.Navigate()
            .ChangeInventoryType("Banners")
            .FillSearch(Data)
            .Search();
            int NewResultBanner = Inventory.GetPositionInventory(1, Data.SearchTerms[0]);
            if (ResultBanner != NewResultBanner)
            {
                throw new Exception(string.Format("Unable to Buy Out inventory in Result Banner"));

            }
            Allorder.Navigate();
            Allorder.SelectGivenOrderByID(OrderLineData.ActiveOrderId);
            Editorder.DeleteAllOrderLine(OrderLineposData);
        }
        #endregion
        #region Wait List Region
        [Test, Order(1)]
        public void ESP_PFP_ItemToWaitList()
        {
            this.LogInToAddManagement();
            var WaitList=PagesRepo.Waitlistpage;
            var Data = WaitListData.EspPfp;
            WaitList.Navigate().
                OpenNewWaitListPage().
                FillNewWaitList(Data).
                VerifySaveNewAdd();
        }
        [Test, Order(2)]
        public void ESP_Website_PFP_ItemToWaitList()
        {
            this.LogInToAddManagement();
            var WaitList = PagesRepo.Waitlistpage;
            var Data = WaitListData.EspPfp;
            Data.ProductGroup = "ESP Websites";
            Data.AddType = "PFP";
            WaitList.Navigate().
                OpenNewWaitListPage().
                FillNewWaitList(Data).
                VerifySaveNewAdd();
        }
        [Test, Order(3)]
        public void ESP_Result_Banner_ItemToWaitList()
        {
            this.LogInToAddManagement();
            var WaitList = PagesRepo.Waitlistpage;
            var Data = WaitListData.EspPfp;
            Data.ProductGroup = "ESP";
            Data.AddType = "Results Banner";
            Data.Position = null;
            Data.defaultProduct = "";
            WaitList.Navigate().
                OpenNewWaitListPage().
                FillNewWaitList(Data).
                VerifySaveNewAdd();
        }
        [Test, Order(4)]
        public void Mobile_PFP_ItemToWaitList()
        {
            this.LogInToAddManagement();
            var WaitList = PagesRepo.Waitlistpage;
            var Data = WaitListData.EspPfp;
            Data.ProductGroup = "ESP Mobile";
            Data.SearchTerms = "ANTIBACTERIAL PRODUCTS";
            Data.AddType = "PFP";
            Data.Position = null;
            Data.DeliveryPrefrnces = "";
            Data.Cost = null;
           // Data.defaultProduct = "";
            WaitList.Navigate().
                OpenNewWaitListPage().
                FillNewWaitList(Data).
                VerifySaveNewAdd();
        }
        [Test, Order(5)]
        public void MobileBanner_ItemToWaitList()
        {
            this.LogInToAddManagement();
            var WaitList = PagesRepo.Waitlistpage;
            var Data = WaitListData.EspPfp;
            Data.ProductGroup = "ESP Mobile";
            Data.AddType = "Results Banner";
            Data.Position = null;
            Data.Cost = null;
            //Data.defaultProduct = "";
            WaitList.Navigate().
                OpenNewWaitListPage().
                FillNewWaitList(Data).
                VerifySaveNewAdd();
        }
        [Test, Order(6)]
        public void Edit_WaitList()
        {
            this.LogInToAddManagement();
            var WaitList = PagesRepo.Waitlistpage;
            var Data = WaitListData.EspPfp;
            Data.defaultProduct=Data.defaultProduct+"Test 1";
            //Data.defaultProduct = "";
            WaitList.Navigate().EditWaitList(Data);
        }
        [Test, Order(7)]
        public void WaitList_Notification()
        {
            this.LogInToAddManagement();
            var WaitList = PagesRepo.Waitlistpage;
            var Data = WaitListData.EspPfp;
            Data.defaultProduct = Data.defaultProduct + "Test 1";
            //Data.defaultProduct = "";
            WaitList.Navigate().Notify(Data);
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
