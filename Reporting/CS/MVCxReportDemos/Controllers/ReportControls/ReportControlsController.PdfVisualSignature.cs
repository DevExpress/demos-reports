using System.Web.Mvc;

namespace DevExpress.Web.Demos {
    public partial class ReportControlsController : ReportDemoController {
        public ActionResult PdfVisualSignatureReport() {
            var model = ReportDemoHelper.CreateModel("PdfVisualSignature", Session, Request);
            return DemoView("PdfVisualSignatureReport", "PdfVisualSignature", model);
        }
    }
}
