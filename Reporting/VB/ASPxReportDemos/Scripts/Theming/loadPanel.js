var showLoadPanel = function() {
    loadPanel.show();
};
var hideLoadPanel = function() {
    setTimeout(function() {
        loadPanel.hide();
    }, 1500);
};
var loadPanel = $("#theming_demo_load_panel").dxLoadPanel({
    shadingColor: "rgba(0,0,0,0.4)",
    position: { of: ".report-designer-preview" },
    visible: false,
    showIndicator: true,
    showPane: true,
    shading: true,
    hideOnOutsideClick: false,
    onShowing: function(e) { e.component.option('container', '.dx-designer-viewport') },
    container: '.report-designer-preview'
}).dxLoadPanel("instance");