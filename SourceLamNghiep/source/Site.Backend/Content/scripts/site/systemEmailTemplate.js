(function (systemEmailTemplate, $) {

    var settings = {};

    systemEmailTemplate.init = function (options) {
        settings = $.extend(settings, options);

        $("#formFilter").data("validator").settings.ignore = ":hidden:not(select)";

        systemEmailTemplate.initCkeditor();
    };

    systemEmailTemplate.searchClick = function() {
        if (!$("#formFilter").valid()) {
            return;
        }

        $("#formFilter").submit();
    };

    systemEmailTemplate.initCkeditor = function() {
        if ($("#Content").length > 0) {
            CKEDITOR.replace('Content', {
                filebrowserBrowseUrl: roxyFileman,
                filebrowserImageBrowseUrl: roxyFileman + '&type=image',
                enterMode: CKEDITOR.ENTER_DIV
            });
        }
    };

    systemEmailTemplate.saveClick = function() {
        for (instance in CKEDITOR.instances) {
            CKEDITOR.instances[instance].updateElement();
        }

        if (!$("#formInfo").valid()) {
            _scrollTop();
            return;
        }

        $("#formInfo").submit();
    };

}(window.systemEmailTemplate = window.systemEmailTemplate || {}, jQuery));

