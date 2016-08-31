(function (appSettingImage, $) {
    var settings = {};

    appSettingImage.init = function (options) {
        settings = $.extend(settings, options);

        appSettingImage.initFileInput();
    };

    appSettingImage.initFileInput = function () {
        $.validator.addMethod('accept', function () { return true; });

        var obj = {};

        if ($("#fileImage").length > 0) {
            obj = $.extend({}, fileInputSettings);

            if (!settings.imagePath.isEmpty()) {
                obj.initialPreview = [
                    "<img src='" + settings.imagePath + "' class='file-preview-image'/>"
                ];
            }

            $("#fileImage").fileinput(obj);

            $('#fileImage').on('filecleared', function (event, key) {
                if ($("#User_ImagePath").length > 0) {
                    $("#User_ImagePath").val("");
                }
                if ($("#ImagePath").length > 0) {
                    $("#ImagePath").val("");
                }
            });
        }

        if ($("#fileImage2").length > 0) {
            obj = $.extend({}, fileInputSettings);

            if (!settings.imagePath2.isEmpty()) {
                obj.initialPreview = [
                    "<img src='" + settings.imagePath2 + "' class='file-preview-image'/>"
                ];
            }

            $("#fileImage2").fileinput(obj);

            $('#fileImage2').on('filecleared', function (event, key) {
                if ($("#User_ImagePath2").length > 0) {
                    $("#User_ImagePath2").val("");
                }
                if ($("#ImagePath2").length > 0) {
                    $("#ImagePath2").val("");
                }
            });
        }

        if ($("#fileImage3").length > 0) {
            obj = $.extend({}, fileInputSettings);

            if (!settings.imagePath3.isEmpty()) {
                obj.initialPreview = [
                    "<img src='" + settings.imagePath3 + "' class='file-preview-image'/>"
                ];
            }

            $("#fileImage3").fileinput(obj);

            $('#fileImage3').on('filecleared', function (event, key) {
                if ($("#User_ImagePath3").length > 0) {
                    $("#User_ImagePath3").val("");
                }
                if ($("#ImagePath3").length > 0) {
                    $("#ImagePath3").val("");
                }
            });
        }
    };

    appSettingImage.saveClick = function () {
        var data = new FormData($("#formInfo")[0]);

        ajaxFormFile(settings.saveImageUrl, data, function (res) {
            if (res.success) {
                gotoUrl(settings.indexUrl);
            } else {
                notify.error(res.message);
                _scrollTop();
            }
        });
    };

}(window.appSettingImage = window.appSettingImage || {}, jQuery));

