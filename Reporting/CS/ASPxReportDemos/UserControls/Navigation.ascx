<%@ Control Language="C#" %>
<script runat="server">
    protected void Page_Load(object sender, EventArgs e) {
        LoadTreeView();
        NavigationBreadCrumbsButton.ClientVisible = Utils.IsSiteMode && DemosModel.Instance.SortedDemoProducts.Count > 1 && !DemosModel.Current.IsRootDemo;
    }
    protected void NavigationTreeView_PreRender(object sender, EventArgs e) {
        if(NavigationTreeView.Nodes.Count == 1 && NavigationTreeView.Nodes[0].Nodes.Count == 1)
            NavigationTreeView.Nodes[0].Nodes[0].Expanded = true;
        TreeViewNode node = NavigationTreeView.Nodes.FindByName(Utils.CurrentDemoNodeName);
        NavigationTreeView.SelectedNode = node;
        if(node != null) {
            NavigationTreeView.ExpandToNode(node);
            node.Expanded = true;
            while(node != null) {
                node.NodeStyle.CssClass += " demo-selected";
                if(node.Parent != null && node.Parent.Parent == null)
                    node.Expanded = false;
                node = node.Parent;
            }
        }
    }
    void LoadTreeView() {
        if(!Utils.IsSiteMode)
            LoadTreeView(Utils.CurrentDemo.Product);
        else {
            foreach(var demoProduct in DemosModel.Instance.SortedDemoProducts) {
                if(!demoProduct.HideNavItem && !demoProduct.IsRootDemo)
                    LoadTreeView(demoProduct);
            }
        }
    }
    void LoadTreeView(DemoProductModel product) {
        TreeViewNode demoProductNode = NavigationTreeView.Nodes.Add(
            product.NavItemTitle,
            string.Empty,
            string.Empty,
            product != Utils.CurrentDemo.Product ? Utils.GenerateDemoUrl(product.Intro) : string.Empty
        );
        demoProductNode.NodeStyle.CssClass += " group-node";
        demoProductNode.Name = product.Key;
        if(product.IsPreview)
            SetPreviewMarker(demoProductNode);
        if(product.IsNew)
            SetNewMarker(demoProductNode);
        if(product == Utils.CurrentDemo.Product)
            LoadTreeView(product, demoProductNode);
        else if(product.GetVisibleGroups().Any(g => g.IsPreview || g.IsNew || g.IsUpdated || g.GetVisibleDemos().Any(d => d.IsPreview || d.IsNew || d.IsUpdated)))
            SetUpdatedMarker(demoProductNode);
    }
    void LoadTreeView(DemoProductModel product, TreeViewNode productNode) {
        if(product.Intro != null) {
            TreeViewNode introNode = productNode.Nodes.Add("Introduction", string.Empty, string.Empty, Utils.GenerateDemoUrl(product.Intro));
            introNode.Name = string.Format("{0}_{1}", product.Key, product.Intro.Key);
            introNode.NodeStyle.CssClass += " group-node";
        }
        foreach(var group in product.GetVisibleGroups()) {
            TreeViewNode groupNode = productNode.Nodes.Add(group.Title);
            groupNode.Name = string.Format("{0}_{1}", product.Key, group.Key);
            groupNode.NodeStyle.CssClass += " group-node";
            foreach(var demo in group.GetAllDemos(true)) {
                TreeViewNode demoNode = groupNode.Nodes.Add(demo.Title, string.Empty, string.Empty, Utils.GenerateDemoUrl(demo));
                demoNode.NodeStyle.CssClass += " demo-node";
                demoNode.Name = string.Format("{0}_{1}_{2}", product.Key, group.Key, demo.Key);
                if(demo.IsUpdated)
                    SetUpdatedMarker(demoNode);
                if(demo.IsNew)
                    SetNewMarker(demoNode);
                if(demo.IsPreview)
                    SetPreviewMarker(demoNode);
            }
            if(group.IsUpdated)
                SetUpdatedMarker(groupNode);
            if(group.IsNew)
                SetNewMarker(groupNode);
        }
    }
    void SetNewMarker(TreeViewNode node) {
        if(!node.NodeStyle.CssClass.Contains("Ico"))
            node.NodeStyle.CssClass += " newIco";
        SetUpdatedMarker(node.Parent);
    }
    void SetUpdatedMarker(TreeViewNode node) {
        while(node != null) {
            if(!node.NodeStyle.CssClass.Contains("Ico"))
                node.NodeStyle.CssClass += " updatedIco";
            node = node.Parent;
        }
    }
    void SetPreviewMarker(TreeViewNode node) {
        if(!node.NodeStyle.CssClass.Contains("Ico"))
            node.NodeStyle.CssClass += " previewIco";
        SetUpdatedMarker(node.Parent);
    }
</script>
<nav>
    <div id="NavControl" class="nav-control" style="visibility:hidden">
        <dx:ASPxButton runat="server" ID="NavigationBreadCrumbsButton" ClientInstanceName="NavigationBreadCrumbsButton" CssClass="navigation-breadcrumbs-button" 
            Text="All Demos" Width="100%" AutoPostBack="false" HorizontalAlign="Left" EnableTheming="false" UseSubmitBehavior="false">
            <FocusRectBorder BorderWidth="0" />
            <FocusRectPaddings Padding="0" />
            <Image SpriteProperties-CssClass="icon"></Image>
            <ClientSideEvents Click="function(){ NavControl.onNavigationBreadCrumbsButtonClick(); }" />
        </dx:ASPxButton>
        <div class="nav-tree-view">
            <div id="breadCrumbs" class="breadCrumbs">All Demos</div>
            <dx:ASPxTreeView runat="server" ID="NavigationTreeView" ClientInstanceName="NavigationTreeView" OnPreRender="NavigationTreeView_PreRender" Width="100%"
                EnableViewState="false" ShowTreeLines="false" ShowExpandButtons="true" EncodeHtml="false" Theme="Metropolis" EnableHotTrack="false">
                <ClientSideEvents Init="DXDemo.treeViewInit" />
            <Images>
                <ExpandButton SpriteProperties-CssClass="icon angle-right" />
                <CollapseButton SpriteProperties-CssClass="icon angle-down" />
            </Images>
            </dx:ASPxTreeView>
        </div>
    </div>
</nav>
