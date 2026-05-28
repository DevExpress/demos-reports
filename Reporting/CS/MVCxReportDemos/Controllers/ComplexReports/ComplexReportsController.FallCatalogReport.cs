using System.Web.Mvc;

namespace DevExpress.Web.Demos {
    public partial class ComplexReportsController: ReportDemoController {
        public ActionResult FallCatalogReport() {
            var model = ReportDemoHelper.CreateModel("FallCatalog", Session, Request);
            return DemoView("FallCatalogReport", "FallCatalog", model);
        }
    }
}
