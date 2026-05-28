<%@ Page Language="vb" MasterPageFile="~/Site.master" AutoEventWireup="true"
    Inherits="DevExpress.Web.Demos.Interaction_EForm" Codebehind="EForm.aspx.vb" %>
<asp:Content ID="Content2" ContentPlaceHolderID="CustomHeadHolder" runat="Server">
    <link rel="stylesheet" type="text/css" href="<%=Me.GetUrlWithVersionSuffix("~/Content/RunDesignerButton.css")%>" />
    <link rel="stylesheet" type="text/css" href="<%=Me.GetUrlWithVersionSuffix("~/Content/ViewerSelector.css")%>" />
    <script type="text/javascript" src="<%=Me.GetUrlWithVersionSuffix("~/Scripts/Designer.js")%>"></script>
    <script type="text/javascript">
        function EditingFieldsHighlightingEnable(s, e) {
            e.reportPreview.editingFieldsHighlighted = true;
        }
        function onInit(s, e) {
            RegisterNationalityEditor("<%=Page.ResolveClientUrl("~/Content/")%>");
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="TopRightMenuRegion" runat="Server">
    <dx:ViewerSelector ID="viewerSelector" runat="server" />
    <dx:ReportControlToggleButton ID="showDesignerButton" runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHolder" runat="Server">
    <%
    If IsHTML5Viewer Then
    %>
    <dx:ASPxWebDocumentViewer runat="server" ID="webDocumentViewer" ClientInstanceName="webDocumentViewer" EnableViewState="False" Height="500px" >
        <ClientSideEvents Init="onInit" BeforeRender="EditingFieldsHighlightingEnable" />
    </dx:ASPxWebDocumentViewer>
    <%
    ElseIf IsMobileViewer Then
    %>
    <dx:MobileEmulator ID="mobileEmulator" runat="server" />
    <%
    End If
    %>
</asp:Content>