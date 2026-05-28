using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using AspNetCoreDemos.DemoShell;
using AspNetCoreDemos.Reporting.Services;
using DevExpress.AIIntegration;
using DevExpress.AIIntegration.Reporting.Common.Models;
using DevExpress.AspNetCore;
using DevExpress.AspNetCore.Reporting;
using DevExpress.DataAccess.Web;
using DevExpress.Security.Resources;
using DevExpress.XtraReports.Web.Extensions;
using DevExpress.XtraReports.Web.ReportDesigner.Services;
using DevExpress.XtraReports.Web.WebDocumentViewer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SignPdfDocumentExample.Services;

namespace AspNetCoreDemos.Reporting {
    public class Startup {
        public Startup(IConfiguration configuration, IWebHostEnvironment env) {
            Configuration = configuration;
            HostingEnvironment = env;
        }

        public static IConfiguration Configuration { get; set; }
        public IWebHostEnvironment HostingEnvironment { get; set; }
        public void ConfigureServices(IServiceCollection services) {
            AppDomain.CurrentDomain.SetData("DataDirectory", HostingEnvironment.ContentRootPath);
            AppDomain.CurrentDomain.SetData("DXResourceDirectory", HostingEnvironment.WebRootPath);
            services.AddDevExpressControls();
            var reportsCacheCleanerSettings = new CacheCleanerSettings(TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
            services.AddSession();
            services
                .AddControllersWithViews()
                .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

            services.ConfigureReportingServices(configurator => {
                configurator.ConfigureWebDocumentViewer(viewerConfigurator => {
                    viewerConfigurator.UseFileDocumentStorage(Path.Combine(HostingEnvironment.ContentRootPath, "PreviewedReports"));
                    viewerConfigurator.UseCachedReportSourceBuilder();
                });
                configurator.ConfigureReportDesigner(designerConfigurator => {
                    designerConfigurator.RegisterDataSourceWizardJsonConnectionStorage<CustomDataSourceWizardJsonDataConnectionStorage>(true);
                    designerConfigurator.RegisterObjectDataSourceWizardTypeProvider<CustomObjectDataSourceWizardTypeProvider>();
                    designerConfigurator.RegisterDataSourceWizardConfigFileConnectionStringsProvider();
                });
            });

            var azureOpenAIEndpoint = Configuration.GetSection("AIIntegrationSettings")["EndpointUrl"];
            var azureOpenAIKey = Configuration.GetSection("AIIntegrationSettings")["Key"];
            var deploymentName = Configuration.GetSection("AIIntegrationSettings")["DeploymentName"];

            var azureOpenAIClient = new Azure.AI.OpenAI.AzureOpenAIClient(
                new Uri(azureOpenAIEndpoint),
                new System.ClientModel.ApiKeyCredential(azureOpenAIKey),
                new Azure.AI.OpenAI.AzureOpenAIClientOptions() {
                    Transport = new PromoteHttpStatusErrorsPipelineTransport()
                });
            IChatClient chatClient = azureOpenAIClient.GetChatClient(deploymentName).AsIChatClient();

            services.AddSingleton(chatClient);
            services.AddDevExpressAI(config => {
                config.AddWebReportingAIIntegration(cfg => {
                    cfg.AddSummarization(configure => configure.SetSummarizationMode(SummarizationMode.Abstractive))
                    .AddTranslation(translationCfg => {
                        translationCfg
                            .EnableTranslation()
                            .EnableInlineTranslation()
                            .SetLanguages(new List<LanguageInfo>() {
                                        new LanguageInfo(){ Id = "es-ES", Text = "Spanish"},
                                        new LanguageInfo(){ Id = "de-DE", Text = "German"},
                            });
                    })
                    .AddLocalization()
                    .AddPromptToExpressionConverter()
                    .AddPromptToReportConverter()
                    .AddTestDataSource();
                });
            });
            services
                .AddTransient<DevExpress.DataAccess.Wizard.Services.ICustomQueryValidator, DevExpress.DataAccess.Wizard.Services.CustomQueryValidator>()
                .AddSingleton<ICachedReportSourceWebResolver, DemoCachedReportSourceWebResolver>()
                .AddSingleton<IDemoReportSource, DemoReportSource>()
                .AddSingleton<CacheCleanerSettings>(reportsCacheCleanerSettings)
                .AddTransient<IJsonDataConnectionProviderFactory, CustomJsonDataConnectionProviderFactory>()
                .AddSingleton<ReportStorageWebExtension, DemoReportStorageWebExtension>()
                .AddScoped<IAIExceptionHandler, AIExceptionHandler>()
                .AddScoped<IReportDesignerExceptionHandler, AIReportDesignerExceptionHandler>()
                .AddScoped<IWebDocumentViewerExceptionHandler, AIWebDocumentViewerExceptionHandler>()
                .AddSingleton<IPdfSignatureOptionsProviderAsync, CustomPdfSignatureOptionsProviderAsync>();
            if(!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                DevExpress.Drawing.Settings.DrawingEngine = DevExpress.Drawing.DrawingEngine.Skia;
            }
#if DEBUG
            services.AddScoped<DemoMenuMeta>();
#else
            services.AddSingleton<DemoMenuMeta>();
#endif
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if(env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseSession();
            app.UseDevExpressControls();
            app.UseReporting(configurator => {
                configurator.DesignAnalyzerOptions.EnableErrorCodeLinks = true;
            });
            System.Net.ServicePointManager.SecurityProtocol |= System.Net.SecurityProtocolType.Tls12;
            AccessSettings.ReportingSpecificResources.SetRules(DirectoryAccessRule.Deny(), UrlAccessRule.Deny());
            AccessSettings.StaticResources.SetRules(DirectoryAccessRule.Deny(), UrlAccessRule.Deny());
            AccessSettings.DataResources.SetRules(DirectoryAccessRule.Allow(Path.Combine(HostingEnvironment.WebRootPath, "Content")), UrlAccessRule.Deny());
            DevExpress.Data.AsyncDownloadPolicy.SuppressAll();
            ReportStorageWebExtension.RegisterExtensionGlobal(app.ApplicationServices.GetService<ReportStorageWebExtension>());
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}",
                    defaults: new { controller = "Default", action = "Index" });
            });
            XtraReportsDemos.ObjectDataSourceTypesRegistrator.RegisterTrustedTypes();
            DemoUtils.DISPLAY_CODE = false;
            DemoUtils.SIMULATOR_NO_BORDER = true;
            DemoUtils.PRODUCT_CSS_BUNDLE_NAME = "reporting";
            DemoUtils.PRODUCT_THEME_CSS_TEMPLATE = "~/xtrareportsjs/css/dx-analytics.{0}.css";
        }
    }
}
