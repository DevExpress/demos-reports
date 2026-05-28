using System.Web.Mvc;

namespace DevExpress.Web.Demos {
    public partial class DataBindingController : ReportDemoController {
        public ActionResult HugeAmountRecordsReport() {
            var model = ReportDemoHelper.CreateModel("HugeAmountRecords", Session, Request);
            return DemoView("HugeAmountRecordsReport", "HugeAmountRecords", model);
        }
    }
}
