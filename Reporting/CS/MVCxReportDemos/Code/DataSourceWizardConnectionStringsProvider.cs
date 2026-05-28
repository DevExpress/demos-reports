using System.Linq;
using System.Configuration;
using System.Collections.Generic;
using DevExpress.DataAccess.Web;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Json;
using System.Web;

public class DataSourceWizardConnectionStringsProvider : IDataSourceWizardConnectionStringsProvider {
    Dictionary<string, string> connectionStrings;
    public DataSourceWizardConnectionStringsProvider() {
        connectionStrings = new Dictionary<string, string>();
        connectionStrings.Add("Homes", ConfigurationManager.ConnectionStrings["HomesConnectionString"].ConnectionString);
        connectionStrings.Add("Contacts", ConfigurationManager.ConnectionStrings["ContactsConnectionString"].ConnectionString);
        connectionStrings.Add("Northwind", ConfigurationManager.ConnectionStrings["NWindConnectionString"].ConnectionString);
        connectionStrings.Add("Vehicles", ConfigurationManager.ConnectionStrings["VehiclesDBConnectionString"].ConnectionString);
    }
    Dictionary<string, string> IDataSourceWizardConnectionStringsProvider.GetConnectionDescriptions() {
        return connectionStrings.ToDictionary(k => k.Key, k => k.Key);
    }
    DataConnectionParametersBase IDataSourceWizardConnectionStringsProvider.GetDataConnectionParameters(string name) {
        return new CustomStringConnectionParameters(connectionStrings[name]);
    }
}

public class JsonDataSourceWizardConnectionStringsProvider {//: IDataSourceWizardJsonDataConnectionProvider {
    Dictionary<string, JsonDataConnection> connectionStrings;

    public JsonDataSourceWizardConnectionStringsProvider() {
        connectionStrings = new Dictionary<string, JsonDataConnection>();
        var uri = new System.Uri("/App_Data/nwind.json", System.UriKind.Relative);
        var dataConnecion = new JsonDataConnection(new UriJsonSource(uri)) {
            Name = "Products (JSON)",
            StoreConnectionNameOnly = true
        };
        connectionStrings.Add("Products", dataConnecion);
    }

    public Dictionary<string, string> GetConnectionDescriptions() {
        return connectionStrings.ToDictionary(x => x.Key, x => x.Key);
    }

    public JsonDataConnection GetJsonDataConnection(string name) {
        return connectionStrings[name];
    }
}
