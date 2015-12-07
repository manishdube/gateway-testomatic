using NUnit.Framework;
using OpenQA.Selenium;
using selenium_runner.utilities;

namespace msrb.org.queryreportperf.utilities
{
    [TestFixture]   
    public class BaseTest
    {
        protected WaitHelper _waitHelper=new WaitHelper();
        protected IWebDriver _driver;

        public void Click(By _by)
        {
            _waitHelper.WaitForElementToAppearThenClickIt(_driver, _by);
        }

        public void SendKeys(By _by,string text)
        {
            _waitHelper.WaitForElementToAppearThenSendKeys(_driver, _by,text);
        }

        public void AssertText(By _by, string text)
        {
            _waitHelper.WaitForElementToAppearThenAssertText(_driver, _by, text);
        }
    }
}
