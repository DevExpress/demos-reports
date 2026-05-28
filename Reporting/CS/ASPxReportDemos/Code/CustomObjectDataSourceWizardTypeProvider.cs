using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.DataAccess.Web;

namespace DevExpress.Web.Demos {
    public class CustomObjectDataSourceWizardTypeProvider : IObjectDataSourceWizardTypeProvider {
        IEnumerable<Type> IObjectDataSourceWizardTypeProvider.GetAvailableTypes(string context) {
            return new[] {
                    typeof(XtraReportsDemos.BalanceSheetReport.BalanceSheetData),
                    typeof(XtraReportsDemos.HierarchicalReport.DataSource),
                    typeof(XtraReportsDemos.MultiColumnReport.DataSource),
                    typeof(XtraReportsDemos.RestaurantMenu.DataSource),
                };
        }
    }
}
