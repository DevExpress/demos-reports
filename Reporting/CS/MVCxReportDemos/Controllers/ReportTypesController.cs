using System.Web.Mvc;

namespace DevExpress.Web.Demos {
    public partial class ReportTypesController: ReportDemoController {
        public override string Name { get { return "ReportTypes"; } }

        public ActionResult Index() {
            return RedirectToAction("TableReport");
        }
    }
}
