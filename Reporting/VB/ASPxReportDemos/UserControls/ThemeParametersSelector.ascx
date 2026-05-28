<%@ Control Language="vb" %>
<script runat="server">
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        If Utils.CanChangeBaseColor Then
            BaseColorEditor.DataSource = Utils.CustomBaseColors.Select(Function(c) New With {Key .Value = c, Key .ImageUrl = Utils.GetColoredSquareUrl(c)})
            BaseColorEditor.DataBind()
            Dim currentBaseColor As String = If(String.IsNullOrWhiteSpace(Utils.CurrentBaseColor), Utils.GetDefaultBaseColor(Utils.CurrentTheme), Utils.CurrentBaseColor)
            Dim item = BaseColorEditor.Items.FindByValue(currentBaseColor)
            If item IsNot Nothing Then
                item.Selected = True
            End If
        End If
        FontEditor.DataSource = Utils.GetFontFamiliesDataSource().Select(Function(i) New With {Key .Value = i.Value, Key .Text = i.Text, Key .ImageUrl = ""})
        FontEditor.DataBind()
        FontEditor.SelectedIndex = If((Not String.IsNullOrEmpty(ASPxWebControl.GlobalThemeFont)), FontEditor.Items.IndexOfValue(ASPxWebControl.GlobalThemeFont), 0)
    End Sub
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs)
        InitEditor(BaseColorEditor)
        InitEditor(FontEditor)
    End Sub
    Protected Sub InitEditor(ByVal editor As ASPxComboBox)
        editor.DropDownStyle = DropDownStyle.DropDownList
        editor.ValueField = "Value"
        editor.ImageUrlField = "ImageUrl"
        editor.ShowImageInEditBox = True
        editor.CssClass &= " themes-parameters-editor"
        editor.CaptionSettings.Position = EditorCaptionPosition.Top
        editor.CaptionSettings.ShowColon = False
        editor.CaptionStyle.CssClass = "themes-parameters-caption"
        editor.ListBoxStyle.CssClass &= " themes-parameters-listbox"
        editor.ButtonStyle.CssClass = "themes-parameters-button-edit"
        editor.DropDownButton.Image.SpriteProperties.CssClass = "icon drop-down"
        editor.DropDownButton.Image.Height = 12
        editor.DropDownButton.Image.Width = 12
    End Sub
</script>
<div class="themes-parameters-container">
<%
If Utils.CanChangeBaseColor Then
%>
    <dx:ASPxComboBox runat="server" ID="BaseColorEditor" Caption="Base Color" EnableTheming="false" TextField="Value" DropDownRows="8">
        <ItemImage Height="18px" Width="18px" />
        <ClientSideEvents SelectedIndexChanged="function(s){ DXDemo.SetCurrentBaseColor(s.GetSelectedItem().value); }" />
    </dx:ASPxComboBox>
<%
End If
%>
    <dx:ASPxComboBox runat="server" ID="FontEditor" Caption="Font" TextField="Text" EnableTheming="false" CssClass="font-editor" >
        <Buttons>
            <dx:EditButton Image-SpriteProperties-CssClass="icon font" Position="Left" />
        </Buttons>
        <ListBoxStyle CssClass="font-editor" />
        <ItemImage SpriteProperties-CssClass="icon font" />
        <ClientSideEvents SelectedIndexChanged="function(s){ DXDemo.SetCurrentFont(s.GetSelectedItem().value); }" ButtonClick="DXDemo.iconButtonClick" />
    </dx:ASPxComboBox>
</div>