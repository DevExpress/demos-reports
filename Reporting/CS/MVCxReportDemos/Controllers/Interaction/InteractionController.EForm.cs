using System.Web.Mvc;

namespace DevExpress.Web.Demos {
    public partial class InteractionController : ReportDemoController {
        public ActionResult EFormReport() {
            var model = ReportDemoHelper.CreateModel("EForm", Session, Request);
            return DemoView("EFormReport", "EForm", model);
        }
    }
}
