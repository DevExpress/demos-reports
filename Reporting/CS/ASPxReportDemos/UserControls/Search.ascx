<%@ Control Language="C#" AutoEventWireup="true" Inherits="DevExpress.Web.Demos.Search" Codebehind="Search.ascx.cs" %>
<div class="search-wrapper" role="search">
    <dx:ASPxButtonEdit runat="server" Id="SearchEditor" ClientInstanceName="searchEditor" CssClass="search-input" NullText="Search Demos" 
        NullTextDisplayMode="UnfocusedAndFocused" EnableTheming="false">
        <Buttons>
            <dx:EditButton Image-SpriteProperties-CssClass="icon search" Position="Left" />
        </Buttons>
        <ClearButton DisplayMode="Always" Image-SpriteProperties-CssClass="icon clear-button" />
        <ClientSideEvents GotFocus="DXDemo.Search.onSearchBoxGotFocus" LostFocus="DXDemo.Search.onSearchBoxLostFocus"
            ButtonClick="DXDemo.iconButtonClick" KeyDown="DXDemo.Search.onSearchEditorKeyDown" />
    </dx:ASPxButtonEdit>
    <dx:ASPxCallbackPanel runat="server" ID="searchResults" ClientInstanceName="searchResults" OnCallback="SearchResults_Callback" 
        EnableTheming="false" CssClass="search-results ">
        <ClientSideEvents EndCallback="DXDemo.Search.onEndCallback" />
        <SettingsLoadingPanel Enabled="false" />
        <PanelCollection>
            <dx:PanelContent>
                <div runat="server" id="resultsContainer" class="search-results-container">
                    <asp:Repeater runat="server" ID="resultsRepeater" EnableViewState="false">
                        <ItemTemplate>
                            <a id="sr_<%#: Container.ItemIndex %>" href="<%#: GetSearchItemUrl(Container.DataItem) %>" class="search-result-item">
                                <span class="demo-title <%#: GetAdditionalInfoMarker(Container.DataItem) %>"><%# Eval("Text") %></span>
                                <span class="additional-info <%#: GetAdditionalInfoMarker(Container.DataItem) %>">
                                    <span><%#: Eval("Product.NavItemTitle") %></span><%-- spacing fix
                                 --%><span class="icon right-arrow arrow-right-long"></span><%----
                                 --%><span class="icon right-arrow arrow-right-long-selected"></span><%--
                                 --%><span><%#: GetDemoGroupTittle(Container.DataItem) %></span>
                                </span>
                            </a>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div id="noResultsContainer" runat="server" class="no-results-container">
                    No results found for <b runat="server" id="requestText"></b>.
                </div>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxCallbackPanel>
</div>
