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

        Public Overridable ReadOnly Property IsMobileViewer() As Boolean
            Get
                Return ViewerSelectorState.GetCurrentViewer() = ViewerSelectorState.MobileViewer
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
            Dim mobileEmulator = TryCast(contentPlaceHolder.FindControl("mobileEmulator"), IMobileEmulator)
            If mobileEmulator IsNot Nothing Then
                mobileEmulator.ReportTypeName = ReportTypeName
            End If
            Dim viewerSelector = TryCast(topRightMenuRegion.FindControl("viewerSelector"), IMobileEmulator)
            If viewerSelector IsNot Nothing Then
                viewerSelector.ReportTypeName = ReportTypeName
            End If

            If IsHTML5Viewer Then
                DemoHelper.Instance.UseDevExtremeThemeSelector = True
            Else
                DemoHelper.Instance.UseDevExtremeThemeSelector = False
            End If
        End Sub

        Protected ReadOnly Property ReportTypeName() As String
            Get
                Return GetType(TReport).FullName
            End Get
        End Property
    End Class
End Namespace
