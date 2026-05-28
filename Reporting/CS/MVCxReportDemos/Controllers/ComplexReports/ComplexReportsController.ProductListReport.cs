using System.Web.Mvc;

namespace DevExpress.Web.Demos {
    public partial class ComplexReportsController: ReportDemoController {
        public ActionResult ProductListReport() {
            var model = ReportDemoHelper.CreateModel("ProductList", Session, Request);
            return DemoView("ProductListReport", "ProductList", model);
        }
    }
}
