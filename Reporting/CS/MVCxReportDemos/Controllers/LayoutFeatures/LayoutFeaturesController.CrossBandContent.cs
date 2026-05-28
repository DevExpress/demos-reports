using System.Web.Mvc;

namespace DevExpress.Web.Demos {
    public partial class LayoutFeaturesController : ReportDemoController {
        public ActionResult CrossBandContent() {
            var model = ReportDemoHelper.CreateModel("CrossBandContent", Session, Request);
            return DemoView("CrossBandContent", "CrossBandContent", model);
        }
    }
}
