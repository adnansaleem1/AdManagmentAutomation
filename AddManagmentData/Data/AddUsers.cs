using AddManagmentData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddManagmentData.Data
{
    public class AddUserData
    {
        public static AddUser AddUser1 { get { return new AddUser() { ASINumber = "30232", PassWord = "pakistan1234@", UserName = "mallick" }; } }
        public static AddUser SalesUser { get { return new AddUser() { ASINumber = "30232", PassWord = "eex30232", UserName = "eex30232" }; } }
        public const int TO = 1;
    }
}
