Imports System

Namespace DevExpress.Web.Demos
    Partial Public Class WebSpecificFeatures_ColorSchemeCustomization
        Inherits PageBase(Of XtraReportsDemos.MasterDetailReport.Report)

        Friend Const ShowDesignerKey As String = "ShowDesigner"
        Public ReadOnly Property ShowViewerButton() As Boolean
            Get
                Return Request.Params(ShowDesignerKey) IsNot Nothing
            End Get
        End Property
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
            Header.Title = Utils.GetCurrentDemoPageTitle()
            DemoHelper.Instance.SuppressThemeSelector = True
            DevExpress.Web.ASPxWebClientUIControl.GlobalColorScheme = DevExpress.Web.ASPxWebClientUIControl.ColorSchemeLight

            Dim topRightMenuRegion = Master.FindControl("TopRightMenuRegion")
            Dim reportControlToggleButton = TryCast(topRightMenuRegion.FindControl("reportControlToggleButton"), IReportControlToggleButton)
            If reportControlToggleButton IsNot Nothing Then
                reportControlToggleButton.ReportTypeName = ReportTypeName
                reportControlToggleButton.ShowViewerButton = ShowViewerButton
                Dim urlParams As String = ""
                If Not ShowViewerButton Then
                    urlParams = $"?{ShowDesignerKey}=true"
                End If
                reportControlToggleButton.RouteUrl = Page.ResolveClientUrl("~/WebSpecificFeatures/ColorSchemeCustomization.aspx" & urlParams)
            End If

            If Not IsPostBack Then
                If ShowViewerButton Then
                    reportDesigner.OpenReport(ReportTypeName)
                Else
                    webDocumentViewer.OpenReport(ReportTypeName)
                End If
            End If
        End Sub
    End Class
End Namespace
