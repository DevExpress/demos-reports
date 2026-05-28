Imports System.Web
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraReports.Web
Imports DevExpress.XtraReports.Web.WebDocumentViewer

Namespace DevExpress.Web.Demos
    Public Class DemoReportResolver
        Implements IWebDocumentViewerReportResolver, ICachedReportSourceWebResolver

        Private Function ICachedReportSourceWebResolver_TryGetCachedReportSourceWeb(ByVal reportEntry As String, ByRef cachedReportSourceWeb As CachedReportSourceWeb) As Boolean Implements ICachedReportSourceWebResolver.TryGetCachedReportSourceWeb
            Dim report As XtraReport = DirectCast(Me, IWebDocumentViewerReportResolver).Resolve(reportEntry)
            cachedReportSourceWeb = If(report Is Nothing, Nothing, New CachedReportSourceWeb(report))
            Return cachedReportSourceWeb IsNot Nothing
        End Function

        Private Function IWebDocumentViewerReportResolver_Resolve(ByVal reportEntry As String) As XtraReport Implements IWebDocumentViewerReportResolver.Resolve
            Return ReportStorageHelper.LoadReport(reportEntry,If(HttpContext.Current IsNot Nothing, HttpContext.Current.Session, Nothing))
        End Function
    End Class
End Namespace
