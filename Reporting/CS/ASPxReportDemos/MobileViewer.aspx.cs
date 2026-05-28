using System;
using DevExpress.Web.Demos;

namespace DevExpress.Web.Demos {
    public partial class MobileViewer : System.Web.UI.Page {
        string reportName;

        protected void Page_Load(object sender, EventArgs e) {
            if((Request.QueryString != null) && (Request.QueryString[DemoQueryKeys.ReportTypeNameKey] != null)) {
                reportName = Request.QueryString[DemoQueryKeys.ReportTypeNameKey];
            } else {
                reportName = typeof(XtraReportsDemos.MasterDetailReport.Report).FullName;
            }

            Header.Title = Utils.GetCurrentDemoPageTitle();
            webDocumentViewer.OpenReport(ReportStorageHelper.LoadReport(reportName, Session));
        }
    }
}
