<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    Inherits="DevExpress.Web.Demos.ReportTypes_MultiColumn" Codebehind="MultiColumn.aspx.cs" %>
<asp:Content ID="Content2" ContentPlaceHolderID="CustomHeadHolder" runat="Server">
    <link rel="stylesheet" type="text/css" href="<%= this.GetUrlWithVersionSuffix("~/Content/RunDesignerButton.css") %>" />
    <link rel="stylesheet" type="text/css" href="<%= this.GetUrlWithVersionSuffix("~/Content/ViewerSelector.css") %>" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="TopRightMenuRegion" runat="Server">
    <dx:ReportControlToggleButton ID="showDesignerButton" runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHolder" runat="Server">
    <dx:ASPxWebDocumentViewer runat="server" ID="webDocumentViewer" ClientInstanceName="webDocumentViewer" EnableViewState="False" />
</asp:Content>
