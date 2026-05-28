using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Web.Extensions;

namespace DevExpress.Web.Demos {
    public class DemoReportStorageExtension : ReportStorageWebExtension {
        public static DemoReportStorageExtension ReportStorageExtension = new DemoReportStorageExtension();
        public override bool CanSetData(string url) {
            return true;
        }
        public override Dictionary<string, string> GetUrls() {
            return (Dictionary<string, string>)HttpContext.Current.Session["urls"];
        }
        public override byte[] GetData(string url) {
            using(MemoryStream ms = new MemoryStream()) {
                XtraReport report = ReportStorageHelper.LoadReport(url, HttpContext.Current.Session);
                if(report == null)
                    report = new XtraReport();
                report.SaveLayoutToXml(ms);
                return ms.ToArray();
            }
        }

        public override bool IsValidUrl(string url) {
            return true;
        }

        public override void SetData(XtraReport report, string url) {
            using(MemoryStream ms = new MemoryStream()) {
                report.SaveLayoutToXml(ms);
                ReportStorageHelper.SaveReportLayout(url, ms.ToArray(), HttpContext.Current.Session);

            }
        }

        public override string SetNewData(XtraReport report, string defaultUrl) {
            var url = ((Dictionary<string, string>)HttpContext.Current.Session["urls"]).FirstOrDefault((x) => { return x.Value == defaultUrl; });
            if(string.IsNullOrEmpty(url.Key)) {
                ((Dictionary<string, string>)HttpContext.Current.Session["urls"]).Add(defaultUrl, defaultUrl);
            } else {
                defaultUrl = url.Key;
            }
            SetData(report, defaultUrl);
            return defaultUrl;
        }
    }
}
