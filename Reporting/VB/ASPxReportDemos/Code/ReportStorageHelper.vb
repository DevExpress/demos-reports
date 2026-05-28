Imports System.IO
Imports System.Web.SessionState
Imports DevExpress.XtraReports.UI

Namespace DevExpress.Web.Demos
    Public NotInheritable Class ReportStorageHelper

        Private Sub New()
        End Sub

        Public Shared Function LoadReport(ByVal reportName As String, ByVal session As HttpSessionState) As XtraReport
            Dim reportLayout = GetReportLayout(reportName, session)
            If reportLayout IsNot Nothing Then
                Using stream = New MemoryStream(reportLayout)
                    Return XtraReport.FromXmlStream(stream, True)
                End Using
            End If
            Dim demoReportsAssembly = GetType(XtraReportsDemos.MasterDetailReport.Report).Assembly
            Return TryCast(demoReportsAssembly.CreateInstance(reportName), XtraReport)
        End Function

        Public Shared Sub SaveReportLayout(ByVal reportName As String, ByVal reportLayout() As Byte, ByVal session As HttpSessionState)
            session(GetKey(reportName)) = reportLayout
        End Sub

        Private Shared Function GetReportLayout(ByVal reportName As String, ByVal session As HttpSessionState) As Byte()
            Return If(session IsNot Nothing, TryCast(session(GetKey(reportName)), Byte()), Nothing)
        End Function

        Private Shared Function GetKey(ByVal reportName As String) As String
            Const SessionPrefix As String = "XRDesignedReportLayout"
            Return SessionPrefix & reportName
        End Function
    End Class
End Namespace
