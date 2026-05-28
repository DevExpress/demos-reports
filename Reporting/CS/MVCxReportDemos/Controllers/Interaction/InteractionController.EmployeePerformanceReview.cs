using System.Web.Mvc;

namespace DevExpress.Web.Demos {
    public partial class InteractionController : ReportDemoController {
        public ActionResult EmployeePerformanceReview() {
            var model = ReportDemoHelper.CreateModel("EmployeePerformanceReview", Session, Request);
            return DemoView("EmployeePerformanceReview", "EmployeePerformanceReview", model);
        }
    }
}
