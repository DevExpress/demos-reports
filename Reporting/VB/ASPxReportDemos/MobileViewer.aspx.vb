Imports System
Imports DevExpress.Web.Demos

Namespace DevExpress.Web.Demos
    Partial Public Class MobileViewer
        Inherits System.Web.UI.Page

        Private reportName As String

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
            If (Request.QueryString IsNot Nothing) AndAlso (Request.QueryString(DemoQueryKeys.ReportTypeNameKey) IsNot Nothing) Then
                reportName = Request.QueryString(DemoQueryKeys.ReportTypeNameKey)
            Else
                reportName = GetType(XtraReportsDemos.MasterDetailReport.Report).FullName
            End If

            Header.Title = Utils.GetCurrentDemoPageTitle()
            webDocumentViewer.OpenReport(ReportStorageHelper.LoadReport(reportName, Session))
        End Sub
    End Class
End Namespace
