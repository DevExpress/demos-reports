using System.Web.Mvc;

namespace DevExpress.Web.Demos {
    public partial class ComplexReportsController: ReportDemoController {
        public ActionResult RestaurantMenu() {
            var model = ReportDemoHelper.CreateModel("RestaurantMenu", Session, Request);
            return DemoView("RestaurantMenu", "RestaurantMenu", model);
        }
    }
}
