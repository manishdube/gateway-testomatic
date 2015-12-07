using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;

namespace msrb.org.queryreportperf.PMExam
{
    public class FileDataLoader
    {
        public List<QReportDataRecord> ReadSpreadSheet(string path)
        {
            using (var sr = new StreamReader(path))
            {
                var reader = new CsvReader(sr);
                return reader.GetRecords<QReportDataRecord>().ToList();
            }
        }
    }
}