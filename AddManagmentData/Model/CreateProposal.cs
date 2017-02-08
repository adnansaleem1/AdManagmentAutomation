using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddManagmentData.Model
{
    class CreateProposal
    {
        public IList<string> PageNames { get; set; }
        public int MemeberID { get; set; }
        public string ComapnyName { get; set; }
        public string Contact { get; set; }
        public string SalesRep { get; set; }
        public DateTime ProposalDate { get; set; }
        public string ProposalName { get; set; }
    }
}
