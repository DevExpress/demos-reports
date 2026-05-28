'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports DevExpress.Web.Internal
Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Web
Imports System.Web.UI
Imports DevExpress.Web
Namespace DevExpress.Web.Demos
    Public Class UnsupportedBrowserHttpModule
        Implements IHttpModule

        Private Const PageContent As String = "" & ControlChars.CrLf & _
"<!DOCTYPE html>" & ControlChars.CrLf & _
"<html xmlns=""http://www.w3.org/1999/xhtml"">" & ControlChars.CrLf & _
"<head>" & ControlChars.CrLf & _
"    <style>" & ControlChars.CrLf & _
"        body {" & ControlChars.CrLf & _
"            font: 16px ""Segoe UI"", ""Helvetica Neue"", Helvetica, ""Droid Sans"", Tahoma, Geneva, sans-serif;" & ControlChars.CrLf & _
"            color: #333333;" & ControlChars.CrLf & _
"            background-color: #fff;" & ControlChars.CrLf & _
"            padding: 48px 8px 8px;" & ControlChars.CrLf & _
"        }" & ControlChars.CrLf & _
"        .container {" & ControlChars.CrLf & _
"            margin: 0 auto;" & ControlChars.CrLf & _
"            max-width: 800px;" & ControlChars.CrLf & _
"            position: relative;" & ControlChars.CrLf & _
"            width: 100%;" & ControlChars.CrLf & _
"        }" & ControlChars.CrLf & _
"        .container .image {" & ControlChars.CrLf & _
"            position: absolute;" & ControlChars.CrLf & _
"            width: 32px;" & ControlChars.CrLf & _
"            padding: 8px 0;" & ControlChars.CrLf & _
"        }" & ControlChars.CrLf & _
"        .container .text {" & ControlChars.CrLf & _
"            padding-left: 44px;" & ControlChars.CrLf & _
"        }" & ControlChars.CrLf & _
"        .container .text > h1 {" & ControlChars.CrLf & _
"            font-weight: 600;" & ControlChars.CrLf & _
"            margin: 0;" & ControlChars.CrLf & _
"        }" & ControlChars.CrLf & _
"        .container .text > p {" & ControlChars.CrLf & _
"            margin: 12px 0;" & ControlChars.CrLf & _
"        }" & ControlChars.CrLf & _
"    </style>" & ControlChars.CrLf & _
"</head>" & ControlChars.CrLf & _
"<body>" & ControlChars.CrLf & _
"    <div class=""container"">" & ControlChars.CrLf & _
"        <div class=""image"">" & ControlChars.CrLf & _
"            <svg xmlns=""http://www.w3.org/2000/svg"" fill=""currentColor"" viewBox=""0 0 18 18"" width=""32px"" height=""32px""><path d=""M 8 7 h 2 V 5 H 8 M 9 16 c -4 0 -7 -3 -7 -7 s 3 -7 7 -7 s 7 3 7 7 S 13 16 9 16 M 9 0 C 4 0 0 4 0 9 c 0 5 4 9 9 9 c 5 0 9 -4 9 -9 C 18 4 14 0 9 0 M 8 13 h 2 V 8 H 8 V 13 Z"" /></svg>" & ControlChars.CrLf & _
"        </div>" & ControlChars.CrLf & _
"        <div class=""text"">" & ControlChars.CrLf & _
"            <h1>Your browser is not supported.</h1>" & ControlChars.CrLf & _
"            <p>Please open this webpage with a <a href=""https://www.devexpress.com/support/versions.xml"">supported web browser</a> to run DevExpress ASP.NET Component Demos.</p>" & ControlChars.CrLf & _
"        </div>" & ControlChars.CrLf & _
"    </div>" & ControlChars.CrLf & _
"</body>" & ControlChars.CrLf & _
"</html>"
        Public Sub Dispose() Implements IHttpModule.Dispose
        End Sub
        Public Sub Init(ByVal context As HttpApplication) Implements IHttpModule.Init
            AddHandler context.BeginRequest, AddressOf Context_BeginRequest
        End Sub
        Private Sub Context_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
            Dim userAgent As String = HttpContext.Current.Request.UserAgent
            If (Not String.IsNullOrWhiteSpace(userAgent)) AndAlso (userAgent.Contains("MSIE") OrElse userAgent.Contains("Trident")) Then
                HttpUtils.WriteToResponse(PageContent)
                HttpUtils.EndResponse()
            End If
        End Sub
    End Class
End Namespace
