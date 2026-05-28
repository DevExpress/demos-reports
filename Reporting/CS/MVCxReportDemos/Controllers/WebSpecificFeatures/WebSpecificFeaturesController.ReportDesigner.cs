using System.Web.Mvc;
using System.Web.Routing;
using DevExpress.Web.Demos.Code.Designer;

namespace DevExpress.Web.Demos {
    public partial class WebSpecificFeaturesController : ReportDemoController {
        public ActionResult ReportDesigner(string reportID, string fromGroup, string fromDemo) {
            if(string.IsNullOrEmpty(reportID) && (string.IsNullOrEmpty(fromGroup) || string.IsNullOrEmpty(fromDemo))) {
                return GetFallbackAction();
            }

            string redirectDemoUrl = SafeRedirectUrl.GetDemoLocalUrl(DemosModel.Current, fromGroup, fromDemo);
            using(var report = ReportStorageHelper.LoadReport(reportID, Session)) {
                if(report == null) {
                    if(!string.IsNullOrEmpty(redirectDemoUrl))
                        return Redirect(redirectDemoUrl);
                    return GetFallbackAction();
                }
            }
            string redirectUrlInputValue = null;
            if(!string.IsNullOrEmpty(redirectDemoUrl)) {
                string redirectUrlWithQuery = redirectDemoUrl;
                var routeValues = new RouteValueDictionary();
                if(ViewerSelectorState.GetSafeCurrentViewerArgFromQuery(Request) == ViewerSelectorState.MobileViewer)
                    routeValues[ViewerSelectorState.Key] = ViewerSelectorState.MobileViewer;

                redirectUrlInputValue = Url.Action(fromDemo, fromGroup, routeValues);
            }

            var model = new ReportsDemoModel {
                ReportID = reportID,
                RedirectUrl = redirectUrlInputValue ?? ""
            };
            return DemoView("ReportDesigner", "ReportDesigner", model);
        }

        public ActionResult ReportDesignerPartial(string reportID) {
            return PartialView("ReportDesignerPartial", ReportDemoHelper.CreateModel(reportID, Session, Request));
        }

        ActionResult GetFallbackAction() {
            return DemoView("ReportDesigner", "ReportDesigner", ReportDemoHelper.CreateModel("MasterDetail", Session, Request));
        }
    }
}
