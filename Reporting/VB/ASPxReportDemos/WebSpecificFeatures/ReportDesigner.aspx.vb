Imports System

Namespace DevExpress.Web.Demos
    Partial Public Class WebSpecificFeatures_ReportDesigner
        Inherits PageBase(Of XtraReportsDemos.MasterDetailReport.Report)

        Private reportName As String
        Private redirectUrlInputValue As String
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
            Header.Title = Utils.GetCurrentDemoPageTitle()
            Dim query = Request.QueryString

            If (query IsNot Nothing) AndAlso (query(DemoQueryKeys.ReportTypeNameKey) IsNot Nothing) Then
                reportName = query(DemoQueryKeys.ReportTypeNameKey)
                Dim fromDemo As String = query(DemoQueryKeys.FromDemoKey)
                Dim fromGroup As String = query(DemoQueryKeys.FromDemoGroupKey)
                Dim redirectDemoUrl As String = DevExpress.Web.Demos.Helper.GetDemoLocalUrl(Utils.CurrentDemoPage?.Product, fromGroup, fromDemo)
                If Not String.IsNullOrEmpty(redirectDemoUrl) Then
                    Dim redirectUrlWithQuery As String = redirectDemoUrl
                    If query(ViewerSelectorState.Key) = ViewerSelectorState.MobileViewer Then
                        redirectUrlWithQuery &= $"?{ViewerSelectorState.Key}={ViewerSelectorState.MobileViewer}"
                    End If

                    redirectUrlInputValue = Page.ResolveUrl(redirectUrlWithQuery)
                End If
            Else
                reportName = ReportTypeName
            End If
            redirectValue.Value = If(redirectUrlInputValue, "")

            If (Not IsRelativeUrl(redirectValue.Value)) OrElse String.IsNullOrEmpty(reportName) Then
                Response.Redirect("~/")
            End If

            reportDesigner.OpenReport(reportName)
        End Sub
        Private Shared Function IsRelativeUrl(ByVal url As String) As Boolean
            Dim ignore As Uri = Nothing
            Return Uri.TryCreate(url, UriKind.Relative, ignore)
        End Function
    End Class
End Namespace
