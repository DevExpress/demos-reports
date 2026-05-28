using System.Web;
using System.Web.UI;

namespace DevExpress.Web.Demos {
    public partial class UserControls_ReportControlToggleButtonControl : UserControl, IReportControlToggleButton {
        public string ReportTypeName { get; set; }
        public bool ShowViewerButton { get; set; }
        string routeUrl;

        public string RouteUrl {
            get {
                if(!string.IsNullOrEmpty(routeUrl))
                    return routeUrl;

                var nvc = HttpUtility.ParseQueryString("");
                nvc[DemoQueryKeys.ReportTypeNameKey] = ReportTypeName;

                if(Utils.CurrentDemoPage != null) {
                    nvc[DemoQueryKeys.FromDemoGroupKey] = Utils.CurrentDemoPage.Group.Key;
                    nvc[DemoQueryKeys.FromDemoKey] = Utils.CurrentDemoPage.Key;
                }

                var currentViewer = ViewerSelectorState.GetCurrentViewer();
                if(currentViewer != ViewerSelectorState.DefaultViewer)
                    nvc[ViewerSelectorState.Key] = currentViewer;

#pragma warning disable DX0025 // not a path (and no traversal here)
                return $"~/WebSpecificFeatures/ReportDesigner.aspx?{nvc}";
#pragma warning restore DX0025
            }
            set {
                routeUrl = value;
            }
        }
    }
}
