using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddManagmentData.Model
{
   public class WaitListModel
    {
        public string MemberId { get; set; }
        public string CompanyName { get; set; }
        public string ProductGroup { get; set; }
        public string AddType { get; set; }
        public string SearchTerms { get; set; }
        public int? Position { get; set; }
        public double? Cost { get; set; }
        public string defaultProduct { get; set; }
        public string DeliveryPrefrnces { get; set; }
        public string SalesRep { get; set; }
        public string Coordinator { get; set; }
        public DateTime DateRequested { get; set; }
    }
}
