using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.Demos;

namespace DevExpress.Web.Demos {
    public partial class UserControls_CodeViewer : CodeViewerControl {
        protected void Page_Load(object sender, EventArgs e) {
        }

        protected string EncodeScriptForAttribute(object content) {
            return HttpUtility.HtmlEncode(DevExpress.Web.Internal.HtmlConvertor.ToScript(content));
        }

        protected string GetClassName() {
            string className = "CodeBlock";
            string title = SourceCodePage.Title;

            int index = title.LastIndexOf('.');
            string extension = string.Empty;

            if(index != -1)
                extension = title.Substring(index, title.Length - index);
            if(SourceCodePage.Title == "ASPX" || extension == ".aspx" || extension == ".ascx")
                className += " ASPX";

            return className;
        }
    }
}
