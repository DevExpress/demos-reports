Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Namespace DevExpress.Web.Demos
    Public MustInherit Class MobileViewerPage(Of TReport)
        Inherits PageBase(Of TReport)

        Public Overrides ReadOnly Property IsMobileViewer() As Boolean
            Get
                Return True
            End Get
        End Property
    End Class
    Partial Public Class WebSpecificFeatures_MobileViewer
        Inherits MobileViewerPage(Of XtraReportsDemos.MasterDetailReport.Report)

    End Class
End Namespace
