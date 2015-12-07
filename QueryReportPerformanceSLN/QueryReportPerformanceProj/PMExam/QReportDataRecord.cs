namespace msrb.org.queryreportperf.PMExam
{
    public class QReportDataRecord
    {
        public string ReportType { get; set; }
        public string PostingDateStart { get; set; }
        public string PostingDateEnd { get; set; }
        public string MsrbId { get; set; }
        public string Cusip6 { get; set; }
        public string Cusip9 { get; set; }
        public string IssuerName { get; set; }
        public string ObligorName { get; set; }
        public string CouponPercent { get; set; }
        public string ClearingId { get; set; }
        public string EBS { get; set; }
        public string Spread { get; set; }
        public string NumTimesToRun { get; set; }
        public string CompanyName { get; set; }
    }
}