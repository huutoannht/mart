var CONST_EMPTY_GUID = "00000000-0000-0000-0000-000000000000";
var POST_SHOW_LOADING = true;
var POST_BLOCK_UI = true;
var SCROLL_TO_INTERVAL = 100;
var DECIMAL_NUMBER = 2;
var JS_DATE_FORMAT_TO_CSHARP_DATE_FORMAT = "MM/DD/YYYY";
var AVARTAR_MAX_SIZE = {
    width: 500,
    height: 500
};

var DEFAULT_LAT = 10.8225352;
var DEFAULT_LNG = 106.7045979;
var DEFAULT_ZOOM = 12;

var ROW_SELECTED_CLASS = "row-selected";

var SPAN_REQUIRED_HTML = '<span class="red">&nbsp;*</span>';

function post(postUrl, data, doneFunc, alwaysFunc, doNotHideNotify) {
    ajax(postUrl, data, doneFunc, alwaysFunc, doNotHideNotify);
};

function ajax(url, data, doneFunc, alwaysFunc, doNotHideNotify) {
    
    if (!doNotHideNotify) {
        notify.hide();
    }
    if (POST_BLOCK_UI) blockUI();
    if (POST_SHOW_LOADING) showLoading();

    $.ajax({
            url: url,
            type: 'POST',
            data: JSON.stringify(data),
            contentType: 'application/json; charset=utf-8',
            headers: {
                "cache-control": "no-cache"
            },
            cache: false
        })
        .done(function (response) {
            if (doneFunc && $.isFunction(doneFunc)) {
                doneFunc(response);
            }
        })
        .always(function () {
            if (POST_BLOCK_UI) unblockUI();
            if (POST_SHOW_LOADING) hideLoading();

            POST_BLOCK_UI = true;
            POST_SHOW_LOADING = true;

            if (alwaysFunc && $.isFunction(alwaysFunc)) {
                alwaysFunc();
            }
        });
};

function ajaxFormFile(url, data, doneFunc, alwaysFunc, doNotHideNotify) {
    if (!doNotHideNotify) {
        notify.hide();
    }
    if (POST_BLOCK_UI) blockUI();
    if (POST_SHOW_LOADING) showLoading();

    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        contentType: false,
        processData: false,
        headers: {
            "cache-control": "no-cache"
        },
        cache: false
    })
    .done(function (response) {
        if (doneFunc && $.isFunction(doneFunc)) {
            doneFunc(response);
        }
    })
    .always(function () {
        if (POST_BLOCK_UI) unblockUI();
        if (POST_SHOW_LOADING) hideLoading();

        POST_BLOCK_UI = true;
        POST_SHOW_LOADING = true;

        if (alwaysFunc && $.isFunction(alwaysFunc)) {
            alwaysFunc();
        }
    });
};

function gotoUrl(url) {
    showLoading();
    top.location.href = url;
}

function showLoading() {
    var $loadingParent = $('#loadingParent');
    $loadingParent.css("left", ($(document).width() / 2 - $loadingParent.width() / 2) + 50);

    $loadingParent.css("top", ($(window).height() / 2 - $loadingParent.height() / 2) + "px");
    //$loadingParent.css("top", "150px");

    $loadingParent.show();
}

function hideLoading() {
    $('#loadingParent').hide();
}

function blockUI() {
    $.blockUI({
        message: '',
        overlayCSS: {
            cursor: 'default',
            backgroundColor: "transparent"
        },
        baseZ: 20000
    });
}

function unblockUI() {
    $.unblockUI();
}

function showNotifyAlert(html) {
    var loadingParent = $('#divNotifyAlert');
    loadingParent.css("left", ($(document).width() / 2 - loadingParent.width() / 2) + 50);

    loadingParent.css("top", "50px");

    $("#divNotifyAlertTitle").html(html);

    loadingParent.fadeIn("slow");

    setTimeout(function() {
        loadingParent.fadeOut("slow");
    }, 2000);
}

function _scrollTop() {
    $(window).scrollTop(0);
}

function _scrollTo(selector) {
    if (selector.length == 0)return;
    $.scrollTo(selector, SCROLL_TO_INTERVAL);
}

function _scrollToValidation($form) {
    _scrollTo($form.parent());
}

function compareTime(fromTime, toTime) {
    var fromdt = "2013/05/29 " + fromTime;
    var todt = "2013/05/29 " + toTime;
    var from = Date.parse(fromdt);
    var to = Date.parse(todt);
    if (from < to) {
        return -1;
    }
    else if (from == to) {
        return 0;
    } else {
        return 1;
    }
}

function resetValidateForm($form) {  // 'this' is the form element
    $form.data("validator").resetForm();

    $form.find(".input-validation-error")
        .removeClass("input-validation-error")
        .removeData("unobtrusiveContainer")
        .find(">*")  // If we were using valmsg-replace, get the underlying error
            .removeData("unobtrusiveContainer");

    $form.find(".validation-summary-errors")
        .addClass("validation-summary-valid")
        .removeClass("validation-summary-errors").find("li").hide();

    $form.find(".field-validation-error")
        .addClass("field-validation-valid")
        .removeClass("field-validation-error")
        .removeData("unobtrusiveContainer")
        .find(">*")  // If we were using valmsg-replace, get the underlying error
            .removeData("unobtrusiveContainer");
}

function setValidateErrorStyle($form) {
    $form.find("div[data-valmsg-summary]").addClass("validation-summary-errors").removeClass("validation-summary-valid");
}

function hideValidateMessage($form) {
    $form.find(".validation-summary-errors")
        .addClass("validation-summary-valid")
        .removeClass("validation-summary-errors").find("li").hide();

    $form.find("input.input-validation-error").each(function() {
        $(this).removeClass("input-validation-error");
    });

    $form.find("span.field-validation-error").each(function() {
        $(this).removeClass("field-validation-error").hide();
    });
}

function ckeditorInserText(target, editorName) {
    var instant = CKEDITOR.instances[editorName];
    if (typeof instant != "undefined" && instant != null) {
        instant.focus();
        instant.insertText($(target).text());
    } else {
        if ($("#" + editorName).length > 0) {
            var val = $("#" + editorName).val();
            $("#" + editorName).val(val + $(target).text()).focus();
        }
    }
}

function toStr(val) {
    if (typeof val == "undefined" || val == null) return "";
    return val.toString();
}

function toCultureNumber(val) {
    if (typeof (val) == "undefined") return 0;
    if (typeof (val) == "number") {
        return val.toString().replace('.', NumberDecimalSeparator);
    }
    if (typeof (val) != "string") return 0;
    if (typeof (val) == "string") {
        return val.replace('.', NumberDecimalSeparator);
    }
    return 0;
}

function toUnCultureNumber(val) {
    var str = val.replaceAll(NumberGroupSeparator, '');

    str = str.replaceAll(NumberDecimalSeparator, '.');

    var num = parseFloat(str);

    return isNaN(num) ? 0 : num;
}

var fileInputSettings = {
    previewFileType: "image",
    browseClass: "btn btn-success",
    browseLabel: " " + PICKUP_IMAGE_CAPTION,
    browseIcon: '<i class="glyphicon glyphicon-picture"></i>',
    removeClass: "btn btn-danger",
    removeLabel: " " + DELETE_CAPTION,
    removeIcon: '<i class="glyphicon glyphicon-trash"></i>',
    uploadClass: "btn btn-info",
    uploadIcon: '<i class="glyphicon glyphicon-upload"></i>',
    showCaption: false,
    showPreview: true,
    maxFileSize: 5000,
    showUpload: false,
    allowedFileExtensions: AllowedImageFileExtensions
};

