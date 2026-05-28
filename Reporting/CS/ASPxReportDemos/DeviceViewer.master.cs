using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace DevExpress.Web.Demos
{
    public partial class DeviceViewer : System.Web.UI.MasterPage
    {
        private bool enableAnimation = true;

        public string Url { get; set; }
        public bool DefaultRotate { get; set; }
        public bool DefaultMobile { get; set; }
        public bool HideRotate { get; set; }

        public bool EnableAnimation {
            get { return enableAnimation; }
            set { enableAnimation = value; }
        }

        protected void Page_Load(object sender, EventArgs e) {
            Utils.RegisterCssLink(Page, "~/Content/deviceViewer.css");

            if(!DevExpress.Web.Internal.RenderUtils.Browser.Platform.IsMobileUI)
                viewer.Attributes["src"] = ResolveUrl(Url);

            QrcodeLink.Attributes["href"] = ResolveUrl(Url);
            QrcodeImg.Attributes["src"] = FullScreenModeUtils.GetQRCodeImageUrl(this.Page, Url);
        }

        protected string GetUrl() {
            return ResolveUrl(Url);
        }
    }
}
