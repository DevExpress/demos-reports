using DevExpress.XtraReports.Web;
using DevExpress.XtraReports.Web.WebDocumentViewer;

namespace AspNetCoreDemos.Reporting.Services {
    public class DemoCachedReportSourceWebResolver : ICachedReportSourceWebResolver {
        IDemoReportSource ReportSource { get; }
        public DemoCachedReportSourceWebResolver(IDemoReportSource reportSource) {
            ReportSource = reportSource;
        }


        public bool TryGetCachedReportSourceWeb(string reportName, out CachedReportSourceWeb cachedReportSource) {
            var report = ReportSource.GetReport(reportName);
            if(report == null) {
                cachedReportSource = null;
                return false;
            }

            cachedReportSource = new CachedReportSourceWeb(report);
            return true;
        }
    }
}
