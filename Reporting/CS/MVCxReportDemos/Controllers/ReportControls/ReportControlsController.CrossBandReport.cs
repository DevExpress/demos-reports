using System.Web.Mvc;

namespace DevExpress.Web.Demos {
    public partial class ReportControlsController: ReportDemoController {
        public ActionResult CrossBandReport() {
            var model = ReportDemoHelper.CreateModel("CrossBand", Session, Request);
            return DemoView("CrossBandReport", "CrossBand", model);
        }
    }
}
