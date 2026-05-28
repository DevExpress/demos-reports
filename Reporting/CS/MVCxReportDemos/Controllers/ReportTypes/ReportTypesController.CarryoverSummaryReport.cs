using System.Web.Mvc;

namespace DevExpress.Web.Demos {
    public partial class ReportTypesController: ReportDemoController {
        public ActionResult CarryoverSummaryReport() {
            var model = ReportDemoHelper.CreateModel("CarryoverSummaryReport", Session, Request);
            return DemoView("CarryoverSummaryReport", "CarryoverSummaryReport", model);
        }
    }
}
