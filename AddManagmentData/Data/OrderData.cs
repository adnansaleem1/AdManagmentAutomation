using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddManagmentData.Model;

namespace AddManagmentData.Data
{
    public class OrderData
    {
        public static AdGroupModel AdGroup
        {
            get
            {
                return new AdGroupModel()
                {
                    AddType = "PFP",
                    Budget = 500,
                    GroupName = "Automation Group",
                    ProductGroup = "ESP",
                    Impression = 4,
                };
            }
        }

        public static string Note
        {
            get
            {
                return "This Is Test Automation Note.";
            }
        }
    }
}
