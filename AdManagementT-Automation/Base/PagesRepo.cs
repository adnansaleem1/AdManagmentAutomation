using AdManagementT_Automation.Pages;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumExtension.Driver;
using AdManagementT_Automation.Controls;

namespace AdManagementT_Automation.Base
{
    public  class PagesRepo
    {
        public static T GetPage<T>() where T : new()
        {
            var page = new T();
            PageFactory.InitElements(SDriver.Browser, page);
            return page;
        }

        public static LoginPage LoginP { get { return PagesRepo.GetPage<LoginPage>(); } }
        public static AM_Tabs AddTabs { get { return PagesRepo.GetPage<AM_Tabs>(); } }

        
    }
}
