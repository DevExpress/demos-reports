using System;
using System.Web.Mvc;
using DevExpress.Web.Demos.Code.Designer;

namespace DevExpress.Web.Demos {
    public partial class WebSpecificFeaturesController: ReportDemoController {
        public ActionResult ColorSchemeCustomization(string reportID) {
            DemoHelper.Instance.SuppressThemeSelector = true;
            ReportDemoHelper.UseDefaultTheme = true;

            var model = ReportDemoHelper.CreateModel("MasterDetail", Session, Request);
            return DemoView("ColorSchemeCustomization", "ColorSchemeCustomization", model);
        }

        public ActionResult ColorSchemeCustomizationPartial(string reportID) {
            return PartialView("ColorSchemeCustomizationPartial", ReportDemoHelper.CreateModel(reportID, Session, Request));
        }
    }
}
