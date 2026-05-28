using System.Web.Mvc;

namespace DevExpress.Web.Demos {
    public partial class DataBindingController: ReportDemoController {
        public ActionResult MailMergeReport() {
            var model = ReportDemoHelper.CreateModel("MailMerge", Session, Request);
            return DemoView("MailMergeReport", "MailMerge", model);
        }
    }
}
