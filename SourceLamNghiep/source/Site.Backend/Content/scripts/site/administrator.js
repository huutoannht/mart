(function (administrator, $) {
    var settings = {};

    administrator.initIndex = function (options) {
        settings = $.extend(settings, options);
    };

    administrator.goPage = function (pageIndex) {
        $("#Pagination_CurrentPageIndex").val(pageIndex);
        $("#formFilter").submit();
    };

    administrator.changePageSize = function (pageSizeElement) {
        $("#Pagination_PageSize").val(pageSizeElement.value);
        $("#Pagination_CurrentPageIndex").val(1);
        $("#formFilter").submit();
    };

    administrator.sort = function (target, colName, direction) {
        $("#SortBy").val(colName);
        $("#SortDirection").val(direction);
        $("#EventFiredFromSortButton").val("True");
        $("#formFilter").submit();
    };

    administrator.initEdit = function (options) {
        settings = $.extend(settings, options);

        administrator.initFileInput();

        administrator.initFormInfo();
    };

    administrator.addNewClick = function () {
        post(settings.addNewUrl, { organizationId: settings.organizationId }, function (res) {
            $("#divEdit").html(res.html).show();
            administrator.initRoleInfoForm();
            _scrollTo($("#divEdit"));
        });
    };

    administrator.editClick = function (id) {
        post(settings.editUrl, { id: id }, function (r) {
            if (r.success) {
                $("#divEdit").html(r.html).show();
                administrator.initRoleInfoForm();
                _scrollTo($("#divEdit"));
            } else {
                notify.error(r.message);
                _scrollTop();
            }
        });
    };

    administrator.initFileInput = function () {

        var obj = {};
        obj = $.extend(obj, fileInputSettings);

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
    };

    administrator.initFormInfo = function () {
        $("#formInfo").data("validator").settings.ignore = ":hidden:not(select)";

        $.validator.addMethod('accept', function () { return true; });

        if (settings.userId == CONST_EMPTY_GUID) {
            $("#Password").rules("add", {
                required: true,
                messages: {
                    required: settings.passwordRequiredMsg
                }
            });
        }

        var initRoleRequired = function() {
            var type = $("#Type").val();
            if (type == "") return;

            var label = $("label[for='RoleId']");
            if (type == settings.adminId) {
                $("#RoleId").rules("add", {
                    required: true,
                    messages: {
                        required: settings.roleRequiredMsg
                    }
                });
                label.find("span").remove();
                label.append(SPAN_REQUIRED_HTML);
            } else {
                label.find("span").remove();
                $("#RoleId").rules('remove', 'required');
            }
        };

        initRoleRequired();

        if ($("#Type").length > 0) {
            $("#Type").change(function () {
                initRoleRequired();
            });
        }
    };

    administrator.saveClick = function() {
        if (!$("#formInfo").valid()) {
            _scrollTop();
            return;
        }

        hideValidateMessage($("#formInfo"));

        var data = new FormData($("#formInfo")[0]);

        ajaxFormFile(settings.saveUrl, data, function (res) {
            if (res.success) {
                POST_BLOCK_UI = false;
                POST_SHOW_LOADING = false;
                gotoUrl(settings.indexUrl);
            } else {
                notify.error(res.message);
                _scrollTop();
            }
        });
    };

    administrator.saveChangePassword = function () {
        notify.hide();

        if (!$("#formInfo").valid()) {
            return;
        }

        var data = $("#formInfo").serializeFormJSON();

        post(settings.saveChangePasswordUrl, data, function (r) {
            if (r.success) {
                notify.success(r.message);
            } else {
                notify.error(r.message);
            }
        });
    };

    administrator.deleteClick = function (id) {
        $("#divEdit").hide();
        jConfirm(CONFIRM_DELETE_MESSAGE, CONFIRM_DELETE_TITLE, function (r) {
            if (!r) return;
            post(settings.deleteUrl, { id: id }, function (res) {
                if (res.success) {
                    administrator.goPage(1);
                } else {
                    notify.error(res.message);
                    _scrollTop();
                }
            });
        });
    };

}(window.administrator = window.administrator || {}, jQuery));