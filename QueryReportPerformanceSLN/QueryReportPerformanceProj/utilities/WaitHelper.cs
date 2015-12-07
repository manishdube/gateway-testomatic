using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace msrb.org.queryreportperf.utilities
{
    public class WaitHelper
    {
        private ILog logger = LogManager.GetLogger("root");

        public void SetImplicitWaitMillis(IWebDriver driver,long milliSeconds)
        {
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromMilliseconds(milliSeconds));
        }

        public void WaitForElementToAppearThenClickIt(IWebDriver driver, By _by, long milliSeconds = 5000)
        {
            var remainingMillis = milliSeconds;
            var wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(milliSeconds * .4));
            while (remainingMillis > 0)
            {
                try
                {
                    var webElement=wait.Until((d) => d.FindElement(_by));
                    if (webElement!=null)
                    {
                        webElement.Click();
                        return;
                    }
                }
                catch (StaleElementReferenceException e)
                {
                    logger.InfoFormat("Element was stale, refreshing...  {0} ", e.Message);
                }
                catch (Exception e2)
                {
                    logger.InfoFormat("{0} ", e2.Message);
                }

                remainingMillis -= (long)(milliSeconds * .4);
            }
            throw new Exception(string.Format("Unable to find element {0}.", _by));
        }

        public IWebElement WaitForElementToAppear(IWebDriver driver,By _by,long milliSeconds=5000)
        {
            var remainingMillis = milliSeconds;
            var wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(milliSeconds * .4));
            while (remainingMillis > 0)
            {
                try
                {
                    return wait.Until((d) => d.FindElement(_by));
                }
                catch (StaleElementReferenceException e)
                {
                    logger.InfoFormat("Element was stale, refreshing...  {0} ", e.Message);
                }
                catch (Exception e2)
                {
                    logger.InfoFormat("{0} ", e2.Message);
                }

                remainingMillis -= (long)(milliSeconds * .4);
            }
            throw new Exception(string.Format("Unable to find element {0}.", _by));
        }

        public ReadOnlyCollection<IWebElement> WaitForElementsToAppear(IWebDriver driver, By _by, long milliSeconds)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(milliSeconds));
            return wait.Until((d) => d.FindElements(_by));
        }

        public IWebElement WaitForElementToAppear(IWebDriver driver, By _by,string searchText, long waitMilliSeconds, int sleepCycleMillis)
        {
            var remainingMillis = waitMilliSeconds;
            var wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(sleepCycleMillis));
            while (remainingMillis>0)
            {
                try
                {
                    ReadOnlyCollection<IWebElement> elements = wait.Until((d) => d.FindElements(_by));
                    if (elements != null && elements.Count > 0)
                    {
                        IWebElement val = elements.Select(s => s).FirstOrDefault(s => s.Text.ToUpper().Contains(searchText.ToUpper()));
                        if (val != null)
                        {
                            return val;
                        }
                    }
                }
                catch (StaleElementReferenceException e)
                {
                    logger.InfoFormat("Element was stale, refreshing...  {0} ",e.Message);
                }
                
                Thread.Sleep(sleepCycleMillis);
                remainingMillis -= sleepCycleMillis;
            }
            throw new Exception(string.Format("Unable to find element {0}.",_by));
        }

        public void WaitForElementToAppearThenSendKeys(IWebDriver _driver, By _by, string _text, long milliSeconds = 5000)
        {
            var remainingMillis = milliSeconds;
            var wait = new WebDriverWait(_driver, TimeSpan.FromMilliseconds(milliSeconds * .4));
            while (remainingMillis > 0)
            {
                try
                {
                    var webElement=wait.Until((d) => d.FindElement(_by));
                    if (webElement!=null)
                    {
                        webElement.SendKeys(_text);
                        return;
                    }
                }
                catch (StaleElementReferenceException e)
                {
                    logger.InfoFormat("Element was stale, refreshing...  {0} ", e.Message);
                }
                catch (Exception e2)
                {
                    logger.InfoFormat("{0} ", e2.Message);
                }

                remainingMillis -= (long)(milliSeconds * .4);
            }
        }

        public void WaitForElementToAppearThenAssertText(IWebDriver _driver, By _by, string _text, long milliSeconds = 5000)
        {
            var remainingMillis = milliSeconds;
            var wait = new WebDriverWait(_driver, TimeSpan.FromMilliseconds(milliSeconds * .4));
            while (remainingMillis > 0)
            {
                try
                {
                    ReadOnlyCollection<IWebElement> elements = wait.Until((d) => d.FindElements(_by));
                    if (elements != null && elements.Count > 0)
                    {
                        IWebElement val = elements.Select(s => s).FirstOrDefault(s => s.Text.ToUpper().Contains(_text.ToUpper()));
                        if (val != null)
                        {
                            return;
                        }
                    }
                }
                catch (StaleElementReferenceException e)
                {
                    logger.InfoFormat("Element was stale, refreshing...  {0} ", e.Message);
                }

                remainingMillis -= (long)(milliSeconds * .4);
            }
            throw new Exception(string.Format("Unable to find text '{0}'  in element {1}.",_text,_by));
        }
    }
}
