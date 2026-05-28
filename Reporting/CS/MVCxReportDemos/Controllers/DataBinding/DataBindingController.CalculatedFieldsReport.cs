using System.Collections;
using System.Linq;
using System.Web.Mvc;

namespace DevExpress.Web.Demos {
    public partial class DataBindingController: ReportDemoController {
        public ActionResult CalculatedFieldsReport() {
            var model = ReportDemoHelper.CreateModel("CalculatedFields", Session, Request);
            return DemoView("CalculatedFieldsReport", "CalculatedFields", model);
        }
    }

    public static class DataBindingDemosHelper {
        static readonly string[] CalcFieldNames = {
            "Extended Price",
            "Extended Unit Price",
            "Full Price"
        };

        public static IEnumerable GetCalcFieldNames() {
            return CalcFieldNames.Select((x, i) => new { Index = i, Text = x });
        }
    }
}
