using System.Web.Mvc;

namespace DevExpress.Web.Demos {
    public partial class ReportTypesController: ReportDemoController {
        public ActionResult HierarchicalReport() {
            var model = ReportDemoHelper.CreateModel("Hierarchical", Session, Request);
            return DemoView("HierarchicalReport", "HierarchicalReport", model);
        }
    }
}
