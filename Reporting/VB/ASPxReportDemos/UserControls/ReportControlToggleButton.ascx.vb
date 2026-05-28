Imports System.Web
Imports System.Web.UI

Namespace DevExpress.Web.Demos
    Partial Public Class UserControls_ReportControlToggleButtonControl
        Inherits UserControl
        Implements IReportControlToggleButton

        Public Property ReportTypeName() As String Implements IReportControlToggleButton.ReportTypeName
        Public Property ShowViewerButton() As Boolean Implements IReportControlToggleButton.ShowViewerButton

        Private routeUrl_Renamed As String

        Public Property RouteUrl() As String Implements IReportControlToggleButton.RouteUrl
            Get
                If Not String.IsNullOrEmpty(routeUrl_Renamed) Then
                    Return routeUrl_Renamed
                End If

                Dim nvc = HttpUtility.ParseQueryString("")
                nvc(DemoQueryKeys.ReportTypeNameKey) = ReportTypeName

                If Utils.CurrentDemoPage IsNot Nothing Then
                    nvc(DemoQueryKeys.FromDemoGroupKey) = Utils.CurrentDemoPage.Group.Key
                    nvc(DemoQueryKeys.FromDemoKey) = Utils.CurrentDemoPage.Key
                End If

                Dim currentViewer = ViewerSelectorState.GetCurrentViewer()
                If currentViewer <> ViewerSelectorState.DefaultViewer Then
                    nvc(ViewerSelectorState.Key) = currentViewer
                End If

'#pragma warning disable DX0025 ' not a path (and no traversal here)
                Return $"~/WebSpecificFeatures/ReportDesigner.aspx?{nvc}"
'#pragma warning restore DX0025
            End Get
            Set(ByVal value As String)
                routeUrl_Renamed = value
            End Set
        End Property
    End Class
End Namespace
