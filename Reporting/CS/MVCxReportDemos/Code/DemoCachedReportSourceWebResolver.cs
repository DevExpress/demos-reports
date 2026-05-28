using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Web;
using DevExpress.XtraReports.Web.WebDocumentViewer;

public class DemoCachedReportSourceWebResolver : ICachedReportSourceWebResolver {
    readonly IWebDocumentViewerReportResolver reportResolver;
    public DemoCachedReportSourceWebResolver(IWebDocumentViewerReportResolver reportResolver) {
        this.reportResolver = reportResolver;
    }
    public bool TryGetCachedReportSourceWeb(string reportEntry, out CachedReportSourceWeb cachedReportSourceWeb) {
        XtraReport report = reportResolver.Resolve(reportEntry);        
        cachedReportSourceWeb = report == null ? null : new CachedReportSourceWeb(report);
        return cachedReportSourceWeb != null;
    }
}
