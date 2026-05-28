using AspNetCoreDemos.Reporting.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace AspNetCoreDemos.Reporting {
    public class AccessibilityController : Controller {
        public IActionResult Index(bool? startTour, decimal? zoom, bool? exploreFeatures, bool? isContrast) {
            return View("Index", new AccessibilityModel() {
                StartTour = startTour ?? false,
                Zoom = zoom ?? 0,
                IsContrast = isContrast ?? false,
                ExploreFeatures = exploreFeatures ?? true
            });
        }
    }
}
