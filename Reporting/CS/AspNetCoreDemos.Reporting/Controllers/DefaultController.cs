using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace AspNetCoreDemos.Reporting {
    public class DefaultController : Controller {
        public IActionResult Index() {
            return RedirectToAction("InvoiceReport", "Demos");
        }
    }
}
