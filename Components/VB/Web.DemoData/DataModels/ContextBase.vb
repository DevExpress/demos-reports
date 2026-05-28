'INSTANT VB TODO TASK: There is no equivalent to #line in VB:
Imports System.Configuration
Imports System.Data.Entity
Imports DevExpress.Internal
Namespace DevExpress.Web.Demos
    Partial Public Class ContextBase
        Inherits DbContext

        Public Sub New(ByVal connectionString As String)
            MyBase.New(GetResolvedConnectionString(connectionString))
        End Sub
        Private Shared Function GetResolvedConnectionString(ByVal connectionString As String) As String
            Dim sqlExpressString As String = ConfigurationManager.ConnectionStrings(connectionString).ConnectionString
            Return If(Utils.PatchConnectionStrings, DbEngineDetector.PatchConnectionString(sqlExpressString), sqlExpressString)
        End Function
    End Class
End Namespace
