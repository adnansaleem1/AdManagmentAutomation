using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddManagmentData.Model
{
   public class InventoryModel
    {
        public string ProductGroup { get; set; }
        public string InventoryType { get; set; }
        public string AdType { get; set; }
        public string Month { get; set; }
        public int? Year { get; set; }
        public IList<string>  SearchTerms { get; set; }
        public bool IncludeSubCat { get; set; }
    }
   public class PropsalModel : InventoryModel {
       public string MemberId { get; set; }
       public string Contact { get; set; }
       public string SalesRep { get; set; }
       public string ProposalName { get; set; }
       public DateTime ProposalDate { get; set; }
   }
}
