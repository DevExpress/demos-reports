<%@ Page Language="vb" MasterPageFile="~/Site.master" AutoEventWireup="true"
    Inherits="DevExpress.Web.Demos.Interaction_VehicleInspection" Codebehind="VehicleInspection.aspx.vb" %>
<asp:Content ID="Content2" ContentPlaceHolderID="CustomHeadHolder" runat="Server">
    <link rel="stylesheet" type="text/css" href="<%=Me.GetUrlWithVersionSuffix("~/Content/RunDesignerButton.css")%>" />
    <link rel="stylesheet" type="text/css" href="<%=Me.GetUrlWithVersionSuffix("~/Content/ViewerSelector.css")%>" />
    <script type="text/javascript">
        function EditingFieldsHighlightingEnable(s, e) {
            e.reportPreview.editingFieldsHighlighted = true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="TopRightMenuRegion" runat="Server">
    <dx:ReportControlToggleButton ID="showDesignerButton" runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHolder" runat="Server">
    <script type="text/javascript" src="<%=Me.GetUrlWithVersionSuffix("~/Scripts/Designer.js")%>"></script>
    <script>
        function onInit(s, e) {
            RegisterDamageDiagramEditor();
        }
    </script>
    <dx:ASPxWebDocumentViewer runat="server" ID="webDocumentViewer" ClientInstanceName="webDocumentViewer" EnableViewState="False" >
        <ClientSideEvents Init="onInit" BeforeRender="EditingFieldsHighlightingEnable" />
    </dx:ASPxWebDocumentViewer>
</asp:Content>