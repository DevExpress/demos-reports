using System.Threading;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace DevExpress.Web.Demos {
    [HandleError]
    public class HomeController : DemoController {
        public override string Name { get { return "Home"; } }

        public ActionResult Index() {
            return DemoView("Index");
        }
        public ActionResult DemoTabsPartial(string group, string demo) {
            Utils.RegisterCurrentMvcDemo(group, demo);
            return PartialView("DemoTabsPartial");
        }
        [ValidateInput(false)]
        public ActionResult SearchListPartial(string text) {
            if(DevExpressHelper.IsCallback)
                System.Threading.Thread.Sleep(500);
            ViewData["RequestText"] = text;
            return PartialView("SearchListPartial", SearchUtils.DoSearch(text));
        }
        public ActionResult Error404() {
            ViewBag.MainMessage = "404 - The page you requested was not found";
            ViewBag.Details = new string[] {
                "The resource you are looking for might have been removed, had its name changed, or is temporarily unavailable.",
                "Please review the resource URL and make sure that it is spelled correctly. You can also return to the product's main demo page or take a look at some of the product demos."
            };
            Response.StatusCode = 404;
            return DemoView("Error404", "ErrorMessage");
        }
        public ActionResult Error500() {
            ViewBag.MainMessage = "Internal server error";
            ViewBag.Details = new string[] {
            "The server encountered an unexpected condition which prevented it from fulfilling the request. Please try to load the page later.",
            "You can also return to the product's main demo page or take a look at some of the product demos."
            };
            Response.StatusCode = 500;
            return DemoView("Error500", "ErrorMessage");
        }
    }
}
