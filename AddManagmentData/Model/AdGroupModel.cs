using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddManagmentData.Model
{
    public class AdGroupModel
    {
        public string ProductGroup { get; set; }
        public string AddType { get; set; }
        public string GroupName { get; set; }
        public int? Impression { get; set; }
        public double? Budget { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
