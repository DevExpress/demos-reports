<%@ Control Language="vb" AutoEventWireup="true" Inherits="DevExpress.Web.Demos.UserControls_ViewerSelectorControl" Codebehind="ViewerSelector.ascx.vb" %>
<a href="javascript:void(0)" class="ViewerSelectorButton" title="Current Viewer: <%=CurrentViewer%>" id="ViewerSelectorPopupButton" style="margin-right: 34px">
    <dx:ASPxImage runat="server" ID="tViewerSelectorPic" SpriteCssClass="Icon ViewerSelectorIcon"></dx:ASPxImage>
    VIEWER
    <dx:ASPxImage runat="server" ID="tViewerSelectorGlyph" SpriteCssClass="Glyph"></dx:ASPxImage>
</a>
<script type="text/javascript" src="<%=Me.GetUrlWithVersionSuffix("~/Scripts/Viewer.js")%>"></script>
<dx:ASPxPopupControl ID="ViewerSelectorPopup" CssClass="ViewerSelectorSelectorPopup ViewerSelectorPopup" runat="server"
    EnableTheming="False" ShowHeader="False" ShowShadow="False" PopupAnimationType="None"
    PopupElementID="ViewerSelectorPopupButton" PopupHorizontalAlign="RightSides" PopupVerticalAlign="TopSides"
    PopupHorizontalOffset="24" PopupVerticalOffset="-19" PopupAlignCorrection="Disabled">
    <ClientSideEvents Init="DXSelectorPopupContainer_Init" />
    <contentcollection>
        <dx:PopupControlContentControl runat="server">
            <div class="ButtonWrapper">
                <div class="ViewerSelectorButtonBorder">
                </div>
            </div>
            <div class="ButtonWrapperBtmBorder"></div>
            <b class="Clear"></b>
            <div class="ContainerWrapper ViewerSelectorContainerWrapper">
                <div class="ViewerSelectorContainer ViewerSelectorContainer">
                    <dx:ASPxMenu ID="ViewerSelectorMenu" CssClass="ViewerSelectorGroupMenu" runat="server" EnableTheming="false"
                        EnableViewState="false" Orientation="Vertical" ItemImagePosition="Left" ItemSpacing="0px">
                        <ItemTextTemplate>
                            <div class="ViewerSelectorIco <%#ViewerSelectorState.GetByName(DirectCast(Eval("Name"), String)).CssClass%>"></div>
                            <%#ViewerSelectorState.GetByName(DirectCast(Eval("Name"), String)).Text%>
                        </ItemTextTemplate>
                    </dx:ASPxMenu>
                    <b class="Clear"></b>
                </div>
            </div>
        </dx:PopupControlContentControl>
    </contentcollection>
</dx:ASPxPopupControl>