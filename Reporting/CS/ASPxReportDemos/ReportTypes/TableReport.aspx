<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    Inherits="DevExpress.Web.Demos.ReportTypes_TableReport" Codebehind="TableReport.aspx.cs" %>
<asp:Content ID="Content2" ContentPlaceHolderID="CustomHeadHolder" runat="Server">
    <link rel="stylesheet" type="text/css" href="<%= this.GetUrlWithVersionSuffix("~/Content/RunDesignerButton.css") %>" />
    <link rel="stylesheet" type="text/css" href="<%= this.GetUrlWithVersionSuffix("~/Content/ViewerSelector.css") %>" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="TopRightMenuRegion" runat="Server">
    <dx:ViewerSelector ID="viewerSelector" runat="server" />
    <dx:ReportControlToggleButton ID="showDesignerButton" runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHolder" runat="Server">
    <% if(IsHTML5Viewer) { %>
    <script>
        function initWebDocumentViewer(webDocumentViewer) {
            webDocumentViewer.CustomizeParameterEditors.AddHandler(function(_, e) {
                if(e.parameter.name === 'OrderIdParameter') {
                    e.info.editor = { header: 'param-order-id' };
                } else if(e.parameter.name === 'MaxRowCountParameter') {
                    e.info.editor = { header: 'param-rows-count' };
                }
            });
        }
    </script>
    <dx:ASPxWebDocumentViewer runat="server" ID="webDocumentViewer" ClientInstanceName="webDocumentViewer" EnableViewState="False" ClientSideEvents-Init="initWebDocumentViewer" />
    
    <script type="text/html" id="param-order-id">
        <div data-bind="dxNumberBox: { value: value, showSpinButtons: true, min: 10248, max: 11077 }"></div>
    </script>
    <script type="text/html" id="param-rows-count">
        <div data-bind="dxNumberBox: { value: value, showSpinButtons: true, min: 1, max: 13 }"></div>
    </script>
    <% } else if(IsMobileViewer) { %>
    <dx:MobileEmulator ID="mobileEmulator" runat="server" />
    <% } %>
</asp:Content>
