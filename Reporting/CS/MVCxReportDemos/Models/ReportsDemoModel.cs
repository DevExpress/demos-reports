using DevExpress.XtraReports.UI;

namespace DevExpress.Web.Demos {
    public class ReportsDemoModel {
        string currentViewer;
        public ReportsDemoModel() {
            DemoHelper.Instance.UseDevExtremeThemeSelector = true;
        }

        public string ReportID { get; set; }
        public XtraReport Report { get; set; }
        public string CurrentViewer {
            get { return currentViewer; }
            set {
                currentViewer = ViewerSelectorState.GetSafeCurrentViewerArgFromString(value);
            }
        }

        public bool IsHTML5Viewer {
            get {
                return CurrentViewer == null;
            }
        }

        public string RedirectUrl { get; set; }
    }
}
