using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddManagmentData.Model.Admin
{
    public class PageModel
    {
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public IList<string> KeyWordList { get; set; }
        public string GenratedPageName { get; set; }
        public IList<string> AdditionalKeyWords { get; set; }
    }
}
