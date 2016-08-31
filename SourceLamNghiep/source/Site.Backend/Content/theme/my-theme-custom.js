$('#wrapper').css('min-height', '100%');

function initThemeCheckbox(scope) {
    if (typeof scope == "undefined" || scope == null) {
        scope = $('input[type="checkbox"]');
    } else {
        scope = scope.find('input[type="checkbox"]');
    }

    //BEGIN CHECKBOX & RADIO
    scope.iCheck({
        checkboxClass: 'icheckbox_minimal-grey',
        increaseArea: '20%' // optional
    });

    scope.on('ifChanged', function (event) {
        if (typeof $(this).attr("onclick") !== typeof undefined) {
            eval($(this).attr("onclick"));
        }
    });
}

function initThemeRadiobox(scope) {
    if (typeof scope == "undefined" || scope == null) {
        scope = $('input[type="radio"]');
    } else {
        scope = scope.find('input[type="radio"]');
    }

    scope.iCheck({
        radioClass: 'iradio_minimal-grey',
        increaseArea: '20%' // optional
    });
}

initThemeCheckbox();
initThemeRadiobox();