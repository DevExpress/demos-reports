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

        public virtual bool IsMobileViewer {
            get {
                return ViewerSelectorState.GetCurrentViewer() == ViewerSelectorState.MobileViewer;
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
            var mobileEmulator = contentPlaceHolder.FindControl("mobileEmulator") as IMobileEmulator;
            if(mobileEmulator != null) {
                mobileEmulator.ReportTypeName = ReportTypeName;
            }
            var viewerSelector = topRightMenuRegion.FindControl("viewerSelector") as IMobileEmulator;
            if(viewerSelector != null) {
                viewerSelector.ReportTypeName = ReportTypeName;
            }

            if(IsHTML5Viewer) {
                DemoHelper.Instance.UseDevExtremeThemeSelector = true;
            } else {
                DemoHelper.Instance.UseDevExtremeThemeSelector = false;
            }
        }

        protected string ReportTypeName {
            get { return typeof(TReport).FullName; }
        }
    }
}
