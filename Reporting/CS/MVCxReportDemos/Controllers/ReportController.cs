using System.Web.Mvc;
using System.Web.Routing;

namespace DevExpress.Web.Demos {
    public abstract class ReportDemoController: DemoController {
        protected override void Initialize(RequestContext requestContext) {
            base.Initialize(requestContext);
            DemoHelper.Instance.ControlAreaMaxWidth = System.Web.UI.WebControls.Unit.Percentage(100);
            ReportDemoHelper.UseDefaultTheme = false;
        }
    }
}
