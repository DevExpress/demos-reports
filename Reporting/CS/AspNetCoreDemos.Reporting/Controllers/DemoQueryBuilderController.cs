using DevExpress.AspNetCore.Reporting.QueryBuilder;
using DevExpress.AspNetCore.Reporting.QueryBuilder.Native.Services;

namespace AspNetCoreDemos.Reporting.Controllers {
    public class DemoQueryBuilderController : QueryBuilderController {
        public DemoQueryBuilderController(IQueryBuilderMvcControllerService controllerService) : base(controllerService) {
        }
    }
}
