<%@ Page Language="vb" MasterPageFile="~/Site.master" AutoEventWireup="true"
    Inherits="DevExpress.Web.Demos.WebSpecificFeatures_ReportDesigner" Codebehind="ReportDesigner.aspx.vb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CustomHeadHolder" runat="Server">
    <link rel="stylesheet" type="text/css" href="<%=Me.GetUrlWithVersionSuffix("~/Content/Designer.css")%>" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="Server">
    <script type="text/javascript" src="<%=Me.GetUrlWithVersionSuffix("~/Scripts/Designer.js")%>"></script>
    <script>
        function onInit(s, e) {
            onDesignerInit(s, e);
            RegisterNationalityEditor("<%=Page.ResolveClientUrl("~/Content/")%>");
            RegisterDamageDiagramEditor();
        }
    </script>
    <div style="border: 1px solid #cccccc; position: relative; min-height: 900px;" runat="server">
        <dx:ASPxReportDesigner runat="server" ID="reportDesigner"
            ClientInstanceName="reportDesigner" CssClass="fullscreen"
            EnableViewState="False" EnableRichTextEditor="true" Height="" ClientSideEvents-Init="onInit" >
        </dx:ASPxReportDesigner>
        <asp:hiddenField runat="server" id="redirectValue" />
    </div>
</asp:Content>