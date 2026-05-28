Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Web
Imports System.Web.SessionState
Imports System.Web.UI
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraReports.Web.Extensions

Namespace DevExpress.Web.Demos
    Public Class DemoReportStorageExtension
        Inherits ReportStorageWebExtension

        Public Shared ReportStorageExtension As New DemoReportStorageExtension()
        Public Overrides Function CanSetData(ByVal url As String) As Boolean
            Return True
        End Function
        Public Overrides Function GetUrls() As Dictionary(Of String, String)
            Return DirectCast(HttpContext.Current.Session("urls"), Dictionary(Of String, String))
        End Function
        Public Overrides Function GetData(ByVal url As String) As Byte()
            Using ms As New MemoryStream()
                Dim report As XtraReport = ReportStorageHelper.LoadReport(url, HttpContext.Current.Session)
                If report Is Nothing Then
                    report = New XtraReport()
                End If
                report.SaveLayoutToXml(ms)
                Return ms.ToArray()
            End Using
        End Function

        Public Overrides Function IsValidUrl(ByVal url As String) As Boolean
            Return True
        End Function

        Public Overrides Sub SetData(ByVal report As XtraReport, ByVal url As String)
            Using ms As New MemoryStream()
                report.SaveLayoutToXml(ms)
                ReportStorageHelper.SaveReportLayout(url, ms.ToArray(), HttpContext.Current.Session)

            End Using
        End Sub

        Public Overrides Function SetNewData(ByVal report As XtraReport, ByVal defaultUrl As String) As String
            Dim url = DirectCast(HttpContext.Current.Session("urls"), Dictionary(Of String, String)).FirstOrDefault(Function(x) x.Value = defaultUrl)
            If String.IsNullOrEmpty(url.Key) Then
                DirectCast(HttpContext.Current.Session("urls"), Dictionary(Of String, String)).Add(defaultUrl, defaultUrl)
            Else
                defaultUrl = url.Key
            End If
            SetData(report, defaultUrl)
            Return defaultUrl
        End Function
    End Class
End Namespace
