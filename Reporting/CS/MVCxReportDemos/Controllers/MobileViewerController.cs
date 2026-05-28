using System.Web.Mvc;

namespace DevExpress.Web.Demos.Controllers {
    public class MobileViewerController : Controller {
        public ActionResult Index(string reportName) {
            if(string.IsNullOrEmpty(reportName)) reportName = "MasterDetail";
            ReportsDemoModel model = ReportDemoHelper.CreateModel(reportName, Session, Request);
            model.CurrentViewer = ViewerSelectorState.MobileViewer;
            return View(model);
        }
    }
}
