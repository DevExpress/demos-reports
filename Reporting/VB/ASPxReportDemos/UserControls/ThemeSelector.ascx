<%@ Control Language="vb" %>
<%@ Import Namespace="System.Web.Script.Serialization" %>
<script runat="server">
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        ThemesContainerRepeater.DataBind()
        ShowAllThemesButton.JSProperties("cpThemesListsNames") = Utils.CurrentThemesModel.GetAllGroups().Select(Function(g) GetThemesListClientName(g.Name)).ToList()
        ShowAllThemesButton.JSProperties("cpCurrenTheme") = Utils.CurrentTheme
        ShowAllThemesButton.JSProperties("cpUseDevExtremeCookieKey") = DemoHelper.Instance.UseDevExtremeThemeSelector
    End Sub
    Protected Sub ThemesList_DataBinding(ByVal sender As Object, ByVal e As EventArgs)
        Dim themesList As ASPxListBox = DirectCast(sender, ASPxListBox)
        Dim item As RepeaterItem = TryCast(themesList.NamingContainer, RepeaterItem)
        If item Is Nothing Then
            Return
        End If
        Dim group As ThemeGroupModel = TryCast(item.DataItem, ThemeGroupModel)
        BindThemesList(themesList, group)
        If Utils.CurrentThemesModel IsNot Nothing Then
            themesList.ItemImage.Height = Utils.CurrentThemesModel.IconHeight
            themesList.ItemImage.Width = Utils.CurrentThemesModel.IconWidth
        End If
    End Sub
    Sub BindThemesList(ByVal themesList As ASPxListBox, ByVal group As ThemeGroupModel)
        Dim isFirstgroup As Boolean = group.Name = "FirstGroup"
        themesList.Caption = group.Title
        If isFirstgroup Then
            themesList.CssClass &= " firstGroup"
        End If
        Dim dataSource = group.Themes.Select(Function(t) New ListEditItem() With {.Value = GetThemeTitle(t.Name), .Text = t.Title, .ImageUrl = If(String.IsNullOrEmpty(t.PreveiwIconResourceName), Utils.GetColoredSquareUrl(t.PreveiwColor), Utils.EncodeSvgIconBase64(Utils.GetCurrentResource(t.PreveiwIconResourceName)))}).ToList()
        themesList.ClientInstanceName = GetThemesListClientName(group.Name)
        themesList.ClientVisible = isFirstgroup
        themesList.Items.Clear()
        themesList.Items.AddRange(dataSource)
    End Sub
    Protected Sub ThemesLists_PreRender(ByVal sender As Object, ByVal e As EventArgs)
        Dim themesList As ASPxListBox = DirectCast(sender, ASPxListBox)
        themesList.UnselectAll()
        Dim selectedItem = themesList.Items.FindByValue(GetThemeTitle(Utils.CurrentTheme))
        If selectedItem IsNot Nothing Then
            selectedItem.Selected = True
        End If
        Dim jsSerializer = New JavaScriptSerializer()
        themesList.JSProperties("cpNewThemes") = themesList.Items.Cast(Of ListEditItem)().Where(Function(item) Utils.CurrentThemesModel.NewThemes.Select(Function(t) t.Title).Contains(item.Text)).Select(Function(item) item.Text)
    End Sub
    Function GetThemesListClientName(ByVal groupName As String) As String
        Return "themesList" & groupName
    End Function
    Function GetThemeTitle(ByVal themeName As String) As String
        Return If((Not String.IsNullOrEmpty(themeName)), themeName, "Default")
    End Function
</script>
<dx:ASPxLabel runat="server" Text="Themes" CssClass="themes-caption"></dx:ASPxLabel>
<asp:Repeater runat="server" ID="ThemesContainerRepeater" EnableViewState="false" DataSource="<%#Utils.CurrentThemesModel.GetAllGroups()%>">
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