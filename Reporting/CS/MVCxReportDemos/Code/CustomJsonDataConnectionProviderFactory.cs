using DevExpress.DataAccess.Json;
using DevExpress.DataAccess.Web;
using System.Collections.Generic;
using System.Web;


public class CustomJsonDataConnectionProviderFactory : IJsonDataConnectionProviderFactory
{

    Dictionary<string, JsonDataConnection> connections;
    public CustomJsonDataConnectionProviderFactory()
    {
        connections = HttpContext.Current == null || HttpContext.Current.Session == null ? null : HttpContext.Current.Session[CustomDataSourceWizardJsonDataConnectionStorage.JsonDataConnectionsKey] as Dictionary<string, JsonDataConnection>;
    }

    public IJsonDataConnectionProviderService Create()
    {
        return new WebDocumentViewerJsonDataConnectionProvider(connections);
    }
}

public class WebDocumentViewerJsonDataConnectionProvider : IJsonDataConnectionProviderService
{
    readonly Dictionary<string, JsonDataConnection> jsonDataConnections;
    public WebDocumentViewerJsonDataConnectionProvider(Dictionary<string, JsonDataConnection> jsonDataConnections)
    {
        this.jsonDataConnections = jsonDataConnections;
    }
    public JsonDataConnection GetJsonDataConnection(string name)
    {
        return jsonDataConnections == null ? null : jsonDataConnections[name];
    }
}
