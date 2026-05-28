using System.Web.Mvc;

namespace DevExpress.Web.Demos {
    public partial class LayoutFeaturesController: ReportDemoController {
        public ActionResult VerticalAnchoringReport() {
            var model = ReportDemoHelper.CreateModel("VerticalAnchoring", Session, Request);
            return DemoView("VerticalAnchoringReport", "VerticalAnchoring", model);
        }
    }
}
