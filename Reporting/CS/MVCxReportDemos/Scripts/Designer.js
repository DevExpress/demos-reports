function onDesignerInit(reportDesigner) {
    reportDesigner && reportDesigner.ExitDesigner.AddHandler(function(_) {
        var redirectUrl = $("#redirectValue").val();
        if(redirectUrl.length > 0)
            window.location.replace(redirectUrl);
    });
    var suppressCloseConfirmation = false;
    $(window).on('beforeunload', function(e) {
        reportDesigner = reportDesigner || window['reportDesigner'];
        if(!reportDesigner) return;
        var viewer = reportDesigner.GetPreviewModel();
        setTimeout(function() {
            viewer && viewer.Close && viewer.Close();
        }, 1);
        if (!suppressCloseConfirmation && reportDesigner.GetDesignerModel().isDirty()) {
            return 'You have unsaved changes on the page';
        }
    });
    if(DevExpress.ui.dxPopup.prototype._zIndexInitValue && $(".dxpnlControl.header-panel").length) {
        $(".dxpnlControl.header-panel").css("z-index", DevExpress.ui.dxPopup.prototype._zIndexInitValue() + 100)
    }
}

function RegisterNationalityEditor(contentPath) {
    DevExpress.Reporting.Editing.EditingFieldExtensions.registerImageEditor({
        name: "Nationality",
        displayName: "Nationality",
        searchEnabled: true,
        images: [
            { url: contentPath + "Flags/Australia.png", text: "Australia" },
            { url: contentPath + "Flags/China.png", text: "China" },
            { url: contentPath + "Flags/France.png", text: "France" },
            { url: contentPath + "Flags/Germany.png", text: "Germany" },
            { url: contentPath + "Flags/India.png", text: "India" },
            { url: contentPath + "Flags/Italy.png", text: "Italy" },
            { url: contentPath + "Flags/Japan.png", text: "Japan" },
            { url: contentPath + "Flags/Russia.png", text: "Russia" },
            { url: contentPath + "Flags/United_Kingdom.png", text: "United Kingdom" },
            { url: contentPath + "Flags/United_States_of_America.png", text: "United States of America" }
        ]
    });
}

function RegisterDamageDiagramEditor() {
    DevExpress.Reporting.Editing.EditingFieldExtensions.registerImageEditor({
        name: "DamageDiagram",
        displayName: "Damage Diagram",
        drawingEnabled: true,
        imageLoadEnabled: false,
        sizeOptionsEnabled: false,
        clearEnabled: false
    });
}
