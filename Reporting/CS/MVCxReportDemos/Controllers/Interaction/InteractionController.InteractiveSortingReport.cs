using System.Web.Mvc;

namespace DevExpress.Web.Demos {
    public partial class InteractionController : ReportDemoController {
        public ActionResult InteractiveSorting() {
            var model = ReportDemoHelper.CreateModel("InteractiveSorting", Session, Request);
            return DemoView("InteractiveSorting", "InteractiveSorting", model);
        }
    }
}
