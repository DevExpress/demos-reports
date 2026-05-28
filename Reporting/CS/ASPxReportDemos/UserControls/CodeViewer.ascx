<%@ Control Language="C#" AutoEventWireup="true" Inherits="DevExpress.Web.Demos.UserControls_CodeViewer" Codebehind="CodeViewer.ascx.cs" %>
<div class="code">
    <div class="copyContainer">
        <dx:ASPxHint runat="server" ID="copyHint" TargetSelector=".copyContainer a" Content="Copy to clipboard" Width="125px" Position="Bottom" />
            <dx:ASPxHyperLink runat="server" ID="hpCopyCode" ClientInstanceName="hyperLinkCopyCode" EnableClientSideAPI="true" Text="Copy" NavigateUrl="javascript:;" CssClass="copyLink">
                <ClientSideEvents Click="DXDemo.HightlightedCode.CopyPage" />
            </dx:ASPxHyperLink>
        </div>
    <div id="CodeBlock<%= Index %>" class="<%= GetClassName() %>"
        <% if(DisableTextWrap) { %>
        style="width: 920px; white-space: nowrap;" <% } %>>
        <%= SourceCodePage.Code %>
    </div>
</div>
