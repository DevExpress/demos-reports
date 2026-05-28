<%@ Control Language="C#" AutoEventWireup="true" Inherits="DevExpress.Web.Demos.UserControls_FeedbackPanel" Codebehind="FeedbackPanel.ascx.cs" %>
<div class="feedbackPanelContainer" id="feedbackContainerDiv">
    <div id="compactButtonContainer" class="compactButtonContainer collapsedElement">
        <dx:ASPxButton runat="server" ID="btnExpandPanel" AutoPostBack="false" RenderMode="Link" ClientInstanceName="btnExpandPanel">
            <ClientSideEvents Click="onExpandPanelClick" />
            <Image>
                <SpriteProperties CssClass="feedbackImage iconChat" HottrackedCssClass="iconChatHottracked" />
            </Image>
        </dx:ASPxButton>
    </div>
    <dx:ASPxRoundPanel ID="ASPxRoundFeedbackPanel" runat="server" ClientInstanceName="feedbackPanel" ShowCollapseButton="false" EnableTheming="false" Width="375px"
        Collapsed="true" HorizontalAlign="Center" CssClass="feedbackPanel" ClientVisible="false" BackColor="White" EnableAnimation="false">
        <ClientSideEvents Init="onFeedbackPanelInit" />
        <HeaderStyle CssClass="feedbackHeaderPanel" />
        <HeaderContentTemplate>
            <div class="headerContainer">
                <dx:ASPxLabel runat="server" ID="lblFeedbackCaption" Text="Did this demo address your needs?" CssClass="label label-width-265" ClientInstanceName="lblFeedbackCaption" EnableTheming="false"></dx:ASPxLabel>
                <div class="buttonsContainer">
                    <dx:ASPxButton runat="server" ID="btnFeedbackPositive" AutoPostBack="false" RenderMode="Link" ClientInstanceName="btnFeedbackPositive">
                        <ClientSideEvents Click="onPositiveFeedbackClick" />
                        <Image>
                            <SpriteProperties CssClass="feedbackImage thumbUp" HottrackedCssClass="thumbUpHottracked" />
                        </Image>
                    </dx:ASPxButton>
                    <dx:ASPxButton runat="server" ID="btnFeedbackNegative" AutoPostBack="false" RenderMode="Link" ClientInstanceName="btnFeedbackNegative">
                        <ClientSideEvents Click="onNegativeFeedbackClick" />
                        <Image>
                            <SpriteProperties CssClass="feedbackImage thumbDown" HottrackedCssClass="thumbDownHottracked" />
                        </Image>
                    </dx:ASPxButton>
                    <dx:ASPxButton runat="server" ID="btnFeedbackClose" AutoPostBack="false" RenderMode="Link" ClientInstanceName="btnFeedbackClose">
                        <ClientSideEvents Click="onCloseFeedbackClick" />
                        <Image>
                            <SpriteProperties CssClass="feedbackImage iconClose" HottrackedCssClass="iconCloseHottracked" />
                        </Image>
                    </dx:ASPxButton>
                </div>
            </div>
        </HeaderContentTemplate>
        <PanelCollection>
            <dx:PanelContent runat="server">
                <dx:ASPxPageControl runat="server" ID="mainContentPanel" Width="100%" ClientInstanceName="pageControlfeedbackContent" ShowTabs="false">
                    <Paddings Padding="0" />
                    <ContentStyle CssClass="feedbackContentPage" />
                    <TabPages>
                        <dx:TabPage Name="pagePositiveFeedback">
                            <ContentCollection>
                                <dx:ContentControl runat="server">
                                    <span>If you’d like us to extend this demo further, please describe your needs below.</span>
                                    <br />
                                    <span>Thank you in advance for your cooperation.</span>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Name="pageNegativeFeedback">
                            <ContentCollection>
                                <dx:ContentControl runat="server">
                                    <span>Please describe your use-case below and we’ll be happy to extend this demo to better serve your needs.</span>
                                    <br />
                                    <br />
                                    <span>If you need immediate help, please submit a support ticket</span>
                                    <br />
                                    <dx:ASPxHyperLink runat="server" ID="linkSupportCenter" OnInit="linkSupportCenter_Init" CssClass="feedbackSClink" />
                                    <br />
                                    <span>We are here to help.</span>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                    </TabPages>
                </dx:ASPxPageControl>
                <dx:ASPxMemo ID="memoFeedback" ClientInstanceName="memoFeedbackText" runat="server" Width="100%" Height="100px" CssClass="feedbackText" EnableTheming="false">
                    <ClientSideEvents UserInput="onFeedbackTextInput" Init="onFeedbackTextInit" />
                </dx:ASPxMemo>
                <div>
                    <dx:ASPxButton runat="server" ID="btnSendFeedback" AutoPostBack="false" UseSubmitBehavior="false" Text="SEND FEEDBACK" ClientInstanceName="btnSendFeedback" CssClass="btnSendFeedback"
                        ClientEnabled="false" EnableTheming="false" Width="100%" Height="32px">
                        <ClientSideEvents Click="onSendFeedbackClick" />
                    </dx:ASPxButton>
                </div>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxRoundPanel>
</div>                       
