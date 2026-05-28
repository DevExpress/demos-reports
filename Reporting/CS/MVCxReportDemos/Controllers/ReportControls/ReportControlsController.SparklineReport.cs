using System.Web.Mvc;

namespace DevExpress.Web.Demos {
    public partial class ReportControlsController: ReportDemoController {
        public ActionResult SparklineReport() {
            var model = ReportDemoHelper.CreateModel("Sparkline", Session, Request);
            return DemoView("SparklineReport", "Sparkline", model);
        }
    }
}
