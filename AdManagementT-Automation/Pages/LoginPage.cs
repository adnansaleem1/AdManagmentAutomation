using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using AddManagmentData.Model;
using AddManagmentData.Common;
using SeleniumExtension.Driver;
using SeleniumExtension.Utilties;
using SeleniumExtension.Controls;



namespace AdManagementT_Automation.Pages
{
    public class LoginPage
    {
        private IWebDriver driver = SDriver.Browser;
        private AddUser LoginUser=null;
        private string Url = AMUrls.BaseUrl + "";

        [FindsBy(How = How.Id, Using = "txtUserName")]
        public IWebElement UserName { get; set; }

        [FindsBy(How = How.Id, Using = "txtPassword")]
        public IWebElement Password { get; set; }

        [FindsBy(How = How.Id, Using = "loginMsg")]
        public IWebElement loginMsg { get; set; }

        [FindsBy(How = How.Id, Using = "SelectedSegment")]
        public IWebElement AppCat { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[value='Go']")]
        public IWebElement GoBtn { get; set; }

        [FindsBy(How = How.Id, Using = "txtAsiNum")]
        public IWebElement AsiNumber { get; set; }
        

        [FindsBy(How = How.CssSelector, Using = "input[value='Login']")]
        public IWebElement Submit { get; set; }

        public LoginPage LoginGivenUser(AddUser userData) {
            if(userData==null){
            throw new Exception("Given informtaion is not Valid for login");
            }
            UserName.Clear();
            UserName.SendKeys(userData.UserName);
            Password.Clear();
            Password.SendKeys(userData.PassWord);
            AsiNumber.Clear();
            AsiNumber.SendKeys(userData.ASINumber);
            Submit.Click();
            Wait.UntilLoading();
            if (DriveURL.GetLocalPath() == "/login")
            {
                throw new Exception("Unable To Login :" + loginMsg.Text);
            }
            LoginUser = userData;

            return this;
        }


        internal LoginPage Navigate()
        {
            driver.Url = Url;
            return this;
        }

        internal void GoToAppCatagory(string p)
        {
            Select.ByText(AppCat, p);
            GoBtn.Click();
            Wait.AM_Loaging_ShowAndHide();
        }
    }
}
