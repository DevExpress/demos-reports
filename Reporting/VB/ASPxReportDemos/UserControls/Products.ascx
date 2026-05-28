<%@ Control Language="vb" %>
<script runat="server">
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        rpOtherProducts.DataSource = Utils.GenerateProductDemos(False)
        rpOtherProducts.DataBind()
        rpLargeProducts.DataSource = Utils.GenerateProductDemos(True)
        rpLargeProducts.DataBind()
    End Sub
</script>
<div class="products-wrapper clearfix">
    <asp:Repeater runat="server" ID="rpLargeProducts">
        <ItemTemplate><div class="product-item-wrapper">
            <div class="product-item">
                <a href="<%#ResolveClientUrl(DirectCast(Eval("NavigateUrl"), String))%>">
                    <img src="<%#Me.GetUrlWithVersionSuffix(DirectCast(Eval("ImageUrl"), String))%>" alt="<%#Eval("Title")%>" />
                </a>
                <div class="description">
                    <h2><a href="<%#ResolveClientUrl(DirectCast(Eval("NavigateUrl"), String))%>"><%#Eval("Title")%></a></h2>
                    <p><%#Eval("Description")%></p>
                </div>
            </div>
        </div></ItemTemplate>
    </asp:Repeater>
</div>
<div class="slogan">
    <h1>More controls</h1>
</div>
<div class="products-wrapper products-wrapper-other clearfix">
    <asp:Repeater runat="server" ID="rpOtherProducts">
        <ItemTemplate><div class="product-item-wrapper">
        <div class="product-item">
            <a href="<%#ResolveClientUrl(DirectCast(Eval("NavigateUrl"), String))%>">
                <img src="<%#Me.GetUrlWithVersionSuffix(DirectCast(Eval("ImageUrl"), String))%>" alt="<%#Eval("Title")%>" />
            </a>
            <div class="description">
                <h2><a href="<%#ResolveClientUrl(DirectCast(Eval("NavigateUrl"), String))%>"><%#Eval("Title")%></a></h2>
            </div>
        </div>
    </div></ItemTemplate>
    </asp:Repeater>
</div>