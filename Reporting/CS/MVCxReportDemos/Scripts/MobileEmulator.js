function createFrameEvent(frameWindow, frame, sourceEvent, newEventType) {
    var event = frameWindow.$.Event(sourceEvent),
        originalEvent = event.originalEvent,
        propNames = frameWindow.$.event.props.slice(),
        fitCoordinateToLimits = function (coordinate, startValue, length) {
            if (coordinate >= startValue) {
                var fittedValue = coordinate - startValue;
                return fittedValue > length ? length : fittedValue;
            } else {
                return 0;
            }
        };
    frameWindow.$.merge(propNames, frameWindow.$.event.mouseHooks.props);
    propNames.forEach(function (entry) {
        event[entry] = originalEvent[entry];
    });



    var framedX = fitCoordinateToLimits(originalEvent.clientX, frame.offsetLeft, frame.offsetWidth),
        framedY = fitCoordinateToLimits(originalEvent.clientY, frame.offsetTop, frame.offsetHeight);
    (['client', 'page', 'offset', 'screen']).forEach(function (entry) {
        event[entry + 'X'] = framedX;
        event[entry + 'Y'] = framedY;
    });

    var setTouches = function (event1) {
        event1.touches = [{}];
        (['client', 'page', 'offset', 'screen']).forEach(function (entry) {
            event1.touches[0][entry + 'X'] = framedX;
            event1.touches[0][entry + 'Y'] = framedY;
        });
    }

    setTouches(event);
    setTouches(event.originalEvent);

    return frameWindow.$.extend(event, {
        srcElement: frameWindow.document,
        currentTarget: frameWindow.document,
        target: frameWindow.document,
        view: frameWindow,
        type: newEventType
    });
}

document.onreadystatechange = function () {
    if (document.readyState == 'complete') {
        var frame = document.getElementById("ios");
        var frameWindow = frame.contentWindow;
        var target = frameWindow.$(".dxrd-preview-wrapper");
        var mousePressed = false;
        target.off("mousemove").on("mousemove", function (event) {
            if (event.buttons && !mousePressed) {
                mousePressed = true;
                target.trigger(createFrameEvent(frameWindow, frame, event, "touchstart"));
                target.trigger(createFrameEvent(frameWindow, frame, event, "touchmove"));
            } else if (event.buttons && mousePressed) {
                target.trigger(createFrameEvent(frameWindow, frame, event, "touchmove"));
            } else if (!event.buttons && mousePressed) {
                mousePressed = false;
                target.trigger(createFrameEvent(frameWindow, frame, event, "touchend"));
            }
        });
    }
}