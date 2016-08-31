(function (authen, $) {

    var settings = {};

    authen.initLogin = function (options) {
        settings = $.extend(settings, options);

        $('#UserName, #Password').bind('keypress', function (e) {
            if (e.keyCode == 13) {
                authen.login();
            }
        });
    };

    authen.login = function() {
        if (!$("#formLogin").valid()) {
            return;
        }

        hideValidateMessage($("#formLogin"));

        var data = $("#formLogin").serializeFormJSON();
        post(settings.validateUserUrl, data, function(res) {
            if (res.success) {
                var returnUrl = $("#ReturnUrl").val();
                if (typeof (returnUrl) != "undefined" && !returnUrl.isEmpty()) {
                    gotoUrl(returnUrl);
                } else {
                    gotoUrl(res.defaultUrl);
                }
            } else {
                notify.error(res.message);
            }
        });
    };

    authen.initRecoverPassword = function (options) {
        settings = $.extend(settings, options);

        $('#Email').bind('keypress', function (e) {
            if (e.keyCode == 13) {
                authen.recoverPassword();
            }
        });
    };

    authen.recoverPassword = function () {

        if (!$("#formRecoverPwd").valid()) {
            return;
        }

        hideValidateMessage($("#formRecoverPwd"));

        var data = $("#formRecoverPwd").serializeFormJSON();
        post(settings.submitRequestPasswordUrl, data, function (res) {
            if (res.success) {
                gotoUrl(settings.indexUrl);
            } else {
                notify.error(res.message);
            }
        });
    };

    authen.submitResetPassword = function() {
        if (!$("#formResetPwd").valid()) {
            return;
        }

        hideValidateMessage($("#formResetPwd"));

        var data = $("#formResetPwd").serializeFormJSON();
        post(settings.saveNewPasswordUrl, data, function (res) {
            if (res.success) {
                gotoUrl(res.loginUrl);
            } else {
                notify.error(res.message);
            }
        });
    };

}(window.authen = window.authen || {}, jQuery));

