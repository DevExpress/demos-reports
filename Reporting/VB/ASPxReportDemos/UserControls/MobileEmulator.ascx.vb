Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

Namespace DevExpress.Web.Demos
    Partial Public Class UserControls_MobileEmulator
        Inherits System.Web.UI.UserControl
        Implements IMobileEmulator

        Public ReadOnly Property IsLandscape() As Boolean
            Get
                Return ReportTypeName = "XtraReportsDemos.CharacterComb.Report" OrElse ReportTypeName = "XtraReportsDemos.PivotGridAndChart.Report"
            End Get
        End Property
        Public Property ReportTypeName() As String Implements IMobileEmulator.ReportTypeName
        Public ReadOnly Property Url() As String
            Get
'#pragma warning disable DX0025 ' not a path (and no traversal here)
                Return $"~/MobileViewer.aspx?{DemoQueryKeys.ReportTypeNameKey}={HttpUtility.UrlEncode(ReportTypeName)}"
'#pragma warning restore DX0025
            End Get
        End Property
        Public ReadOnly Property QRCodeUrl() As String
            Get
'#pragma warning disable DX0025 ' not a path (and no traversal here)
                Return "~/Content/QRCode.png"
'#pragma warning restore DX0025
            End Get
        End Property
    End Class
End Namespace
