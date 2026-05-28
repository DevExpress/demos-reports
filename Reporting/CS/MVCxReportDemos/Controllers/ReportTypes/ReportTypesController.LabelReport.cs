using System.Web.Mvc;

namespace DevExpress.Web.Demos {
    public partial class ReportTypesController: ReportDemoController {
        public ActionResult LabelReport() {
            var model = ReportDemoHelper.CreateModel("Label", Session, Request);
            return DemoView("LabelReport", "Label", model);
        }
    }
}
