Imports DevExpress.DataAccess.Json
Imports DevExpress.DataAccess.Web
Imports System.Collections.Generic
Imports System.Web


Namespace DevExpress.Web.Demos
    Public Class CustomJsonDataConnectionProviderFactory
        Implements IJsonDataConnectionProviderFactory

        Private connections As Dictionary(Of String, JsonDataConnection)
        Public Sub New()
            connections = If(HttpContext.Current Is Nothing OrElse HttpContext.Current.Session Is Nothing, Nothing, TryCast(HttpContext.Current.Session(CustomDataSourceWizardJsonDataConnectionStorage.JsonDataConnectionsKey), Dictionary(Of String, JsonDataConnection)))
        End Sub

        Private Function IJsonDataConnectionProviderFactory_Create() As IJsonDataConnectionProviderService Implements IJsonDataConnectionProviderFactory.Create
            Return New WebDocumentViewerJsonDataConnectionProvider(connections)
        End Function
    End Class

    Public Class WebDocumentViewerJsonDataConnectionProvider
        Implements IJsonDataConnectionProviderService

        Private ReadOnly jsonDataConnections As Dictionary(Of String, JsonDataConnection)
        Public Sub New(ByVal jsonDataConnections As Dictionary(Of String, JsonDataConnection))
            Me.jsonDataConnections = jsonDataConnections
        End Sub

        Private Function IJsonDataConnectionProviderService_GetJsonDataConnection(ByVal name As String) As JsonDataConnection Implements IJsonDataConnectionProviderService.GetJsonDataConnection
            Return If(jsonDataConnections Is Nothing, Nothing, jsonDataConnections(name))
        End Function
    End Class
End Namespace
