(function() {
    var doWithViewer = function(func) {
        var viewer = window['webDocumentViewer'];
        viewer && func(viewer);
    };
    var stopTimeout;
    window.addEventListener("focus", function() { stopTimeout && clearTimeout(stopTimeout); });
    window.addEventListener("blur", function() {
        stopTimeout && clearTimeout(stopTimeout);
        stopTimeout = setTimeout(function() {
            doWithViewer(function(viewer) {
                var reportPreview = viewer.GetReportPreview();
                reportPreview && reportPreview.documentBuilding && reportPreview.stopBuild();
            });
        }, 3000);
    });
    window.addEventListener("beforeunload", function() {
        doWithViewer(function(viewer) {
            setTimeout(function() { viewer.Close && viewer.Close(); }, 1);
        });
    });
})()
