using System.Web.Mvc;

namespace DevExpress.Web.Demos.Controllers {
    public class ReportDesignerController : Controller {
        public ActionResult Index(string reportID) {
            if(string.IsNullOrEmpty(reportID)) {
                return RedirectToAction("ReportDesigner", "WebSpecificFeatures");
            } else {
                var routeValues = new {
                    reportID = reportID,
                };
                return RedirectToAction("ReportDesigner", "WebSpecificFeatures", routeValues);
            }
        }
    }
}
