using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.Demos;

namespace DevExpress.Web.Demos {
    public partial class Error404 : Page {
        protected void Page_Load(object sender, EventArgs e) {
            Response.StatusCode = 404;
        }
    }
}
