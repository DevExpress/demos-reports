using System.Web.Mvc;

namespace DevExpress.Web.Demos {
    public partial class ComplexReportsController: ReportDemoController {
        public override string Name { get { return "ComplexReports"; } }

        public ActionResult Index() {
            return RedirectToAction("FallCatalogReport");
        }
    }
}
