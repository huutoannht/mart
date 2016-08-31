(function (file, $) {
    var settings = {};

    file.init = function (options) {
        settings = $.extend(settings, options);
    };

    file.goPage = function (pageIndex) {
        $("#Pagination_CurrentPageIndex").val(pageIndex);
        $("#formFilter").submit();
    };

    file.changePageSize = function (pageSizeElement) {
        $("#Pagination_PageSize").val(pageSizeElement.value);
        $("#Pagination_CurrentPageIndex").val(1);
        $("#formFilter").submit();
    };

    file.sort = function (target, colName, direction) {
        $("#SortBy").val(colName);
        $("#SortDirection").val(direction);
        $("#EventFiredFromSortButton").val("True");
        $("#formFilter").submit();
    };

    file.addClick = function() {
        post(settings.addUrl, null, function(res) {
            if (res.success) {
                $("#divEdit").html(res.html).show();
                file.initFormInfo();
                _scrollTo($("#divEdit"));
            } else {
                notify.error(res.message, $("#divNotifyMain"));
            }
        });
    };

    file.editClick = function (id) {
        post(settings.editUrl, {
            id: id
        }, function (res) {
            if (res.success) {
                $("#divEdit").html(res.html).show();
                file.initFormInfo();
                _scrollTo($("#divEdit"));
            } else {
                notify.error(res.message, $("#divNotifyMain"));
            }
        });
    };

    file.initFormInfo = function() {
        $.validator.unobtrusive.parse($("#divEdit"));
        $("#fileUpload").change(function() {
            $("#formInfo #Name").val(file.getFileName());
        });
    };

    file.getFileName = function() {
        var fullPath = $("#fileUpload").get(0).value;
        if (fullPath) {
            return fullPath.split(/(\\|\/)/g).pop();
        }
        return "";
    };

    file.save = function () {
        if (!$("#formInfo").valid()) {
            return;
        }

        if (file.getFileName().indexOf(".") < 0) {
            notify.error(settings.fileNameInvalid, $("#divEdit"));
            return;
        }

        var ele = $("#fileUpload").get(0);
        if (ele.value.length < 4) {
            notify.error(settings.fileIsRequiredMsg, $("#divEdit"));
            return;
        }

        var doSave = function() {
            var data = new FormData($("#formInfo")[0]);

            ajaxFormFile(settings.saveUrl, data, function (res) {
                if (res.success) {
                    file.goPage(1);
                } else {
                    notify.error(res.message, $("#divEdit"));
                }
            });
        };

        post(settings.verifyNameIsExistedUrl, {
            name: $("#formInfo #Name").val(),
            id: $("#formInfo #Id").val()
        }, function(res) {
            if (res.success) {
                if (res.existed) {
                    jConfirm(settings.fileExistedConfirm, settings.fileExistedConfirmTitle, function(r) {
                        if (!r) return;
                        doSave();
                    });
                } else {
                    doSave();
                }
            } else {
                notify.error(res.message, $("#divNotifyEdit"));
            }
        });
    };

    file.deleteClick = function(id) {
        $("#divEdit").hide();
        jConfirm(CONFIRM_DELETE_MESSAGE, CONFIRM_DELETE_TITLE, function (r) {
            if (!r) return;
            post(settings.deleteUrl, { id: id }, function (res) {
                if (res.success) {
                    file.goPage(1);
                } else {
                    notify.error(res.message, $("#divNotifyMain"));
                    _scrollTop();
                }
            });
        });
    };

}(window.file = window.file || {}, jQuery));
