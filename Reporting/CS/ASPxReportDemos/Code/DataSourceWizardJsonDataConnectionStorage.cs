using DevExpress.DataAccess.Json;
using DevExpress.DataAccess.Web;
using DevExpress.DataAccess.Wizard.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevExpress.Web.Demos {
    public class CustomDataSourceWizardJsonDataConnectionStorage : IDataSourceWizardJsonConnectionStorage
    {
        public const string JsonDataConnectionsKey = "dxJsonDataConnections";

        public Dictionary<string, JsonDataConnection> Connections
        {
            get {
                if(HttpContext.Current == null || HttpContext.Current.Session == null) {
                    return null;
                } else if(HttpContext.Current.Session[JsonDataConnectionsKey] == null) {
                    HttpContext.Current.Session[JsonDataConnectionsKey] = GetDefaults();
                }
                return HttpContext.Current.Session[JsonDataConnectionsKey] as Dictionary<string, JsonDataConnection>;
            }
        }

        bool IJsonConnectionStorageService.CanSaveConnection { get { return HttpContext.Current != null && HttpContext.Current.Session != null; } }
        bool IJsonConnectionStorageService.ContainsConnection(string connectionName)
        {
            return Connections == null ? false : Connections.ContainsKey(connectionName);
        }

        IEnumerable<JsonDataConnection> IJsonConnectionStorageService.GetConnections()
        {
            if (Connections == null)
            {
                return new List<JsonDataConnection>();
            }
            return Connections.Select(x => x.Value);

        }

        JsonDataConnection IJsonDataConnectionProviderService.GetJsonDataConnection(string name)
        {
            if (Connections == null || !Connections.ContainsKey(name))
                throw new InvalidOperationException();
            return Connections[name];
        }

        void IJsonConnectionStorageService.SaveConnection(string connectionName, JsonDataConnection dataConnection, bool saveCredentials)
        {
            if (Connections == null)
            {
                return;
            }
            dataConnection.Name = connectionName;
            dataConnection.StoreConnectionNameOnly = true;
            Connections[connectionName] = dataConnection;
        }

        Dictionary<string, JsonDataConnection> GetDefaults()
        {
            var connections = new Dictionary<string, JsonDataConnection>();
            var dataConnecion = new JsonDataConnection("Uri=" + System.IO.Path.Combine(Helper.AppData_Path, "nwind.json"))
            {
                Name = "Products (JSON)",
                StoreConnectionNameOnly = true
            };
            connections.Add("Products (JSON)", dataConnecion);
            return connections;
        }
    }
}
