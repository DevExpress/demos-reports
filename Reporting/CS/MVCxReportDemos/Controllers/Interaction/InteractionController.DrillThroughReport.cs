using System.Web.Mvc;

namespace DevExpress.Web.Demos {
    public partial class InteractionController: ReportDemoController {
        public ActionResult DrillThroughReport() {
            var model = ReportDemoHelper.CreateModel("DrillThrough", Session, Request);
            return DemoView("DrillThroughReport", "DrillThrough", model);
        }
    }
}
