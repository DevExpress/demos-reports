using DevExpress.XtraReports.UI;

namespace DevExpress.Web.Demos {
    public class ReportsDemoModel {
        string currentViewer;
        public ReportsDemoModel() {
            DemoHelper.Instance.UseDevExtremeThemeSelector = true;
        }

        public string ReportID { get; set; }
        public XtraReport Report { get; set; }
        public MobileEmulatorModel EmulatorModel { get; set; }
        public string CurrentViewer {
            get { return currentViewer; }
            set {
                currentViewer = ViewerSelectorState.GetSafeCurrentViewerArgFromString(value);
                if(currentViewer == ViewerSelectorState.MobileViewer) {
                    DemoHelper.Instance.SuppressThemeSelector = true;
                }
            }
        }

        public bool IsHTML5Viewer {
            get {
                return CurrentViewer == null;
            }
        }

        public bool IsMobileViewer {
            get {
                return CurrentViewer == ViewerSelectorState.MobileViewer;

            }
        }
        public string RedirectUrl { get; set; }
    }
}
