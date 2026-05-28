using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraReports.UI;

namespace AspNetCoreDemos.Reporting.Services {
    public class DemoReportSource : IDemoReportSource {
        readonly Dictionary<string, Func<XtraReport>> predefinedReports = new Dictionary<string, Func<XtraReport>> {
            ["AIPoweredInvoiceReport"] = () => new XtraReportsDemos.NorthwindTraders.InvoiceReport(),
            ["DrillDownReport"] = () => new XtraReportsDemos.DrillDownReport.DrillDownReport(),
            ["DrillThroughReport"] = () => new XtraReportsDemos.DrillThroughReport.Report(),
            ["InteractiveSorting"] = () => new XtraReportsDemos.InteractiveSorting.Report(),
            ["CharacterComb"] = () => new XtraReportsDemos.CharacterComb.Report(),
            ["TableReport"] = () => new XtraReportsDemos.TableReport.Report(),
            ["MasterDetailReport"] = () => new XtraReportsDemos.MasterDetailReport.Report(),
            ["Subreports.MasterReport"] = () => new XtraReportsDemos.Subreports.MasterReport(),
            ["MultiColumnReport"] = () => new XtraReportsDemos.MultiColumnReport.Report(),
            ["ProductLabelsReport"] = () => new XtraReportsDemos.BarCodes.ProductLabelsReport(),
            ["ReportMerging.MergedReport"] = () => new XtraReportsDemos.ReportMerging.MergedReport(),
            ["SideBySideReports.EmployeeComparisonReport"] = () => new XtraReportsDemos.SideBySideReports.EmployeeComparisonReport(),
            ["PivotGridAndChart"] = () => new XtraReportsDemos.PivotGridAndChart.Report(),
            ["HiddenColumns"] = () => new XtraReportsDemos.IListDataSource.Report(),
            ["CalculatedFieldsReport"] = () => new XtraReportsDemos.CalculatedFieldsReport.Report(),
            ["NorthwindTraders.ProductListReport"] = () => new XtraReportsDemos.NorthwindTraders.ProductListReport(),
            ["NorthwindTraders.CatalogReport"] = () => new XtraReportsDemos.NorthwindTraders.CatalogReport(),
            ["NorthwindTraders.InvoiceReport"] = () => new XtraReportsDemos.NorthwindTraders.InvoiceReport(),
            ["ProfitAndLossReport"] = () => new XtraReportsDemos.ProfitAndLossReport.Report(),
            ["ShrinkGrow"] = () => new XtraReportsDemos.ShrinkGrow.Report(),
            ["MailMerge"] = () => new XtraReportsDemos.MailMerge.Report(),
            ["AnchorVertical"] = () => new XtraReportsDemos.AnchorVertical.Report(),
            ["Sparkline"] = () => new XtraReportsDemos.Sparkline.Report(),
            ["BarCodeTypesReport"] = () => new XtraReportsDemos.BarCodes.BarCodeTypesReport(),
            ["Shape"] = () => new XtraReportsDemos.Shape.Report(),
            ["Charts"] = () => new XtraReportsDemos.Charts.Report(),
            ["CrossBandContent"] = () => new XtraReportsDemos.CrossBandContent.Report(),
            ["CrossBandControls"] = () => new XtraReportsDemos.CrossBandControls.Report(),
            ["CustomDraw"] = () => new XtraReportsDemos.CustomDraw.Report(),
            ["FormattingRules"] = () => new XtraReportsDemos.FormattingRules.Report(),
            ["EmployeePerformanceReview"] = () => new XtraReportsDemos.EmployeePerformanceReview.Report(),
            ["SwissQRBill"] = () => new XtraReportsDemos.SwissQRCode.SwissQRBill(),
            ["CachedDocumentSource"] = () => new XtraReportsDemos.CachedDocumentSourceReport.ReportWeb(),
            ["RollPaper"] = () => new XtraReportsDemos.RollPaper.Report(),
            ["HierarchicalReport"] = () => new XtraReportsDemos.HierarchicalReport.Report(),
            ["VehicleInspectionReport"] = () => new XtraReportsDemos.VehicleInspectionReport.Report(),
            ["RestaurantMenu"] = () => new XtraReportsDemos.RestaurantMenu.Report(),
            ["BalanceSheetReport"] = () => new XtraReportsDemos.BalanceSheetReport.Report(),
            ["SalesSummary"] = () => new XtraReportsDemos.SalesSummary.Report(),
            ["ReportMergingWithPdf"] = () => new XtraReportsDemos.ReportMergingWithPdf.Report(),
            ["PdfVisualSignatureReport"] = () => new XtraReportsDemos.PdfVisualSignature.Report(),
            ["EmbeddedPDFContentReport"] = () => new XtraReportsDemos.EmbeddedPDFContent.Invoice(),
            ["CarryoverSummaryReport"] = () => new XtraReportsDemos.CarryoverSummaryReport.Report(),
            ["AccessibilityMasterDetailReport"] = () => new XtraReportsDemos.MasterDetailReport.Report() { RequestParameters = true },
        };

        public Dictionary<string, string> GetReportList() {
            return predefinedReports.ToDictionary(i => i.Key, i => i.Key);
        }

        public XtraReport GetReport(string reportName) {
            Func<XtraReport> ctor;
            if(predefinedReports.TryGetValue(reportName, out ctor))
                return ctor();
            return null;
        }
    }
}
