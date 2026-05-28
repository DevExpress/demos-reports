Imports System
Imports System.Collections.Generic
Imports System.Text.RegularExpressions
Imports System.Web
Imports System.Web.SessionState
Imports System.Web.UI
Imports DevExpress.Web

Namespace DevExpress.Web.Demos
    Public Class [Global]
        Inherits HttpApplication

        Private Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
            Helper.AppData_Path = Server.MapPath("~/App_Data/")
            XtraReportsDemos.ObjectDataSourceTypesRegistrator.RegisterTrustedTypes()
            DevExpress.XtraReports.Web.ReportDesigner.Native.ReportDesignerBootstrapper.SessionState = SessionStateBehavior.Required
            DevExpress.XtraReports.Web.WebDocumentViewer.Native.WebDocumentViewerBootstrapper.SessionState = SessionStateBehavior.Required
            DevExpress.XtraReports.Web.QueryBuilder.Native.QueryBuilderBootstrapper.SessionState = System.Web.SessionState.SessionStateBehavior.Required
            DevExpress.Internal.DataDirectoryHelper.DataFolderName = "App_Data"
            DevExpress.XtraReports.Web.Extensions.ReportStorageWebExtension.RegisterExtensionGlobal(DemoReportStorageExtension.ReportStorageExtension)
            Utils.EnsureRequestValidationMode()
            AddHandler DevExpress.Web.ASPxWebControl.CallbackError, AddressOf CallbackError
            DevExpress.XtraReports.Web.ClientControls.LoggerService.Initialize(AddressOf LogException)
            DevExpress.XtraReports.Web.WebDocumentViewer.DefaultWebDocumentViewerContainer.UseFileDocumentStorage(Server.MapPath("~/App_Data/WebDocumentViewerCache"))
            DevExpress.XtraReports.Web.WebDocumentViewer.DefaultWebDocumentViewerContainer.RegisterSingleton(Of DevExpress.XtraReports.Web.WebDocumentViewer.IWebDocumentViewerReportResolver, DemoReportResolver)()
            DevExpress.XtraReports.Web.WebDocumentViewer.DefaultWebDocumentViewerContainer.RegisterSingleton(Of DevExpress.XtraReports.Web.WebDocumentViewer.ICachedReportSourceWebResolver, DemoReportResolver)()
            DevExpress.XtraReports.Web.WebDocumentViewer.DefaultWebDocumentViewerContainer.RegisterSingleton(Of DevExpress.XtraReports.Web.WebDocumentViewer.IPdfSignatureOptionsProviderAsync, CustomPdfSignatureOptionsProviderAsync)()
            Dim cacheCleanerSettings = New DevExpress.XtraReports.Web.WebDocumentViewer.CacheCleanerSettings(TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1))
            DevExpress.XtraReports.Web.WebDocumentViewer.DefaultWebDocumentViewerContainer.RegisterSingleton(Of DevExpress.XtraReports.Web.WebDocumentViewer.CacheCleanerSettings)(cacheCleanerSettings)
            DevExpress.XtraReports.Web.ReportDesigner.DefaultReportDesignerContainer.RegisterDataSourceWizardConnectionStringsProvider(Of DataSourceWizardConnectionStringsProvider)()
            DevExpress.XtraReports.Web.ReportDesigner.DefaultReportDesignerContainer.RegisterDataSourceWizardJsonConnectionStorage(Of CustomDataSourceWizardJsonDataConnectionStorage)(True)
            DevExpress.XtraReports.Web.ReportDesigner.DefaultReportDesignerContainer.RegisterObjectDataSourceWizardTypeProvider(Of CustomObjectDataSourceWizardTypeProvider)()
            DevExpress.XtraReports.Web.ReportDesigner.DefaultReportDesignerContainer.Register(Of DevExpress.DataAccess.Wizard.Services.ICustomQueryValidator, DevExpress.DataAccess.Wizard.Services.CustomQueryValidator)()
            DevExpress.XtraReports.Web.WebDocumentViewer.DefaultWebDocumentViewerContainer.Register(Of DevExpress.DataAccess.Web.IJsonDataConnectionProviderFactory, CustomJsonDataConnectionProviderFactory)()
            DevExpress.XtraReports.Web.QueryBuilder.DefaultQueryBuilderContainer.Register(Of DevExpress.DataAccess.Web.IJsonDataConnectionProviderFactory, CustomJsonDataConnectionProviderFactory)()
            DevExpress.XtraReports.Web.WebDocumentViewer.DefaultWebDocumentViewerContainer.UseCachedReportSourceBuilder()
            DevExpress.Security.Resources.AccessSettings.DataResources.SetRules(DevExpress.Security.Resources.DirectoryAccessRule.Allow(Helper.AppData_Path), DevExpress.Security.Resources.UrlAccessRule.Deny())
            DevExpress.Security.Resources.AccessSettings.StaticResources.SetRules(DevExpress.Security.Resources.DirectoryAccessRule.Deny(), DevExpress.Security.Resources.UrlAccessRule.Deny())
            DevExpress.Security.Resources.AccessSettings.ReportingSpecificResources.SetRules(DevExpress.Security.Resources.DirectoryAccessRule.Deny(), DevExpress.Security.Resources.UrlAccessRule.Deny())
            DevExpress.Data.AsyncDownloadPolicy.SuppressAll()
            DevExpress.XtraReports.Web.ASPxReportDesigner.ShouldClearReportScripts = True
            System.Net.ServicePointManager.SecurityProtocol = System.Net.ServicePointManager.SecurityProtocol Or System.Net.SecurityProtocolType.Tls12
            DevExpress.XtraReports.Configuration.Settings.Default.DesignAnalyzerOptions.EnableErrorCodeLinks = True
        End Sub

        Private Sub CallbackError(ByVal sender As Object, ByVal e As EventArgs)
            ' Logging exceptions occur on callback events of DevExpress ASP.NET controls.
            ' To learn more, see https://supportcenter.devexpress.com/ticket/details/e2398/
        End Sub

        Private Sub LogException(ByVal exception As Exception, ByVal message As String)
            DevExpress.Utils.About.UAlgo.Default.DoEventException(exception)
        End Sub

        Private Sub Application_PreRequestHandlerExecute(ByVal sender As Object, ByVal e As EventArgs)
            DevExpress.Web.ASPxWebClientUIControl.GlobalColorScheme = Utils.CurrentStoredTheme(Utils.CurrentThemeDevExtremeCookieKey)
            DevExpress.Web.ASPxWebControl.GlobalTheme = Utils.CurrentStoredTheme(Utils.CurrentThemeAspCookieKey)
            If CurrentPage IsNot Nothing Then
                AddHandler CurrentPage.PreInit, AddressOf Page_PreInit
            End If
        End Sub

        Protected Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
            Session("urls") = New Dictionary(Of String, String)() From {
                {"XtraReportsDemos.SalesSummary.Report", "Sales Summary Report"},
                {"XtraReportsDemos.MailMerge.Report", "Mail Merge"},
                {"XtraReportsDemos.CrossBandControls.Report", "Cross-Band Controls"},
                {"XtraReportsDemos.ProfitAndLossReport.Report", "Profit And Loss Report"},
                {"XtraReportsDemos.ReportMerging.MergedReport", "Report Merging" },
                {"XtraReportsDemos.AnchorVertical.Report", "Anchoring"},
                {"XtraReportsDemos.NorthwindTraders.ProductListReport", "Product List"},
                {"XtraReportsDemos.NorthwindTraders.CatalogReport", "Fall Catalog"},
                {"XtraReportsDemos.NorthwindTraders.InvoiceReport", "Invoice"},
                {"XtraReportsDemos.InteractiveSorting.Report", "Interactive Sorting"},
                {"XtraReportsDemos.MasterDetailReport.Report", "Master-Detail Report"},
                {"XtraReportsDemos.MultiColumnReport.Report", "Multi-Column Report"},
                {"XtraReportsDemos.IListDataSource.Report", "Hidden Columns"},
                {"XtraReportsDemos.CustomDraw.Report", "Population"},
                {"XtraReportsDemos.ShrinkGrow.Report", "Shrink and Grow"},
                {"XtraReportsDemos.LabelReport.ProductLabelsReport", "Label Report"},
                {"XtraReportsDemos.BarCodes.BarCodeTypesReport", "Bar Code"},
                {"XtraReportsDemos.Sparkline.Report", "Sparkline"},
                {"XtraReportsDemos.Subreports.MasterReport", "Subreports"},
                {"XtraReportsDemos.TableReport.Report", "Table Report"},
                {"XtraReportsDemos.Charts.Report", "Chart"},
                {"XtraReportsDemos.Shape.Report", "Shapes"},
                {"XtraReportsDemos.SideBySideReports.EmployeeComparisonReport", "Side-by-side Reports"},
                {"XtraReportsDemos.CalculatedFieldsReport.Report", "Calculated Fields"},
                {"XtraReportsDemos.FormattingRules.Report", "Conditional Formatting"},
                {"XtraReportsDemos.CachedDocumentSourceReport.ReportWeb", "Large Dataset"},
                {"XtraReportsDemos.PivotGridAndChart.Report", "PivotGrid And Chart"},
                {"XtraReportsDemos.DrillDownReport.DrillDownReport", "Drill-Down Report"},
                {"XtraReportsDemos.DrillThroughReport.Report", "Drill-Through Report" },
                {"XtraReportsDemos.EmployeePerformanceReview.Report", "Employee Performance Review"},
                {"XtraReportsDemos.CharacterComb.Report", "E-Form"},
                {"XtraReportsDemos.SwissQRCode.SwissQRBill", "Swiss QR Bill"},
                {"XtraReportsDemos.RollPaper.Report", "Roll Paper Report" },
                {"XtraReportsDemos.HierarchicalReport.Report", "Hierarchical Report" },
                {"XtraReportsDemos.VehicleInspectionReport.Report", "Vehicle Inspection Report" },
                {"XtraReportsDemos.RestaurantMenu.Report", "Restaurant Menu Report" },
                {"XtraReportsDemos.BalanceSheetReport.Report", "Balance Sheet" },
                {"XtraReportsDemos.CrossBandContent.Report", "Cross-Band Content" },
                {"XtraReportsDemos.EmbeddedPDFContent.Invoice", "Embedded PDF Content" }
            }
        End Sub

        Private Shared ReadOnly Property CurrentPage() As Page
            Get
                Return TryCast(HttpContext.Current.CurrentHandler, Page)
            End Get
        End Property

        Private Shared Sub Page_PreInit(ByVal sender As Object, ByVal e As EventArgs)
            Utils.RegisterCurrentWebFormsDemo(CurrentPage)
        End Sub
    End Class
End Namespace
