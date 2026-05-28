<%@ Control Language="C#" AutoEventWireup="true" Inherits="DevExpress.Web.Demos.UserControls_MobileEmulator" Codebehind="MobileEmulator.ascx.cs" %>
<link rel="stylesheet" type="text/css" href="<%= this.GetUrlWithVersionSuffix("~/Content/MobileEmulator.css") %>" />
<div class="<%: IsLandscape ? "main landscape" : "main" %>">
    <div class="<%: IsLandscape ? "ios-image landscape" : "ios-image" %>"></div>
    <iframe name="ios" id="ios" class="<%: IsLandscape ? "landscape" : "" %>" src="<%: Page.ResolveUrl(Url) %>"></iframe>
    <a href="<%: Page.ResolveUrl(Url) %>">
        <div class="<%: IsLandscape ? "QRCode landscape" : "QRCode" %>">
            <img src='<%: this.GetUrlWithVersionSuffix(QRCodeUrl) %>' alt="QRCode" />
            <div>View on your device</div>
        </div>
    </a>
</div>
