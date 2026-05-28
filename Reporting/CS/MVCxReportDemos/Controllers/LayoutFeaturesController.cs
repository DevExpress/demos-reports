using System.Web.Mvc;

namespace DevExpress.Web.Demos {
    public partial class LayoutFeaturesController: ReportDemoController {
        public override string Name { get { return "LayoutFeatures"; } }

        public ActionResult Index() {
            return RedirectToAction("VerticalAnchoringReport");
        }
    }
}
