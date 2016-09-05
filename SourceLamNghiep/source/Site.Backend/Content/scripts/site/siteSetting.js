(function (siteSetting, $) {
    var settings = {};

    siteSetting.init = function (options) {
        settings = $.extend(settings, options);
    };

    siteSetting.goPage = function (pageIndex) {
        $("#Pagination_CurrentPageIndex").val(pageIndex);
        $("#formFilter").submit();
    };

    siteSetting.changePageSize = function (pageSizeElement) {
        $("#Pagination_PageSize").val(pageSizeElement.value);
        $("#Pagination_CurrentPageIndex").val(1);
        $("#formFilter").submit();
    };

    siteSetting.sort = function (target, colName, direction) {
        $("#SortBy").val(colName);
        $("#SortDirection").val(direction);
        $("#EventFiredFromSortButton").val("True");
        $("#formFilter").submit();
    };

    siteSetting.deleteClick = function (id) {
        $('#divEdit').hide();
        jConfirm(CONFIRM_DELETE_MESSAGE, CONFIRM_DELETE_TITLE, function (r) {
            if (!r) return;
            post(settings.deleteUrl, { id: id }, function (res) {
                if (res.success) {
                    siteSetting.goPage(1);
                } else {
                    notify.error(res.message);
                    _scrollTop();
                }
            });
        });
    };

    siteSetting.addNewClick = function () {
        post(settings.addNewUrl, {
            settingType: $("#formFilter #SettingType").val()
        }, function(res) {
            if (res.success) {
                $("#divEdit").html(res.html).show();
                siteSetting.initFormEdit();
                _scrollTo($("#divEdit"));
            } else {
                notify.error(res.message);
                _scrollTop();
            }
        });
    };

    siteSetting.editClick = function (id) {
        post(settings.editUrl, {
            id: id
        }, function (res) {
            if (res.success) {
                $("#divEdit").html(res.html).show();
                siteSetting.initFormEdit();
                _scrollTo($("#divEdit"));
            } else {
                notify.error(res.message);
                _scrollTop();
            }
        });
    };

    siteSetting.initFormEdit = function () {
        setupSelect($("#formInfo"));
        $.validator.unobtrusive.parse($("#formInfo").parent());
        $("#formInfo").data("validator").settings.ignore = ":hidden:not(select)";

        setupSmallQuantity($("#formInfo"));
        setupCurrency($("#formInfo"));

        $("#formInfo").submit(function () {
            return false;
        });

        initICheck($("#formInfo"));
    };

    siteSetting.saveClick = function () {

        if (!$("#formInfo").valid()) {
            return;
        }

        hideValidateMessage($("#formInfo"));

        var data = $("#formInfo").serializeFormJSON();

        post(settings.saveUrl, data, function (res) {
            if (res.success) {
                $("#divEdit").hide();

                POST_BLOCK_UI = false;
                POST_SHOW_LOADING = false;

                siteSetting.goPage(1);
            } else {
                notify.error(res.message);
                _scrollTop();
            }
        });
    };

    siteSetting.updateValue = function(target, val) {
        $("#formInfo #Value").val(val);
        
        $(".colours-wrapper div").removeClass("active");
        $(target).addClass("active");
    };

}(window.siteSetting = window.siteSetting || {}, jQuery));