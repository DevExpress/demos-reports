<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="DevExpress.Web.Demos.UserControls_ReportControlToggleButtonControl" Codebehind="ReportControlToggleButton.ascx.cs" %>
<% if(ShowViewerButton) { %>
    <a id="ViewerSelectorPopupButton" class="ViewerSelectorButton" href="<%: Page.ResolveUrl(RouteUrl) %>" title="Show WebDocument Viewer"  style="margin-right: 34px">
        <dx:ASPxImage runat="server" ID="ASPxImage1" SpriteCssClass="Icon ViewerSelectorIcon"></dx:ASPxImage>
        VIEWER
        <dx:ASPxImage runat="server" ID="ASPxImage2" SpriteCssClass="Glyph"></dx:ASPxImage>
    </a>
<% } else { %>
    <a id="run-designer-button" class="ViewerSelectorButton run-designer-button" href="<%: Page.ResolveUrl(RouteUrl) %>"
        title="Customize the report on the client side using the in-browser Report Designer.">
        <img class="run-designer-image" src="<%: this.GetUrlWithVersionSuffix("~/Content/RunDesigner.png") %>" alt="run report designer" />
        REPORT DESIGNER
    </a>
<% } %>
