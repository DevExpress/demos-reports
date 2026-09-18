Imports System
Imports System.Web.UI
Imports DevExpress.XtraReports.Web

Namespace DevExpress.Web.Demos
    Public MustInherit Class PageBase(Of TReport)
        Inherits Page

        Public Overridable ReadOnly Property IsHTML5Viewer() As Boolean
            Get
                Return ViewerSelectorState.GetCurrentViewer() = ViewerSelectorState.ModernViewer
            End Get
        End Property

        Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs)
            DemoHelper.Instance.ControlAreaMaxWidth = System.Web.UI.WebControls.Unit.Percentage(100)

            Dim topRightMenuRegion = Master.FindControl("TopRightMenuRegion")
            Dim showDesignerButton = TryCast(topRightMenuRegion.FindControl("showDesignerButton"), IReportControlToggleButton)
            If showDesignerButton IsNot Nothing Then
                showDesignerButton.ReportTypeName = ReportTypeName
            End If

            Dim contentPlaceHolder = Master.FindControl("ContentHolder")
            Dim webDocumentViewer = TryCast(contentPlaceHolder.FindControl("webDocumentViewer"), ASPxWebDocumentViewer)
            If webDocumentViewer IsNot Nothing AndAlso IsHTML5Viewer Then
                webDocumentViewer.OpenReport(ReportTypeName)
            End If

            DemoHelper.Instance.UseDevExtremeThemeSelector = True
        End Sub

        Protected ReadOnly Property ReportTypeName() As String
            Get
                Return GetType(TReport).FullName
            End Get
        End Property
    End Class
End Namespace
