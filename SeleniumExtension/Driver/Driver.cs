using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using SeleniumExtension.Ref;
using System;

namespace SeleniumExtension.Driver
{
    public class SDriver
    {
        

    private static WebDriverWait browserWait;
    private static IWebDriver browser;
    public static IWebDriver Browser
    {
        get
        {
            if (browser == null)
            {
                throw new NullReferenceException("The WebDriver browser instance was not initialized. You should first call the method Start.");
            }
            return browser;
        }
        private set
        {
            browser = value;
        }
    }
    public static WebDriverWait BrowserWait
    {
        get
        {
            if (browserWait == null || browser == null)
            {
                throw new NullReferenceException("The WebDriver browser wait instance was not initialized. You should first call the method Start.");
            }
            return browserWait;
        }
        private set
        {
            browserWait = value;
        }
    }
    public static void StartBrowser(BrowserTypes browserType = BrowserTypes.Firefox, int defaultTimeOut = 30)
    {
        switch (browserType)
        {
            case BrowserTypes.Firefox:
                SDriver.Browser = new FirefoxDriver();
                break;
            case BrowserTypes.InternetExplorer:
                SDriver.Browser = new InternetExplorerDriver();
                break;
            case BrowserTypes.Chrome:
                SDriver.Browser = new ChromeDriver();
                break;
            default:
                break;
        }
        BrowserWait = new WebDriverWait(SDriver.Browser, TimeSpan.FromSeconds(defaultTimeOut));
    }
    public static void StopBrowser()
    {
        Browser.Quit();
        Browser = null;
        BrowserWait = null;
    }

    }
}
