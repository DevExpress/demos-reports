<%@ Control Language="C#" %>
<%@ Import Namespace="System.Web.Script.Serialization" %>
<script runat="server">
    protected void Page_Load(object sender, EventArgs e) {
        ThemesContainerRepeater.DataBind();
        ShowAllThemesButton.JSProperties["cpThemesListsNames"] = Utils.CurrentThemesModel.GetAllGroups().Select(g => GetThemesListClientName(g.Name)).ToList();
        ShowAllThemesButton.JSProperties["cpCurrenTheme"] = Utils.CurrentTheme;
        ShowAllThemesButton.JSProperties["cpUseDevExtremeCookieKey"] = DemoHelper.Instance.UseDevExtremeThemeSelector;
    }
    protected void ThemesList_DataBinding(object sender, EventArgs e) {
        ASPxListBox themesList = (ASPxListBox)sender;
        RepeaterItem item = themesList.NamingContainer as RepeaterItem;
        if(item == null)
            return;
        ThemeGroupModel group = item.DataItem as ThemeGroupModel;
        BindThemesList(themesList, group);
        if(Utils.CurrentThemesModel != null) {
            themesList.ItemImage.Height = Utils.CurrentThemesModel.IconHeight;
            themesList.ItemImage.Width = Utils.CurrentThemesModel.IconWidth;
        }
    }
    void BindThemesList(ASPxListBox themesList, ThemeGroupModel group) {
        bool isFirstgroup = group.Name == "FirstGroup";
        themesList.Caption = group.Title;
        if (isFirstgroup)
            themesList.CssClass += " firstGroup";
        var dataSource = group.Themes.Select(t => new ListEditItem() {
            Value = GetThemeTitle(t.Name),
            Text = t.Title,
            ImageUrl = String.IsNullOrEmpty(t.PreveiwIconResourceName) ? Utils.GetColoredSquareUrl(t.PreveiwColor) : Utils.EncodeSvgIconBase64(Utils.GetCurrentResource(t.PreveiwIconResourceName)),
        }).ToList();
        themesList.ClientInstanceName = GetThemesListClientName(group.Name);
        themesList.ClientVisible = isFirstgroup;
        themesList.Items.Clear();
        themesList.Items.AddRange(dataSource);
    }
    protected void ThemesLists_PreRender(object sender, EventArgs e) {
        ASPxListBox themesList = (ASPxListBox)sender;
        themesList.UnselectAll();
        var selectedItem = themesList.Items.FindByValue(GetThemeTitle(Utils.CurrentTheme));
        if(selectedItem != null)
            selectedItem.Selected = true;
        var jsSerializer = new JavaScriptSerializer();
        themesList.JSProperties["cpNewThemes"] = themesList.Items.Cast<ListEditItem>()
            .Where(item => Utils.CurrentThemesModel.NewThemes.Select(t => t.Title).Contains(item.Text))
            .Select(item => item.Text);
    }
    string GetThemesListClientName(string groupName) {
        return "themesList" + groupName;
    }
    string GetThemeTitle(string themeName) {
        return !string.IsNullOrEmpty(themeName) ? themeName : "Default";
    }
</script>
<dx:ASPxLabel runat="server" Text="Themes" CssClass="themes-caption"></dx:ASPxLabel>
<asp:Repeater runat="server" ID="ThemesContainerRepeater" EnableViewState="false" DataSource="<%# Utils.CurrentThemesModel.GetAllGroups() %>">
    <ItemTemplate>
        <dx:ASPxListBox runat="server" CssClass="themes-list" Width="100%" Caption="Themes" CaptionSettings-Position="Top"
            ValueField="Value" TextField="Text" ImageUrlField="ImageUrl" OnDataBinding="ThemesList_DataBinding" EnableCallbackMode="false"
            OnPreRender="ThemesLists_PreRender">
            <CaptionCellStyle CssClass="themes-list-caption-cell" />     
            <CaptionSettings ShowColon="false" />
            <CaptionStyle CssClass="themes-list-caption" />
            <ItemStyle CssClass="item" SelectedStyle-CssClass="item-selected" HoverStyle-CssClass="item-hover" />
            <ClientSideEvents SelectedIndexChanged="DXDemo.OnThemesListSelectedIndexChanged" Init="DXDemo.OnThemesListInit" />
        </dx:ASPxListBox>
    </ItemTemplate>
</asp:Repeater>
<dx:ASPxButton runat="server" ID="ShowAllThemesButton" ClientInstanceName="ShowAllThemesButton" Text="Show All Themes" Width="100%"
    AutoPostBack="false" CssClass="show-all-themes-button" HorizontalAlign="Left">
    <FocusRectBorder BorderWidth="0" />
    <Image SpriteProperties-CssClass="icon three-dots"/>
    <ClientSideEvents Click="function(){ DXDemo.OnShowAllThemesClick(); }" />
</dx:ASPxButton>
