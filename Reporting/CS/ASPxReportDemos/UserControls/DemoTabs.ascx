<%@ Control Language="C#" AutoEventWireup="true" Inherits="DevExpress.Web.Demos.UserControls_DemoTabs" Codebehind="DemoTabs.ascx.cs" %>
<script runat="server">
    protected bool IsOpenLocalDemoEnabled {
        get {
            string openLocalDemoEnabledValue = ConfigurationManager.AppSettings["OpenLocalDemo"];
            return openLocalDemoEnabledValue != null && openLocalDemoEnabledValue.Equals("true", StringComparison.InvariantCultureIgnoreCase);
        }
    }
</script>
<dx:ASPxPageControl ID="DemoPageControl" ClientInstanceName="DemoPageControl" CssClass="page-control" runat="server" EnableTheming="false" 
    EnableViewState="false" Width="100%" EnableCallBacks="True" EnableTabScrolling="true" TabAlign="Justify">
    <Paddings Padding="0" />
    <ContentStyle CssClass="tabs-content" />
    <TabStyle CssClass="tab" />
    <ActiveTabStyle CssClass="tab-active" />
    <ClientSideEvents ActiveTabChanged="DXDemo.HightlightedCode.DemoPageControl_OnActiveTabChanged" />
    <TabPages>
        <dx:TabPage Name="Description" Text="Description">
            <ContentCollection>
                <dx:ContentControl>
                    <article class="description-section text-content">
                        <div class="responsive-text">
                            <%= Utils.CurrentDemo.Description %>
                            <% if(Utils.CurrentDemoPage.SeeAlsoLinks.Count > 0) { %>
                            <br />
                            See Also:
                            <ul>
                                <% foreach(var sa in Utils.CurrentDemoPage.SeeAlsoLinks) { %>
                                <li><a href="<%=sa.Url %>"><%=sa.Title %></a></li>
                                <% } %>
                            </ul>
                            <% } %>
                        </div>
                        <% if(IsOpenLocalDemoEnabled && DevExpress.Web.Internal.RenderUtils.Browser.Platform.IsWindows) { %>
                        <div class="localDemoContainer">
                            <dx:ASPxRoundPanel runat="server" ID="panelLocalDemo" ShowCollapseButton="true" ShowHeader="true" EnableTheming="false" Collapsed="true" CssClass="localDemoPanel localDemoPanelContent">
                                <ContentPaddings PaddingLeft="0" />
                                <HeaderStyle CssClass="localDemoPanel localDemoPanelContent" />
                                <ExpandButtonImage>
                                    <SpriteProperties CssClass="headerImage expandButtonImage" />
                                </ExpandButtonImage>
                                <CollapseButtonImage>
                                    <SpriteProperties CssClass="headerImage collapseButtonImage" />
                                </CollapseButtonImage>
                                <CollapseButtonStyle CssClass="headerExpandCollapseButton" />
                                <HeaderContentTemplate>
                                    <span class="headerTitleText">Local Copy of this Demo</span>
                                    <br />
                                    <span>To inspect the source code for this demo on your machine, you must first install our components via the </span><a href="https://docs.devexpress.com/GeneralInformation/15655/installation/install-devexpress-net-products/gui-install-mode">DevExpress Component Installer</a><span>.</span>
                                </HeaderContentTemplate>
                                <PanelCollection>
                                    <dx:PanelContent runat="server">
                                        <% if(DevExpress.Web.Demos.Utils.CurrentDemoPage.Product.Key.ToLower().Contains("report")) { %>
                                            <span>The source code files for this report are installed (by default) in the following directories:</span>
                                        <%}
                                        else {%>
                                            <span>You can open a local copy of this online demo directly from this webpage (if using v20.2.8, 21.1.4 or higher).</span>
                                            <div>
                                                <dx:ASPxButton runat="server" ID="btnOpenSolutionCS" AutoPostBack="false" UseSubmitBehavior="false" Text="Open CS Solution" CssClass="btnOpenSolution"
                                                    EnableTheming="false" Width="160px" Height="32px" OnInit="btnOpenSolution_Init">
                                                </dx:ASPxButton>
                                                <dx:ASPxButton runat="server" ID="btnOpenSolutionVB" AutoPostBack="false" UseSubmitBehavior="false" Text="Open VB Solution" CssClass="btnOpenSolution"
                                                    EnableTheming="false" Width="160px" Height="32px" OnInit="btnOpenSolution_Init">
                                                </dx:ASPxButton>
                                            </div>
                                            <span>The source code files for this demo are installed (by default) in the following directories:</span>                                        
                                        <% } %>
                                        <br />
                                        <dx:ASPxLabel runat="server" Font-Bold="true" ID="lblPathCS" OnInit="lblPath_Init" />
                                        <br />
                                        <dx:ASPxLabel runat="server" Font-Bold="true" ID="lblPathVB" OnInit="lblPath_Init" />
                                    </dx:PanelContent>
                                </PanelCollection>
                            </dx:ASPxRoundPanel>
                        </div>
                        <% } %>
                    </article>
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
    </TabPages>
</dx:ASPxPageControl>
