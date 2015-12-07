using System;
using msrb.org.queryreportperf.PMExam;

namespace msrb.org.queryreportperf
{
    class Program
    {
        static void Main(string[] args)
        {
            var test=new QueryReportPerformanceTests();
            test.TestFixtureSetUp();
            test.SetupTest();
            test.QPerfTest();

            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();
        }
    }
}
