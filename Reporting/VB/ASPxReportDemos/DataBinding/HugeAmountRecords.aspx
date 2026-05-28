<%@ Page Language="vb" MasterPageFile="~/Site.master" AutoEventWireup="true"
    Inherits="DevExpress.Web.Demos.DataBinding_HugeAmountRecords" Codebehind="HugeAmountRecords.aspx.vb" %>
<asp:Content ID="Content2" ContentPlaceHolderID="CustomHeadHolder" runat="Server">
    <link rel="stylesheet" type="text/css" href="<%=Me.GetUrlWithVersionSuffix("~/Content/RunDesignerButton.css")%>" />
    <link rel="stylesheet" type="text/css" href="<%=Me.GetUrlWithVersionSuffix("~/Content/ViewerSelector.css")%>" />
    <script>
        function CustomizeExportOptions(s, e) {
            ['XLS', 'XLSX', 'Text', 'MHT', 'HTML', 'Image', 'DOCX', 'CSV', 'RTF'].forEach(function(format) { e.HideFormat(DevExpress.Reporting.Viewer.ExportFormatID[format]); });
        }
        function CustomizeViewerMenuActions(s, e) {
            DevExpress.Reporting.Viewer.Settings.EditablePreviewEnabled(false);
            UsePdfExportOnly(s, e);
        }
        function UsePdfExportOnly(s, e) {
            var exportAction = e.GetById(DevExpress.Reporting.Viewer.ActionId.ExportTo);
            if(!exportAction)
                return;
            exportAction.imageTemplateName = "dxrd-svg-preview-export-export-to-pdf";
            exportAction.templateName = undefined;
            exportAction.clickAction = function(_args) {
                s.ExportTo('pdf');
            }
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
    <dx:ASPxWebDocumentViewer runat="server" ID="webDocumentViewer" ClientInstanceName="webDocumentViewer" EnableViewState="False">
        <ClientSideEvents CustomizeMenuActions="CustomizeViewerMenuActions" CustomizeExportOptions="CustomizeExportOptions"></ClientSideEvents>
    </dx:ASPxWebDocumentViewer>
    <%
    ElseIf IsMobileViewer Then
    %>
    <dx:MobileEmulator ID="mobileEmulator" runat="server" />
    <%
    End If
    %>
</asp:Content>