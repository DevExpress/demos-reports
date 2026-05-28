using System.Web.Mvc;

namespace DevExpress.Web.Demos {
    public partial class InteractionController: ReportDemoController {
        public ActionResult DrillDownReport() {
            var model = ReportDemoHelper.CreateModel("DrillDown", Session, Request);
            return DemoView("DrillDownReport", "DrillDown", model);
        }
    }
}
