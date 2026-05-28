using System;

namespace DevExpress.Web.Demos {
    public partial class WebSpecificFeatures_ColorSchemeCustomization : PageBase<XtraReportsDemos.MasterDetailReport.Report> {
        internal const string ShowDesignerKey = "ShowDesigner";
        public bool ShowViewerButton {
            get {
                return Request.Params[ShowDesignerKey] != null;
            }
        }
        protected void Page_Load(object sender, EventArgs e) {
            Header.Title = Utils.GetCurrentDemoPageTitle();
            DemoHelper.Instance.SuppressThemeSelector = true;
            DevExpress.Web.ASPxWebClientUIControl.GlobalColorScheme = DevExpress.Web.ASPxWebClientUIControl.ColorSchemeLight;

            var topRightMenuRegion = Master.FindControl("TopRightMenuRegion");
            var reportControlToggleButton = topRightMenuRegion.FindControl("reportControlToggleButton") as IReportControlToggleButton;
            if(reportControlToggleButton != null) {
                reportControlToggleButton.ReportTypeName = ReportTypeName;
                reportControlToggleButton.ShowViewerButton = ShowViewerButton;
                string urlParams = "";
                if(!ShowViewerButton)
                    urlParams = $"?{ShowDesignerKey}=true";
                reportControlToggleButton.RouteUrl = Page.ResolveClientUrl("~/WebSpecificFeatures/ColorSchemeCustomization.aspx" + urlParams);
            }

            if(!IsPostBack) {
                if(ShowViewerButton) {
                    reportDesigner.OpenReport(ReportTypeName);  
                } else {
                    webDocumentViewer.OpenReport(ReportTypeName);
                }
            }
        }
    }
}
