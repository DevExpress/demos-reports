using DevExpress.AspNetCore.Reporting.WebDocumentViewer;
using DevExpress.AspNetCore.Reporting.WebDocumentViewer.Native.Services;

namespace AspNetCoreDemos.Reporting.Controllers {
    public class DemoDocumentViewerController : WebDocumentViewerController {
        public DemoDocumentViewerController(IWebDocumentViewerMvcControllerService controllerService) : base(controllerService) {
        }
    }
}
