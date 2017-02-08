using AddManagmentData.Model.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumExtension.Ref;

namespace AddManagmentData.Data
{
    public class PagesData
    {
        public static PageModel Page1
        {
            get
            {
                return new PageModel()
                {
                    Category = "General",
                    KeyWordList = new List<string>() { "Auto" + Config.GetUniqueId() },
                    AdditionalKeyWords=new List<string>(){"Add"+Config.GetUniqueId()} 
                };
            }
        }


    }
}
