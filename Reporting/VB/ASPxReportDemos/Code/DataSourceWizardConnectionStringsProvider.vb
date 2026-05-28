Imports System.Linq
Imports System.Configuration
Imports System.Collections.Generic
Imports DevExpress.DataAccess.Web
Imports DevExpress.DataAccess.ConnectionParameters
Imports DevExpress.DataAccess.Json

Namespace DevExpress.Web.Demos
    Public Class DataSourceWizardConnectionStringsProvider
        Implements IDataSourceWizardConnectionStringsProvider

        Private connectionStrings As Dictionary(Of String, String)
        Public Sub New()
            connectionStrings = New Dictionary(Of String, String)()
            connectionStrings.Add("Homes", ConfigurationManager.ConnectionStrings("HomesConnectionString").ConnectionString)
            connectionStrings.Add("Contacts", ConfigurationManager.ConnectionStrings("ContactsConnectionString").ConnectionString)
            connectionStrings.Add("Northwind", ConfigurationManager.ConnectionStrings("NWindConnectionString").ConnectionString)
            connectionStrings.Add("Vehicles", ConfigurationManager.ConnectionStrings("VehiclesDBConnectionString").ConnectionString)
        End Sub
        Private Function IDataSourceWizardConnectionStringsProvider_GetConnectionDescriptions() As Dictionary(Of String, String) Implements IDataSourceWizardConnectionStringsProvider.GetConnectionDescriptions
            Return connectionStrings.ToDictionary(Function(k) k.Key, Function(k) k.Key)
        End Function
        Private Function IDataSourceWizardConnectionStringsProvider_GetDataConnectionParameters(ByVal name As String) As DataConnectionParametersBase Implements IDataSourceWizardConnectionStringsProvider.GetDataConnectionParameters
            Return New CustomStringConnectionParameters(connectionStrings(name))
        End Function
    End Class

    Public Class JsonDataSourceWizardConnectionStringsProvider ': IDataSourceWizardJsonDataConnectionProvider {
        Private connectionStrings As Dictionary(Of String, JsonDataConnection)

        Public Sub New()
            connectionStrings = New Dictionary(Of String, JsonDataConnection)()
            Dim uri = New System.Uri("/App_Data/nwind.json", System.UriKind.Relative)
            Dim dataConnecion = New JsonDataConnection(New UriJsonSource(uri)) With {.Name = "Products (JSON)", .StoreConnectionNameOnly = True}
            connectionStrings.Add("Products", dataConnecion)
        End Sub

        Private Function GetConnectionDescriptions() As Dictionary(Of String, String)
            Return connectionStrings.ToDictionary(Function(x) x.Key, Function(x) x.Key)
        End Function

        Private Function GetJsonDataConnection(ByVal name As String) As JsonDataConnection
            Return connectionStrings(name)
        End Function
    End Class
End Namespace
