using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Web.Demos.Code.Designer;
using DevExpress.XtraReports.UI;

namespace DevExpress.Web.Demos {
    public class ReportHelperTuple {
        public string ReportId { get; set; }
        public string DisplayName { get; set; }
        public Func<XtraReport> OpenReport { get; set; }
        public ReportHelperTuple(string id, string displayName, Func<XtraReport> openReport) {
            DevExpress.Utils.Guard.ArgumentNotNull(openReport, "openReport");
            ReportId = id;
            DisplayName = displayName;
            OpenReport = openReport;
        }
    }

    public static class ReportDemoHelper {
        public static readonly List<ReportHelperTuple> Reports = new List<ReportHelperTuple> {
            { new ReportHelperTuple( "SalesSummary",  "Sales Summary Report", () => new XtraReportsDemos.SalesSummary.Report() ) },
            { new ReportHelperTuple( "MailMerge",  "Mail Merge", () => new XtraReportsDemos.MailMerge.Report() ) },
            { new ReportHelperTuple( "CrossBand",  "Cross-Band Controls", () => new XtraReportsDemos.CrossBandControls.Report() ) },
            { new ReportHelperTuple( "ProfitAndLoss",  "Profit And Loss Report", () => new XtraReportsDemos.ProfitAndLossReport.Report() ) },
            { new ReportHelperTuple( "ReportMerging", "Report Merging", () => new XtraReportsDemos.ReportMerging.MergedReport()) },
            { new ReportHelperTuple( "VerticalAnchoring",  "Anchoring", () => new XtraReportsDemos.AnchorVertical.ReportWeb() ) },
            { new ReportHelperTuple( "ProductList",  "Product List", () => new XtraReportsDemos.NorthwindTraders.ProductListReport() ) },
            { new ReportHelperTuple( "FallCatalog",  "Fall Catalog", () => new XtraReportsDemos.NorthwindTraders.CatalogReport() ) },
            { new ReportHelperTuple( "Invoice",  "Invoice", () => new XtraReportsDemos.NorthwindTraders.InvoiceReport() ) },
            { new ReportHelperTuple( "InteractiveSorting",  "Interactive Sorting", () => new XtraReportsDemos.InteractiveSorting.Report() ) },
            { new ReportHelperTuple( "MasterDetail",  "Master-Detail Report", () => new XtraReportsDemos.MasterDetailReport.Report() ) },
            { new ReportHelperTuple( "MultiColumn",  "Multi-Column Report", () => new XtraReportsDemos.MultiColumnReport.Report() ) },
            { new ReportHelperTuple( "HiddenColumns",  "Hidden Columns", () => new XtraReportsDemos.IListDataSource.Report() ) },
            { new ReportHelperTuple( "CustomControl",  "Population", () => new XtraReportsDemos.CustomDraw.Report() ) },
            { new ReportHelperTuple( "ShrinkGrow",  "Shrink and Grow", () => new XtraReportsDemos.ShrinkGrow.Report() ) },
            { new ReportHelperTuple( "Label",  "Label Report", () => new XtraReportsDemos.LabelReport.ProductLabelsReport() ) },
            { new ReportHelperTuple( "BarCode",  "Bar Code", () => new XtraReportsDemos.BarCodes.BarCodeTypesReport() ) },
            { new ReportHelperTuple( "Sparkline",  "Sparkline", () => new XtraReportsDemos.Sparkline.Report() ) },
            { new ReportHelperTuple( "Subreports",  "Subreports", () => new XtraReportsDemos.Subreports.MasterReport() ) },
            { new ReportHelperTuple( "Table",  "Table Report", () => new XtraReportsDemos.TableReport.Report() ) },
            { new ReportHelperTuple( "Chart",  "Chart", () => new XtraReportsDemos.Charts.Report() ) },
            { new ReportHelperTuple( "Shapes",  "Shapes", () => new XtraReportsDemos.Shape.Report() ) },
            { new ReportHelperTuple( "SideBySide",  "Side-by-side Reports", () => new XtraReportsDemos.SideBySideReports.EmployeeComparisonReport() ) },
            { new ReportHelperTuple( "CalculatedFields",  "Calculated Fields", () => new XtraReportsDemos.CalculatedFieldsReport.Report() ) },
            { new ReportHelperTuple( "FormattingRules",  "Conditional Formatting", () => new XtraReportsDemos.FormattingRules.Report() ) },
            { new ReportHelperTuple( "HugeAmountRecords",  "Large Dataset", () => new XtraReportsDemos.CachedDocumentSourceReport.ReportWeb() ) },
            { new ReportHelperTuple( "PivotGridAndChart",  "PivotGrid And Chart", () => new XtraReportsDemos.PivotGridAndChart.Report() ) },
            { new ReportHelperTuple( "DrillDown",  "Drill-Down Report", () => new XtraReportsDemos.DrillDownReport.DrillDownReport() ) },
            { new ReportHelperTuple( "DrillThrough",  "Drill-Through Report", () => new XtraReportsDemos.DrillThroughReport.Report()) },
            { new ReportHelperTuple( "EmployeePerformanceReview",  "Employee Performance Review", () => new XtraReportsDemos.EmployeePerformanceReview.Report() ) },
            { new ReportHelperTuple( "SwissQRBill",  "Swiss QR Bill Report", () => new XtraReportsDemos.SwissQRCode.SwissQRBill() ) },
            { new ReportHelperTuple( "EForm",  "E-Form", () => new XtraReportsDemos.CharacterComb.Report() ) },
            { new ReportHelperTuple( "RollPaper", "Roll Paper Report", () => new XtraReportsDemos.RollPaper.Report() ) },
            { new ReportHelperTuple( "Hierarchical", "Hierarchical Report", () => new XtraReportsDemos.HierarchicalReport.Report() ) },
            { new ReportHelperTuple( "VehicleInspection", "Vehicle Inspection Report", () => new XtraReportsDemos.VehicleInspectionReport.Report() ) },
            { new ReportHelperTuple( "RestaurantMenu", "Restaurant Menu Report", () => new XtraReportsDemos.RestaurantMenu.Report() ) },
            { new ReportHelperTuple( "BalanceSheetReport", "Balance Sheet", () => new XtraReportsDemos.BalanceSheetReport.Report() ) },
            { new ReportHelperTuple( "CrossBandContent", "Cross-Band Content", () => new XtraReportsDemos.CrossBandContent.Report() ) },
            { new ReportHelperTuple( "ReportMergingWithPdf", "Report Merging with PDF", () => new XtraReportsDemos.ReportMergingWithPdf.Report() ) },
            { new ReportHelperTuple( "PdfVisualSignature", "PDF Visual Signature", () => new XtraReportsDemos.PdfVisualSignature.Report() ) },
            { new ReportHelperTuple( "EmbeddedPDFContent", "Embedded PDF Content", () => new XtraReportsDemos.EmbeddedPDFContent.Invoice() ) },
            { new ReportHelperTuple( "CarryoverSummaryReport", "Carryover Summary", () => new XtraReportsDemos.CarryoverSummaryReport.Report() ) }
        };

        public static ReportsDemoModel CreateModel(string reportID, HttpSessionStateBase session, HttpRequestBase request) {
            var currentViewer = ViewerSelectorState.GetSafeCurrentViewerArgFromQuery(request);
            return new ReportsDemoModel {
                ReportID = reportID,
                Report = ReportStorageHelper.LoadReport(reportID, session),
                CurrentViewer = currentViewer,
                EmulatorModel = new MobileEmulatorModel(reportID, request)
            };
        }

        internal static XtraReport GetReport(string reportID) {
            var reportInfo = Reports.FirstOrDefault(x => x.ReportId == reportID);
            return reportInfo == null ? null : reportInfo.OpenReport();
        }

        public static bool UseDefaultTheme { get; set; }

        public static string getCurrentTheme() {
            return ReportDemoHelper.UseDefaultTheme ? ASPxWebClientUIControl.ColorSchemeLight : Utils.CurrentStoredTheme(Utils.CurrentThemeDevExtremeCookieKey);
        }
    }
}
