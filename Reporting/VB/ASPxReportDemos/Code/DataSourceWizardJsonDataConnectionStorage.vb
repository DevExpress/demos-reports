Imports DevExpress.DataAccess.Json
Imports DevExpress.DataAccess.Web
Imports DevExpress.DataAccess.Wizard.Services
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web

Namespace DevExpress.Web.Demos
    Public Class CustomDataSourceWizardJsonDataConnectionStorage
        Implements IDataSourceWizardJsonConnectionStorage

        Public Const JsonDataConnectionsKey As String = "dxJsonDataConnections"

        Public ReadOnly Property Connections() As Dictionary(Of String, JsonDataConnection)
            Get
                If HttpContext.Current Is Nothing OrElse HttpContext.Current.Session Is Nothing Then
                    Return Nothing
                ElseIf HttpContext.Current.Session(JsonDataConnectionsKey) Is Nothing Then
                    HttpContext.Current.Session(JsonDataConnectionsKey) = GetDefaults()
                End If
                Return TryCast(HttpContext.Current.Session(JsonDataConnectionsKey), Dictionary(Of String, JsonDataConnection))
            End Get
        End Property

        Private ReadOnly Property IJsonConnectionStorageService_CanSaveConnection() As Boolean Implements IJsonConnectionStorageService.CanSaveConnection
            Get
                Return HttpContext.Current IsNot Nothing AndAlso HttpContext.Current.Session IsNot Nothing
            End Get
        End Property
        Private Function IJsonConnectionStorageService_ContainsConnection(ByVal connectionName As String) As Boolean Implements IJsonConnectionStorageService.ContainsConnection
            Return If(Connections Is Nothing, False, Connections.ContainsKey(connectionName))
        End Function

        Private Function IJsonConnectionStorageService_GetConnections() As IEnumerable(Of JsonDataConnection) Implements IJsonConnectionStorageService.GetConnections
            If Connections Is Nothing Then
                Return New List(Of JsonDataConnection)()
            End If
            Return Connections.Select(Function(x) x.Value)

        End Function

        Private Function IJsonDataConnectionProviderService_GetJsonDataConnection(ByVal name As String) As JsonDataConnection Implements IJsonDataConnectionProviderService.GetJsonDataConnection
            If Connections Is Nothing OrElse (Not Connections.ContainsKey(name)) Then
                Throw New InvalidOperationException()
            End If
            Return Connections(name)
        End Function

        Private Sub IJsonConnectionStorageService_SaveConnection(ByVal connectionName As String, ByVal dataConnection As JsonDataConnection, ByVal saveCredentials As Boolean) Implements IJsonConnectionStorageService.SaveConnection
            If Connections Is Nothing Then
                Return
            End If
            dataConnection.Name = connectionName
            dataConnection.StoreConnectionNameOnly = True
            Connections(connectionName) = dataConnection
        End Sub

        Private Function GetDefaults() As Dictionary(Of String, JsonDataConnection)

            Dim connections_Renamed = New Dictionary(Of String, JsonDataConnection)()
            Dim dataConnecion = New JsonDataConnection("Uri=" & System.IO.Path.Combine(Helper.AppData_Path, "nwind.json")) With {.Name = "Products (JSON)", .StoreConnectionNameOnly = True}
            connections_Renamed.Add("Products (JSON)", dataConnecion)
            Return connections_Renamed
        End Function
    End Class
End Namespace
