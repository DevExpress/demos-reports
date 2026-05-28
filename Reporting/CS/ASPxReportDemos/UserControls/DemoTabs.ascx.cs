using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.Demos;

namespace DevExpress.Web.Demos {
    public partial class UserControls_DemoTabs : System.Web.UI.UserControl {
        protected void Page_Load(object sender, EventArgs e) {
            var index = 0;
            if(Utils.CurrentDemoPage != null && !Utils.IsIntro && !Utils.CurrentDemo.Product.HideSourceCode && !Utils.CurrentDemo.HideSourceCode) {
                foreach(SourceCodePage page in Utils.GetCurrentSourceCodePages()) {
                    var tab = DemoPageControl.TabPages.Add(page.Title, page.Title);
                    var codeViewer = (CodeViewerControl)LoadControl("~\\UserControls\\CodeViewer.ascx");
                    codeViewer.SourceCodePage = page;
                    codeViewer.Index = index;
                    DemosModel currentModel = DemosModel.Instance;
                    if (currentModel != null)
                        codeViewer.DisableTextWrap = DemosModel.Instance.DisableTextWrap;
                    tab.Controls.Add(codeViewer);
                    index++;
                }
            }
        }
        protected void btnOpenSolution_Init(object sender, EventArgs e) {
            if(Utils.CurrentDemoPage == null) return;
            DevExpress.Web.ASPxButton button = sender as DevExpress.Web.ASPxButton;
            if(Utils.CurrentDemoPage.Product.Key.ToLower().Contains("report"))
                button.Visible = false;
            else
                button.ClientSideEvents.Click = "function (s, e) { window.open('" + GenerateOpenSolutionLink(button.ID.Contains("VB") ? "VB" : "CS") + "', '_self'); }";
        }
        private string GenerateOpenSolutionLink(string language) {
            DevExpress.Web.Demos.DemoPageModel pageModel = Utils.CurrentDemoPage;
            List<string> viewsWithSeparatePages = new List<string>() { "adaptivelayout", "responsivelayout", "floatingactionbuttonforgridview" };
            string viewName = viewsWithSeparatePages.Contains(pageModel.Key.ToLower()) ? pageModel.Key + "Page" : pageModel.Key;
#pragma warning disable DX0025 // not a path (and no travesal here)
            return String.Format("dxdemo://{0}/{1}/MainDemo/{2}/{3}/{4}",
                pageModel.Product.Key.ToLower() == "officefileapi" ? "Docs" : "Asp",
                pageModel.Product.Key.ToLower() == "officefileapi" ? "DocumentServerForAsp" : pageModel.Product.Key.Replace(".", ""),
                pageModel.Group == null ? "" : pageModel.Group.Key,
                viewName,
                language.ToUpper());
#pragma warning restore DX0025
        }
        protected void lblPath_Init(object sender, EventArgs e) {
            if(Utils.CurrentDemoPage == null) return;
            DevExpress.Web.ASPxLabel lbl = (DevExpress.Web.ASPxLabel)sender;
            string pathPrefix = lbl.ID.Contains("VB") ? "VB" : "CS";
            if(Utils.CurrentDemoPage.Product.Key.ToLower().Contains("report")) {
                var pathSuffix = DevExpress.Web.Demos.Utils.CurrentDemoPage.Group.Key == "WebSpecificFeatures" ? @"ASPxReportDemos\WebSpecificFeatures" : @"DevExpress.DemoReports";
                lbl.Text = string.Format(@"\Users\Public\Documents\DevExpress Demos XX.X\Components\Reporting\{0}\{1}", pathPrefix, pathSuffix);
            } else {
                lbl.Text = String.Format(@"\Users\Public\Documents\DevExpress Demos XX.X\Components\ASP.NET\{0}\{1}", pathPrefix, DevExpress.Web.Demos.Utils.CurrentDemoPage.Product.Url);
            }
        }
    }
}
