using System.Web.Mvc;

namespace DevExpress.Web.Demos {
    public partial class WebSpecificFeaturesController: ReportDemoController {
        public override string Name { get { return "WebSpecificFeatures"; } }

        public ActionResult Index() {
            return RedirectToAction("ReportDesigner");
        }
    }
}
