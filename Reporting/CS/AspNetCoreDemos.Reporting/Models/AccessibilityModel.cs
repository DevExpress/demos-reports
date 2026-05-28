namespace AspNetCoreDemos.Reporting.Models {
    public class AccessibilityModel {
        public bool IsContrast { get; set; } = false;
        public decimal Zoom { get; set; }
        public bool StartTour { get; set; } = false;
        public bool ExploreFeatures { get; set; } = true;
        public bool HidePanel { get; set; } = false;
    }
}
