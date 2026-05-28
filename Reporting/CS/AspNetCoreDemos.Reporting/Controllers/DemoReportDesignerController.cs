using DevExpress.AspNetCore.Reporting.ReportDesigner;
using DevExpress.AspNetCore.Reporting.ReportDesigner.Native.Services;

namespace AspNetCoreDemos.Reporting.Controllers {
    public class DemoReportDesignerController : ReportDesignerController {
        public DemoReportDesignerController(IReportDesignerMvcControllerService controllerService) : base(controllerService) {
        }
    }
}
