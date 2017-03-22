using AddManagmentData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddManagmentData.Data
{
   public class WaitListData
    {

        public static WaitListModel EspPfp
        {
            get
            {
                return new WaitListModel() {
                    MemberId = "88660",
                    ProductGroup="ESP",
                    AddType="PFP",
                    SearchTerms = "3-D PRODUCTS",
                    Position=1,
                    DeliveryPrefrnces="Standard",
                    Cost=10,
                    defaultProduct = "5625470",
                    SalesRep="Ryan David",
                    Coordinator="Kate Irish"
                };
            }
        }
    }
}
