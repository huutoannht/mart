(function (guiMail, $) {

    var settings = {};

    guiMail.init = function (options) {
        settings = $.extend(settings, options);
        $.validator.addMethod('accept', function () { return true; });
        $("form").data("validator").settings.ignore = "";

        CKEDITOR.replace($("#formInfo #Content").get(0), {
            filebrowserBrowseUrl: roxyFileman,
            filebrowserImageBrowseUrl: roxyFileman + '&type=image',
            enterMode: CKEDITOR.ENTER_DIV
        });
    };

    guiMail.addNewFile = function () {

        var count = $("#divFiles input:file").length + 1;

        var html = '<div class="col-md-12 mt5"><input type="file" name="fileImage' + count + '" id="fileImage' + count + '"/></div>';
        $("#divFiles").append(html);

        $("#fileImage" + count).click();
    };

    guiMail.send = function () {
        for (var instanceName in CKEDITOR.instances) {
            CKEDITOR.instances[instanceName].updateElement();
        }

        notify.hide();
        if (!$("#SendToAllRegisteredCustomer").is(":checked") && !$("#SendToAllSubcriber").is(":checked")) {
            var emails = $("#Emails").val();
            if (emails.isEmpty()) {
                notify.error(settings.emailsRequiredMsg);
                _scrollTop();
                return;
            }
        }

        var form = $("#formInfo");
        if (!form.valid()) {
            _scrollTop();
            return;
        }

        var data = new FormData(form.get(0));

        ajaxFormFile(settings.sendUrl, data, function (res) {
            if (res.success) {
                $("#Emails").val("");
                $("#Subject").val("");
                $("#Content").val("");

                CKEDITOR.instances['Content'].setData("");

                $("#divFiles").empty();
                notify.success(res.message);
            } else {
                notify.error(res.message);
            }
            _scrollTop();
        });
    };

}(window.guiMail = window.guiMail || {}, jQuery));