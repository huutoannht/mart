(function (htmlContent, $) {

    var settings = {};

    htmlContent.init = function (options) {
        settings = $.extend(settings, options);

        $("#formFilter").data("validator").settings.ignore = ":hidden:not(select)";

        if (settings.isCkeditor) {
            htmlContent.initCkeditor();
        } else {
            $("#Content").attr("maxlength", 162);
        }
    };

    htmlContent.initCkeditor = function () {
        if ($("#Content").length > 0) {
            CKEDITOR.replace('Content', {
                filebrowserBrowseUrl: roxyFileman,
                filebrowserImageBrowseUrl: roxyFileman + '&type=image',
                enterMode: CKEDITOR.ENTER_DIV
            });
        }
    };

    htmlContent.searchClick = function() {
        if (! $("#formFilter").valid()) {
            return;
        }

        $("#formFilter").submit();
    };

    htmlContent.saveClick = function () {
        for (instance in CKEDITOR.instances) {
            CKEDITOR.instances[instance].updateElement();
        }

        if (!$("#formInfo").valid()) {
            _scrollTop();
            return;
        }

        $("#formInfo").submit();
    };

}(window.htmlContent = window.htmlContent || {}, jQuery));

