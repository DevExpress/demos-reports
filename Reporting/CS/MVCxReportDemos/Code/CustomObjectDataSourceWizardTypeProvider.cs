using System;
using System.Collections.Generic;
using DevExpress.DataAccess.Web;

public class CustomObjectDataSourceWizardTypeProvider : IObjectDataSourceWizardTypeProvider {
    public IEnumerable<Type> GetAvailableTypes(string context) {
        return new[] {
                typeof(XtraReportsDemos.BalanceSheetReport.BalanceSheetData),
                typeof(XtraReportsDemos.HierarchicalReport.DataSource),
                typeof(XtraReportsDemos.MultiColumnReport.DataSource),
                typeof(XtraReportsDemos.RestaurantMenu.DataSource),
            };
    }
}
