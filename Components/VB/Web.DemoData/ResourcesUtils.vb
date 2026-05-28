'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Reflection
Imports System.Text
Namespace DevExpress.Web.Demos
    Public NotInheritable Class ResourcesUtils

        Private Sub New()
        End Sub

        Private Const ResourcePrefix As String = "DevExpress.Web.Demos."
        Public Shared Function GetEmbeddedResource(ByVal resourceName As String) As Stream
            Dim assembly = GetCurrentAssembly()
            Return assembly.GetManifestResourceStream(GetResourceName(resourceName))
        End Function
        Private Shared Function GetResourceName(ByVal resourceName As String) As String
            Return GetCurrentAssembly().GetManifestResourceNames().FirstOrDefault(Function(x) x.EndsWith(resourceName))
        End Function
        Private Shared Function GetCurrentAssembly() As System.Reflection.Assembly
            Return System.Reflection.Assembly.GetExecutingAssembly()
        End Function
    End Class
End Namespace
