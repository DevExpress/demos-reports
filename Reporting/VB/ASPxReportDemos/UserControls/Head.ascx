<%@ Control Language="vb" %>
<script runat="server">
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs)
        AddHandler Page.Load, AddressOf OwnerPage_Load
    End Sub
    Protected Sub OwnerPage_Load(ByVal sender As Object, ByVal e As EventArgs)

        Dim page_Renamed As Page = (TryCast(sender, Control)).Page
        Utils.RegisterCssLink(page_Renamed, "~/Content/Site.css?v=" & Utils.GetVersionSuffix())
        Utils.RegisterCssLink(page_Renamed, "~/Content/sprite.css?v=" & Utils.GetVersionSuffix())
        Utils.RegisterCssLink(page_Renamed, "~/Content/CodeFormatter.css?v=" & Utils.GetVersionSuffix())
        Utils.RegisterCssLink(page_Renamed, "~/Content/feedback.css?v=" & Utils.GetVersionSuffix())
    End Sub
</script>
<script type="text/javascript" src="<%=Me.GetUrlWithVersionSuffix("~/Scripts/Demo.js")%>"></script>
<script type="text/javascript" src="<%=Me.GetUrlWithVersionSuffix("~/Scripts/Feedback.js")%>"></script>
<style>
    @font-face
    {
        font-family: "arima madurai";
        src:url("<%=Me.GetUrlWithVersionSuffix("~/Content/Fonts/CustomFont0.woff")%>") format('woff');
    }
    @font-face
    {
        font-family: "comfortaa";
        src:url("<%=Me.GetUrlWithVersionSuffix("~/Content/Fonts/CustomFont1.woff")%>") format('woff');
    }
    @font-face
    {
        font-family: "asap";
        src:url("<%=Me.GetUrlWithVersionSuffix("~/Content/Fonts/CustomFont2.woff")%>") format('woff');
    }
</style>