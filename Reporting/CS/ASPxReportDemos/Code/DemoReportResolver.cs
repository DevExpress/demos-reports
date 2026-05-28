using System.Web;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Web;
using DevExpress.XtraReports.Web.WebDocumentViewer;

namespace DevExpress.Web.Demos {
    public class DemoReportResolver : IWebDocumentViewerReportResolver, ICachedReportSourceWebResolver {
        bool ICachedReportSourceWebResolver.TryGetCachedReportSourceWeb(string reportEntry, out CachedReportSourceWeb cachedReportSourceWeb) {
            XtraReport report = ((IWebDocumentViewerReportResolver)this).Resolve(reportEntry);
            cachedReportSourceWeb = report == null ? null : new CachedReportSourceWeb(report);
            return cachedReportSourceWeb != null;
        }

        XtraReport IWebDocumentViewerReportResolver.Resolve(string reportEntry) {
            return ReportStorageHelper.LoadReport(reportEntry, HttpContext.Current != null ? HttpContext.Current.Session : null);
        }
    }
}
