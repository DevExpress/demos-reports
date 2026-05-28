var DYNAMIC_REPORTDESIGNER_STYLES_ID = "dynamic-reportdesigner-styles";
var MODIFIED_REPORTDESIGNER_STYLES_ID = "modified-reportdesigner-styles";
var DYNAMIC_DEVEXTREME_STYLES_ID = "dynamic-devextreme-styles";
var MODIFIED_DEVEXTREME_STYLES_ID = "modified-devextreme-styles";

DataHelper = (function() {
    function DataHelper() {
        var that = this;
        this.selectedThemeId = ko.observable(1);
        this.currentColorScheme = ko.observable(DevExpress.ui.themes.current().split(".")[1]);
        this.getColor = function(color) {
            const colorsScheme = DataHelper.colorsScheme[that.currentColorScheme()] || DataHelper.colorsScheme["light"];
            return colorsScheme[color];
        }
    }

    DataHelper.colorsScheme = {
        "light": {
            "@color1": "#FFFFFF",
            "@color2": "#F5F5F5",
            "@color3": "#E6E6E6",
            "@color4": "#DDDDDD",
            "@color5": "#AAAAAA",
            "@color6": "#606060",
            "@color7": "#525252",
            "@color8": "#333333",
            "@color9": "#337AB7"
        },
        "dark": {
            "@color1": "#343434",
            "@color2": "#2A2A2A",
            "@color3": "#444444",
            "@color4": "#4D4D4D",
            "@color5": "#808080",
            "@color6": "#B1B1B1",
            "@color7": "#C8C8C8",
            "@color8": "#DEDEDE",
            "@color9": "#1CA8DD"
        },
        "softblue": {
            "@color1": "#FFFFFF",
            "@color2": "#FAFAFA",
            "@color3": "#F5F5F5",
            "@color4": "#DEE1E3",
            "@color5": "#92BCD7",
            "@color6": "#5580A4",
            "@color7": "#386086",
            "@color8": "#333333",
            "@color9": "#7AB8EB"
        },
        "darkmoon": {
            "@color1": "#465672",
            "@color2": "#42516C",
            "@color3": "#4E607F",
            "@color4": "#596980",
            "@color5": "#8E9CB1",
            "@color6": "#AFBBCD",
            "@color7": "#D8DDE4",
            "@color8": "#FFFFFF",
            "@color9": "#3DEBD3"
        },
        "darkviolet": {
            "@color1": "#1F1F2B",
            "@color2": "#17171F",
            "@color3": "#353540",
            "@color4": "#414152",
            "@color5": "#626978",
            "@color6": "#9FA4B0",
            "@color7": "#CFD1D6",
            "@color8": "#FFFFFF",
            "@color9": "#9C63FF"
        },
        "greenmist": {
            "@color1": "#F5F5F5",
            "@color2": "#EBEBEB",
            "@color3": "#E3E3E3",
            "@color4": "#DEDEDE",
            "@color5": "#A0BEC4",
            "@color6": "#5E7B82",
            "@color7": "#405B62",
            "@color8": "#28484F",
            "@color9": "#3CBAB2"
        },
        "carmine": {
            "@color1": "#FFFFFF",
            "@color2": "#FAFAFA",
            "@color3": "#F5F5F5",
            "@color4": "#DEE1E3",
            "@color5": "#A7AFB4",
            "@color6": "#80868A",
            "@color7": "#5B5D5F",
            "@color8": "#333333",
            "@color9": "#F05B41"
        }
    }
    DataHelper.generic_devExtreme_themes = [
        { themeId: 1, name: "generic", colorScheme: "light", text: "Light", group: "Generic" },
        { themeId: 2, name: "generic", colorScheme: "dark", text: "Dark", group: "Generic" },
        { themeId: 13, name: "generic", colorScheme: "carmine", text: "Carmine", group: "Generic" },
        { themeId: 14, name: "generic", colorScheme: "darkmoon", text: "Dark Moon", group: "Generic" },
        { themeId: 15, name: "generic", colorScheme: "softblue", text: "Soft Blue", group: "Generic" },
        { themeId: 16, name: "generic", colorScheme: "darkviolet", text: "Dark Violet", group: "Generic" },
        { themeId: 17, name: "generic", colorScheme: "greenmist", text: "Green Mist", group: "Generic" },
    ];
    DataHelper.generic_devExtreme_metadata = {
        color1: {
            "Name": "50. Background color",
            "Key": "@base-bg",
        },
        color3: {
            "Name": "70. Border color",
            "Key": "@base-border-color",
        },
        color8: {
            "Name": "10. Text color",
            "Key": "@base-text-color",
        },
        color9: {
            "Name": "60. Accent color",
            "Key": "@base-accent",
        },
    }

    DataHelper.prototype.modifiedMetaData = function(stringify) {
        if(stringify === void 0) { stringify = false; }
        var data = [];
        var isAccentAdd = false;
        $.each(DataHelper.generic_devExtreme_metadata, function(i, group) {
            if(group.isModified) {
                if(!isAccentAdd || group.Key !== "@base-accent") {
                    if(stringify) {
                        data.push(JSON.stringify({ "key": group.Key, "value": group.Value }));
                    } else {
                        data.push({ "key": group.Key, "value": group.Value });
                    }
                }
                if(group.Key === "@base-accent")
                    isAccentAdd = true;
            }
        });
        return data;
    }

    DataHelper.prototype.resetDevExtremeMetaData = function() {
        $.each(DataHelper.generic_devExtreme_metadata, function(i, elem) {
            delete elem.Value;
            delete elem.isModified;
        })
    }

    return DataHelper;
})();

CssTemplateLoader = function(dataHelper) {
    var cssBeautifier = { beautify: css_beautify };
    this._dataHelper = dataHelper;

    this.applyInlineStyles = function(dinamicCss, applyDevExtreme, theme, colorScheme, changeTheme) {
        var d = $.Deferred();
        var promises = [],
            that = this;
        if(applyDevExtreme)
            promises.push(that.load(theme, colorScheme, changeTheme));
        $.when.apply($.when, promises).done(function() {
            that.createDynamicStyles(dinamicCss, changeTheme ? DYNAMIC_REPORTDESIGNER_STYLES_ID : MODIFIED_REPORTDESIGNER_STYLES_ID, "body");
            d.resolve();
        });
        return d.promise();
    };

    this.load = function(theme, colorScheme, changeTheme) {
        var that = this;
        var d = $.Deferred();
        var variables = this._dataHelper.modifiedMetaData();
        var modifyVars = {};
        variables.forEach(function(group) {
            modifyVars[group.key.replace("@", "")] = group.value;
        });

        var lesspromises = [];
        if(changeTheme || variables.length) {
            lesspromises.push(that._loadLess(theme, colorScheme));
            //} else if(variables.length) {
            // Object.keys(modifyVars).forEach(function(modifyVarKey) {
            //     lesspromises.push(that._loadLess("generic-variable", modifyVarKey, true));
            // });
        }

        $.when.apply($.when, lesspromises).then(function() {
            var allLess = [];
            for(var _i = 0; _i < arguments.length; _i++) {
                allLess[_i] = arguments[_i];
            }
            var defCss = $.Deferred();
            var csspromises = allLess.map(function(less) { return that.compileLess(less, modifyVars); });
            $.when.apply($.when, csspromises).then(function() {
                var allCss = [];
                for(var _i = 0; _i < arguments.length; _i++) {
                    allCss[_i] = arguments[_i];
                }
                var inlineCss = "";
                allCss.forEach(function(css) {
                    inlineCss += css;
                })
                that.createDynamicStyles(inlineCss, changeTheme ? DYNAMIC_DEVEXTREME_STYLES_ID : MODIFIED_DEVEXTREME_STYLES_ID, "head");
                d.resolve(inlineCss);
            });
        });

        return d.promise();
    };
    this.compileLess = function(less, modifyVars) {
        var d = new $.Deferred();
        window.less.render(less, {
            modifyVars: modifyVars,
            math: 'always',
            logLevel: 2  // Errors only (suppress warnings)
        }).then(function(output) {
            d.resolve(output.css);
        }, function(error) {
            console.error(error.message);
            d.reject();
        });

        return d.promise();
    }
    this._loadLess = function(theme, colorScheme, customCallback) {
        if(customCallback === void 0) { customCallback = false; }
        var themeName = (theme ? theme + "-" : "");
        return this._loadLessByFileName(LESS_RESOURCE_PATH + "theme-builder-" + themeName + colorScheme + ".less.js", customCallback ? colorScheme.replace(/-/g, "") : "");
    };
    this._loadLessByFileName = function(fileName, subname) {
        var d = $.Deferred();
        window["lessTemplateLoadedCallback" + subname] = function(less) {
            d.resolve(less);
        };
        $.ajax({
            url: fileName,
            dataType: "jsonp",
            crossDomain: true,
            jsonpCallback: "lessTemplateLoadedCallback" + subname
        });
        return d.promise();
    }
    this._makeInfoHeader = function() {
        var generatedBy = "* Generated by the DevExpress Reporting Demo",
            link = "* " + window.location.origin + window.location.pathname;

        return ["/**", generatedBy, link, "*/"].join("\n");
    };

    this.createDynamicStyles = function(css, id, position) {
        $("#" + id).remove();
        $("<style>" + css + "</style>")
            .attr("type", "text/css")
            .attr("id", id)
            .appendTo(position);
    };

    this._getModifiedCss = function(id) {
        var css = this._makeInfoHeader();
        css += $("#" + id).html();
        css = cssBeautifier.beautify(css);
        return css;
    }
    this._getBuildDate = function() {
        return DevExpress.localization.date.format(new Date(), "M/d/yyyy");
    }
    this._metadataVersion = DevExpress.VERSION || "18.2.1";
    this._getModifiedMeta = function() {
        var items = "",
            themeId = "\"themeId\": \"" + this._dataHelper.selectedThemeId() + "\"",
            date = "\"date\": \"" + this._getBuildDate() + "\"",
            hue = "\"hue\": null",
            version = "\"version\": \"" + this._metadataVersion + "\"";

        if(this._dataHelper.modifiedMetaData().length)
            items = "\"items\": [" + this._dataHelper.modifiedMetaData(true).join(",\n") + "]";

        return "{" + [version, date, themeId, hue, items].join(",\n") + "}";
    }
}

function _customizerightPanel(part) {
    var _dataHelper = new DataHelper();
    var _cssTemplateLoader = new CssTemplateLoader(_dataHelper);
    var isResetValue = false;
    var createCustomization = function(_self) {
        _self.actions = [];
        _self.getInfo().forEach(function(info) {
            _self[info.propertyName].subscribe(function(value) {
                if(!isResetValue) {
                    if(info.subcribeDevextreame) {
                        DataHelper.generic_devExtreme_metadata[info.propertyName].Value = ko.unwrap(value);
                        DataHelper.generic_devExtreme_metadata[info.propertyName].isModified = true;
                    }
                    colorSchemeTab.generateDynamicCss();
                }
            });
        });
        _self.resetValue = function(propertyName) {
            _self[propertyName](ko.unwrap(_self.getPropertyDefaultValue(propertyName)));
        };
        _self.actions.push({ action: _self.resetValue, title: "Reset", visible: ko.observable(true) });
        return _self;
    }

    var colorBoxEditor = { header: "dx-theme-colorbox" };
    /*
     * Customiztion by groups 
     * Advansed mode
    
        var backgroundHighlightedColorSchemeOptionsSerializationInfo = function(getColor) {
            return [
                { propertyName: "backgroundHover", modelName: "@BackgroundHover", displayName: "Hovered State Color", className: ".dxd-back-highlighted:hover", defaultVal: getColor("@color2"), editor: colorBoxEditor },
                { propertyName: "backgroundSelected", modelName: "@BackgroundSelected", displayName: "Selected State Color", className: ".dxd-back-highlighted.dxd-state-selected, .dxd-back-highlighted.dxd-state-selected:hover", defaultVal: getColor("@color4"), editor: colorBoxEditor },
                { propertyName: "backgroundActive", modelName: "@BackgroundActive", displayName: "Active State Color", className: ".dxd-back-highlighted.dxd-state-active", defaultVal: getColor("@color5"), editor: colorBoxEditor },
                { propertyName: "backgroundHoveredActive", modelName: "@BackgroundHoveredActive", displayName: "Hovered Active State Color", className: ".dxd-back-highlighted.dxd-state-active:hover", defaultVal: getColor("@color6"), editor: colorBoxEditor },
                { propertyName: "backgroundHoveredContrast", modelName: "@BackgroundHoveredContrast", displayName: "Hovered Contrast State Color", className: ".dxd-back-contrast .dxd-back-highlighted:hover, .dxd-back-contrast .dxd-back-highlighted.dxd-state-active", defaultVal: getColor("@color7"), editor: colorBoxEditor },
                { propertyName: "backgroundHoveredButton", modelName: "@BackgroundHoveredButton", displayName: "Hovered Button Color", className: ".dxd-back-highlighted.dxd-state-normal:hover", defaultVal: getColor("@color4"), editor: colorBoxEditor },
            ];
        }
    
        var backgroundColorSchemeOptionsSerializationInfo = function(getColor) {
            return [
                { propertyName: "backgroundPrimary", modelName: "@BackgroundPrimary", displayName: "Primary Color 1", className: ".dxd-back-primary", subcribeDevextreame: true, defaultVal: getColor("@color2"), editor: colorBoxEditor },
                { propertyName: "backgroundPrimary2", modelName: "@BackgroundPrimary2", displayName: "Primary Color 2", className: ".dxd-back-primary2", defaultVal: getColor("@color1"), editor: colorBoxEditor },
                { propertyName: "backgroundSecondary", modelName: "@BackgroundSecondary", displayName: "Secondary Color", className: ".dxd-back-secondary", defaultVal: getColor("@color3"), editor: colorBoxEditor },
                { propertyName: "backgroundContrast", modelName: "@BackgroundContrast", displayName: "Contrast Color", className: ".dxd-back-contrast", defaultVal: getColor("@color6"), editor: colorBoxEditor },
                { propertyName: "backgroundAccent", modelName: "@BackgroundAccent", displayName: "Accent Color", className: ".dxd-back-accented", subcribeDevextreame: true, defaultVal: getColor("@color9"), editor: colorBoxEditor },
                { propertyName: "backgroundHighlighted", modelName: "BackgroundHighlighted", displayName: "Highlighted Colors", styleVariable: "background-color", info: backgroundHighlightedColorSchemeOptionsSerializationInfo(getColor), editor: DevExpress.JS.Widgets.editorTemplates.objecteditor }
            ];
        }
    
        var iconsColorSchemeOptionsSerializationInfo = function(getColor) {
            return [
                { propertyName: "iconsPrimary", modelName: "@IconsPrimary", displayName: "Fill Color", className: ".dxd-icon-fill", defaultVal: getColor("@color6"), editor: colorBoxEditor },
                { propertyName: "iconsContrast", modelName: "@IconsContrast", displayName: "Contrast Fill Color", className: ".dxd-back-contrast .dxd-icon-fill", defaultVal: getColor("@color5"), editor: colorBoxEditor },
                { propertyName: "iconsActive", modelName: "@IconsActive", displayName: "Active State Fill Color", className: ".dxd-state-active .dxd-icon-fill", defaultVal: getColor("@color1"), editor: colorBoxEditor },
            ];
        }
    
        var textColorSchemeOptionsSerializationInfo = function(getColor) {
            return [
                { propertyName: "textPrimary", modelName: "@TextPrimary", displayName: "Primary Color", className: ".dxd-text-primary", subcribeDevextreame: true, defaultVal: getColor("@color8"), editor: colorBoxEditor },
                { propertyName: "textInfo", modelName: "@TextInfo", displayName: "Info Color", className: ".dxd-text-info", defaultVal: getColor("@color7"), editor: colorBoxEditor },
                { propertyName: "textAccent", modelName: "@TextAccent", displayName: "Accent Color", className: ".dxd-text-accented", subcribeDevextreame: true, defaultVal: getColor("@color9"), editor: colorBoxEditor },
                { propertyName: "textActive", modelName: "@TextActive", displayName: "Active State Color", className: ".dxd-state-active .dxd-text-primary", defaultVal: getColor("@color1"), editor: colorBoxEditor },
            ];
        }
    
        var bordersColorSchemeOptionsSerializationInfo = function(getColor) {
            return [
                { propertyName: "borderPrimary", modelName: "@BorderPrimary", displayName: "Primary Color", className: ".dxd-border-primary", subcribeDevextreame: true, defaultVal: getColor("@color3"), editor: colorBoxEditor },
                { propertyName: "borderSecondary", modelName: "@BorderSecondary", displayName: "Secondary Color", className: ".dxd-border-secondary", defaultVal: getColor("@color4"), editor: colorBoxEditor },
                { propertyName: "borderAccent", modelName: "@BorderAccent", displayName: "Accent Color", className: ".dxd-border-accented", subcribeDevextreame: true, defaultVal: getColor("@color9"), editor: colorBoxEditor },
            ];
        }
    
        var colorSchemeOptionsSerializationInfo = function(getColor) {
            return [
                { propertyName: "background", modelName: "Background", displayName: "Background", styleVariable: "background-color", info: backgroundColorSchemeOptionsSerializationInfo(getColor), editor: DevExpress.JS.Widgets.editorTemplates.objecteditor },
                { propertyName: "icons", modelName: "Icons", displayName: "Icons", styleVariable: "fill", info: iconsColorSchemeOptionsSerializationInfo(getColor), editor: DevExpress.JS.Widgets.editorTemplates.objecteditor },
                { propertyName: "text", modelName: "Text", displayName: "Text", styleVariable: "color", info: textColorSchemeOptionsSerializationInfo(getColor), editor: DevExpress.JS.Widgets.editorTemplates.objecteditor },
                { propertyName: "borders", modelName: "Borders", displayName: "Borders", styleVariable: "border-color", info: bordersColorSchemeOptionsSerializationInfo(getColor), editor: DevExpress.JS.Widgets.editorTemplates.objecteditor },
            ];
        }
     */

    /*
     * Middle mode 9 colors
     */
    var displayNames = [
        "Background Color 1",
        "Background Color 2",
        "Background Color 3",
        "Hover State Color",
        "Secondary Icon Color",
        "Primary Icon Color",
        "Active State Color",
        "Text Color",
        "Accent Color"
    ];
    var classMatrix = [
        ".dx-designer-viewport .dxd-state-active .dxd-icon-fill {fill: {0};} .dx-designer-viewport .dxd-back-contrast .dxd-back-highlighted:hover .dxd-icon-fill {fill: {0};} .dx-designer-viewport .dxrd-scripts-editor.ace_editor, .dx-designer-viewport .dx-sql_editor.ace_editor, .dx-designer-viewport .dx-expressioneditor-textarea.ace_editor, .dx-designer-viewport .dx-filtereditor-ace.ace_editor {background-color: {0};} .dx-designer-viewport .dxd-state-active .dxd-text-primary {color: {0};} .dx-designer-viewport .dxd-back-primary2 {background-color: {0};} .dx-designer-viewport .dxrd-navigation-panel-wrapper .dx-tab {background-color: {0};} .dx-designer-viewport .dxrd-navigation-panel-wrapper .dx-tab.dx-tab-selected {color: {0};} .dx-designer-viewport .dxrd-navigation-panel-wrapper .dx-tab.dx-tab-selected .dx-icon {color: {0};} .dx-designer-viewport .dxd-state-active .dxd-text-primary, .dx-designer-viewport .dxd-back-contrast .dxd-text-primary {color: {0};}",
        ".dx-designer-viewport .dxrd-scripts-editor.ace_editor .ace_gutter, .dx-designer-viewport .dx-sql_editor.ace_editor .ace_gutter, .dx-designer-viewport .dx-expressioneditor-textarea.ace_editor .ace_gutter, .dx-designer-viewport .dx-filtereditor-ace.ace_editor .ace_gutter {background-color: {0};} .dx-designer-viewport .dxd-back-primary {background-color: {0};} .dx-designer-viewport .dxd-back-highlighted:hover:not(.dxd-state-no-hover) {background-color: {0};} .dx-designer-viewport .dxrd-navigation-panel-wrapper .dx-tab:hover {background-color: {0};}",
        ".dx-designer-viewport .dxd-border-primary {border-color: {0};} .dx-designer-viewport .dxd-back-secondary {background-color: {0};} .dx-designer-viewport .dxrd-scripts-editor.ace_editor .ace_gutter, .dx-designer-viewport .dx-sql_editor.ace_editor .ace_gutter, .dx-designer-viewport .dx-expressioneditor-textarea.ace_editor .ace_gutter, .dx-designer-viewport .dx-filtereditor-ace.ace_editor .ace_gutter {border-color: {0};} .dx-designer-viewport .dxd-scrollbar-color {scrollbar-face-color: {0};   -ms-scrollbar-face-color: {0};} .dx-designer-viewport .dxd-scrollbar-color ::-webkit-scrollbar-corner {background-color: {0};} .dx-designer-viewport .dxd-scrollbar-color ::-webkit-scrollbar {background-color: {0};} .dx-designer-viewport .dxrd-scripts-editor.ace_editor .ace_gutter, .dx-designer-viewport .dx-sql_editor.ace_editor .ace_gutter, .dx-designer-viewport .dx-expressioneditor-textarea.ace_editor .ace_gutter, .dx-designer-viewport .dx-filtereditor-ace.ace_editor .ace_gutter {border-color: {0};}",
        ".dx-designer-viewport .dxd-border-secondary {border-color: {0};} .dx-designer-viewport .dxd-back-highlighted.dxd-state-normal:hover:not(.dxd-state-no-hover) {background-color: {0};} .dx-designer-viewport .dxd-back-highlighted.dxd-state-selected, .dx-designer-viewport .dxd-back-highlighted.dxd-state-selected:hover {background-color: {0};}",
        ".dx-designer-viewport .dxd-back-contrast .dxd-icon-fill {fill: {0};} .dx-designer-viewport .dxd-back-highlighted.dxd-state-active {background-color: {0};} .dx-designer-viewport .dxd-scrollbar-color {scrollbar-track-color: {0};   -ms-scrollbar-track-color: {0};} .dx-designer-viewport .dxd-scrollbar-color ::-webkit-scrollbar-thumb {background-color: {0};} .dx-designer-viewport .dxd-qb-relationship-line-color {stroke: {0};} .dx-designer-viewport .dxrd-navigation-panel-wrapper .dx-tab {border-color: {0};} .dx-designer-viewport .dxrd-navigation-panel-wrapper .dx-tab.dx-tab-selected, .dx-designer-viewport .dxrd-navigation-panel-wrapper .dx-tab.dx-tab-selected:hover {background-color: {0};} .dx-designer-viewport .dxd-back-contrast .dxd-border-primary { border-color: {0};} .dx-designer-viewport .dxd-back-secondary-invariant .dxd-border-primary { border-color: {0};}",
        ".dx-designer-viewport .dxd-back-contrast {background-color: {0};} .dx-designer-viewport .dxd-back-highlighted.dxd-state-active:hover:not(.dxd-state-no-hover) {background-color: {0};} .dx-designer-viewport .dxd-scrollbar-color {scrollbar-arrow-color: {0};   -ms-scrollbar-arrow-color: {0};}",
        ".dx-designer-viewport .dxrd-navigation-panel-wrapper .dx-icon {color: {0};} .dx-designer-viewport .dxd-text-info {color: {0};} .dx-designer-viewport .dxd-back-contrast .dxd-back-highlighted:hover, .dx-designer-viewport .dxd-back-contrast .dxd-back-highlighted.dxd-state-active {background-color: {0};}",
        ".dx-designer-viewport .dxd-text-primary {color: {0};} .dx-designer-viewport .dxrd-navigation-panel-wrapper .dx-tab {color: {0};} .dx-designer-viewport .dxrd-scripts-editor.ace_editor .ace_gutter, .dx-designer-viewport .dx-sql_editor.ace_editor .ace_gutter, .dx-designer-viewport .dx-expressioneditor-textarea.ace_editor .ace_gutter, .dx-designer-viewport .dx-filtereditor-ace.ace_editor .ace_gutter {color: {0};}",
        ".dx-designer-viewport .dxd-border-accented {border-color: {0};} .dx-designer-viewport .dxd-text-accented {color: {0};} .dx-designer-viewport .dxd-back-accented {background-color: {0};} .dx-designer-viewport .dxdd-connector.dxd-state-selected .dxd-qb-relationship-line-color {stroke: {0};} .dx-designer-viewport .dxd-icon-accented .dxd-icon-fill, .dx-designer-viewport .dxd-icon-highlighted:hover .dxd-icon-fill {fill: {0};}"
    ];

    var themesSlpit = {
        darkThemeIds: [2, 14, 16],
        lightThemeIds: [1, 13, 15, 17]
    };

    var extendClassMatrix = function(themeId) {
        var extendedMatrix = classMatrix.slice();
        var backPrimaryInvariant = " .dx-designer-viewport .dxd-back-primary-invariant {background-color: {0};}";
        var backPrimary2Invariant = " .dx-designer-viewport .dxd-back-primary2-invariant {background-color: {0};}";
        var backSecondaryInvariant = " .dx-designer-viewport .dxd-back-secondary-invariant {background-color: {0};}";
        var iconFill = " .dx-designer-viewport .dxd-icon-fill {fill: {0};}";

        if(themesSlpit.darkThemeIds.indexOf(themeId) != -1) {
            extendedMatrix[0] += backPrimary2Invariant;
            extendedMatrix[1] += backPrimaryInvariant;
            extendedMatrix[2] += backSecondaryInvariant;
            extendedMatrix[6] += iconFill;
        } else {
            extendedMatrix[0] += backPrimary2Invariant;
            extendedMatrix[1] += backSecondaryInvariant;
            extendedMatrix[2] += backPrimaryInvariant;
            extendedMatrix[5] += iconFill;
        }
        return extendedMatrix;
    }

    var colorSchemeOptionsSerializationInfo = function(getColor) {
        var extendedMatrix = extendClassMatrix(_dataHelper.selectedThemeId());
        var info = extendedMatrix.map(function(classNames, index) {
            var classNumber = index + 1;
            var name = "color" + classNumber;
            var subcribeDevextreame = !!DataHelper.generic_devExtreme_metadata[name] ? true : false;
            return {
                propertyName: name,
                modelName: "@Color" + classNumber,
                displayName: displayNames[index] || "Color" + classNumber,
                className: classNames,
                subcribeDevextreame: subcribeDevextreame,
                defaultVal: getColor("@color" + classNumber),
                editor: colorBoxEditor
            }
        });
        return info;
    }
    var colorSchemeGridModel = function() {
        return createCustomization((function(object) {
            new DevExpress.Analytics.Utils.ModelSerializer().deserialize(object, {}, colorSchemeOptionsSerializationInfo(_dataHelper.getColor));
            return object
        })({
            getInfo: function() { return colorSchemeOptionsSerializationInfo(_dataHelper.getColor); },
            getPropertyDefaultValue: function(name) {
                var info = this.getInfo().filter(function(info) { return info.propertyName === name; })[0];
                return ko.unwrap(info && new DevExpress.Analytics.Utils.ModelSerializer().deserializeProperty(info, {}));
            },
            isPropertyModified: function(name) {
                var defaultValue = ko.unwrap(this.getPropertyDefaultValue(name)), propertyValue = ko.unwrap(this[name]);
                return defaultValue !== propertyValue;
            },
            getActionClassName: function(propertyName) {
                return this.isPropertyModified(propertyName) ? "dxrd-editormenu-modified" : "";
            },
            getClassNameByName: function(propertyName) {
                return this.getInfo().filter(function(info) { return info.propertyName === name; })[0].className;
            },
            isDevExtremeProperty: function(propertyName) {
                return this.getInfo().filter(function(info) { return info.propertyName === name; })[0].subcribeDevextreame;
            },
            dxColorBoxCustomized: (data, root, element) => {
                return {
                    value: data.value,
                    dropDownOptions: {
                        container: ".dx-designer-viewport",
                        position: { at: 'left bottom', collision: 'flipfit flipfit', my: 'left top', boundary: root.getPopupContainer(element), of: element }
                    },
                    disabled: data.disabled,
                }
            }
        }))
    }
    var model = ko.observable(colorSchemeGridModel());
    colorSchemeGrid = new DevExpress.Analytics.Widgets.ObjectProperties(model);

    var colorSchemeTab = new DevExpress.Analytics.Utils.TabInfo({
        text: "Color Scheme Customization",
        template: "dx-colorScheme-tab",
        model: colorSchemeGrid,
        imageClassName: "colorScheme",
        imageTemplateName: "dx-theme-icon-palette"
    });
    var customConfirmOptions = function(title, message) {
        return {
            title: title,
            messageHtml: "<div class='color-scheme-popup-message-text'>" + message + "</div>",
            buttons: [{
                text: "Yes",
                onClick: function() {
                    return true
                }
            }, {
                text: "No",
                onClick: function() {
                    return false
                }
            }],
            popupOptions: {
                container: ".dx-designer-viewport"
            },
        }
    }

    colorSchemeTab.colorSchemeSource = {
        dataSource: new DevExpress.data.DataSource({
            store: DataHelper.generic_devExtreme_themes,
            key: "colorScheme"
        }),
        valueExpr: 'colorScheme',
        value: _dataHelper.currentColorScheme,
        dropDownOptions: {
            container: ".dx-designer-viewport"
        },
        displayExpr: 'text',
        itemTemplate: 'item',
        deferRendering: false,
        onValueChanged: function(data) {
            _dataHelper.selectedThemeId(data.component.option('selectedItem').themeId);
            var _changeColor = function() {
                showLoadPanel();
                model(colorSchemeGridModel());
                _dataHelper.resetDevExtremeMetaData();
                colorSchemeTab.generateDynamicCss(true);
                $("#" + MODIFIED_REPORTDESIGNER_STYLES_ID).remove();
                hideLoadPanel();
            }
            if(colorSchemeTab.isEditorsModified && colorSchemeTab.isEditorsModified()) {
                if(!data.jQueryEvent) return;
                return DevExpress
                    .ui
                    .dialog
                    .custom(customConfirmOptions("Are you sure you want to change the base color scheme?<br />All customization will be lost.", "Change Color Scheme" ))
                    .show()
                    .done(function(dialogResult) {
                        if(dialogResult) {
                            _changeColor();
                        } else {
                            _dataHelper.currentColorScheme(data.previousValue);
                        }
                    });
            } else {
                _changeColor();
            }
        }
    }

    colorSchemeTab.generateDynamicCss = function(changeTheme) {
        if(changeTheme === void 0) { changeTheme = false; }
        showLoadPanel();
        var applyDevExtreme = changeTheme;
        var dynamicCss = "";
        $("#" + MODIFIED_REPORTDESIGNER_STYLES_ID).remove();
        $("#" + MODIFIED_DEVEXTREME_STYLES_ID).remove();
        model().getInfo().forEach(function(property) {
            if(changeTheme || model().isPropertyModified(property.propertyName)) {
                dynamicCss += DevExpress.Analytics.Internal.formatUnicorn(property.className, model()[property.propertyName]());
            }
            if(property.subcribeDevextreame) {
                applyDevExtreme = true;
            }
        });
        _cssTemplateLoader.applyInlineStyles(dynamicCss, applyDevExtreme, "generic", _dataHelper.currentColorScheme(), changeTheme);
        hideLoadPanel();
    }
    colorSchemeTab.popupService = new DevExpress.Analytics.Internal.PopupService();
    colorSchemeTab.createEditorAddOn = function(editor) {
        var editorAddOn = new DevExpress.Analytics.Internal.EditorAddOn(editor, colorSchemeTab.popupService);
        editor._disposables.push(editorAddOn);
        return {
            templateName: 'dx-editor-addons',
            data: [editorAddOn]
        };
    }

    colorSchemeTab.saveCSSAs = function() {
        var color = _dataHelper.currentColorScheme();
        var zip = new JSZip();
        zip.file("reporting." + color + ".custom.css", _cssTemplateLoader._getModifiedCss(MODIFIED_REPORTDESIGNER_STYLES_ID));
        zip.file("devextreme." + color + ".custom.css", _cssTemplateLoader._getModifiedCss(MODIFIED_DEVEXTREME_STYLES_ID));
        zip.file("dx.themebuilder.metadata.json", _cssTemplateLoader._getModifiedMeta());
        zip.generateAsync({ type: "blob" }).then(function(content) {
            var fileName = [color, "customization", "zip"].join(".");
            DevExpress.clientExporter.fileSaver._saveBlobAs(fileName, "ZIP", content);
        });
    };

    colorSchemeTab.resetEditors = function() {
        return DevExpress.ui.dialog.custom(customConfirmOptions("Are you sure you want to reset all changes?", "Reset Changes")).show().done(function(dialogResult) {
            if(dialogResult) {
                showLoadPanel();
                isResetValue = true;
                model().getInfo().forEach(function(info) {
                    model().resetValue(info.propertyName);
                });
                isResetValue = false;
                _dataHelper.resetDevExtremeMetaData();
                colorSchemeTab.generateDynamicCss(true);
                hideLoadPanel();
            }
        });
    }
    colorSchemeTab.cssPopupOptions = {
        title: "Customization Results",
        visible: ko.observable(false),
        height: 450,
        width: 600,
        dragEnabled: true
    }
    colorSchemeTab.CSSContent = ko.observable();
    colorSchemeTab.CSSContentDevExtreme = ko.observable();
    colorSchemeTab.MetaDxContent = ko.observable();
    colorSchemeTab.tabPanelItems = [{
        "title": "Reporting Styles",
        "templateName": "dx-colorScheme-tab-item",
        "text": colorSchemeTab.CSSContent
    }, {
        "title": "DevExtreme Widgets Styles",
        "templateName": "dx-colorScheme-tab-item",
        "text": colorSchemeTab.CSSContentDevExtreme
    }, {
        "title": "Theme Builder Metadata",
        "templateName": "dx-colorScheme-dxMeta",
        "text": colorSchemeTab.MetaDxContent
    }];

    colorSchemeTab.showCSSPopup = function(content) {
        colorSchemeTab.cssPopupOptions.visible(true);
        colorSchemeTab.CSSContent(_cssTemplateLoader._getModifiedCss(MODIFIED_REPORTDESIGNER_STYLES_ID));
        colorSchemeTab.CSSContentDevExtreme(_cssTemplateLoader._getModifiedCss(MODIFIED_DEVEXTREME_STYLES_ID));
        colorSchemeTab.MetaDxContent(_cssTemplateLoader._getModifiedMeta());
    };

    colorSchemeTab.isEditorsModified = ko.computed(function() {
        return model().getInfo().some(function(info) { return model().isPropertyModified(info.propertyName); });
    }).extend({ throttle: 200 });

    part.viewModel.getModel().addTab(colorSchemeTab);
    part.viewModel.getModel().selectTab({ model: colorSchemeTab });
}

function customizeDesignerElements(s, e) {
    _customizerightPanel(e.GetById(DevExpress.Reporting.Designer.Utils.ReportDesignerElements.RightPanel));
}

function customizeViewerElements(s, e) {
    _customizerightPanel(e.GetById(DevExpress.Reporting.Viewer.PreviewElements.RightPanel));
}
