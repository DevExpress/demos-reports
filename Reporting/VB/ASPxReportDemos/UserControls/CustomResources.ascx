<%@ Control Language="vb" %>
<dx:ASPxStyleSheetManager ID="styleSheetManager" runat="server">
    <Items>
        <dx:ResourceStyleSheet Suite="Editors" Theme = "Default" />
        <dx:ResourceStyleSheet Suite="NavigationAndLayout" Theme="Default" />
        <dx:ResourceStyleSheet Suite="Editors" />
        <dx:ResourceStyleSheet Suite="Editors" Theme = "MaterialCompactOrange" />
        <dx:ResourceStyleSheet Suite="NavigationAndLayout" />
        <dx:ResourceStyleSheet Suite="NavigationAndLayout" Theme="Metropolis" />
        <dx:ResourceStyleSheet Suite="NavigationAndLayout" Theme="MaterialCompactOrange" />
        <dx:ResourceStyleSheet Suite="Reporting" />
        <dx:ResourceStyleSheet Suite="ClientRichEdit" />
        <dx:ResourceStyleSheet Suite="Icons" />
    </Items>
</dx:ASPxStyleSheetManager>
<dx:ASPxScriptManager ID="scriptManager" runat="server">
    <Items>            
        <dx:ResourceScript Suite="Editors" />
        <dx:ResourceScript Suite="NavigationAndLayout" />
        <dx:ResourceScript Suite="Reporting" />
        <dx:ResourceScript Suite="ClientRichEdit" />
        <dx:ResourceScript Suite="Icons" />
    </Items>
</dx:ASPxScriptManager>