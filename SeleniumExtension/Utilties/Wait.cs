using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtension.Controls;
using SeleniumExtension.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeleniumExtension.Utilties
{
    public class Wait
    {
        public static void Second(int sec)
        {
            Thread.Sleep(sec * 1000);
        }
        public static void UntilLoading()
        {
            int MaxWaited = 30;
            int TimeToCalculate = 0;
            int loopWaitTime = 1;
            IWebDriver driver = SDriver.Browser;
            while (Element.Dispaly(By.ClassName("blockOverlay")))
            {
                // driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(Config.LoopTimeOutToCheckElement));
                Thread.Sleep(loopWaitTime * 1000);
                TimeToCalculate += loopWaitTime;
                if (TimeToCalculate >= MaxWaited)
                {
                    break;
                    throw new Exception("Max Wait Reached for Element Search Wait");
                }
            }
            Wait.Second(1);
        }

        public static void AM_Loaging_ShowAndHide()
        {
            var Loading = By.ClassName("block-ui-overlay");
            Wait.UntilDisply(Loading);
            Wait.UntilHide(Loading);
        }

        public static void UntilHide(By by)
        {
            int MaxWaited = 30;
            int TimeToCalculate = 0;
            int loopWaitTime = 1;
            IWebDriver driver = SDriver.Browser;
            while (Element.Dispaly(by))
            {
                // driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(Config.LoopTimeOutToCheckElement));
                Thread.Sleep(loopWaitTime * 1000);
                TimeToCalculate += loopWaitTime;
                if (MaxWaited <= TimeToCalculate)
                {
                    break;
                    throw new Exception("Max Wait Reached for Element Search Wait");
                }
            }
        }

        public static void UntilDisply(By by)
        {
            int MaxWaited = 30;
            int TimeToCalculate = 0;
            int loopWaitTime = 1;
            IWebDriver driver = SDriver.Browser;
            while (!Element.Dispaly(by))
            {
                // driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(Config.LoopTimeOutToCheckElement));
                Thread.Sleep(loopWaitTime * 500);
                TimeToCalculate += loopWaitTime;
                if (MaxWaited <= TimeToCalculate)
                {
                    break;
                    throw new Exception("Max Wait Reached for Element Search Wait");
                }
            }
        }
        public static void UntilDisply(IWebElement ele)
        {
            int MaxWaited = 30;
            int TimeToCalculate = 0;
            int loopWaitTime = 1;
            IWebDriver driver = SDriver.Browser;
            while (!Element.Dispaly(ele))
            {
                // driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(Config.LoopTimeOutToCheckElement));
                Thread.Sleep(loopWaitTime * 500);
                TimeToCalculate += loopWaitTime;
                if (MaxWaited <= TimeToCalculate)
                {
                    break;
                    throw new Exception("Max Wait Reached for Element Search Wait");
                }
            }
        }

        public static void MLSeconds(int p)
        {
            Thread.Sleep(p);
        }

        public static By UntilDisply(IList<By> elelist)
        {


            int MaxWaited = 30;
            int TimeToCalculate = 0;
            int loopWaitTime = 1;
            IWebDriver driver = SDriver.Browser;
            By ResultBy = Element.Dispaly(elelist);
            while (ResultBy == null)
            {
                // driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(Config.LoopTimeOutToCheckElement));
                Thread.Sleep(loopWaitTime * 500);
                TimeToCalculate += loopWaitTime;
                if (MaxWaited <= TimeToCalculate)
                {
                    break;
                    throw new Exception("Max Wait Reached for Element Search Wait");
                }
                ResultBy = Element.Dispaly(elelist);
            }
            return ResultBy;
        }


        public static void UntilDownloading()
        {
            int MaxTimeinSeconds = 500;
            try
            {
                int timeWaited = 0;
                int waitEachInterval = 5000;
                while (!FileHandler.FileDownloaded() && timeWaited < (MaxTimeinSeconds * 1000))
                {
                    timeWaited += waitEachInterval;
                    Thread.Sleep(waitEachInterval);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void UntilTextFill(IWebElement PagesResultResult)
        {
            int MaxTimeinSeconds = 15;
            try
            {
                int timeWaited = 0;
                int waitEachInterval = 5000;
                while (PagesResultResult.Text.Count() < 0 && timeWaited < (MaxTimeinSeconds * 1000))
                {
                    timeWaited += waitEachInterval;
                    Thread.Sleep(waitEachInterval);
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static IList<string> UntilToastMessageShow()
        {
            IList<string> msg=new List<string>();
            Wait.InstantUntilDisply(By.ClassName("toast-message"));
            IWebDriver driver = SDriver.Browser;
            IReadOnlyList<IWebElement> ToastEle=driver.FindElements(By.ClassName("toast-message"));
            foreach (var item in ToastEle)
            {
                msg.Add(item.Text);
                try
                {

                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].style.visibility='hidden'", item);
                }
                catch (Exception)
                {
                }
                
            } return msg;
        }

        public static void UntilAllToastMessageHide()
        {
            IWebDriver driver = SDriver.Browser;
            var ToastList= driver.FindElements(By.ClassName("toast-message"));
            foreach(var toast in ToastList){
                try
                {

                    toast.Click();

                }
                catch (Exception)
                {
                }
            }
            Wait.UntilHide(By.ClassName("toast-message"));
        }
        private static void InstantUntilDisply(By by)
        {
            int MaxWaited = 30;
            int TimeToCalculate = 0;
            int loopWaitTime = 0;
            IWebDriver driver = SDriver.Browser;
            while (!Element.Dispaly(by))
            {
                // driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(Config.LoopTimeOutToCheckElement));
                Thread.Sleep(loopWaitTime * 500);
                TimeToCalculate += loopWaitTime;
                if (MaxWaited <= TimeToCalculate)
                {
                    break;
                    throw new Exception("Max Wait Reached for Element Search Wait");
                }
            }
        }

        public static void UntilClickAble(IWebElement CreatePageBtn)
        {
            IWebDriver driver = SDriver.Browser;
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(10));
            wait.Until(ExpectedConditions.ElementToBeClickable(CreatePageBtn));
        }
    }
}
