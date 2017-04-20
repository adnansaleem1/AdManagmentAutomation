using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddManagmentData.Model
{
    public class AdvertisementSearchModel
    {
        public string MemberID { get; set; }
        public IList<int> Positions { get; set; }
        public IList<string> SearchTerms { get; set; }
        public bool IncludeSubCat { get; set; }
        public IList<string> SalesReps { get; set; }
        public IList<string> Statuses { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }
        public IList<string> Rates { get; set; }
        public string SearchField { get; set; }
        public IList<string> ProductGroup { get; set; }
        public IList<string> AddType { get; set; }
    
    }
}
