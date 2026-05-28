using System;
using System.Web.UI;
using DevExpress.Web.Demos;

namespace DevExpress.Web.Demos {
    public partial class UserControls_ViewerSelectorControl : UserControl, IMobileEmulator {
        public string CurrentViewer {
            get {
                return ViewerSelectorState.GetCurrentViewer();
            }
        }

        protected void AddMenuItem(ViewerSelectorState.Item selectorItem) {
            var menuItem = ViewerSelectorMenu.Items.Add();
            menuItem.Name = selectorItem.Name;
            menuItem.Selected = selectorItem.Name == CurrentViewer;
            menuItem.NavigateUrl = selectorItem.GenerateUrl(Request.Url);
            menuItem.Image.Url = "../Content/1x1.gif";
        }

        protected void Page_Load(object sender, EventArgs e) {
            foreach(ViewerSelectorState.Item selectorItem in ViewerSelectorState.Items) {
                AddMenuItem(selectorItem);
            }
        }

        public string ReportTypeName {
            get;
            set;
        }
    }
}
