using System.Web.Mvc;

namespace DevExpress.Web.Demos {
    public partial class ComplexReportsController: ReportDemoController {
        public ActionResult ProfitAndLossReport() {
            var model = ReportDemoHelper.CreateModel("ProfitAndLoss", Session, Request);
            return DemoView("ProfitAndLossReport", "ProfitAndLoss", model);
        }
    }
}
