using System.Web.Mvc;

namespace DevExpress.Web.Demos {
    public partial class InteractionController : ReportDemoController {
        public override string Name { get { return "Interaction"; } }

        public ActionResult Index() {
            return RedirectToAction("DrillDownReport");
        }
    }
}
