using System;

namespace TestomaticSupport
{
    public class SummaryReportItem
    {
        public SummaryReportItem(string type)
        {
            Type = type;
        }

        public string Type { get; set; }
        public string Name { get; set; }
        public string Objective { get; set; }
        public string Precondition { get; set; }
        public string Postcondition { get; set; }
        public DateTime? BeginTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool? Pass { get; set; }
    }
}
