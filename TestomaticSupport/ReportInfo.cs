using System;
using System.Collections.Generic;

namespace TestomaticSupport
{
    public class ReportInfo
    {
        public ReportInfo()
        {
            Parameters = new List<string>();
        }

        public string TestApplicationName { get; set; }
        public string Version { get; set; }
        public DateTime? ExecutionTime { get; set; }
        public string Browser { get; set; }
        public List<string> Parameters { get; set; }
        public int DurationMinutes { get; set; }
        public int DurationSeconds { get; set; }
        public int PassCount { get; set; }
        public int FailCount { get; set; }
    }
}
