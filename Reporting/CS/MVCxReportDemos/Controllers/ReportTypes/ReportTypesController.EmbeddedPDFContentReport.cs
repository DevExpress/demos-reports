using System.Web.Mvc;

namespace DevExpress.Web.Demos
{
    public partial class ReportTypesController : ReportDemoController {
        public ActionResult EmbeddedPDFContent() {
            var model = ReportDemoHelper.CreateModel("EmbeddedPDFContent", Session, Request);
            return DemoView("EmbeddedPDFContent", "EmbeddedPDFContent", model);
        }
    }
}
