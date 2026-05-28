<%@ Page Language="vb" MasterPageFile="~/Site.master" AutoEventWireup="true"
    Inherits="DevExpress.Web.Demos.WebSpecificFeatures_ColorSchemeCustomization" Codebehind="ColorSchemeCustomization.aspx.vb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CustomHeadHolder" runat="Server">
    <script type="text/javascript">
        (function() {
            window.LESS_RESOURCE_PATH = "<%=Page.ResolveClientUrl("~/Scripts/Theming/Less_themes/")%>";
            window.ICONS_RESOURCE_PATH = "<%=Page.ResolveClientUrl("~/Content/ThemeIcons/")%>";
        })();
    </script>
    <link rel="stylesheet" type="text/css" href="<%=Me.GetUrlWithVersionSuffix("~/Content/Designer.css")%>" />
    <script type="text/javascript" src="<%=Me.GetUrlWithVersionSuffix("~/Scripts/Theming/less.min.js")%>"></script>
    <script type="text/javascript" src="<%=Me.GetUrlWithVersionSuffix("~/Scripts/Theming/beautify-css.js")%>"></script>
    <script type="text/javascript" src="<%=Me.GetUrlWithVersionSuffix("~/Scripts/Theming/jszip.min.js")%>"></script>
    <script type="text/javascript" src="<%=Me.GetUrlWithVersionSuffix("~/Scripts/Theming/index.js")%>"></script>
    <link rel="stylesheet" type="text/css" href="<%=Me.GetUrlWithVersionSuffix("~/Content/RunDesignerButton.css")%>" />
    <link rel="stylesheet" type="text/css" href="<%=Me.GetUrlWithVersionSuffix("~/Content/ViewerSelector.css")%>" />
    <link rel="stylesheet" type="text/css" href="<%=Me.GetUrlWithVersionSuffix("~/Scripts/Theming/index.css")%>" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="TopRightMenuRegion" runat="Server">
    <dx:ReportControlToggleButton ID="reportControlToggleButton" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="Server">
    <!-- #include file ="~/Scripts/Theming/ThemingTemplates.html" -->
    <div class="report-designer-preview" style="position: relative; min-height: 900px; overflow: hidden; border: 1px solid #cccccc;" runat="server">
        <div id="theming_demo_load_panel"></div>
        <%
        If ShowViewerButton Then
        %>
        <dx:ASPxReportDesigner runat="server" ID="reportDesigner"
            ClientInstanceName="reportDesigner" CssClass="fullscreen"
            EnableViewState="False" Height="" ClientSideEvents-CustomizeElements="customizeDesignerElements" />
        <asp:HiddenField runat="server" />
        <%
        Else
        %>
        <dx:ASPxWebDocumentViewer runat="server" ID="webDocumentViewer" ClientInstanceName="webDocumentViewer" EnableViewState="False"
            ClientSideEvents-CustomizeElements="customizeViewerElements" />
        <%
        End If
        %>
    </div>
    <script type="text/javascript" src="<%=Me.GetUrlWithVersionSuffix("~/Scripts/Theming/loadPanel.js")%>"></script>
</asp:Content>