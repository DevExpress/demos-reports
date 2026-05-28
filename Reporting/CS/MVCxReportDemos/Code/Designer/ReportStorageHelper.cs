using System.IO;
using System.Text;
using System.Web;
using DevExpress.XtraReports.UI;

namespace DevExpress.Web.Demos.Code.Designer {
    public static class ReportStorageHelper {
        public static XtraReport LoadReport(string reportId, HttpSessionStateBase session) {
            var reportLayout = GetReportLayout(reportId, session);
            if(reportLayout != null) {
                using(var stream = new MemoryStream(reportLayout)) {
                    return XtraReport.FromXmlStream(stream, true);
                }
            }
            return ReportDemoHelper.GetReport(reportId);
        }

        public static void SaveReportLayout(string reportName, byte[] reportLayout, HttpSessionStateBase session) {
            session[GetKey(reportName)] = reportLayout;
        }

        static byte[] GetReportLayout(string reportName, HttpSessionStateBase session) {
            return session[GetKey(reportName)] as byte[];
        }

        static string GetKey(string reportName) {
            const string SessionPrefix = "XRDesignedReportLayout";
            return SessionPrefix + reportName;
        }
    }
}
