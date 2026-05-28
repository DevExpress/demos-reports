using System.Web.Mvc;

namespace DevExpress.Web.Demos {
    public partial class ReportTypesController: ReportDemoController {
        public ActionResult ReportMergingWithPdf() {
            var model = ReportDemoHelper.CreateModel("ReportMergingWithPdf", Session, Request);
            return DemoView("ReportMergingWithPdf", "ReportMergingWithPdf", model);
        }
    }
}
