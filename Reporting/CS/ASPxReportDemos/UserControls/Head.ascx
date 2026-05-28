<%@ Control Language="C#" %>
<script runat="server">
    protected void Page_Init(object sender, EventArgs e) {
        Page.Load += OwnerPage_Load;
    }
    protected void OwnerPage_Load(object sender, EventArgs e) {
        Page page = (sender as Control).Page;
        Utils.RegisterCssLink(page, "~/Content/Site.css?v=" + Utils.GetVersionSuffix());
        Utils.RegisterCssLink(page, "~/Content/sprite.css?v=" + Utils.GetVersionSuffix());
        Utils.RegisterCssLink(page, "~/Content/CodeFormatter.css?v=" + Utils.GetVersionSuffix());
        Utils.RegisterCssLink(page, "~/Content/feedback.css?v=" + Utils.GetVersionSuffix());
    }
</script>
<script type="text/javascript" src="<%= this.GetUrlWithVersionSuffix("~/Scripts/Demo.js") %>"></script>
<script type="text/javascript" src="<%= this.GetUrlWithVersionSuffix("~/Scripts/Feedback.js") %>"></script>
<style>
    @font-face
    {
        font-family: "arima madurai";
        src:url("<%= this.GetUrlWithVersionSuffix("~/Content/Fonts/CustomFont0.woff") %>") format('woff');
    }
    @font-face
    {
        font-family: "comfortaa";
        src:url("<%= this.GetUrlWithVersionSuffix("~/Content/Fonts/CustomFont1.woff") %>") format('woff');
    }
    @font-face
    {
        font-family: "asap";
        src:url("<%= this.GetUrlWithVersionSuffix("~/Content/Fonts/CustomFont2.woff") %>") format('woff');
    }
</style>
