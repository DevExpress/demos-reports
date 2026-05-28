<%@ Control Language="vb" %>
<script runat="server">
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        LoadTreeView()
        NavigationBreadCrumbsButton.ClientVisible = Utils.IsSiteMode AndAlso DemosModel.Instance.SortedDemoProducts.Count > 1 AndAlso Not DemosModel.Current.IsRootDemo
    End Sub
    Protected Sub NavigationTreeView_PreRender(ByVal sender As Object, ByVal e As EventArgs)
        If NavigationTreeView.Nodes.Count = 1 AndAlso NavigationTreeView.Nodes(0).Nodes.Count = 1 Then
            NavigationTreeView.Nodes(0).Nodes(0).Expanded = True
        End If
        Dim node As TreeViewNode = NavigationTreeView.Nodes.FindByName(Utils.CurrentDemoNodeName)
        NavigationTreeView.SelectedNode = node
        If node IsNot Nothing Then
            NavigationTreeView.ExpandToNode(node)
            node.Expanded = True
            Do While node IsNot Nothing
                node.NodeStyle.CssClass &= " demo-selected"
                If node.Parent IsNot Nothing AndAlso node.Parent.Parent Is Nothing Then
                    node.Expanded = False
                End If
                node = node.Parent
            Loop
        End If
    End Sub
    Sub LoadTreeView()
        If Not Utils.IsSiteMode Then
            LoadTreeView(Utils.CurrentDemo.Product)
        Else
            For Each demoProduct In DemosModel.Instance.SortedDemoProducts
                If (Not demoProduct.HideNavItem) AndAlso (Not demoProduct.IsRootDemo) Then
                    LoadTreeView(demoProduct)
                End If
            Next demoProduct
        End If
    End Sub
    Sub LoadTreeView(ByVal product As DemoProductModel)
        Dim demoProductNode As TreeViewNode = NavigationTreeView.Nodes.Add(product.NavItemTitle, String.Empty, String.Empty,If(product IsNot Utils.CurrentDemo.Product, Utils.GenerateDemoUrl(product.Intro), String.Empty))
        demoProductNode.NodeStyle.CssClass &= " group-node"
        demoProductNode.Name = product.Key
        If product.IsPreview Then
            SetPreviewMarker(demoProductNode)
        End If
        If product.IsNew Then
            SetNewMarker(demoProductNode)
        End If
        If product Is Utils.CurrentDemo.Product Then
            LoadTreeView(product, demoProductNode)
        ElseIf product.GetVisibleGroups().Any(Function(g) g.IsPreview OrElse g.IsNew OrElse g.IsUpdated OrElse g.GetVisibleDemos().Any(Function(d) d.IsPreview OrElse d.IsNew OrElse d.IsUpdated)) Then
            SetUpdatedMarker(demoProductNode)
        End If
    End Sub
    Sub LoadTreeView(ByVal product As DemoProductModel, ByVal productNode As TreeViewNode)
        If product.Intro IsNot Nothing Then
            Dim introNode As TreeViewNode = productNode.Nodes.Add("Introduction", String.Empty, String.Empty, Utils.GenerateDemoUrl(product.Intro))
            introNode.Name = String.Format("{0}_{1}", product.Key, product.Intro.Key)
            introNode.NodeStyle.CssClass &= " group-node"
        End If
        For Each group In product.GetVisibleGroups()
            Dim groupNode As TreeViewNode = productNode.Nodes.Add(group.Title)
            groupNode.Name = String.Format("{0}_{1}", product.Key, group.Key)
            groupNode.NodeStyle.CssClass &= " group-node"
            For Each demo In group.GetAllDemos(True)
                Dim demoNode As TreeViewNode = groupNode.Nodes.Add(demo.Title, String.Empty, String.Empty, Utils.GenerateDemoUrl(demo))
                demoNode.NodeStyle.CssClass &= " demo-node"
                demoNode.Name = String.Format("{0}_{1}_{2}", product.Key, group.Key, demo.Key)
                If demo.IsUpdated Then
                    SetUpdatedMarker(demoNode)
                End If
                If demo.IsNew Then
                    SetNewMarker(demoNode)
                End If
                If demo.IsPreview Then
                    SetPreviewMarker(demoNode)
                End If
            Next demo
            If group.IsUpdated Then
                SetUpdatedMarker(groupNode)
            End If
            If group.IsNew Then
                SetNewMarker(groupNode)
            End If
        Next group
    End Sub
    Sub SetNewMarker(ByVal node As TreeViewNode)
        If Not node.NodeStyle.CssClass.Contains("Ico") Then
            node.NodeStyle.CssClass &= " newIco"
        End If
        SetUpdatedMarker(node.Parent)
    End Sub
    Sub SetUpdatedMarker(ByVal node As TreeViewNode)
        Do While node IsNot Nothing
            If Not node.NodeStyle.CssClass.Contains("Ico") Then
                node.NodeStyle.CssClass &= " updatedIco"
            End If
            node = node.Parent
        Loop
    End Sub
    Sub SetPreviewMarker(ByVal node As TreeViewNode)
        If Not node.NodeStyle.CssClass.Contains("Ico") Then
            node.NodeStyle.CssClass &= " previewIco"
        End If
        SetUpdatedMarker(node.Parent)
    End Sub
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