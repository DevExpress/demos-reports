// Write your JavaScript code.
const docURL = 'https://docs.devexpress.com/XtraReports/402561/web-reporting/accessibility';
const accessibilityCheckerURL = new URL("http://wave.webaim.org/report");
accessibilityCheckerURL.hash = `/${location.href}`;

var defaultFontSize = parseInt(window.getComputedStyle(document.documentElement).getPropertyValue('font-size')) / 16 * 100;
var configuration = {
    exploreFeatures: ko.observable(true),
    isContrast: ko.observable(false),
    zoom: ko.observable(defaultFontSize)
};

function initializeArgsData(isContrast, zoom, exploreFeatures) {
    configuration.isContrast(isContrast);
    configuration.zoom(zoom === 0 ? defaultFontSize : zoom);
    configuration.exploreFeatures(exploreFeatures);
    if(configuration.zoom() && configuration.zoom() !== defaultFontSize) {
        document.documentElement.style.fontSize = (configuration.zoom() / 100 * 16).toString() + "px";
    }
}

function applyChanges() {
    const searchParams = new URLSearchParams(window.location.search);
    Object.keys(configuration).forEach(key => {
        searchParams.set(key, configuration[key]());
    });
    window.location.search = searchParams.toString();
}

var fonts = [
    {
        icon: 'shrinkfont',
        size: 100,
        hint: 'Standard font'
    },
    {
        icon: 'fontsize',
        size: 125,
        hint: 'Increased font'
    },
    {
        icon: 'growfont',
        size: 150,
        hint: 'Large font'
    },
    {
        icon: 'growfont',
        size: 175,
        hint: 'Large font'
    },
    {
        icon: 'growfont',
        size: 200,
        hint: 'Large font'
    }
].map(x => {
    x.click = function() {
        configuration.zoom(x.size);
    };
    return x;
});
function setObjectModel(item) {
    if(!item.exploreFeaturesProperties) {
        item.exploreFeaturesProperties = {};
        item.exploreFeaturesProperties.fonts = fonts;
        item.exploreFeaturesProperties.applyChanges = applyChanges;
        item.exploreFeaturesProperties.exploreFeaturesTab = {
            visible: ko.observable(true),
            action: function() {
                configuration.exploreFeatures(!configuration.exploreFeatures())
            },
            active: configuration.exploreFeatures
        };
        item.exploreFeaturesProperties.exploreFeatures = configuration.exploreFeatures;
        item.exploreFeaturesProperties.showDoc = function() {
            window.open(docURL, '_blank');
        };
        item.exploreFeaturesProperties.showWave = function() {
            window.open(accessibilityCheckerURL, '_blank');
        }
        $.extend(item.exploreFeaturesProperties, configuration);
        Object.defineProperty(item.exploreFeaturesProperties, 'minWidthTabPanel', {
            get: function() {
                return configuration.exploreFeatures() ? (340 + 400) : 400;
            }
        });
        item.exploreFeaturesProperties.exploreFeatures(configuration.exploreFeatures())
    }
    return item;
}

function customizeElements(s, e) {
    const panel = e.GetById('dxrd-right-panel-template-base');
    panel.templateName = 'dxrd-right-panel-template-base-customized';
    configuration.exploreFeatures.subscribe(newVal => {
        var zoom = configuration.zoom() / 100;
        var oldWidth = panel.model.tabPanel.width || 340 * zoom;
        var maxWidth = 1000;
        if(newVal)
            panel.model.tabPanel.width = Math.min(maxWidth, oldWidth + (400 * zoom));
        else if(!newVal && oldWidth >= 740)
            panel.model.tabPanel.width = oldWidth - (400 * zoom);
    });
    configuration.exploreFeatures.valueHasMutated();
    setObjectModel(panel.model.__viewModel);
    [configuration.zoom, configuration.isContrast].forEach(function(obs) {
        obs.subscribe(function() { setTimeout(applyChanges); });
    })
}
