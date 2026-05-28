using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace AspNetCoreDemos.Reporting.Services {
    public interface IDemoReportSource {
        XtraReport GetReport(string reportName);
        Dictionary<string, string> GetReportList();
    }
}
