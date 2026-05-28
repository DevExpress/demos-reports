using System.Web.Mvc;

namespace DevExpress.Web.Demos {
    public partial class LayoutFeaturesController : ReportDemoController {
        public ActionResult HiddenColumnsReport() {
            var model = ReportDemoHelper.CreateModel("HiddenColumns", Session, Request);
            return DemoView("HiddenColumnsReport", "HiddenColumns", model);
        }
    }
}
