function getDateTimeOptions(tbx) {
    tbx = $(tbx);

    var obj = {
        pickTime: false,
        format: DATE_FORMAT_JS,
        useCurrent: false,
        language: DATEPICKER_LANGUAGE
    };

    return obj;
}

function initDateTimeTextBoxValue(tbx) {
    tbx = $(tbx);
    var val = tbx.val();
    if (val.indexOf('01/01/0001') >= 0 || val.indexOf('1/1/0001') >= 0) {
        tbx.val('');
    }
}

function setupDatePicker(scope) {
    if (scope.length == 0 || !scope.hasClass("date-picker")) {
        scope = scope.find(".date-picker");
    }

    scope.each(function () {
        initDateTimeTextBoxValue(this);
        $(this).datetimepicker(getDateTimeOptions(this));
    });

    scope.each(function () {
        var tbx = this;
        $(this).parent().find("span.glyphicon").click(function () {
            $(tbx).focus();
        });

        $(this).parent().find("span.input-group-addon").click(function () {
            $(tbx).focus();
        });

        $(this).parent().find("i").parent().find("input").click(function () {
            $(tbx).focus();
        });
    });
}

function setupDateRange(scope) {

    if (scope.length == 0 || !scope.hasClass("date-range")) {
        scope = scope.find(".date-range");
    }

    scope.each(function() {
        var eventStartDate = $(this).find('.from-date');
        var eventEndDate = $(this).find('.to-date');

        eventStartDate.each(function () {
            initDateTimeTextBoxValue(this);
            $(this).datetimepicker(getDateTimeOptions(this));
        });

        eventEndDate.each(function () {
            initDateTimeTextBoxValue(this);
            $(this).datetimepicker(getDateTimeOptions(this));
        });

        eventStartDate.on("dp.change", function (e) {
            eventEndDate.data("DateTimePicker").setMinDate(e.date);
        });

        eventEndDate.on("dp.change", function (e) {
            eventStartDate.data("DateTimePicker").setMaxDate(e.date);
        });

        eventStartDate.prev().click(function() {
            eventStartDate.focus();
        });

        eventEndDate.next().click(function () {
            eventEndDate.focus();
        });
    });
}

function setupTimePicker(scope) {
    if (scope.length == 0 || !scope.hasClass("timepicker")) {
        scope = scope.find(".timepicker");
    }

    scope.datetimepicker({
        pickDate: false
    });

    scope.each(function () {
        var tbx = this;
        $(this).parent().find("span").click(function () {
            $(tbx).focus();
        });
    });
}

function setupNumberInteger(scope) {
    if (scope.length == 0 || !scope.hasClass("number-integer")) {
        scope = scope.find(".number-integer");
    }
    scope.numeric({ decimal: NumberDecimalSeparator, negative: true });
}

function setupPositiveInteger(scope) {
    if (scope.length == 0 || !scope.hasClass("number-positive-integer")) {
        scope = scope.find(".number-positive-integer");
    }
    scope.numeric({ decimal: NumberDecimalSeparator, negative: false });
}

function setupPhoneMask(scope) {
    if (scope.length == 0 || !scope.hasClass("phone")) {
        scope = scope.find(".phone");
    }
    scope.inputmask("999-999-9999", { placeholder: "_" });
}

function setupCurrency(scope, mDec, vMax) {
    if (typeof (mDec) == "undefined") {
        mDec = DECIMAL_NUMBER;
    }

    if (scope.length == 0) return;

    if (scope.tagName() != "input") {
        if (scope.length == 0 || !scope.hasClass("currency")) {
            scope = scope.find(".currency");
        }
    }

    scope.each(function() {
        if ($(this).val().toString().isEmpty()) {
            $(this).val(0);
        }
    });

    var option = {
        aSep: NumberGroupSeparator,
        aDec: NumberDecimalSeparator,
        mDec: mDec
    };

    if (typeof (vMax) != "undefined") {
        option.vMax = vMax;
    }

    scope.autoNumeric('init', option);
}

function setupQuantity(scope) {
    if (scope.length == 0) return;

    if (scope.tagName() != "input") {
        if (scope.length == 0 || !scope.hasClass("quantity")) {
            scope = scope.find(".quantity");
        }
    }
    setupCurrency(scope, 0, 32767);
}

function setupSmallQuantity(scope) {
    if (scope.length == 0) return;

    if (scope.tagName() != "input") {
        if (scope.length == 0 || !scope.hasClass("small-quantity")) {
            scope = scope.find(".small-quantity");
        }
    }

    scope.each(function () {
        if ($(this).val().isEmpty()) {
            $(this).val(0);
        }
    });

    var option = {
        aSep: "",
        aDec: ".",
        mDec: 0,
        vMax: 9999
    };

    scope.autoNumeric('init', option);
}

function toggleIsActive(el) {
    return el.find(".toggle-slide").hasClass("active");
}

function setupSelect(scope) {
    if (scope.length == 0 || !scope.hasClass("chosen")) {
        scope = scope.find(".chosen");
    }

    scope.chosen({search_contains:true});

    scope.change(function() {
        if ($(this).val() != "") {
            $(this).removeClass("input-validation-error").parent().find("div.chosen-container").removeClass("input-validation-error");
        }
    });

    if (scope.length != 0) {
        if (typeof scope.attr("multiple") != "undefined") {
            scope.parent().find(".chosen-choices").attr("style", "border-radius:0 !important;-webkit-border-radius: 0 !important;-moz-border-radius: 0 !important;");
        }
    }
}

function setupCreditCardInput(scope) {
    if (scope.length == 0 || !scope.hasClass("credit-card-number")) {
        scope = scope.find(".credit-card-number");
    }

    scope.creditCardInput();
}

function setupCreditCardCVVInput(scope) {
    if (scope.length == 0 || !scope.hasClass("credit-card-cvv")) {
        scope = scope.find(".credit-card-cvv");
    }

    scope.creditCardCVVInput();
}

function initICheck(scope) {
    scope.find('input[type="checkbox"]').iCheck({
        checkboxClass: 'icheckbox_minimal-grey',
        increaseArea: '20%' // optional
    });
    scope.find('input[type="radio"]').iCheck({
        radioClass: 'iradio_minimal-grey',
        increaseArea: '20%' // optional
    });
}

function initCheckboxDeleteInList(scope) {
    var arr = [];
    if (typeof scope != "undefined" && scope != null) {
        arr = scope.find('#cbxDeleteAll');
    } else {
        arr = $('#cbxDeleteAll');
    }

    arr.on('ifClicked', function () {
        var target = $(this);
        var tbl = target.parents("table:first");
        tbl.find("input[cbxdelete='True']").each(function () {
            if (tbl.find('#cbxDeleteAll').is(":checked")) {
                $(this).iCheck('uncheck');
            } else {
                $(this).iCheck('check');
            }
        });
    });

    if (typeof scope != "undefined" && scope != null) {
        arr = scope.find("input[cbxdelete='True']");
    } else {
        arr = $("input[cbxdelete='True']");
    }

    arr.on('ifClicked', function () {
        var cbx = $(this);
        var tbl = cbx.parents("table:first");

        setTimeout(function () {
            var tr = cbx.parents("tr:first");
            if (cbx.is(":checked")) {
                tr.removeClass(ROW_SELECTED_CLASS).addClass(ROW_SELECTED_CLASS);
            } else {
                tr.removeClass(ROW_SELECTED_CLASS);
            }

            var checkedCount = tbl.find("input[cbxdelete='True']:checked").length;

            if (checkedCount == tbl.find("input[cbxdelete='True']").length) {
                tbl.find('#cbxDeleteAll').iCheck('check');
            } else {
                tbl.find('#cbxDeleteAll').iCheck('uncheck');
            }
        }, 10);
    });
}

$(document).ready(function () {

    setupNumberInteger($(".number-integer"));

    setupPositiveInteger($(".number-positive-integer"));

    setupPhoneMask($(".phone"));

    setupCurrency($('.currency'));

    setupQuantity($('.quantity'));

    setupSmallQuantity($('.small-quantity'));

    setupDatePicker($(".date-picker"));

    setupTimePicker($(".timepicker"));

    setupSelect($(".chosen"));

    setupDateRange($(".date-range"));

    setupCreditCardInput($(".credit-card-number"));

    setupCreditCardCVVInput($(".credit-card-cvv"));

    $(".panel-heading .tools .fa-chevron-down, .panel-heading .tools .fa-chevron-up").click(function (e) {
        e.stopPropagation();
        var parent = $(this).parents(".panel:first");
        parent.find(".panel-body:first").slideToggle("fast");

        if ($(this).hasClass("fa-chevron-down")) {
            $(this).removeClass("fa-chevron-down").addClass("fa-chevron-up");
            parent.find("#FilterVisible").val("True");
        } else {
            $(this).removeClass("fa-chevron-up").addClass("fa-chevron-down");
            parent.find("#FilterVisible").val("False");
        }
    }).each(function() {
        var parent = $(this).parents(".panel:first");
        parent.find(".panel-heading:first").addClass("cur-pointer").click(function() {
            var panel = $(this).parents(".panel:first");
            panel.find(".panel-body:first").slideToggle("fast");

            var slideControl = panel.find(".slide-control-filter");

            if (slideControl.hasClass("fa-chevron-down")) {
                slideControl.removeClass("fa-chevron-down").addClass("fa-chevron-up");
                panel.find("#FilterVisible").val("True");
            } else {
                slideControl.removeClass("fa-chevron-up").addClass("fa-chevron-down");
                panel.find("#FilterVisible").val("False");
            }
        });
    });

    initCheckboxDeleteInList();
});
