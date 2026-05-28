using System.Web.Mvc;

namespace DevExpress.Web.Demos {
    public partial class DataBindingController: ReportDemoController {
        public ActionResult PivotGridAndChartReport() {
            var model = ReportDemoHelper.CreateModel("PivotGridAndChart", Session, Request);
            return DemoView("PivotGridAndChartReport", "PivotGridAndChart", model);
        }
    }
}
