using System.Collections;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using DevExpress.XtraReports.UI;

namespace DevExpress.Web.Demos {
    public partial class ReportControlsController : ReportDemoController {
        public override string Name { get { return "ReportControls"; } }

        public ActionResult Index() {
            return RedirectToAction("ChartReport");
        }
    }
}
