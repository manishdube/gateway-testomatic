using System;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;

namespace TestomaticSupport
{
    public class ExcelReporter : IDisposable
    {
        private ExcelPackage _excel;
        private ExcelWorksheet _dataSheet;
        private int _currentRow = 8;

        public void Init(string templateFileName)
        {
            _excel = new ExcelPackage(new FileInfo(Path.Combine(".", templateFileName)));
            _dataSheet = _excel.Workbook.Worksheets[1];
        }

        public void FormatReport(ReportInfo info, List<SummaryReportItem> items)
        {
            FormatHeader(info);
            foreach (var item in items)
            {
                if (item.Type == "Context")
                {
                    _dataSheet.Cells[_currentRow, 1].Value = item.Name;
                    _dataSheet.Cells[_currentRow, 5].Value = CalcDuration(item);
                }
                else if (item.Type == "Test")
                {
                    _dataSheet.Cells[_currentRow, 2].Value = item.Name;
                    _dataSheet.Cells[_currentRow, 4].Value = FormatPass(item.Pass);
                    _dataSheet.Cells[_currentRow, 5].Value = CalcDuration(item);
                    if (!string.IsNullOrEmpty(item.Objective))
                    {
                        _currentRow++;
                        _dataSheet.Cells[_currentRow, 3].Value = "Objective: " + item.Objective;
                    }
                    if (!string.IsNullOrEmpty(item.Precondition))
                    {
                        _currentRow++;
                        _dataSheet.Cells[_currentRow, 3].Value = "Precondition: " + item.Precondition;
                    }
                    if (!string.IsNullOrEmpty(item.Postcondition))
                    {
                        _currentRow++;
                        _dataSheet.Cells[_currentRow, 3].Value = "Postcondition: " + item.Postcondition;
                    }
                }
                _currentRow++;
            }
        }

        private void FormatHeader(ReportInfo info)
        {
            _dataSheet.Cells["C1"].Value = info.TestApplicationName;
            _dataSheet.Cells["C2"].Value = info.Version;
            _dataSheet.Cells["C3"].Value = info.ExecutionTime;
            _dataSheet.Cells["C4"].Value = info.Browser;
            _dataSheet.Cells["C5"].Value = string.Join(": ", info.Parameters);
            _dataSheet.Cells["E2"].Value = info.PassCount;
            _dataSheet.Cells["E3"].Value = info.FailCount;
            _dataSheet.Cells["E4"].Value = FormatSuiteDuration(info.DurationMinutes, info.DurationSeconds);
        }

        private string FormatPass(bool? pass)
        {
            if (pass.HasValue && pass.Value)
            {
                return "Pass";
            }
            if (pass.HasValue && !pass.Value)
            {
                return "Fail";
            }
            return "Unknown";
        }

        private string CalcDuration(SummaryReportItem item)
        {
            if (item.BeginTime.HasValue && item.EndTime.HasValue)
            {
                var seconds = (item.EndTime.Value - item.BeginTime.Value).TotalSeconds;
                int minutes = (int)seconds/60;
                seconds = seconds%60;
                return minutes.ToString("0") + ":" + seconds.ToString("00.0");
            }
            return null;
        }

        private string FormatSuiteDuration(int durationMinutes, int durationSeconds)
        {
            int hours = durationMinutes/60;
            durationMinutes = durationMinutes%60;
            return hours.ToString("0") + ":" + durationMinutes.ToString("00") + ":" + durationSeconds.ToString("00");
        }

        public void Save(string outputFile)
        {
            var output = new FileInfo(outputFile);
            if (output.Exists)
            {
                output.Delete();
                output = new FileInfo(outputFile);
            }
            _excel.SaveAs(output);
        }

        public void Dispose()
        {
            if (_excel == null)
            {
                return;
            }
            _excel.Dispose();
            _excel = null;
        }
    }
}
