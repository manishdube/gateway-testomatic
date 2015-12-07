using System.Configuration;
using OpenQA.Selenium;
using selenium_runner.utilities;

namespace msrb.org.queryreportperf.utilities
{
    public class LoginHelper
    {
        public string DefaultUser1 {
            get { return ConfigurationManager.AppSettings["DefaultUser1"]; }
        }

        public string DefaultPassword1 {
            get { return ConfigurationManager.AppSettings["DefaultPassword1"]; } 
        }

        public string GlobalAdminUser1 {
            get { return ConfigurationManager.AppSettings["GlobalAdminUser1"]; }
        }

        public string GlobalAdminPassword1 {
            get { return ConfigurationManager.AppSettings["GlobalAdminPassword1"]; } 
        }

        public string PSOAdminUser1
        {
            get { return ConfigurationManager.AppSettings["PSOAdminUser1"]; }
        }

        public string AccountingUser1
        {
            get { return ConfigurationManager.AppSettings["AccountingUser1"]; }
        }
        
        public string CallCenterUser1
        {
            get { return ConfigurationManager.AppSettings["CallCenterUser1"]; }
        }
        
        public string DealerUser1
        {
            get { return ConfigurationManager.AppSettings["DealerUser1"]; }
        }

        public string RegressionUserEmail
        {
            get { return ConfigurationManager.AppSettings["RegressionUserEmail"]; }
        }

        public string RegressionUserEmailPassword
        {
            get { return ConfigurationManager.AppSettings["RegressionUserEmailPassword"]; }
        }

        public void LoginToEmmaAsUser(IWebDriver driver,string username,string password)
        {
            driver.Navigate().GoToUrl(new BaseUrlHelper().EmmaBaseUrl + "/Home");
            driver.FindElement(By.LinkText("EMMA Dataport")).Click();
            driver.FindElement(By.Id("ctl00_mainContentArea_loginButton")).Click();
            driver.FindElement(By.Id("UserName")).Clear();
            driver.FindElement(By.Id("UserName")).SendKeys(username);
            driver.FindElement(By.Id("UPass")).Clear();
            driver.FindElement(By.Id("UPass")).SendKeys(password);
            driver.FindElement(By.Id("LoginButton")).Click();
        }

        public void LoginToGatewayAsUser(IWebDriver driver, string username, string password)
        {
            driver.Navigate().GoToUrl(new BaseUrlHelper().GatewayBaseUrl + "/msrb1/control/");
            driver.FindElement(By.Id("UserName")).Clear();
            driver.FindElement(By.Id("UserName")).SendKeys(username);
            driver.FindElement(By.Id("UPass")).Clear();
            driver.FindElement(By.Id("UPass")).SendKeys(password);
            driver.FindElement(By.Id("LoginButton")).Click();
        }
    }
}