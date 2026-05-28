<%@ Control Language="C#" %>
<script runat="server">
    protected void Page_Load(object sender, EventArgs e) {
        rpOtherProducts.DataSource = Utils.GenerateProductDemos(false);
        rpOtherProducts.DataBind();
        rpLargeProducts.DataSource = Utils.GenerateProductDemos(true);
        rpLargeProducts.DataBind();
    }
</script>
<div class="products-wrapper clearfix">
    <asp:Repeater runat="server" ID="rpLargeProducts">
        <ItemTemplate><div class="product-item-wrapper">
            <div class="product-item">
                <a href="<%# ResolveClientUrl((string)Eval("NavigateUrl")) %>">
                    <img src="<%# this.GetUrlWithVersionSuffix((string)Eval("ImageUrl")) %>" alt="<%# Eval("Title") %>" />
                </a>
                <div class="description">
                    <h2><a href="<%# ResolveClientUrl((string)Eval("NavigateUrl")) %>"><%# Eval("Title") %></a></h2>
                    <p><%# Eval("Description") %></p>
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
            <a href="<%# ResolveClientUrl((string)Eval("NavigateUrl")) %>">
                <img src="<%# this.GetUrlWithVersionSuffix((string)Eval("ImageUrl")) %>" alt="<%# Eval("Title") %>" />
            </a>
            <div class="description">
                <h2><a href="<%# ResolveClientUrl((string)Eval("NavigateUrl")) %>"><%# Eval("Title") %></a></h2>
            </div>
        </div>
    </div></ItemTemplate>
    </asp:Repeater>
</div>
