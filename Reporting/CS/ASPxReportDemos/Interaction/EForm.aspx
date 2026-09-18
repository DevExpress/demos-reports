<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    Inherits="DevExpress.Web.Demos.Interaction_EForm" Codebehind="EForm.aspx.cs" %>
<asp:Content ID="Content2" ContentPlaceHolderID="CustomHeadHolder" runat="Server">
    <link rel="stylesheet" type="text/css" href="<%= this.GetUrlWithVersionSuffix("~/Content/RunDesignerButton.css") %>" />
    <link rel="stylesheet" type="text/css" href="<%= this.GetUrlWithVersionSuffix("~/Content/ViewerSelector.css") %>" />
    <script type="text/javascript" src="<%= this.GetUrlWithVersionSuffix("~/Scripts/Designer.js") %>"></script>
    <script type="text/javascript">
        function EditingFieldsHighlightingEnable(s, e) {
            e.reportPreview.editingFieldsHighlighted = true;
        }
        function onInit(s, e) {
            RegisterNationalityEditor("<%= Page.ResolveClientUrl("~/Content/") %>");
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="TopRightMenuRegion" runat="Server">
    <dx:ReportControlToggleButton ID="showDesignerButton" runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHolder" runat="Server">
    <dx:ASPxWebDocumentViewer runat="server" ID="webDocumentViewer" ClientInstanceName="webDocumentViewer" EnableViewState="False" Height="500px" >
        <ClientSideEvents Init="onInit" BeforeRender="EditingFieldsHighlightingEnable" />
    </dx:ASPxWebDocumentViewer>
</asp:Content>
