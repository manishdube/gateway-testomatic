using System;
using System.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Safari;
using selenium_runner.utilities;

namespace msrb.org.queryreportperf.utilities
{
    class DriverHelper
    {
        public IWebDriver CurrentDriver
        {
            get
            {
                var driverSetting = ConfigurationManager.AppSettings["CurrentDriver"];

                if (typeof(ChromeDriver).FullName.Contains(driverSetting))
                {
                    var chromeDriverPath=new FilePathHelper().FindFileInThisProject("chromedriver.exe");
                    return new ChromeDriver(chromeDriverPath.DirectoryName);
                }

                if (typeof(FirefoxDriver).FullName.Contains(driverSetting))
                {
                    return new FirefoxDriver();
                }

                if (typeof(InternetExplorerDriver).FullName.Contains(driverSetting))
                {
                    return new InternetExplorerDriver();
                }

                if (typeof(SafariDriver).FullName.Contains(driverSetting))
                {
                    return new SafariDriver();
                }
                
                throw new Exception("Driver not supported.");
            }
        }
    }
}
