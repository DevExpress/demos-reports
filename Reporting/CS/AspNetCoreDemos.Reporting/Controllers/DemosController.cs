using AspNetCoreDemos.DemoShell;
using AspNetCoreDemos.Reporting.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace AspNetCoreDemos.Reporting {

    [Route("[action]")]
    public class DemosController : Controller {
        // AI-Powered Extensions
        public ActionResult AIPoweredInvoiceReport() {
            return View("_Viewer", "NorthwindTraders.InvoiceReport");
        }
        // Data binding
        public ActionResult PivotGridAndChart() {
            return View("_Viewer", "PivotGridAndChart");
        }
        public ActionResult CalculatedFields() {
            return View("_Viewer", "CalculatedFieldsReport");
        }
        public ActionResult CachedDocumentSource() {
            return View("_LargeDataset", "CachedDocumentSource");
        }
        public ActionResult MailMerge() {
            return View("_Viewer", "MailMerge");
        }
        // Report Types
        public ActionResult TableReport() {
            return View("_Viewer", "TableReport");
        }
        public ActionResult HierarchicalReport() {
            return View("_Viewer", "HierarchicalReport");
        }
        public ActionResult MasterDetailReport() {
            return View("_Viewer", "MasterDetailReport");
        }
        public ActionResult ProductLabelsReport() {
            return View("_Viewer", "ProductLabelsReport");
        }
        public ActionResult MergedReport() {
            return View("_ViewerWithCustomZoom", new ZoomedViewerModel() { ReportUrl = "ReportMerging.MergedReport", ZoomFactor = 0.33 });
        }
        public ActionResult ReportMergingWithPdf() {
            return View("_ViewerWithCustomZoom", new ZoomedViewerModel() { ReportUrl = "ReportMergingWithPdf", ZoomFactor = 0.65 });
        }
        public ActionResult EmbeddedPDFContentReport() {
            return View("_Viewer", "EmbeddedPDFContentReport");
        }
        public ActionResult SideBySideReports() {
            return View("_Viewer", "SideBySideReports.EmployeeComparisonReport");
        }
        public ActionResult Subreports() {
            return View("_Viewer", "Subreports.MasterReport");
        }
        public ActionResult MultiColumnReport() {
            return View("_Viewer", "MultiColumnReport");
        }

        public ActionResult CarryoverSummaryReport() {
            return View("_Viewer", "CarryoverSummaryReport");
        }

        // Interaction
        public ActionResult DrillDownReport() {
            return View("_Viewer", "DrillDownReport");
        }
        public ActionResult DrillThroughReport() {
            return View("_Viewer", "DrillThroughReport");
        }
        public ActionResult EmployeePerformanceReview() {
            return View("_EmployeePerformanceReviewReportViewer", "EmployeePerformanceReview");
        }
        public ActionResult InteractiveSorting() {
            return View("_Viewer", "InteractiveSorting");
        }
        public ActionResult CharacterComb() {
            return View("_EFormReportViewer", "CharacterComb");
        }
        public ActionResult VehicleInspectionReport() {
            return View("_VehicleInspectionReportViewer", "VehicleInspectionReport");
        }

        // Real-life Reports
        public ActionResult ProductListReport() {
            return View("_Viewer", "NorthwindTraders.ProductListReport");
        }
        public ActionResult CatalogReport() {
            return View("_Viewer", "NorthwindTraders.CatalogReport");
        }
        public ActionResult InvoiceReport() {
            return View("_Viewer", "NorthwindTraders.InvoiceReport");
        }
        public ActionResult SwissQRBill() {
            return View("_Viewer", "SwissQRBill");
        }
        public ActionResult ProfitAndLossReport() {
            return View("_Viewer", "ProfitAndLossReport");
        }

        public ActionResult RollPaper() {
            return View("_Viewer", "RollPaper");
        }

        public ActionResult RestaurantMenu() {
            return View("_Viewer", "RestaurantMenu");
        }

        public ActionResult BalanceSheetReport() {
            return View("_Viewer", "BalanceSheetReport");
        }

        public ActionResult SalesSummaryReport() {
            return View("_Viewer", "SalesSummary");
        }

        // Layout Features
        public ActionResult ShrinkGrow() {
            return View("_Viewer", "ShrinkGrow");
        }
        public ActionResult AnchorVertical() {
            return View("_Viewer", "AnchorVertical");
        }
        public ActionResult HiddenColumns() {
            return View("_Viewer", "HiddenColumns");
        }
        public ActionResult CrossBandContent() {
            return View("_Viewer", "CrossBandContent");
        }

        //Report Controls
        public ActionResult Sparkline() {
            return View("_Viewer", "Sparkline");
        }
        public ActionResult RichText() {
            return View("_Viewer", "RichText");
        }
        public ActionResult BarCodeTypesReport() {
            return View("_Viewer", "BarCodeTypesReport");
        }
        public ActionResult Shape() {
            return View("_Viewer", "Shape");
        }
        public ActionResult PdfVisualSignatureReport() {
            return View("_Viewer", "PdfVisualSignatureReport");
        }
        public ActionResult Charts() {
            return View("_Viewer", "Charts");
        }
        public ActionResult CrossBandControls() {
            return View("_Viewer", "CrossBandControls");
        }

        // WebSpecificFeatures
        public ActionResult ReportDesigner(DemoInputReportDesignerModel model, [FromServices] DemoMenuMeta demoMenuMeta){
            DemoReportDesignerModel safeModel = GetSafeReportDesignerDemoModel(model, demoMenuMeta, "MasterDetailReport");
            return View("_ReportDesigner", safeModel);
        }
        public ActionResult AIPoweredReportDesigner(DemoInputReportDesignerModel model, [FromServices] DemoMenuMeta demoMenuMeta) {
            DemoReportDesignerModel safeModel = GetSafeReportDesignerDemoModel(model, demoMenuMeta, "AIPoweredInvoiceReport");
            return View("_ReportDesigner", safeModel);
        }
        DemoReportDesignerModel GetSafeReportDesignerDemoModel(DemoInputReportDesignerModel deserializedModel, DemoMenuMeta demoMenuMeta, string reportNameFallback) {
            DemoReportDesignerModel safeModel = new DemoReportDesignerModel();
            safeModel.ReportID = string.IsNullOrEmpty(deserializedModel.ReportID) ? reportNameFallback : deserializedModel.ReportID;

            if(!string.IsNullOrEmpty(deserializedModel.FromDemo)) {
                Demo demo = demoMenuMeta.GetDemoByKey(demoMenuMeta.GetDemoKey("Demos", deserializedModel.FromDemo));
                if(demo != null) {
                    string redirectUrl = DemoUtils.KeepQueryString(Url.Action(demo.Action, demo.Controller), Request, null, "theme", "onlyview");
                    safeModel.RedirectUrl = redirectUrl;
                }
            }
            return safeModel;
        }
        public ActionResult ColorSchemeCustomization() {
            HttpContext.Items.Add("ignoreTheme", "true");
            return View("_ColorSchemeCustomizationViewer", "MasterDetailReport");
        }
    }
}
