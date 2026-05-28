using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using DevExpress.Web.Mvc;

namespace DevExpress.Web.Demos {
    // Note: For instructions on enabling IIS6 or IIS7 classic mode,
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication {
        public static string AppData_Path = "";
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.ashx/{*pathInfo}");

            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
            );
        }

        protected void Application_Start() {
            RegisterRoutes(RouteTable.Routes);
            MvcApplication.AppData_Path = Server.MapPath("~/App_Data/");
            XtraReportsDemos.ObjectDataSourceTypesRegistrator.RegisterTrustedTypes();
            DevExpress.XtraReports.Configuration.Settings.Default.DesignAnalyzerOptions.EnableErrorCodeLinks = true;
            DevExpress.XtraReports.Web.ReportDesigner.Native.ReportDesignerBootstrapper.SessionState = System.Web.SessionState.SessionStateBehavior.Required;
            DevExpress.XtraReports.Web.WebDocumentViewer.Native.WebDocumentViewerBootstrapper.SessionState = System.Web.SessionState.SessionStateBehavior.Required;
            DevExpress.XtraReports.Web.QueryBuilder.Native.QueryBuilderBootstrapper.SessionState = System.Web.SessionState.SessionStateBehavior.Required;
            DevExpress.XtraReports.Web.WebDocumentViewer.DefaultWebDocumentViewerContainer.RegisterSingleton<DevExpress.XtraReports.Web.WebDocumentViewer.ICachedReportSourceWebResolver, DemoCachedReportSourceWebResolver>();
            DevExpress.XtraReports.Web.WebDocumentViewer.DefaultWebDocumentViewerContainer.RegisterSingleton<DevExpress.XtraReports.Web.WebDocumentViewer.IPdfSignatureOptionsProviderAsync, CustomPdfSignatureOptionsProviderAsync>();
            var cacheCleanerSettings = new DevExpress.XtraReports.Web.WebDocumentViewer.CacheCleanerSettings(TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
            DevExpress.XtraReports.Web.WebDocumentViewer.DefaultWebDocumentViewerContainer.RegisterSingleton<DevExpress.XtraReports.Web.WebDocumentViewer.CacheCleanerSettings>(cacheCleanerSettings);
            DevExpress.XtraReports.Web.ReportDesigner.DefaultReportDesignerContainer.RegisterDataSourceWizardConnectionStringsProvider<DataSourceWizardConnectionStringsProvider>();
            DevExpress.XtraReports.Web.ReportDesigner.DefaultReportDesignerContainer.RegisterDataSourceWizardJsonConnectionStorage<CustomDataSourceWizardJsonDataConnectionStorage>(true);
            DevExpress.XtraReports.Web.ReportDesigner.DefaultReportDesignerContainer.RegisterObjectDataSourceWizardTypeProvider<CustomObjectDataSourceWizardTypeProvider>();
            DevExpress.XtraReports.Web.ReportDesigner.DefaultReportDesignerContainer.Register<DevExpress.DataAccess.Wizard.Services.ICustomQueryValidator, DevExpress.DataAccess.Wizard.Services.CustomQueryValidator>();
            DevExpress.XtraReports.Web.WebDocumentViewer.DefaultWebDocumentViewerContainer.Register<DevExpress.DataAccess.Web.IJsonDataConnectionProviderFactory, CustomJsonDataConnectionProviderFactory>();
            DevExpress.XtraReports.Web.QueryBuilder.DefaultQueryBuilderContainer.Register<DevExpress.DataAccess.Web.IJsonDataConnectionProviderFactory, CustomJsonDataConnectionProviderFactory>();
            DevExpress.XtraReports.Web.WebDocumentViewer.DefaultWebDocumentViewerContainer.UseCachedReportSourceBuilder();
            ModelBinders.Binders.DefaultBinder = new DevExpressEditorsBinder();
            DevExpress.Internal.DataDirectoryHelper.DataFolderName = "App_Data";
            DevExpress.XtraReports.Web.Extensions.ReportStorageWebExtension.RegisterExtensionGlobal(DemoReportStorageExtension.ReportStorageExtension);
            DevExpress.Web.ASPxWebControl.CallbackError += new EventHandler(CallbackError);
            DevExpress.XtraReports.Web.WebDocumentViewer.DefaultWebDocumentViewerContainer.UseFileDocumentStorage(Server.MapPath("~/App_Data/WebDocumentViewerCache"));
            DevExpress.XtraReports.Web.ClientControls.LoggerService.Initialize(LogException);
            DevExpress.Security.Resources.AccessSettings.DataResources.SetRules(DevExpress.Security.Resources.DirectoryAccessRule.Allow(MvcApplication.AppData_Path), DevExpress.Security.Resources.UrlAccessRule.Deny());
            DevExpress.Security.Resources.AccessSettings.StaticResources.SetRules(DevExpress.Security.Resources.DirectoryAccessRule.Deny(), DevExpress.Security.Resources.UrlAccessRule.Deny());
            DevExpress.Security.Resources.AccessSettings.ReportingSpecificResources.SetRules(DevExpress.Security.Resources.DirectoryAccessRule.Deny(), DevExpress.Security.Resources.UrlAccessRule.Deny());
            DevExpress.Data.AsyncDownloadPolicy.SuppressAll();
            System.Net.ServicePointManager.SecurityProtocol |= System.Net.SecurityProtocolType.Tls12;
            DevExpress.XtraReports.Web.ASPxReportDesigner.ShouldClearReportScripts = true;
            MVCxReportDesigner.StaticInitialize();
        }

        void CallbackError(object sender, EventArgs e) {
            // Logging exceptions occur on callback events of DevExpress ASP.NET MVC controls.
            // To learn more, see https://supportcenter.devexpress.com/ticket/details/e4588/
        }

        void LogException(Exception exception, string message) {
            DevExpress.Utils.About.UAlgo.Default.DoEventException(exception);
        }

        protected void Session_Start(object sender, EventArgs e) {
            Session["urls"] = ReportDemoHelper.Reports.ToDictionary((x) => x.ReportId, (x) => x.DisplayName);
        }

        protected void Application_PreRequestHandlerExecute(object sender, EventArgs e) {
            DevExpressHelper.Theme = Utils.CurrentStoredTheme(Utils.CurrentThemeAspCookieKey);
            if(DevExpressHelper.IsCallback)
                Utils.RegisterCurrentMvcDemoOnCallback();
        }
    }
}
