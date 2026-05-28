using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DevExpress.Web.Demos {
    public partial class UserControls_MobileEmulator : System.Web.UI.UserControl, IMobileEmulator {
        public bool IsLandscape {
            get {
                return ReportTypeName == "XtraReportsDemos.CharacterComb.Report" ||
                    ReportTypeName == "XtraReportsDemos.PivotGridAndChart.Report";
            }
        }
        public string ReportTypeName { get; set; }
        public string Url {
            get {
#pragma warning disable DX0025 // not a path (and no traversal here)
                return $"~/MobileViewer.aspx?{DemoQueryKeys.ReportTypeNameKey}={HttpUtility.UrlEncode(ReportTypeName)}";
#pragma warning restore DX0025
            }
        }
        public string QRCodeUrl {
            get {
#pragma warning disable DX0025 // not a path (and no traversal here)
                return "~/Content/QRCode.png";
#pragma warning restore DX0025
            }
        }
    }
}
