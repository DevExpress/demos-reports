using System;
using System.Web;
using System.Web.Mvc;

namespace DevExpress.Web.Demos {
    public class MobileEmulatorModel {
        public MobileEmulatorModel(string reportName, HttpRequestBase request) {
            this.ReportName = reportName;
            this.CurrentRequest = request;
        }
        public string ReportName { get; set; }
        public bool IsLandscape {
            get {
                return ReportName == "EForm" || ReportName == "PivotGridAndChart";
            }
        }
        public HttpRequestBase CurrentRequest { get; set; }
        public string Url {
            get {
#pragma warning disable DX0025 // not a path (and no traversal here)
                return string.Format("~/MobileViewer?reportName={0}", HttpUtility.UrlEncode(ReportName));
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
