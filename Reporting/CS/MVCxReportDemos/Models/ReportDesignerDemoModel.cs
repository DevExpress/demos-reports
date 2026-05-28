using System.Collections.Generic;
namespace DevExpress.Web.Demos {
    public class ReportDesignerDemoModel : ReportsDemoModel {
        public Dictionary<string, string> Subreports { get; set; }
    }

    public static class ReportDesignerDemoModelExtension {
        public static void CopySubreportsTo(this ReportDesignerDemoModel model, IDictionary<string, string> dest) {
            if(model.Subreports == null) {
                return;
            }
            foreach(var pair in model.Subreports) {
                dest.Add(pair);
            }
        }
    }
}
