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
using AddManagmentData.Model.Admin;
using SeleniumExtension.Controls;
using SeleniumExtension.Utilties;

namespace AdManagementT_Automation
{
    [TestFixture]
    public class AddManagmentTest
    {
        int baseOrder = 1;
        private IWebDriver driver = null;
        private LoginPage LoginPageManager = null;

        #region Inventory Tests
        [Test, Order(AddUserData.TO + 1)]
        public void Inventory_SearchWithTermsIncludingSubterms()
        {
            this.LogInToAddManagement();
            var Inventory = PagesRepo.Inventory;
            Inventory.Navigate()
                .FillSearch(InventoryData.ForBags)
                .Search()
                .VerifySearchResultWithSubTerms(InventoryData.ForBags.SearchTerms[0]);
        }
        [Test, Order(AddUserData.TO + 2)]
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
        [Test, Order(AddUserData.TO + 3)]
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
            OrderLineposData.ProductId_ManualSelection = OrderLineData.ParmanentActiveProductId == "" ? OrderLineposData.ProductId_ManualSelection : new List<string>() { OrderLineData.ParmanentActiveProductId };
            Allorder.SelectGivenOrderByID(OrderLineData.ParmanentActiveOrderId);
            Editorder.AddNewOrderLine("PFP");
            for (var count = 1; count < 10; count++)
            {
                OrderLineposData.Position = count;
                EditorderLine.FillOrderLine_Admin(OrderLineposData);
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
        [Test, Order(AddUserData.TO + 4)]
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
            OrderLineposData.ProductId_ManualSelection = OrderLineData.ParmanentActiveProductId == "" ? OrderLineposData.ProductId_ManualSelection : new List<string>() { OrderLineData.ParmanentActiveProductId };
            Allorder.SelectGivenOrderByID(OrderLineData.ParmanentActiveOrderId);
            Editorder.AddNewOrderLine("PFP");
            for (var count = 1; count < 5; count++)
            {
                OrderLineposData.Position = count;
                EditorderLine.FillOrderLine_Admin(OrderLineposData);
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
        [Test, Order(AddUserData.TO + 5)]
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
            Data.SearchTerms = new List<string>() { "ANTIMICROBIAL ENHANCED PRODUCTS" };
            Data.ProductGroup = "ESP";
            Data.AdType = "Banner";
            OrderLineposData.ProductInformation = "ANTIMICROBIAL ENHANCED PRODUCTS";
            OrderLineposData.SearchTerm = "ANTIMICROBIAL ENHANCED PRODUCTS";
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
            Allorder.SelectGivenOrderByID(OrderLineData.ParmanentActiveOrderId);
            Editorder.AddNewOrderLine("Banner");

            //OrderLineposData.Position = count;
            EditorderLine.FillOrderLine_Admin(OrderLineposData);
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
            if (NewResultBanner + InventoryBookCount != ResultBanner)
            {
                throw new Exception(string.Format("Unable to verify postion update for booked inventory in Tile,Result Banner,"));

            }
            Allorder.Navigate();
            Allorder.SelectGivenOrderByID(OrderLineData.ActiveOrderId);
            Editorder.DeleteAllOrderLine(OrderLineposData);
        }
        [Test, Order(AddUserData.TO + 6)]
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
            Data.SearchTerms = new List<string>() { "ANTIMICROBIAL ENHANCED PRODUCTS" };
            Data.ProductGroup = "ESP";
            Data.AdType = "Banner";
            OrderLineposData.ProductInformation = "ANTIMICROBIAL ENHANCED PRODUCTS";
            OrderLineposData.SearchTerm = "ANTIMICROBIAL ENHANCED PRODUCTS";
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
            Allorder.SelectGivenOrderByID(OrderLineData.ParmanentActiveOrderId);
            Editorder.AddNewOrderLine("Banner");

            //OrderLineposData.Position = count;
            //EditorderLine.FillOrderLine(OrderLineposData);
            //EditorderLine.SaveAndAdd();
            //EditorderLine.VerifySaveAndAdd();
            OrderLineposData.AddType = "Results Tile";
            for (var count = 1; count < 4; count++)
            {
                OrderLineposData.Position = count;
                EditorderLine.FillOrderLine_Admin(OrderLineposData);
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
        [Test, Order(AddUserData.TO + 7)]
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
            Data.SearchTerms = new List<string>() { "ANTIMICROBIAL ENHANCED PRODUCTS" };
            Data.ProductGroup = "ESP";
            Data.AdType = "Banner";
            OrderLineposData.ProductInformation = "ANTIMICROBIAL ENHANCED PRODUCTS";
            OrderLineposData.SearchTerm = "ANTIMICROBIAL ENHANCED PRODUCTS";
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
            Allorder.SelectGivenOrderByID(OrderLineData.ParmanentActiveOrderId);
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

            EditorderLine.FillOrderLine_Admin(OrderLineposData);
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
        [Test, Order(AddUserData.TO + 8)]
        public void InventoryImpact_Buyout()
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
            Data.SearchTerms = new List<string>() { "ANTIMICROBIAL ENHANCED PRODUCTS" };
            Data.ProductGroup = "ESP";
            Data.AdType = "Banner";
            OrderLineposData.ProductInformation = "ANTIMICROBIAL ENHANCED PRODUCTS";
            OrderLineposData.SearchTerm = "ANTIMICROBIAL ENHANCED PRODUCTS";
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
            Allorder.SelectGivenOrderByID(OrderLineData.ParmanentActiveOrderId);
            Editorder.AddNewOrderLine("Banner");
            EditorderLine.FillOrderLine_Admin(OrderLineposData);
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
        [Test, Order(AddUserData.TO + 9)]
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
            Allorder.SelectGivenOrderByID(OrderLineData.ParmanentActiveOrderId);
            Editorder.AddNewOrderLine("Banner");
            EditorderLine.FillOrderLine_Admin(OrderLineposData);
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
        [Test, Order(AddUserData.TO + 10)]
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
            Allorder.SelectGivenOrderByID(OrderLineData.ParmanentActiveOrderId);
            Editorder.AddNewOrderLine("Banner");
            EditorderLine.FillOrderLine_Admin(OrderLineposData);
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
        [Test, Order(AddUserData.TO + 11)]
        public void ESP_PFP_Proposal()
        {
            this.LogInToAddManagement();
            var ProposalPage = PagesRepo.CreatePropsal;
            var Data = InventoryData.CreateProposal;
            ProposalPage.Navigate()
                .CreateProposal(Data);

        }
        #endregion
        #region Wait List Region
        [Test, Order(AddUserData.TO + 12)]
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
        [Test, Order(AddUserData.TO + 13)]
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
        [Test, Order(AddUserData.TO + 14)]
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
        [Test, Order(AddUserData.TO + 15)]
        public void Mobile_PFP_ItemToWaitList()
        {
            var AllOrders = PagesRepo.AllOrders;
            var EditOrder = PagesRepo.EditOrder;
            var OrderLinedata = OrderLineData.Mobile_PFP;
            var WaitListdata = WaitListData.EspPfp;
            WaitListdata.ProductGroup = "ESP Mobile";
            WaitListdata.SearchTerms = "ANTIBACTERIAL PRODUCTS";
            WaitListdata.AddType = "PFP";
            WaitListdata.Position = null;
            WaitListdata.DeliveryPrefrnces = "";
            WaitListdata.Cost = null;
            OrderLinedata.ProductInformation=OrderLinedata.SearchTerm = WaitListdata.SearchTerms;
            OrderLinedata.Priority = null;
            OrderLinedata.ProductId_ManualSelection = OrderLineData.ParmanentActiveProductId == "" ? OrderLinedata.ProductId_ManualSelection : new List<string>() { OrderLineData.ParmanentActiveProductId };

            this.LogInToAddManagement();
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.ParmanentActiveOrderId);
            EditOrder.DeleteResultBannerIfAlreadyExists(OrderLinedata);
            PagesRepo.EditOrder.AddNewOrderLine("PFP");
            PagesRepo.EditOrderLine.
            FillOrderLine_Admin(OrderLinedata).Save();
            var WaitList = PagesRepo.Waitlistpage;
            WaitList.Navigate().
                OpenNewWaitListPage().
                FillNewWaitList(WaitListdata).
                VerifySaveNewAdd();
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.OrderId);
            EditOrder.DeleteResultBannerIfAlreadyExists(OrderLinedata);
        }
        [Test, Order(AddUserData.TO + 16)]
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
            .SelectGivenOrderByID(OrderLineData.ParmanentActiveOrderId);
            data.DeliveryPreferences = null;
            data.AddType = "Results Banner";
            data.ProductGroup = "ESP Mobile";
            data.Position = null;

            data.ProductInformation=data.SearchTerm = Data.SearchTerms;
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
        [Test, Order(AddUserData.TO + 17)]
        public void Edit_WaitList()
        {
            this.LogInToAddManagement();
            var WaitList = PagesRepo.Waitlistpage;
            var Data = WaitListData.EspPfp;
            Data.defaultProduct = Data.defaultProduct + "Test 1";
            //Data.defaultProduct = "";
            WaitList.Navigate().EditWaitList(Data);
        }
        [Test, Order(AddUserData.TO + 18)]
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
        #region CreateProposal Page

        [Test, Order(AddUserData.TO + 19)]
        public void CreateProposal_ESP_PFP_Proposal()
        {
            this.LogInToAddManagement();
            var ProposalPage = PagesRepo.CreatePropsal;
            var Data = InventoryData.CreateProposal;
            ProposalPage.Navigate()
                .CreateProposal(Data);

        }
        [Test, Order(AddUserData.TO + 20)]
        public void ESP_Websites_PFP_Proposal()
        {
            this.LogInToAddManagement();
            var ProposalPage = PagesRepo.CreatePropsal;
            var Data = InventoryData.CreateProposal;
            Data.ProductGroup = "ESP Websites";
            ProposalPage.Navigate()
                .CreateProposal(Data);

        }
        [Test, Order(AddUserData.TO + 21)]
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
        [Test, Order(AddUserData.TO + 22)]
        public void CreateProposal_SaveAndCopy()
        {
            this.LogInToAddManagement();
            var ProposalPage = PagesRepo.CreatePropsal;
            var Data = InventoryData.CreateProposal;
            ProposalPage.Navigate()
                .CreateProposalSaveAndCopy(Data);

        }
        [Test, Order(AddUserData.TO + 23)]
        public void CreateProposal_SaveAndAdd()
        {
            this.LogInToAddManagement();
            var ProposalPage = PagesRepo.CreatePropsal;
            var Data = InventoryData.CreateProposal;
            ProposalPage.Navigate()
                .CreateProposalSaveAndAdd(Data);

        }
        [Test, Order(AddUserData.TO + 24)]
        public void Mobile_PFP_Proposal()
        {
            this.LogInToAddManagement();
            var ProposalPage = PagesRepo.CreatePropsal;
            var Data = InventoryData.CreateProposal;
            Data.SearchTerms = new List<string>() {"MUGS & STEINS/Mugs & Steins-Ceramic"};
            Data.ProductGroup = "ESP Mobile";
            ProposalPage.Navigate()
                .CreateProposal(Data);

        }
        [Test, Order(AddUserData.TO + 25)]
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
        //[Test, Order(AddUserData.TO + 26)]
        //public void CreateProposal_ExportToExcel()
        //{
        //    this.LogInToAddManagement();
        //    var ProposalPage = PagesRepo.CreatePropsal;
        //    var allPro = PagesRepo.AllProposalPage;

        //    var Data = InventoryData.CreateProposal;
        //    ProposalPage.Navigate()
        //        .CreateProposal(Data);
        //    allPro.Navigate().OpenProposal(Data);
        //    ProposalPage.Export("excel");
        //}
        [Test, Order(AddUserData.TO + 27)]
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
        #region Advertisement Test Cases


        [Test, Order(AddUserData.TO + 28)]
        public void Advertisement_SearchByMemberID()
        {
            this.LogInToAddManagement();
            var Addpage = PagesRepo.Advertisement;
            Addpage.Navigate()
                .SearchByMemeberID(OrderLineData.OrderId);

        }
        [Test, Order(AddUserData.TO + 29)]
        public void Advertisement_SearchByPostition()
        {
            this.LogInToAddManagement();
            var Addpage = PagesRepo.Advertisement;
            Addpage.Navigate()
                .SearchByPosition(OrderLineData.SearchForPosition1.Positions);

        }
        [Test, Order(AddUserData.TO + 30)]
        public void Advertisement_SearchByTerms()
        {
            this.LogInToAddManagement();
            var Addpage = PagesRepo.Advertisement;
            Addpage.Navigate()
                .SearchByTerms(OrderLineData.SearchForPosition1.SearchTerms);

        }
        [Test, Order(AddUserData.TO + 31)]
        public void Advertisement_SearchByAddStatus()
        {
            this.LogInToAddManagement();
            var Addpage = PagesRepo.Advertisement;
            Addpage.Navigate()
                .SearchByAddStatus(OrderLineData.SearchForPosition1.Statuses);

        }
        [Test, Order(AddUserData.TO + 32)]
        public void Advertisement_SearchByRate()
        {
            this.LogInToAddManagement();
            var Addpage = PagesRepo.Advertisement;
            Addpage.Navigate()
                .SearchByRate(OrderLineData.SearchForPosition1.Rates);

        }
        [Test, Order(AddUserData.TO + 33)]
        public void Advertisement_SearchByProductGroup()
        {
            this.LogInToAddManagement();
            var Addpage = PagesRepo.Advertisement;
            var Data = OrderLineData.ShellToFill;
            //Data.AddType = new List<string>() { "PFP" };
            Data.ProductGroup = new List<string>() { "ESP" };


            Addpage.Navigate()
                .FillSearchParamitters(Data);

        }
        [Test, Order(AddUserData.TO + 34)]
        public void Advertisement_SearchByAdType()
        {
            this.LogInToAddManagement();
            var Addpage = PagesRepo.Advertisement;
            var Data = OrderLineData.ShellToFill;
            Data.AddType = new List<string>() { "PFP" };
            //Data.ProductGroup = new List<string>() { "ESP" };


            Addpage.Navigate()
                .FillSearchParamitters(Data);

        }
        [Test, Order(AddUserData.TO + 35)]
        public void Advertisement_SearchByProductGroupAndAdType()
        {
            this.LogInToAddManagement();
            var Addpage = PagesRepo.Advertisement;
            var Data = OrderLineData.ShellToFill;
            Data.AddType = new List<string>() { "PFP" };
            Data.ProductGroup = new List<string>() { "ESP" };


            Addpage.Navigate()
                .FillSearchParamitters(Data);

        }
        [Test, Order(AddUserData.TO + 36)]
        public void Advertisement_FreeFormSearch()
        {
            this.LogInToAddManagement();
            var Addpage = PagesRepo.Advertisement;
            Addpage.Navigate()
                .FreeFormSerch();

        }
        [Test, Order(AddUserData.TO + 37)]
        public void Advertisement_ExportAllAds()
        {
            this.LogInToAddManagement();
            var Addpage = PagesRepo.Advertisement;
            Addpage.Navigate()
                .ExportAllAdds();

        }
        [Test, Order(AddUserData.TO + 38)]
        public void Advertisement_ChangeStatusOfAdd()
        {
            this.LogInToAddManagement();
            var Addpage = PagesRepo.Advertisement;
            Addpage.Navigate()
                .ChangeAdStatus(OrderLineData.SearchForPosition1);

        }
        [Test, Order(AddUserData.TO + 39)]
        public void Advertisement_FilterByYearAndMonth()
        {
            this.LogInToAddManagement();
            var Addpage = PagesRepo.Advertisement;
            var Data = OrderLineData.ShellToFill;
            Data.Year = DateTime.Now.Year - 1;
            Data.Month = DateTime.Now.Month - 1;
            Addpage.Navigate()
                .FillSearchParamitters(Data);

        }
        [Test, Order(AddUserData.TO + 40)]
        public void Advertisement_disableCheckBoxForFutureYear()
        {
            this.LogInToAddManagement();
            var Addpage = PagesRepo.Advertisement;
            var Data = OrderLineData.ShellToFill;
            Data.Year = DateTime.Now.Year + 1;
            Data.Month = DateTime.Now.Month - 1;
            Addpage.Navigate()
                .FillSearchParamitters(Data).VerifydisableCheckBox();

        }
        #endregion
        #region Reports
        [Test, Order(AddUserData.TO + 41)]
        public void DropedProducts_ESP_PFP()
        {
            this.LogInToAddManagement();
            var repo = PagesRepo.Reports;
            repo.Navigate().DropProductReport("ESP", "PFP");
        }
        [Test, Order(AddUserData.TO + 42)]
        public void DropedProducts_ESP_Websites()
        {
            this.LogInToAddManagement();
            var repo = PagesRepo.Reports;
            repo.Navigate().DropProductReport("ESP Websites", "PFP");

        }
        [Test, Order(AddUserData.TO + 43)]
        public void DropedProducts_Export_PDF()
        {
            this.LogInToAddManagement();
            var repo = PagesRepo.Reports;
            repo.Navigate().DropProductReport("ESP Websites", "PFP").Export("PDF");

        }
        [Test, Order(AddUserData.TO + 44)]
        public void DropedProducts_Export_Excel()
        {
            this.LogInToAddManagement();
            var repo = PagesRepo.Reports;
            repo.Navigate().DropProductReport("ESP Websites", "PFP").Export("Excel");

        }
        [Test, Order(AddUserData.TO + 45)]
        public void OrderBySearchTerms_ESP_PFP()
        {
            this.LogInToAddManagement();
            var repo = PagesRepo.Reports;
            repo.Navigate().OrderBySearchTerms("ESP", "PFP", "BAGS").Export("Excel");

        }
        [Test, Order(AddUserData.TO + 46)]
        public void OrderBySearchTerms_ESP_Banner()
        {
            this.LogInToAddManagement();
            var repo = PagesRepo.Reports;
            repo.Navigate().OrderBySearchTerms("ESP", "Banner", "BAGS").Export("Excel");

        }
        [Test, Order(AddUserData.TO + 47)]
        public void OrderBySearchTerms_ESP_Websites_PFP()
        {
            this.LogInToAddManagement();
            var repo = PagesRepo.Reports;
            repo.Navigate().OrderBySearchTerms("ESP Websites", "PFP", "BAGS").Export("Excel");

        }
        [Test, Order(AddUserData.TO + 48)]
        public void AdsBelowRateCard()
        {
            this.LogInToAddManagement();
            var repo = PagesRepo.Reports;
            repo.Navigate().AdsBelowRateCard().Export("Excel");
            //repo.Export("Pdf");
        }
        [Test, Order(AddUserData.TO + 49)]
        public void DefaultProductReport()
        {
            var oerderId = OrderLineData.ActiveOrderId;
            this.LogInToAddManagement();
            var repo = PagesRepo.Reports;
            repo.Navigate().DefaultProductReport(oerderId, "All").Export("Excel");

        }
        [Test, Order(AddUserData.TO + 50)]
        public void InventoryHistoryReport_ESP()
        {
            //var oerderId = OrderLineData.ActiveOrderId;
            this.LogInToAddManagement();
            var repo = PagesRepo.Reports;
            repo.Navigate().InventoryHistoryReport("ESP").Export("Excel");
        }
        [Test, Order(AddUserData.TO + 51)]
        public void InventoryHistoryReport_ESP_Subterms()
        {
            //var oerderId = OrderLineData.ActiveOrderId;
            this.LogInToAddManagement();
            var repo = PagesRepo.Reports;
            repo.Navigate().InventoryHistoryReport("ESP", "BAGS").Export("pdf");
        }
        #endregion
        #region Admin Pages
        [Test, Order(AddUserData.TO + 52)]
        public void Pages_AddNewPage()
        {
            PageModel Data = PagesData.Page1;
            var aPages = PagesRepo.adminpages;
            this.LogInToAddManagement();
            aPages.Navigate()
                .FillCratePageForm(Data)
                .SavePage();

        }
        [Test, Order(AddUserData.TO + 53)]
        public void Pages_AddAdditionalKeyWords()
        {
            PageModel Data = PagesData.Page1;
            var aPages = PagesRepo.adminpages;
            this.LogInToAddManagement();
            aPages.Navigate()
                .SearchPage(Data).SortLatestRecords()
                .AddAddionalKeyWordsinPage(Data);
        }
        [Test, Order(AddUserData.TO + 54)]
        public void Pages_DeleteAdditionalKeyWords()
        {
            PageModel Data = PagesData.Page1;
            var aPages = PagesRepo.adminpages;
            this.LogInToAddManagement();
            aPages.Navigate()
                .SearchPage(Data).SortLatestRecords()
                .DeleteAddionalKeyWordsinPage(Data);
        }
        [Test, Order(AddUserData.TO + 55)]
        public void Pages_DeletePage()
        {
            PageModel Data = PagesData.Page1;
            var aPages = PagesRepo.adminpages;
            this.LogInToAddManagement();
            aPages.Navigate()
                .SearchPage(Data).SortLatestRecords()
                .DeletePage(Data);
        }
        [Test, Order(AddUserData.TO + 56)]
        public void Pages_ActivePage()
        {
            PageModel Data = PagesData.Page1;
            var aPages = PagesRepo.adminpages;
            this.LogInToAddManagement();
            aPages.Navigate()
                 .SearchPage(Data).SortLatestRecords()
                .ActivePage(Data);
        }
        #endregion
        #region Admin Simulate Seach
        [Test, Order(AddUserData.TO + 57)]
        public void SimulateSearch_ESP()
        {
            var SimulateSearch = PagesRepo.SimulateSearchPage;
            this.LogInToAddManagement();
            SimulateSearch.Navigate()
                .SimulateSearch_Page("BAGS", "ESP Websites");

        }
        [Test, Order(AddUserData.TO + 58)]
        public void SimulateSearch_ESP_Websites()
        {
            var SimulateSearch = PagesRepo.SimulateSearchPage;
            this.LogInToAddManagement();
            SimulateSearch.Navigate()
                .SimulateSearch_Page("BAGS", "ESP");

        }
        [Test, Order(AddUserData.TO + 59)]
        public void ESP_PFP_TestCall()
        {
            var oerderId = OrderLineData.ParmanentActiveOrderId;
            var SimulateSearch = PagesRepo.SimulateSearchPage;
            var Data = OrderLineData.ESP_PFP;
            Data.DeliveryPreferences = "Standard";
            Data.Position = 7;
            //Data.SearchTerm = "/BAGS/BEACH";
            //Data.ProductInformation= "/BAGS/BEACH";
            Data.ProductId_ManualSelection = OrderLineData.ParmanentActiveProductId == "" ? Data.ProductId_ManualSelection : new List<string>() { OrderLineData.ParmanentActiveProductId };
            var AllOrders = PagesRepo.AllOrders;
            var EditOrder = PagesRepo.EditOrder;
            this.LogInToAddManagement();
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.ParmanentActiveOrderId);
            EditOrder.DeleteAllOrderLine(Data);
            SimulateSearch.Navigate().
            TestCall("/BAGS/BEACH", oerderId, "ESP", "PFP");
            var Pos1_Data = SimulateSearch.GetPostionData(7, 1);
            // var AllOrders = PagesRepo.AllOrders;
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.ParmanentActiveOrderId);
            EditOrder.AddNewOrderLine("PFP");
            PagesRepo.EditOrderLine.
                FillOrderLine_Admin(Data).Save();
            SimulateSearch.Navigate().
            TestCall("/BAGS/BEACH", oerderId, "ESP", "PFP");
            var Pos1_Data_Update = SimulateSearch.GetPostionData(7, 1);

        }
        [Test, Order(AddUserData.TO + 60)]
        public void ESP_Websites_PFP_TestCall()
        {
            var AllOrders = PagesRepo.AllOrders;
            var EditOrder = PagesRepo.EditOrder;
            var oerderId = OrderLineData.ActiveOrderId;
            var SimulateSearch = PagesRepo.SimulateSearchPage;
            var Data = OrderLineData.ESP_Website;
            //Data.ProductGroup = "ESP Websites";
            Data.ProductId_ManualSelection = OrderLineData.ParmanentActiveProductId == "" ? Data.ProductId_ManualSelection : new List<string>() { OrderLineData.ParmanentActiveProductId };

            Data.DeliveryPreferences = "Standard";
            //Data.Position = 9;
            Data.SearchTerm = "BAGS/BEACH BAGS";
            Data.ProductInformation = "BAGS/BEACH BAGS";
            this.LogInToAddManagement();
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.ParmanentActiveOrderId);
            EditOrder.DeleteAllOrderLine(Data);
            SimulateSearch.Navigate().
            TestCall("/BAGS/BEACH", oerderId, "ESP Websites", "PFP");
            var Pos1_Data = SimulateSearch.GetPostionData(1, 1);
            //var AllOrders = PagesRepo.AllOrders;
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.ParmanentActiveOrderId);
            PagesRepo.EditOrder.AddNewOrderLine("PFP");
            PagesRepo.EditOrderLine.
                FillOrderLine_Admin(Data).Save();
            SimulateSearch.Navigate().
            TestCall("/BAGS/BEACH", oerderId, "ESP Websites", "PFP");
            var Pos1_Data_Update = SimulateSearch.GetPostionData(1, 1);

        }
        #endregion
        #region Orders Test cases
        [Test, Order(AddUserData.TO + 61)]
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
        [Test, Order(AddUserData.TO + 62)]
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
        [Test, Order(AddUserData.TO + 63)]
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
        //[Test, Order(AddUserData.TO + 64)]
        //public void ExportToExcel()
        //{
        //    this.LogInToAddManagement();
        //    var AllOrders = PagesRepo.AllOrders;
        //    var editOrder = PagesRepo.EditOrder;
        //    AllOrders.Navigate()
        //    .SelectGivenOrderByID(OrderLineData.OrderId);
        //    editOrder.ExportToExcel();
        //}
        //[Test, Order(AddUserData.TO + 65)]
        //public void ExportToPDF()
        //{
        //    this.LogInToAddManagement();
        //    var AllOrders = PagesRepo.AllOrders;
        //    var editOrder = PagesRepo.EditOrder;
        //    AllOrders.Navigate()
        //    .SelectGivenOrderByID(OrderLineData.OrderId);
        //    editOrder.ExportToPDF();
        //}
        [Test, Order(AddUserData.TO + 66)]
        public void AddAnAdVerifyTotalUpdate()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var EditOrder = PagesRepo.EditOrder;
            var OrderLine = PagesRepo.EditOrderLine;
            var data = OrderLineData.ESP_Website;
            data.ProductId_ManualSelection = OrderLineData.ParmanentActiveProductId == "" ? data.ProductId_ManualSelection : new List<string>() { OrderLineData.ParmanentActiveProductId };

            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.ParmanentActiveOrderId);
            EditOrder.DeleteResultBannerIfAlreadyExists(data);
            int PreAdCount = EditOrder.GetAdCount();
            EditOrder.AddNewOrderLine("PFP");
            OrderLine.
            FillOrderLine_Admin(data).Save();
            OrderLine.GoBackOrder();
            if (PreAdCount + 1 != EditOrder.GetAdCount())
            {
                throw new Exception("Add Count Verified.");
            }
        }
        [Test, Order(AddUserData.TO + 67)]
        public void ChangeTheStatusOfAd()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var EditOrder = PagesRepo.EditOrder;
            var OrderLine = PagesRepo.EditOrderLine;
            var data = OrderLineData.ESP_Website;
            data.ProductId_ManualSelection = OrderLineData.ParmanentActiveProductId == "" ? data.ProductId_ManualSelection : new List<string>() { OrderLineData.ParmanentActiveProductId };

            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.ParmanentActiveOrderId);
            EditOrder.DeleteResultBannerIfAlreadyExists(data);
            int PreAdCount = EditOrder.GetAdCount();
            EditOrder.AddNewOrderLine("PFP");
            OrderLine.
            FillOrderLine_Admin(data).Save();
            data.Status = "Completed";
            OrderLine.GoBackOrder();
            EditOrder.OpenOrderLine(data);
            OrderLine.Update(data);
            OrderLine.GoBackOrder();

        }
        [Test, Order(AddUserData.TO + 67)]
        public void ChangeTheStatusOfOrder()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var EditOrder = PagesRepo.EditOrder;
            var OrderLine = PagesRepo.EditOrderLine;
            var data = OrderLineData.ESP_Website;
            AllOrders.Navigate().ChangeStatusOfOrder(OrderLineData.OrderId, "Completed");

        }
        [Test, Order(AddUserData.TO + 68)]
        public void OrderSummary_ActiveTotal()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var EditOrder = PagesRepo.EditOrder;
            var OrderLine = PagesRepo.EditOrderLine;
            var data = OrderLineData.OrderSummary_VerificationData;
            var OrderID = OrderLineData.ParmanentActiveOrderId;
            data.ProductId_ManualSelection = OrderLineData.ParmanentActiveProductId == "" ? data.ProductId_ManualSelection : new List<string>() { OrderLineData.ParmanentActiveProductId };

            //AllOrders.Navigate().ChangeStatusOfOrder(OrderLineData.ParmanentActiveOrderId, "Active");
            AllOrders.Navigate();
            AllOrders.ClearFilter();
            Double Active = AllOrders.GetactiveAdAmmount();
            Double Total = AllOrders.GetTotalAdAmmount();
            Double Suspanded = AllOrders.GetSuspandedAdAmmount();
            Double Completed = AllOrders.GetCompletedAdAmmount();
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderID);
            EditOrder.ClearFilter();
            Double AdTotal = EditOrder.GetTotalCost();
            Double AdActive = EditOrder.GetActiveCost();
            EditOrder.AddNewOrderLine("PFP");
            OrderLine.FillOrderLine_Admin(data).Save();
            OrderLine.GoBackOrder();
            Double AdNewTotal = EditOrder.GetTotalCost();
            Double AdNewActive = EditOrder.GetActiveCost();
            EditOrder.GoBackToAllOrders();
            AllOrders.ClearFilter();
            AllOrders.Reload();
            Double NewActive = AllOrders.GetactiveAdAmmount();
            Double NewTotal = AllOrders.GetTotalAdAmmount();
            Double NewSuspanded = AllOrders.GetSuspandedAdAmmount();
            Double NewCompleted = AllOrders.GetCompletedAdAmmount();
            var Cost = data.Cost;
            if (Cost + AdTotal != AdNewTotal)
            {
                throw new Exception(string.Format("Active Information did't match after add order line --Information: Order ID :{0}  old Total:{1} New Total:{2} Cost:{3} ", OrderID, AdTotal, AdNewTotal, Cost));
            }
            if (Cost + Active != NewActive )
            {
                throw new Exception(string.Format("Active Information did't match after add order line(All Orders) --Information: Order ID :{0}  old Active:{1} New Active:{2} Cost:{3} ", OrderID, Active, NewActive, Cost));

            }
            if (Cost + Total != NewTotal)
            {
                throw new Exception(string.Format("Active Information did't match after add order line(All Orders) --Information: Order ID :{0}  old Active:{1} New Active:{2} Cost:{3} ", OrderID, Active, NewActive, Cost));

            }
        }
        [Test, Order(AddUserData.TO + 69)]
        public void OrderSummary_Complete()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var EditOrder = PagesRepo.EditOrder;
            var OrderLine = PagesRepo.EditOrderLine;
            var data = OrderLineData.OrderSummary_VerificationData;
            //var OrderId = "12555";
            data.Status = "Suspended";
            data.ProductId_ManualSelection = null;
            var OrderID = OrderLineData.CompleteOrderId;
           // AllOrders.Navigate().ChangeStatusOfOrder(OrderID, "Completed");
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
            OrderLine.FillOrderLine_Admin(data).Save();
            OrderLine.GoBackOrder();
            Double AdNewTotal = EditOrder.GetTotalCost();
            Double AdNewActive = EditOrder.GetActiveCost();
            EditOrder.GoBackToAllOrders();
            AllOrders.ClearFilter();
            AllOrders.Reload();
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
        [Test, Order(AddUserData.TO + 70)]
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
           // AllOrders.Navigate().ChangeStatusOfOrder(OrderLineData.OrderId, "Suspended");           
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
            OrderLine.FillOrderLine_Admin(data).Save();
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
        [Test, Order(AddUserData.TO + 71)]
        public void OrderSummary_ForFutureYears()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            AllOrders.Navigate();
            AllOrders.ClearFilter().SetFutureYearFilter();
        }
        #endregion
        #region Order Line Test Cases
        public void LogInToAddManagement()
        {
            var LoginPageManager = PagesRepo.LoginP;
            if (!LoginPageManager.LoginVerification(AddUserData.SalesUser))
            {
                LoginPageManager
            .Navigate()
            .LoginGivenUser(AddUserData.AddUser1)
            .GoToAppCatagory("ESP");
            }
        }
        [Test, Order(AddUserData.TO + 72)]
        public void AddPFPOrderLine_SaveAndCopy()
        {

            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            //AllOrders.Navigate().ChangeStatusOfOrder(OrderLineData.OrderId, "Active");                       
            var data = OrderLineData.SaveAndCopy;
            data.ProductId_ManualSelection = OrderLineData.ParmanentActiveProductId == "" ? data.ProductId_ManualSelection : new List<string>() { OrderLineData.ParmanentActiveProductId };
            
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.ParmanentActiveOrderId);
            PagesRepo.EditOrder.AddNewOrderLine("PFP");
            PagesRepo.EditOrderLine.
                FillOrderLine_Admin(data).
                SaveAndCopy().VerifySaveAndCopy();
            PagesRepo.EditOrderLine.GoBackOrderLineByLink();
        }
        [Test, Order(AddUserData.TO + 73)]
        public void AddPFPOrderLine_SaveAndAdd()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var Data=OrderLineData.SaveAndAdd;
            Data.ProductId_ManualSelection = OrderLineData.ParmanentActiveProductId == "" ? Data.ProductId_ManualSelection : new List<string>() { OrderLineData.ParmanentActiveProductId };

            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.ParmanentActiveOrderId);
            PagesRepo.EditOrder.AddNewOrderLine("PFP");
            PagesRepo.EditOrderLine.
            FillOrderLine_Admin(Data).SaveAndAdd().VerifySaveAndAdd();
            PagesRepo.EditOrderLine.GoBackOrderLineByLink();
        }
        [Test, Order(AddUserData.TO + 74)]
        public void AssignAnAdGroup()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var editOrder = PagesRepo.EditOrder;
            var adgroup = OrderData.AdGroup;
            adgroup.GroupName = "Automation AssignGroup";
            var orderLineData = OrderLineData.SaveAndCopy;
            orderLineData.ProductId_ManualSelection = OrderLineData.ParmanentActiveProductId == "" ? orderLineData.ProductId_ManualSelection : new List<string>() { OrderLineData.ParmanentActiveProductId };
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.ParmanentActiveOrderId);
            if (editOrder.GetAddGroup(adgroup) == null)
            {
                editOrder.CreateNewAddGroup(adgroup);
            }
            editOrder.CloseAdGroupPanel();
            orderLineData.AddGroupName = adgroup.GroupName;
            orderLineData.Impressions = null;
            orderLineData.ProductSelectionManual = true;
            editOrder.AddNewOrderLine("PFP");
            PagesRepo.EditOrderLine.
                FillOrderLine_Admin(orderLineData).
                Save();
        }
        [Test, Order(AddUserData.TO + 75)]
        public void VerifyAutoSeletion()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var editOrder = PagesRepo.EditOrder;
            var adgroup = OrderData.AdGroup;
            var orderLineData = OrderLineData.AutoSelection;
            AllOrders.Navigate()
          .SelectGivenOrderByID(OrderLineData.OrderId);
            editOrder.AddNewOrderLine("PFP");
            PagesRepo.EditOrderLine.
                FillOrderLine_Admin(orderLineData).
                VerifyAutoSelect();
        }
        [Test, Order(AddUserData.TO + 76)]
        public void AddPFPOrderLine_Add3ProdcutsManually()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var Data = OrderLineData.MultipleProductInManualSelection;
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.ParmanentActiveOrderId);
            PagesRepo.EditOrder.AddNewOrderLine("PFP");
            PagesRepo.EditOrderLine.
            FillOrderLine_Admin(Data)
            .Save();
        }
        [Test, Order(AddUserData.TO + 77)]
        public void ResultBanner_WithButOut()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var EditOrder = PagesRepo.EditOrder;
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.ParmanentActiveOrderId);
            EditOrder.DeleteResultBannerIfAlreadyExists(OrderLineData.ResultBanner);
            PagesRepo.EditOrder.AddNewOrderLine("Banner");
            PagesRepo.EditOrderLine.
            FillOrderLine_Admin(OrderLineData.ResultBanner).Save();
        }
        [Test, Order(AddUserData.TO + 78)]
        public void ResultTile_WithRemnant()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var EditOrder = PagesRepo.EditOrder;
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.ParmanentActiveOrderId);
            EditOrder.DeleteResultBannerIfAlreadyExists(OrderLineData.ResultTile);
            PagesRepo.EditOrder.AddNewOrderLine("Banner");
            PagesRepo.EditOrderLine.
            FillOrderLine_Admin(OrderLineData.ResultTile).Save();
        }
        [Test, Order(AddUserData.TO + 79)]
        public void ResultTower_WithBuyRemaining()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var EditOrder = PagesRepo.EditOrder;
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.ParmanentActiveOrderId);
            EditOrder.DeleteResultBannerIfAlreadyExists(OrderLineData.ResultTower);
            PagesRepo.EditOrder.AddNewOrderLine("Banner");
            PagesRepo.EditOrderLine.
            FillOrderLine_Admin(OrderLineData.ResultTower).Save();
        }
        [Test, Order(AddUserData.TO + 80)]
        public void ESP_PFP_withButOut()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var EditOrder = PagesRepo.EditOrder;
            var data = OrderLineData.ESP_PFP;
            data.ProductId_ManualSelection = OrderLineData.ParmanentActiveProductId == "" ? data.ProductId_ManualSelection : new List<string>() { OrderLineData.ParmanentActiveProductId };
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.ParmanentActiveOrderId);
            EditOrder.DeleteResultBannerIfAlreadyExists(data);
            data.DeliveryPreferences = "Buy Out";
            PagesRepo.EditOrder.AddNewOrderLine("PFP");
            PagesRepo.EditOrderLine.
            FillOrderLine_Admin(data).Save();
        }
        [Test, Order(AddUserData.TO + 81)]
        public void ESP_PFP_withRemnant()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var EditOrder = PagesRepo.EditOrder;
            var data = OrderLineData.ESP_PFP;
            data.ProductId_ManualSelection = OrderLineData.ParmanentActiveProductId == "" ? data.ProductId_ManualSelection : new List<string>() { OrderLineData.ParmanentActiveProductId };
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.ParmanentActiveOrderId);
            EditOrder.DeleteResultBannerIfAlreadyExists(data);
            data.DeliveryPreferences = "Buy Remnant";
            PagesRepo.EditOrder.AddNewOrderLine("PFP");
            PagesRepo.EditOrderLine.
            FillOrderLine_Admin(data).Save();
        }
        [Test, Order(AddUserData.TO + 82)]
        public void ESP_PFP_withRemaining()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var EditOrder = PagesRepo.EditOrder;
            var data = OrderLineData.ESP_PFP;
            data.ProductId_ManualSelection = OrderLineData.ParmanentActiveProductId == "" ? data.ProductId_ManualSelection : new List<string>() { OrderLineData.ParmanentActiveProductId };
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.ParmanentActiveOrderId);
            EditOrder.DeleteResultBannerIfAlreadyExists(data);
            data.DeliveryPreferences = "Buy Remaining";
            PagesRepo.EditOrder.AddNewOrderLine("PFP");
            PagesRepo.EditOrderLine.
            FillOrderLine_Admin(data).Save();
        }
        [Test, Order(AddUserData.TO + 83)]
        public void ESP_Website_withBuyOut()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var EditOrder = PagesRepo.EditOrder;
            var data = OrderLineData.ESP_Website;
            data.ProductId_ManualSelection = OrderLineData.ParmanentActiveProductId == "" ? data.ProductId_ManualSelection : new List<string>() { OrderLineData.ParmanentActiveProductId };
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.ParmanentActiveOrderId);
            EditOrder.DeleteResultBannerIfAlreadyExists(data);
            data.DeliveryPreferences = "Buy Out";
            PagesRepo.EditOrder.AddNewOrderLine("PFP");
            PagesRepo.EditOrderLine.
            FillOrderLine_Admin(data).Save();
        }
        [Test, Order(AddUserData.TO + 84)]
        public void ESP_Website_withRemnant()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var EditOrder = PagesRepo.EditOrder;
            var data = OrderLineData.ESP_Website;
            data.ProductId_ManualSelection = OrderLineData.ParmanentActiveProductId == "" ? data.ProductId_ManualSelection : new List<string>() { OrderLineData.ParmanentActiveProductId };
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.ParmanentActiveOrderId);
            EditOrder.DeleteResultBannerIfAlreadyExists(data);
            data.DeliveryPreferences = "Buy Remnant";
            PagesRepo.EditOrder.AddNewOrderLine("PFP");
            PagesRepo.EditOrderLine.
            FillOrderLine_Admin(data).Save();
        }
        [Test, Order(AddUserData.TO + 85)]
        public void ESP_Website_withRemaining()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var EditOrder = PagesRepo.EditOrder;
            var data = OrderLineData.ESP_Website;
            data.ProductId_ManualSelection = OrderLineData.ParmanentActiveProductId == "" ? data.ProductId_ManualSelection : new List<string>() { OrderLineData.ParmanentActiveProductId };
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.ParmanentActiveOrderId);
            EditOrder.DeleteResultBannerIfAlreadyExists(data);
            data.DeliveryPreferences = "Buy Remaining";
            PagesRepo.EditOrder.AddNewOrderLine("PFP");
            PagesRepo.EditOrderLine.
            FillOrderLine_Admin(data).Save();
        }
        [Test, Order(AddUserData.TO + 86)]
        public void Homepage_Banner_withStandard()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var EditOrder = PagesRepo.EditOrder;
            var data = OrderLineData.ResultBanner;
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.ParmanentActiveOrderId);
            data.DeliveryPreferences = "Standard";
            data.AddType = "Homepage Banner";
            data.Position = null;
            data.SearchTerm = "";
            EditOrder.DeleteResultBannerIfAlreadyExists(data);
            PagesRepo.EditOrder.AddNewOrderLine("Banner");
            PagesRepo.EditOrderLine.
            FillOrderLine_Admin(data).Save();
        }
        [Test, Order(AddUserData.TO + 87)]
        public void Homepage_Tile_withStandard()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var EditOrder = PagesRepo.EditOrder;
            var data = OrderLineData.ResultBanner;
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.ParmanentActiveOrderId);
            data.DeliveryPreferences = "Standard";
            data.AddType = "Homepage Tile";
            data.Position = null;
            data.SearchTerm = "";
            EditOrder.DeleteResultBannerIfAlreadyExists(data);
            PagesRepo.EditOrder.AddNewOrderLine("Banner");
            PagesRepo.EditOrderLine.
            FillOrderLine_Admin(data).Save();
        }
        [Test, Order(AddUserData.TO + 87)]
        public void MobilePFP()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var EditOrder = PagesRepo.EditOrder;
            var data = OrderLineData.Mobile_PFP;
            data.ProductId_ManualSelection = OrderLineData.ParmanentActiveProductId == "" ? data.ProductId_ManualSelection : new List<string>() { OrderLineData.ParmanentActiveProductId };

            data.Priority = null;
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.ParmanentActiveOrderId);
            EditOrder.DeleteResultBannerIfAlreadyExists(data);
            PagesRepo.EditOrder.AddNewOrderLine("PFP");
            PagesRepo.EditOrderLine.
            FillOrderLine_Admin(data).Save();
        }
        [Test, Order(AddUserData.TO + 88)]
        public void ESP_Mobile_HomePage_Tile()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var EditOrder = PagesRepo.EditOrder;
            var data = OrderLineData.ResultBanner;
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.ParmanentActiveOrderId);
            data.DeliveryPreferences = null;
            data.SearchTerm = "";
            data.AddType = "Homepage Tile";
            data.ProductGroup = "ESP Mobile";
            data.Position = null;
            data.SearchTerm = "";
            EditOrder.DeleteResultBannerIfAlreadyExists(data);
            PagesRepo.EditOrder.AddNewOrderLine("Banner");
            PagesRepo.EditOrderLine.
            FillOrderLine_Admin(data).Save();
        }
        [Test, Order(AddUserData.TO + 89)]
        public void ESP_Mobile_ResultsBanner()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var EditOrder = PagesRepo.EditOrder;
            var data = OrderLineData.ResultBanner;
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.ParmanentActiveOrderId);
            data.DeliveryPreferences = null;
            data.AddType = "Results Banner";
            data.ProductGroup = "ESP Mobile";
            data.Position = null;
            EditOrder.DeleteResultBannerIfAlreadyExists(data);
            PagesRepo.EditOrder.AddNewOrderLine("Banner");
            PagesRepo.EditOrderLine.
            FillOrderLine_Admin(data).Save();
        }
        [Test, Order(AddUserData.TO + 90)]
        public void ESP_Mobile_LoginBanner()
        {
            this.LogInToAddManagement();
            var AllOrders = PagesRepo.AllOrders;
            var EditOrder = PagesRepo.EditOrder;
            var data = OrderLineData.ResultBanner;
            AllOrders.Navigate()
            .SelectGivenOrderByID(OrderLineData.ParmanentActiveOrderId);
            data.DeliveryPreferences = null;
            //data.SearchTerm = "";
            data.AddType = "Login Banner";
            data.ProductGroup = "ESP Mobile";
            data.Position = null;
            data.SearchTerm = "";
            EditOrder.DeleteResultBannerIfAlreadyExists(data);
            PagesRepo.EditOrder.AddNewOrderLine("Banner");
            PagesRepo.EditOrderLine.
            FillOrderLine_Admin(data).Save();
        }
        #endregion
        #region Miscellaneous
        [Test, Order(AddUserData.TO + 91)]
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
        [Test, Order(AddUserData.TO + 92)]
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
        [Test, Order(AddUserData.TO + 93)]
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
        #region AdGroupAdmin
        [Test, Order(AddUserData.TO + 94)]
        public void ManageAdGroup_Search_Export_CurrentYear()
        {
            this.LogInToAddManagement();
            var AdGroupPAge = PagesRepo.AdGropPage;
            AdGroupPAge.Navigate().ClearAllfilter().Search().Export();
        }
        [Test, Order(AddUserData.TO + 95)]
        public void ManageAdGroup_Search_Export_NextYear()
        {
            this.LogInToAddManagement();
            var AdGroupPAge = PagesRepo.AdGropPage;
            AdGroupPAge.Navigate().ClearAllfilter().FillSearchFilters("", null, null, (DateTime.Now.Year + 1).ToString()).Search().Export();
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
