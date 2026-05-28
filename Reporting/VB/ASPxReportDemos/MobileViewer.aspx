<%@ Page Language="vb" AutoEventWireup="true" Inherits="DevExpress.Web.Demos.MobileViewer" Codebehind="MobileViewer.aspx.vb" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=0" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:PlaceHolder runat="server">
            <link rel="stylesheet" type="text/css" href="<%=Me.GetUrlWithVersionSuffix("~/Content/Designer.css")%>" />
            <script type="text/javascript" src="<%=Me.GetUrlWithVersionSuffix("~/Scripts/Designer.js")%>"></script>
            <script type="text/javascript">
                function OnInit(s, e) {
                    RegisterNationalityEditor("<%=Page.ResolveClientUrl("~/Content/")%>");
                    RegisterDamageDiagramEditor();
                }
            </script>
            <dx:ASPxWebDocumentViewer runat="server" CssClass="fullscreen" Height="100%" MobileMode="true" ID="webDocumentViewer" EnableViewState="False">
                <ClientSideEvents init="OnInit" />
            </dx:ASPxWebDocumentViewer>                
        </asp:PlaceHolder>
    </form>
</body>
</html>