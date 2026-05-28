using System.Web.Mvc;

namespace DevExpress.Web.Demos {
    public partial class WebSpecificFeaturesController : ReportDemoController {
        public ActionResult MobileViewer() {
            var model = ReportDemoHelper.CreateModel("MasterDetail", Session, Request);
            model.CurrentViewer = ViewerSelectorState.MobileViewer;
            return DemoView("MobileViewer", "MobileViewer", model);
        }
    }
}
