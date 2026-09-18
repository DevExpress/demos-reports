using System;

namespace DevExpress.Web.Demos {
    public partial class WebSpecificFeatures_ReportDesigner : PageBase<XtraReportsDemos.MasterDetailReport.Report> {
        string reportName;
        string redirectUrlInputValue;
        protected void Page_Load(object sender, EventArgs e) {
            Header.Title = Utils.GetCurrentDemoPageTitle();
            var query = Request.QueryString;

            if((query != null) && (query[DemoQueryKeys.ReportTypeNameKey] != null)) {
                reportName = query[DemoQueryKeys.ReportTypeNameKey];
                string fromDemo = query[DemoQueryKeys.FromDemoKey];
                string fromGroup = query[DemoQueryKeys.FromDemoGroupKey];
                string redirectDemoUrl = DevExpress.Web.Demos.Helper.GetDemoLocalUrl(Utils.CurrentDemoPage?.Product, fromGroup, fromDemo);
                if(!string.IsNullOrEmpty(redirectDemoUrl)) {
                    string redirectUrlWithQuery = redirectDemoUrl;
                    redirectUrlInputValue = Page.ResolveUrl(redirectUrlWithQuery);
                }
            } else {
                reportName = ReportTypeName;
            }
            redirectValue.Value = redirectUrlInputValue ?? "";

            if(!IsRelativeUrl(redirectValue.Value) || string.IsNullOrEmpty(reportName)) {
                Response.Redirect("~/");
            }

            reportDesigner.OpenReport(reportName);
        }
        static bool IsRelativeUrl(string url) {
            Uri ignore = null;
            return Uri.TryCreate(url, UriKind.Relative, out ignore);
        }
    }
}
