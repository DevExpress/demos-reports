using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace DevExpress.Web.Demos {
    public abstract class MobileViewerPage<TReport> : PageBase<TReport> {
        public override bool IsMobileViewer {
            get {
                return true;
            }
        }
    }
    public partial class WebSpecificFeatures_MobileViewer : MobileViewerPage<XtraReportsDemos.MasterDetailReport.Report> {
    }
}
