using System.Web.Mvc;

namespace DevExpress.Web.Demos {
    public partial class InteractionController : ReportDemoController {
        public ActionResult VehicleInspection() {
            var model = ReportDemoHelper.CreateModel("VehicleInspection", Session, Request);
            return DemoView("VehicleInspection", "VehicleInspection", model);
        }
    }
}
