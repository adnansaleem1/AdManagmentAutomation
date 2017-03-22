using AddManagmentData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddManagmentData.Data
{
    public class InventoryData
    {
        public static InventoryModel ForBags
        {
            get
            {
                return new InventoryModel()
                {
                    InventoryType = "PFP",
                    ProductGroup = "ESP Websites",
                    AdType = "PFP",
                    IncludeSubCat = true,
                    SearchTerms = new List<string>() { "Bags" }
                };
            }
        }

        public static InventoryModel PositionUpdate
        {
            get
            {
                return new InventoryModel()
                {
                    InventoryType = "PFP",
                    ProductGroup = "ESP",
                    AdType = "PFP",
                    IncludeSubCat = true,
                    SearchTerms = new List<string>() { "GENERAL/screen printing" }
                };
            }
        }

        public static PropsalModel CreateProposal
        {
            get
            {
                return new PropsalModel()
                {
                    InventoryType = "PFP",
                    ProductGroup = "ESP Websites",
                    AdType = "PFP",
                    IncludeSubCat = true,
                    SearchTerms = new List<string>() { "BAGS/BAGS-BOTTLE" },
                    MemberId = "88660",
                    Contact = "Alana Wechsler",
                    SalesRep = "Ryan David",
                    ProposalName="Test Automation"
                };
            }
        }

    }
}
