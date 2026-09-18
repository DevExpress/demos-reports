using System;
using System.Web.UI;
using DevExpress.XtraReports.Web;

namespace DevExpress.Web.Demos {
    public abstract class PageBase<TReport> : Page {
        public virtual bool IsHTML5Viewer {
            get {
                return ViewerSelectorState.GetCurrentViewer() == ViewerSelectorState.ModernViewer;
            }
        }

        protected void Page_Init(object sender, EventArgs e) {
            DemoHelper.Instance.ControlAreaMaxWidth = System.Web.UI.WebControls.Unit.Percentage(100);

            var topRightMenuRegion = Master.FindControl("TopRightMenuRegion");
            var showDesignerButton = topRightMenuRegion.FindControl("showDesignerButton") as IReportControlToggleButton;
            if(showDesignerButton != null) {
                showDesignerButton.ReportTypeName = ReportTypeName;
            }

            var contentPlaceHolder = Master.FindControl("ContentHolder");
            var webDocumentViewer = contentPlaceHolder.FindControl("webDocumentViewer") as ASPxWebDocumentViewer;
            if(webDocumentViewer != null && IsHTML5Viewer) {
                webDocumentViewer.OpenReport(ReportTypeName);
            }

            DemoHelper.Instance.UseDevExtremeThemeSelector = true;
        }

        protected string ReportTypeName {
            get { return typeof(TReport).FullName; }
        }
    }
}
