Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports DevExpress.DataAccess.Web

Namespace DevExpress.Web.Demos
    Public Class CustomObjectDataSourceWizardTypeProvider
        Implements IObjectDataSourceWizardTypeProvider

        Private Function IObjectDataSourceWizardTypeProvider_GetAvailableTypes(ByVal context As String) As IEnumerable(Of Type) Implements IObjectDataSourceWizardTypeProvider.GetAvailableTypes
            Return { GetType(XtraReportsDemos.BalanceSheetReport.BalanceSheetData), GetType(XtraReportsDemos.HierarchicalReport.DataSource), GetType(XtraReportsDemos.MultiColumnReport.DataSource), GetType(XtraReportsDemos.RestaurantMenu.DataSource)}
        End Function
    End Class
End Namespace
