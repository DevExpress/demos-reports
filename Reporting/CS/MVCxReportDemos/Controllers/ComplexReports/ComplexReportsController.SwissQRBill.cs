using System.Web.Mvc;

namespace DevExpress.Web.Demos {
    public partial class ComplexReportsController: ReportDemoController {
        public ActionResult SwissQRBill() {
            var model = ReportDemoHelper.CreateModel("SwissQRBill", Session, Request);
            return DemoView("SwissQRBill", "SwissQRBill", model);
        }
    }
}
