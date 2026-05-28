<%@ Control Language="C#" %>
<script runat="server">
    protected void Page_Load(object sender, EventArgs e) {
        if(Utils.CanChangeBaseColor) {
            BaseColorEditor.DataSource = Utils.CustomBaseColors.Select(c => new { Value = c, ImageUrl = Utils.GetColoredSquareUrl(c) });
            BaseColorEditor.DataBind();
            string currentBaseColor = string.IsNullOrWhiteSpace(Utils.CurrentBaseColor) ? Utils.GetDefaultBaseColor(Utils.CurrentTheme) : Utils.CurrentBaseColor;
            var item = BaseColorEditor.Items.FindByValue(currentBaseColor);
            if(item != null)
                item.Selected = true;
        }
        FontEditor.DataSource = Utils.GetFontFamiliesDataSource().Select(i => new { Value = i.Value, Text = i.Text, ImageUrl = "" });
        FontEditor.DataBind();
        FontEditor.SelectedIndex = !string.IsNullOrEmpty(ASPxWebControl.GlobalThemeFont) ? FontEditor.Items.IndexOfValue(ASPxWebControl.GlobalThemeFont) : 0;
    }
    protected void Page_Init(object sender, EventArgs e) {
        InitEditor(BaseColorEditor);
        InitEditor(FontEditor);
    }
    protected void InitEditor(ASPxComboBox editor) {
        editor.DropDownStyle = DropDownStyle.DropDownList;
        editor.ValueField = "Value";
        editor.ImageUrlField = "ImageUrl";
        editor.ShowImageInEditBox = true;
        editor.CssClass += " themes-parameters-editor";
        editor.CaptionSettings.Position = EditorCaptionPosition.Top;
        editor.CaptionSettings.ShowColon = false;
        editor.CaptionStyle.CssClass = "themes-parameters-caption";
        editor.ListBoxStyle.CssClass += " themes-parameters-listbox";
        editor.ButtonStyle.CssClass = "themes-parameters-button-edit";
        editor.DropDownButton.Image.SpriteProperties.CssClass = "icon drop-down";
        editor.DropDownButton.Image.Height = 12;
        editor.DropDownButton.Image.Width = 12;
    }
</script>
<div class="themes-parameters-container">
<% if(Utils.CanChangeBaseColor) { %>
    <dx:ASPxComboBox runat="server" ID="BaseColorEditor" Caption="Base Color" EnableTheming="false" TextField="Value" DropDownRows="8">
        <ItemImage Height="18px" Width="18px" />
        <ClientSideEvents SelectedIndexChanged="function(s){ DXDemo.SetCurrentBaseColor(s.GetSelectedItem().value); }" />
    </dx:ASPxComboBox>
<% } %>
    <dx:ASPxComboBox runat="server" ID="FontEditor" Caption="Font" TextField="Text" EnableTheming="false" CssClass="font-editor" >
        <Buttons>
            <dx:EditButton Image-SpriteProperties-CssClass="icon font" Position="Left" />
        </Buttons>
        <ListBoxStyle CssClass="font-editor" />
        <ItemImage SpriteProperties-CssClass="icon font" />
        <ClientSideEvents SelectedIndexChanged="function(s){ DXDemo.SetCurrentFont(s.GetSelectedItem().value); }" ButtonClick="DXDemo.iconButtonClick" />
    </dx:ASPxComboBox>
</div>
