using AdManagementT_Automation.Pages;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumExtension.Driver;
using AdManagementT_Automation.Controls;
using AdManagementT_Automation.Pages.Insertion_Orders;
using AdManagementT_Automation.Pages.Adevertisements;
using AdManagementT_Automation.Pages.Inventory;
using AdManagementT_Automation.Pages.Admin;

namespace AdManagementT_Automation.Base
{
    public class PagesRepo
    {
        public static T GetPage<T>() where T : new()
        {
            var page = new T();
            PageFactory.InitElements(SDriver.Browser, page);
            return page;
        }

        public static LoginPage LoginP { get { return PagesRepo.GetPage<LoginPage>(); } }
        public static AM_Tabs AddTabs { get { return PagesRepo.GetPage<AM_Tabs>(); } }
        public static AllOrdersPage AllOrders { get { return PagesRepo.GetPage<AllOrdersPage>(); } }
        public static EditOrderPage EditOrder { get { return PagesRepo.GetPage<EditOrderPage>(); } }
        public static EditOrderLinePage EditOrderLine { get { return PagesRepo.GetPage<EditOrderLinePage>(); } }
        public static AdvertisementPage Advertisement { get { return PagesRepo.GetPage<AdvertisementPage>(); } }
        public static InventoryPage Inventory { get { return PagesRepo.GetPage<InventoryPage>(); } }
        public static PagesPage adminpages { get { return PagesRepo.GetPage<PagesPage>(); } }







    }
}
