using System.Web.Mvc;

namespace DevExpress.Web.Demos {
    public partial class ComplexReportsController: ReportDemoController {
        public ActionResult SalesSummary() {
            var model = ReportDemoHelper.CreateModel("SalesSummary", Session, Request);
            return DemoView("SalesSummary", "SalesSummary", model);
        }
    }
}
