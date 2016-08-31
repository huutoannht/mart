var NOTIFY_CONTAINER_ID = "#divNotifyContainer";
(function (notify, $) {

    var settings = {};

    notify.init = function (options) {
        settings = $.extend(settings, options);
    };

    notify.error = function (message, findScope) {
        var id = "div#notifyError";
        notify.go(id, message, findScope);
    };

    notify.warning = function (message, findScope) {
        var id = "div#notifyWarning";

        notify.go(id, message, findScope);
    };

    notify.success = function (message, findScope) {
        var id = "div#notifySuccess";

        notify.go(id, message, findScope);
    };

    notify.info = function (message, findScope) {
        var id = "div#notifyInformation";

        notify.go(id, message, findScope);
    };

    notify.go = function (id, message, findScope) {
        var div;
        if (typeof findScope != "undefined" && findScope != null) {
            div = findScope.find(id + " div");
        } else {
            div = $(id + " div");
        }

        notify.hide();
        var arr = parseHTML(message);
        if (arr == null) return;
        appendMessage(div, arr);
        slide(div.parents(id + ":first"));
    };

    notify.hide = function () {
        $("#divNotifyContainer").hide();
        $("#divNotifyContainer div[content]").empty();
        $("div#notifyError").hide();
        $("div#notifyInformation").hide();
        $("div#notifySuccess").hide();
        $("div#notifyWarning").hide();
    };

    function appendMessage(div, arr) {
        if (arr == null) return;
        $(arr).each(function () {
            $(this).appendTo(div);
        });
        div.parent().show();
    }

    function parseHTML(str) {
        if (str == null) {
            return null;
        }

        str = String(str).replace(/\n/g, '<br/>');
        var arr = $.parseHTML(str);
        return arr;
    }

    function slide(selector) {
        $(selector).parents("#divNotifyContainer:first").show();
        $(selector).slideDown("normal");
    }

}(window.notify = window.notify || {}, jQuery));


