using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddManagmentData.Model
{
   public class OrderLineModel
    {
        public string ProductGroup { get; set; }
        public string AddType { get; set; }
        public int? Position { get; set; }
        public string SearchTerm { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string DeliveryPreferences { get; set; }
        public string ProductInformation { get; set; }
        public int? Impressions { get; set; }
        public double? Cost { get; set; }
        public bool RateEnable { get; set; }
        public double Rate { get; set; }
        public bool GeoTargetEnable { get; set; }
        public string Countries { get; set; }
        public string States { get; set; }
        public bool KeyWordsEnable { get; set; }
        public bool CatogoriesEnable { get; set; }
        public bool SubsitutionsAllow { get; set; }
        public bool DisplayMultipleAddsAllow { get; set; }
        public bool SearchLeadingTextAllow { get; set; }
        public int? Priority { get; set; }
        public int? ImpressionsPerDay { get; set; }
        public string AddGroupName { get; set; }
        public bool? ProductSelectionManual { get; set; }
        public string Status { get; set; }
        public IList<string> ProductId_ManualSelection { get; set; }
    }
}
