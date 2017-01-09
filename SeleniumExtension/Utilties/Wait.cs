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
       public static void Second(int sec) {
           Thread.Sleep(sec * 1000);
       }
        public static void UntilLoading() {
            int MaxWaited = 30;
            int TimeToCalculate =0;
            int loopWaitTime = 1;
            IWebDriver driver = SDriver.Browser;
            while (Element.Dispaly(By.ClassName("blockOverlay")))
            {
                // driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(Config.LoopTimeOutToCheckElement));
                Thread.Sleep(loopWaitTime*1000);
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

        private static void UntilHide(By by)
        {
            int MaxWaited = 30;
            int TimeToCalculate = 0;
            int loopWaitTime = 1;
            IWebDriver driver = SDriver.Browser;
            while (Element.Dispaly(by))
            {
                // driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(Config.LoopTimeOutToCheckElement));
                Thread.Sleep(loopWaitTime * 1000);
                MaxWaited +=loopWaitTime;
                if (MaxWaited <= TimeToCalculate)
                {
                    break;
                    throw new Exception("Max Wait Reached for Element Search Wait");
                }
            }
        }

        private static void UntilDisply(By by)
        {
            int MaxWaited = 30;
            int TimeToCalculate = 0;
            int loopWaitTime = 1;
            IWebDriver driver = SDriver.Browser;
            while (Element.Dispaly(by))
            {
                // driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(Config.LoopTimeOutToCheckElement));
                Thread.Sleep(loopWaitTime * 500);
                MaxWaited += loopWaitTime;
                if (MaxWaited <= TimeToCalculate)
                {
                    break;
                    throw new Exception("Max Wait Reached for Element Search Wait");
                }
            }
        }
    }
}
