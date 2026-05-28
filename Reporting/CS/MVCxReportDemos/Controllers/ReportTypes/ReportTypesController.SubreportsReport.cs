using System.Web.Mvc;

namespace DevExpress.Web.Demos {
    public partial class ReportTypesController: ReportDemoController {
        public ActionResult SubreportsReport() {
            var model = ReportDemoHelper.CreateModel("Subreports", Session, Request);
            return DemoView("SubreportsReport", "Subreports", model);
        }
    }
}
