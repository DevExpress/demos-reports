using System.Web.Mvc;

namespace DevExpress.Web.Demos {
    public partial class ReportTypesController: ReportDemoController {
        public ActionResult TableReport() {
            var model = ReportDemoHelper.CreateModel("Table", Session, Request);
            return DemoView("TableReport", "Table", model);
        }
    }
}
