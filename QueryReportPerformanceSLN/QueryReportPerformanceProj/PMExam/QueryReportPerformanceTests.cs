using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Text;
using log4net;
using msrb.org.queryreportperf.utilities;
using NUnit.Framework;
using OpenQA.Selenium;

namespace msrb.org.queryreportperf.PMExam
{
    [Category("PerformanceTest")]
    [TestFixture]   
    public class QueryReportPerformanceTests : BaseTest
    {
        private string _perfDataFileName =ConfigurationManager.AppSettings["InputCsvFileName"];
        private readonly ILog _logger = LogManager.GetLogger("root");
        private StringBuilder _verificationErrors;
        private LoginHelper _loginHelper;
        private Random _rng = new Random();

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            log4net.Config.XmlConfigurator.Configure();
            _loginHelper = new LoginHelper();
            _logger.InfoFormat("QueryReportPerformanceTests.TestFixtureSetUp()");
        }

        [SetUp]
        public void SetupTest()
        {
            _driver = new DriverHelper().CurrentDriver;
            _verificationErrors = new StringBuilder();
            _waitHelper.SetImplicitWaitMillis(_driver, 1000);
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                _driver.Quit();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            Assert.AreEqual("", _verificationErrors.ToString());
        }

        [Test]
        public void QPerfTest()
        {
            var file = new FilePathHelper().FindFileInThisProject(_perfDataFileName);
            var records = new FileDataLoader().ReadSpreadSheet(file.FullName);
            QReportDataRecord record;

            GoToRqsLandingPage();
            do
            {
                record = NextRandomRecord(records);
                if (record != null)
                {
                    Debug.Print("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}", record.ReportType,
                        record.PostingDateStart, record.PostingDateEnd,
                        record.MsrbId, record.Cusip6, record.Cusip9, record.IssuerName, record.ObligorName,
                        record.NumTimesToRun);

                    switch (record.ReportType.ToUpper().Trim())
                    {
                        case "Q1":
                            Debug.Print("Record was submitted to Q1.");
                            Q1(record);
                            break;
                        case "Q2":
                            Debug.Print("Record was submitted to Q2.");
                            Q2(record);
                            break;
                        case "Q6":
                            Debug.Print("Record was submitted to Q6.");
                            Q6(record);
                            break;
                        case "Q8":
                            Debug.Print("Record was submitted to Q8.");
                            Q8(record);
                            break;
                        case "Q10":
                            Debug.Print("Record was submitted to Q10.");
                            Q10(record);
                            break;
                        case "Q11":
                            Debug.Print("Record was submitted to Q11.");
                            Q11(record);
                            break;
                        case "Q12":
                            Debug.Print("Record was submitted to Q12.");
                            Q12(record);
                            break;
                        case "Q14":
                            Debug.Print("Record was submitted to Q14.");
                            Q14(record);
                            break;
                        case "Q20":
                            Q20(record);
                            break;
                        case "Q30":
                            Q30(record);
                            break;
                        default:
                            Debug.Print("Unknown report type {0}", record.ReportType);
                            break;
                    }
                }
            } while (record != null);

            Debug.Print("Finished");
        }

        private void Q30(QReportDataRecord record)
        {
            if (record.IssuerName.Trim().Length > 0)
            {
                Debug.Print("Record was submitted to Q30_Submit_IssuerName");
                Q30_Submit_IssuerName(record);
            }
            else if (record.ObligorName.Trim().Length > 0)
            {
                Debug.Print("Record was submitted to Q30_Submit_ObligorName");
                Q30_Submit_ObligorName(record);
            }
            else
            {
                Debug.Print("Record was submitted to Q30_Submit_ByCusip");
                Q30_Submit_ByCusip(record);
            }
        }

        private void Q20(QReportDataRecord record)
        {
            if (record.MsrbId.Trim().Length > 0)
            {
                Debug.Print("Record was submitted to Q20_Submit_ByUnderwriter");
                Q20_Submit_ByUnderwriter(record);
            }
            else
            {
                Debug.Print("Record was submitted to Q20_Submit_ByCusip");
                Q20_Submit_ByCusip(record);
            }
        }

        private void Q14(QReportDataRecord record)
        {
            Click(By.LinkText("All Trades by One Dealer Above Specified Spread (Q14)"));
            SendKeys(By.Id("BeginDate"), record.PostingDateStart);
            SendKeys(By.Id("EndDate"), record.PostingDateEnd);
            SendKeys(By.Name("Spread"), record.Spread);
            SendKeys(By.Id("MsrbId"), record.MsrbId);
            SendKeys(By.Id("CompanyName"), record.CompanyName);
            Click(By.XPath("//*[@type='submit'][@value='Continue']"));
            Click(By.XPath("//*[@type='submit'][@value='Continue']"));
            AssertText(By.CssSelector("p"), "Request for Q14 report has been submitted.");
            Click(By.XPath("//*[@type='button'][@value='Return to Query Menu']"));
            Click(By.LinkText("Trade Data"));
        }

        private void Q12(QReportDataRecord record)
        {
            Click(By.LinkText("All Interdealer Trades by Multiple Clearing Brokers and Executing Dealers (Q12)"));
            SendKeys(By.Id("BeginDate"), record.PostingDateStart);
            SendKeys(By.Id("EndDate"), record.PostingDateEnd);
            SendKeys(By.Name("ClearingIds"), record.ClearingId);
            SendKeys(By.Name("Ebses"), record.EBS);
            Click(By.XPath("//*[@type='submit'][@value='Submit']"));
            AssertText(By.CssSelector("p"), "Request for Q12 report has been submitted.");
            Click(By.XPath("//*[@type='button'][@value='Return to Query Menu']"));
            Click(By.LinkText("Trade Data"));
        }

        private void Q11(QReportDataRecord record)
        {
            Click(By.LinkText("All Interdealer Trades by One Clearing Broker (Q11)"));
            SendKeys(By.Id("BeginDate"), record.PostingDateStart);
            SendKeys(By.Id("EndDate"), record.PostingDateEnd);
            SendKeys(By.Id("ClearingId"), record.ClearingId);
            Click(By.XPath("//*[@type='submit'][@value='Submit']"));
            AssertText(By.CssSelector("p"), "Request for Q11 report has been submitted.");
            Click(By.XPath("//*[@type='button'][@value='Return to Query Menu']"));
            Click(By.LinkText("Trade Data"));
        }

        private void Q10(QReportDataRecord record)
        {
            Click(By.LinkText("All Trades by All Dealers Above Specified Spread (Q10)"));
            SendKeys(By.Id("BeginDate"), record.PostingDateStart);
            SendKeys(By.Id("EndDate"), record.PostingDateEnd);
            SendKeys(By.Name("Spread"), record.Spread);
            Click(By.XPath("//*[@type='submit'][@value='Submit']"));
            AssertText(By.CssSelector("p"), "Request for Q10 report has been submitted.");
            Click(By.XPath("//*[@type='button'][@value='Return to Query Menu']"));
            Click(By.LinkText("Trade Data"));
        }

        private void Q8(QReportDataRecord record)
        {
            Click(By.LinkText("All Trades by All Dealers in Issues with Coupon Exceeding x% (Q8)"));
            SendKeys(By.Id("BeginDate"), record.PostingDateStart);
            SendKeys(By.Id("EndDate"), record.PostingDateEnd);
            SendKeys(By.Id("Coupon"), record.CouponPercent);
            Click(By.XPath("//*[@type='submit'][@value='Submit']"));
            AssertText(By.CssSelector("p"), "Request for Q8 report has been submitted.");
            Click(By.XPath("//*[@type='button'][@value='Return to Query Menu']"));
            Click(By.LinkText("Trade Data"));
        }

        private void Q6(QReportDataRecord record)
        {
            Click(By.LinkText("For Multiple CUSIPs, All Trades by Dealers (Q6)"));
            SendKeys(By.Id("BeginDate"), record.PostingDateStart);
            SendKeys(By.Id("EndDate"), record.PostingDateEnd);
            SendKeys(By.Id("CusipsRawString"), record.Cusip9);
            Click(By.XPath("//*[@type='submit'][@value='Submit']"));
            AssertText(By.CssSelector("p"), "Request for Q6 report has been submitted.");
            Click(By.XPath("//*[@type='button'][@value='Return to Query Menu']"));
            Click(By.LinkText("Trade Data"));
        }

        private void Q2(QReportDataRecord record)
        {
            
            Click(By.LinkText("All Trades in One CUSIP by One Dealer (Q2)"));
            SendKeys(By.Id("BeginDate"), record.PostingDateStart);
            SendKeys(By.Id("EndDate"), record.PostingDateEnd);
            SendKeys(By.Id("Cusip"), record.Cusip9);
            SendKeys(By.Id("MsrbId"), record.MsrbId);
            Click(By.XPath("//*[@type='submit'][@value='Continue']"));
            Click(By.XPath("//*[@type='submit'][@value='Continue']"));
            AssertText(By.CssSelector("p"), "Request for Q2 report has been submitted.");
            Click(By.XPath("//*[@type='button'][@value='Return to Query Menu']"));
            Click(By.LinkText("Trade Data"));
        }

        private void Q1(QReportDataRecord record)
        {
            Click(By.LinkText("All Trades by One Dealer (Q1)"));
            SendKeys(By.Id("BeginDate"),record.PostingDateStart);
            SendKeys(By.Id("EndDate"),record.PostingDateEnd);
            SendKeys(By.Id("MsrbId"),record.MsrbId);
            Click( By.XPath("//*[@type='submit'][@value='Continue']"));
            Click( By.XPath("//*[@type='submit'][@value='Continue']"));
            AssertText( By.CssSelector("p"), "Request for Q1 report has been submitted.");
            Click(By.XPath("//*[@type='button'][@value='Return to Query Menu']"));
            Click(By.LinkText("Trade Data"));
        }

        public void GoToRqsLandingPage()
        {
            _loginHelper.LoginToGatewayAsUser(_driver, _loginHelper.PSOAdminUser1, _loginHelper.GlobalAdminPassword1);
           Click(By.LinkText("Regulator Web"));
           Click( By.LinkText("Regulator Query System"));
        }

        public void Q20_Submit_ByUnderwriter(QReportDataRecord record)
        {
            Click(By.LinkText("Disclosure"));
            Click(By.LinkText("By Underwriter"));
            SendKeys(By.Id("BeginDate"),record.PostingDateStart);
            SendKeys(By.Id("EndDate"),record.PostingDateEnd);
            SendKeys(By.Id("MsrbId"),record.MsrbId);
            Click(By.CssSelector("input.filterSetBtn"));
            AssertText(By.CssSelector("p"), "Request for Q20 report has been submitted.");
            Click( By.CssSelector("input[type=\"button\"]"));
            Click(By.LinkText("Trade Data"));
        }

        public void Q20_Submit_ByCusip(QReportDataRecord record)
        {
            var cusip6 = record.Cusip6;
            var cusip9 = record.Cusip9;
            ValidateCusipEntry(cusip6, cusip9);

            Click(By.LinkText("Disclosure"));
            Click(By.XPath("(//a[contains(text(),'By Security CUSIP Number')])[1]"));
            SendKeys(By.Id("BeginDate"),record.PostingDateStart);
            SendKeys(By.Id("EndDate"),record.PostingDateEnd);
            if (!string.IsNullOrEmpty(cusip6))
            {
                SendKeys(By.Id("Cusip6"),cusip6);
            }

            if (!string.IsNullOrEmpty(cusip9))
            {
                SendKeys(By.Id("Cusip9"), cusip9);
            }

            Click(By.CssSelector("input.filterSetBtn"));
            AssertText(By.CssSelector("p"), "Request for Q20 report has been submitted.");
            Click(By.CssSelector("input[type=\"button\"]"));
            Click(By.LinkText("Trade Data"));
        }

        public void Q30_Submit_ByCusip(QReportDataRecord record)
        {
            var cusip6 = record.Cusip6;
            var cusip9 = record.Cusip9;
            ValidateCusipEntry(cusip6, cusip9);

            Click(By.LinkText("Disclosure"));
            Click(By.XPath("(//a[contains(text(),'By Security CUSIP Number')])[2]"));
            SendKeys(By.Id("BeginDate"),record.PostingDateStart);
            SendKeys(By.Id("EndDate"),record.PostingDateEnd);
            if (!string.IsNullOrEmpty(cusip6))
            {
                SendKeys(By.Id("Cusip6"),cusip6);
            }

            if (!string.IsNullOrEmpty(cusip9))
            {
                SendKeys(By.Id("Cusip9"),cusip9);
            }

            Click(By.CssSelector("input.filterSetBtn"));
            AssertText( By.CssSelector("p"), "Request for Q30 report has been submitted.");
            Click(By.CssSelector("input[type=\"button\"]"));
            Click(By.LinkText("Trade Data"));
        }

        public void Q30_Submit_ObligorName(QReportDataRecord record)
        {
            Click(By.LinkText("Disclosure"));
            Click(By.LinkText("By CUSIP-6 or Obligor Name"));
            SendKeys(By.Id("BeginDate"),record.PostingDateStart);
            SendKeys(By.Id("EndDate"),record.PostingDateEnd);
            SendKeys(By.Id("ObligorName"),record.ObligorName);
            Click(By.CssSelector("input.filterSetBtn"));
            AssertText( By.CssSelector("p"), "Request for Q30 report has been submitted.");
            Click(By.CssSelector("input[type=\"button\"]"));
            Click(By.LinkText("Trade Data"));
        }

        public void Q30_Submit_IssuerName(QReportDataRecord record)
        {
            Click(By.LinkText("Disclosure"));
            Click(By.LinkText("By CUSIP-6 or Obligor Name"));
            SendKeys(By.Id("BeginDate"),record.PostingDateStart);
            SendKeys(By.Id("EndDate"),record.PostingDateEnd);
            SendKeys(By.Id("IssuerName"),record.IssuerName);
            Click(By.CssSelector("input.filterSetBtn"));
            AssertText(By.CssSelector("p"), "Request for Q30 report has been submitted.");
            Click(By.CssSelector("input[type=\"button\"]"));
            Click(By.LinkText("Trade Data"));
        }

        private QReportDataRecord NextRandomRecord(List<QReportDataRecord> records)
        {
            if (records.Count == 0)
            {
                return null;
            }
            int rInt = _rng.Next(0, records.Count - 1);
            var record = records[rInt];
            if (int.Parse(record.NumTimesToRun) > 0)
            {
                record.NumTimesToRun = int.Parse(record.NumTimesToRun) - 1 + "";
                return record;
            }
            records.RemoveAt(rInt);
            return NextRandomRecord(records);
        }

        private static void ValidateCusipEntry(string cusip6, string cusip9)
        {
            if (string.IsNullOrEmpty(cusip6) && string.IsNullOrEmpty(cusip9))
            {
                throw new Exception("No values detected for CUSIP-6 or CUSIP-9. Please provide either a CUSIP-6 or a CUSIP-9.");
            }

            if (!string.IsNullOrEmpty(cusip6) && !string.IsNullOrEmpty(cusip9))
            {
                throw new Exception(
                    "Detected values for both CUSIP-6 and CUSIP-9. Please provide one value for CUSIP-6 or CUSIP-9.");
            }
        }
    }
}
