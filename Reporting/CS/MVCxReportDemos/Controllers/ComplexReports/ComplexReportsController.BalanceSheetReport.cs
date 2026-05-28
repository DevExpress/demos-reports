using System.Web.Mvc;

namespace DevExpress.Web.Demos {
    public partial class ComplexReportsController: ReportDemoController {
        public ActionResult BalanceSheetReport() {
            var model = ReportDemoHelper.CreateModel("BalanceSheetReport", Session, Request);
            return DemoView("BalanceSheetReport", "BalanceSheetReport", model);
        }
    }
}
