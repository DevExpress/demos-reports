Imports System
Imports System.Linq
Imports System.Web
Imports DevExpress.Security

Namespace DevExpress.Web.Demos
    Public NotInheritable Class Helper

        Private Sub New()
        End Sub

        Public Shared AppData_Path As String = ""
        '
        Public Shared Function GetRelativePath(ByVal name As String) As String
            Dim request = HttpContext.Current.Request
            Dim virtualPath As String = VirtualPathUtility.Combine("~/App_Data/", name)
            Dim appDataRoot As String = request.MapPath("~/App_Data")
            Dim appDataRelativePath As String = request.MapPath(virtualPath)
            If Not SafePath.IsRooted(appDataRoot, appDataRelativePath) Then
                Throw New UnauthorizedAccessException("Access outside App_Data is not allowed.")
            End If
            Return appDataRelativePath
        End Function

        Public Shared Function GetDemoLocalUrl(ByVal demoProductModel As DemoProductModel, ByVal groupName As String, ByVal demoName As String) As String
            If demoProductModel Is Nothing OrElse String.IsNullOrEmpty(demoName) OrElse String.IsNullOrEmpty(groupName) Then
                Return Nothing
            End If
            Dim demo = demoProductModel.Groups.FirstOrDefault(Function(x) x.Key = groupName) ?.Demos.FirstOrDefault(Function(x) x.Key = demoName)
            If demo Is Nothing Then
                Return Nothing
            End If
            Return Utils.GenerateDemoUrl(demo)
        End Function
    End Class
End Namespace
