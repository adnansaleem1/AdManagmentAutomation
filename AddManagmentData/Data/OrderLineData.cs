using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddManagmentData.Model;

namespace AddManagmentData.Data
{
    public class OrderLineData
    {
        public static OrderLineModel SaveAndCopy
        {
            get
            {
                return new OrderLineModel()
                {
                    ProductGroup = "ESP",
                    AddType = "PFP",
                    Position = 4,
                    SearchTerm = "ENVIRONMENTALLY FRIENDLY PRODUCTS",
                    DeliveryPreferences = "Standard",
                    ProductInformation = "ENVIRONMENTALLY FRIENDLY PRODUCTS",
                    Impressions = 20,
                    GeoTargetEnable = false,
                    KeyWordsEnable = true,
                    CatogoriesEnable = true,
                    SubsitutionsAllow = true,
                    DisplayMultipleAddsAllow = true,
                    SearchLeadingTextAllow = true,
                    Priority = 10,
                    ProductSelectionManual = true,
                    ProductId_ManualSelection = new List<string>() { "5625470" },
                    Status = "Active"
                };
            }
        }

        public static OrderLineModel SaveAndAdd
        {
            get
            {
                return new OrderLineModel()
                {
                    ProductGroup = "ESP",
                    AddType = "PFP",
                    Position = 5,
                    SearchTerm = "ENVIRONMENTALLY FRIENDLY PRODUCTS",
                    DeliveryPreferences = "Standard",
                    ProductInformation = "ENVIRONMENTALLY FRIENDLY PRODUCTS",
                    Impressions = 20,
                    GeoTargetEnable = false,
                    KeyWordsEnable = true,
                    CatogoriesEnable = true,
                    SubsitutionsAllow = true,
                    DisplayMultipleAddsAllow = true,
                    SearchLeadingTextAllow = true,
                    Priority = 10,
                    ProductSelectionManual = true,
                    ProductId_ManualSelection = new List<string>() { "5625470" },
                    Status = "Active"
                };
            }
        }

        public static OrderLineModel ResultBanner
        {
            get
            {
                return new OrderLineModel()
                {
                    ProductGroup = "ESP",
                    AddType = "Results Banner",
                    //Position = 5,
                    SearchTerm = "PENS/ALUMINUM PENS",
                    DeliveryPreferences = "Buy Out",
                    ProductInformation = "PENS/ALUMINUM PENS",
                   // Impressions = 20,
                    GeoTargetEnable = false,
                    KeyWordsEnable = true,
                    CatogoriesEnable = true,
                    SubsitutionsAllow = true,
                    DisplayMultipleAddsAllow = true,
                    SearchLeadingTextAllow = true,
                    Priority = 10,
                    //ProductSelectionManual = true,
                    //ProductId_ManualSelection = new List<string>() { "5625470" },
                    Status = "Active"
                };
            }
        }

        public static OrderLineModel ResultTile
        {
            get
            {
                return new OrderLineModel()
                {
                    ProductGroup = "ESP",
                    AddType = "Results Tile",
                    Position = 1,
                    SearchTerm = "BAGS/BAGS-LUNCH",
                    DeliveryPreferences = "Buy Remnant",
                    ProductInformation = "BAGS/BAGS-LUNCH",
                   // Impressions = 20,
                    GeoTargetEnable = false,
                    KeyWordsEnable = true,
                    CatogoriesEnable = true,
                    SubsitutionsAllow = true,
                    DisplayMultipleAddsAllow = true,
                    SearchLeadingTextAllow = true,
                    Priority = 10,
                    //ProductSelectionManual = true,
                    //ProductId_ManualSelection = new List<string>() { "5625470" },
                    Status = "Active"
                };
            }
        }

        public static OrderLineModel ResultTower
        {
            get
            {
                return new OrderLineModel()
                {
                    ProductGroup = "ESP",
                    AddType = "Results Tower",
                    //Position = 1,
                    SearchTerm = "BAGS/BAGS-FOOD",
                    DeliveryPreferences = "Buy Remaining",
                    ProductInformation = "BAGS/BAGS-FOOD",
                   // Impressions = 20,
                    GeoTargetEnable = false,
                    KeyWordsEnable = true,
                    CatogoriesEnable = true,
                    SubsitutionsAllow = true,
                    DisplayMultipleAddsAllow = true,
                    SearchLeadingTextAllow = true,
                    Priority = 10,
                    Status = "Active"
                };
            }
        }

        public static OrderLineModel ESP_PFP
        {
            get
            {
                return new OrderLineModel()
                {
                    ProductGroup = "ESP",
                    AddType = "PFP",
                    Position = 1,
                    SearchTerm = "BAGS/BEACH BAGS",
                    DeliveryPreferences = "Buy Out",
                    ProductInformation = "BAGS/BEACH BAGS",
                   // Impressions = 20,
                    GeoTargetEnable = false,
                    KeyWordsEnable = true,
                    CatogoriesEnable = true,
                    SubsitutionsAllow = true,
                    DisplayMultipleAddsAllow = true,
                    SearchLeadingTextAllow = true,
                    ProductSelectionManual = true,
                    ProductId_ManualSelection = new List<string>() { "5625470" },
                    Priority = 10,
                    Status = "Active"
                };
            }
        }
        public static OrderLineModel Mobile_PFP
        {
            get
            {
                return new OrderLineModel()
                {
                    ProductGroup = "ESP Mobile",
                    AddType = "PFP",
                    //Position = 1,
                    SearchTerm = "BAGS/BAGS-RESEALABLE/RECLOSABLE",
                    //DeliveryPreferences = "Buy Out",
                    ProductInformation = "BAGS/BAGS-RESEALABLE/RECLOSABLE",
                   // Impressions = 20,
                    GeoTargetEnable = false,
                    KeyWordsEnable = true,
                    CatogoriesEnable = true,
                    SubsitutionsAllow = true,
                    DisplayMultipleAddsAllow = true,
                    SearchLeadingTextAllow = true,
                    ProductSelectionManual = true,
                    ProductId_ManualSelection = new List<string>() { "5625470" },
                    Priority = 10,
                    Status = "Active"
                };
            }
        }
        
        public static OrderLineModel ESP_Website
        {
            get
            {
                return new OrderLineModel()
                {
                    ProductGroup = "ESP Websites",
                    AddType = "PFP",
                    Position = 1,
                    SearchTerm = "GARMENT BAGS",
                    DeliveryPreferences = "Buy Out",
                    ProductInformation = "GARMENT BAGS",
                    // Impressions = 20,
                    GeoTargetEnable = false,
                    KeyWordsEnable = true,
                    CatogoriesEnable = true,
                    SubsitutionsAllow = true,
                    DisplayMultipleAddsAllow = true,
                    SearchLeadingTextAllow = true,
                    ProductSelectionManual = true,
                    ProductId_ManualSelection = new List<string>() { "5625470" },
                    Priority = 10,
                    Status = "Active"
                };
            }
        }

        
        public static OrderLineModel MultipleProductInManualSelection
        {
            get
            {
                return new OrderLineModel()
                {
                    ProductId_ManualSelection = new List<string>() { "5625470", "550168664", "5848093", "" }
                };
            }
        }

        public static AdvertisementSearchModel SearchForPosition1
        {
            get
            {
                return new AdvertisementSearchModel()
                {
                    Positions = new List<int>() { 1 },
                    SearchTerms = new List<string>() { "ENVIRONMENTALLY FRIENDLY PRODUCTS" },
                    IncludeSubCat = false,
                    Statuses = new List<string>() { "Active" },
                    Rates = new List<string>() {"Supplier Rate Card" },
                };
            }
        }


        public static string OrderId = "88660";

        public static string ActiveOrderId = "88660";
    }
}
