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
        public static InventoryModel ForBags { get{
        return  new InventoryModel(){
        InventoryType="PFP",
        ProductGroup="ESP Websites",
        AdType="PFP",
        IncludeSubCat=true,
        SearchTerms=new List<string>(){"Bags"}
        };
        }}
    }
}
