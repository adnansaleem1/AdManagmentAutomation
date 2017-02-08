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
    }
}
