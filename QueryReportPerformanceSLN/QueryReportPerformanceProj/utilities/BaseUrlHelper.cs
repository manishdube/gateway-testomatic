using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace selenium_runner.utilities
{
    class BaseUrlHelper
    {
        public string EmmaBaseUrl
        {
            get
            {
                return string.Format("http://emma{0}.msrb.org/", ConfigurationManager.AppSettings["TestEnv"]);
            }
        }

        public string GatewayBaseUrl
        {
            get
            {
                return string.Format("https://gw{0}.msrb.org/", ConfigurationManager.AppSettings["TestEnv"]);
            }
        }
    }
}
