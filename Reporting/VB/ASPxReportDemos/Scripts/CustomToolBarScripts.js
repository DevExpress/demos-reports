var CustomToolbarClientControl = {
    DocumentMapToggleCommandName: "ShowDocumentMapToggleCommand",
    ParametersPanelToggleCommandName: "ShowParametersPanelToggleCommand",
    CommandExecuted: function(s, e) {
        var btnId = e.item.name;
        var toggleChecked = e.item.GetValue();
        this.Unsubscribe();
        var isDocumentMapPane = btnId === this.DocumentMapToggleCommandName;
        var currentPane = isDocumentMapPane ? this.documentMapPane : this.parametersPanelPane;
        var otherPane = isDocumentMapPane ? this.parametersPanelPane : this.documentMapPane;

        if(!currentPane || (!toggleChecked && currentPane.IsCollapsed()))
            return;
        if(toggleChecked) {
            currentPane.Expand();
            if(this.sideCenterPane.IsCollapsed())
                if(otherPane && !otherPane.CollapseForward())
                    otherPane.CollapseBackward();
            this.sideCenterPane.Expand();
        } else if(!this.sideCenterPane.IsCollapsed()) {
            if(!otherPane || otherPane.IsCollapsed()) {
                this.sideCenterPane.CollapseForward();
            } else {
                if(!currentPane.CollapseForward())
                    currentPane.CollapseBackward();
            }
        }
        this.Subscribe();
    },
    PaneCollapsed: function(s, e) {
        var pane = e.pane;
        if(this.sideCenterPane && pane.name === this.sideCenterPane.name) {
            this.parametersPanelToggle.SetValue(false);
            this.documentMapToggle.SetValue(false);
        }
        if(this.parametersPanelPane && pane.name === this.parametersPanelPane.name) {
            this.parametersPanelToggle.SetValue(false);
        }
        if(this.documentMapPane && pane.name === this.documentMapPane.name) {
            this.documentMapToggle.SetValue(false);
        }
    },
    PaneExpanded: function(s, e) {
        if(this.parametersPanelPane && !this.parametersPanelPane.IsCollapsed())
            this.parametersPanelToggle.SetValue(true);
        if(this.documentMapPane && !this.documentMapPane.IsCollapsed())
            this.documentMapToggle.SetValue(true);
    },
    OnInit: function(s, e) {
        this.ribbon = s;
        this.parametersPanelToggle = s.GetItemByName(this.ParametersPanelToggleCommandName);
        this.documentMapToggle = s.GetItemByName(this.DocumentMapToggleCommandName);
    },
    AssignDocumentViewer: function(documentViewer) {
        this.splitter = documentViewer.GetSplitter();
        this.documentMapPane = this.splitter.GetPaneByName('DocumentMapPane');
        this.parametersPanelPane = this.splitter.GetPaneByName('ParametersPanelPane');
        this.sideCenterPane = this.splitter.GetPaneByName('SideCenterPane');
    },
    OnPageLoad: function(s, e) {
        if(!this.sideCenterPane) {
            this.DisableDocumentMapToggle();
            this.DisableParametersPanelToggle();
        }
        if(!this.documentMapPane)
            this.DisableDocumentMapToggle();
        if(!this.parametersPanelPane)
            this.DisableParametersPanelToggle();
        this.Subscribe();
    },
    DisableDocumentMapToggle: function() {
        this.documentMapToggle.SetValue(false);
        this.documentMapToggle.SetEnabled(false);
    },
    DisableParametersPanelToggle: function() {
        if(this.parametersPanelToggle) {
            this.parametersPanelToggle.SetValue(false);
            this.parametersPanelToggle.SetEnabled(false);
        }
    },
    Subscribe: function() {
        if(this.splitter) {
            this.splitter.PaneCollapsed.AddHandler(this.PaneCollapsed.aspxBind(this));
            this.splitter.PaneExpanded.AddHandler(this.PaneExpanded.aspxBind(this));
        }
    },
    Unsubscribe: function() {
        if(this.splitter) {
            this.splitter.PaneCollapsed.RemoveHandler(this.PaneCollapsed.aspxBind(this));
            this.splitter.PaneExpanded.RemoveHandler(this.PaneExpanded.aspxBind(this));
        }
    }
};
