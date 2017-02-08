using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumExtension.Controls
{
    public class Modal
    {
        public static void ClickYes(IWebElement dialog) {
            Element.FindByTagAndTextInContainer(dialog,"Yes","button").Click();
        }
    }
}
