using System.Web.Mvc;

namespace DevExpress.Web.Demos {
    public partial class ComplexReportsController: ReportDemoController {
        public ActionResult RollPaper() {
            var model = ReportDemoHelper.CreateModel("RollPaper", Session, Request);
            return DemoView("RollPaper", "RollPaper", model);
        }
    }
}
