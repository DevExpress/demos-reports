<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    Inherits="DevExpress.Web.Demos.Interaction_EmployeePerformanceReview" Codebehind="EmployeePerformanceReview.aspx.cs" %>
<asp:Content ID="Content2" ContentPlaceHolderID="CustomHeadHolder" runat="Server">
    <link rel="stylesheet" type="text/css" href="<%= this.GetUrlWithVersionSuffix("~/Content/RunDesignerButton.css") %>" />
    <link rel="stylesheet" type="text/css" href="<%= this.GetUrlWithVersionSuffix("~/Content/ViewerSelector.css") %>" />
    <script type="text/javascript">
        function EditingFieldsHighlightingEnable(s, e) {
            e.reportPreview.editingFieldsHighlighted = true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="TopRightMenuRegion" runat="Server">
    <dx:ViewerSelector ID="viewerSelector" runat="server" />
    <dx:ReportControlToggleButton ID="showDesignerButton" runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHolder" runat="Server">
    <% if(IsHTML5Viewer) { %>
    <dx:ASPxWebDocumentViewer runat="server" ID="webDocumentViewer"  ClientInstanceName="webDocumentViewer" EnableViewState="False" >
        <ClientSideEvents BeforeRender="EditingFieldsHighlightingEnable" />
    </dx:ASPxWebDocumentViewer>
    <% } else if(IsMobileViewer) { %>
    <dx:MobileEmulator ID="mobileEmulator" runat="server" />
    <% } %>
</asp:Content>
