(function (baiViet, $) {

    var settings = {};

    baiViet.init = function (options) {
        settings = $.extend(settings, options);
        $.validator.addMethod('accept', function () { return true; });
        baiViet.initList();
    };

    baiViet.goPage = function (pageIndex) {
        $("#Pagination_CurrentPageIndex").val(pageIndex);
        baiViet.getListArtice();
    };

    baiViet.changePageSize = function (pageSizeElement) {
        $("#Pagination_PageSize").val(pageSizeElement.value);
        $("#Pagination_CurrentPageIndex").val(1);
        baiViet.getListArtice();
    };

    baiViet.sort = function (target, colName, direction) {
        $("#SortBy").val(colName);
        $("#SortDirection").val(direction);
        $("#EventFiredFromSortButton").val("True");
        baiViet.getListArtice();
    };

    baiViet.getListArtice = function () {
        $("#formFilter").submit();
    };

    baiViet.addArtice = function () {
        post(settings.editArticeUrl, {
            categoryId: $("#formFilter #CategoryId").val()
        }, function (res) {
            if (res.success) {
                $("#divEdit").html(res.html).show();
                baiViet.initInfoForm(res);
                _scrollTo($("#divEdit"));
            } else {
                notify.error(res.message);
                _scrollTop();
            }
        });
    };

    baiViet.editArtice = function (id) {
        post(settings.editArticeUrl, {
            id: id
        }, function (res) {
            if (res.success) {
                $("#divEdit").html(res.html).show();
                baiViet.initInfoForm(res);
                _scrollTo($("#divEdit"));
            } else {
                notify.error(res.message);
                _scrollTop();
            }
        });
    };

    baiViet.initInfoForm = function (res) {
        $.validator.unobtrusive.parse($("#divEdit"));
        setupSelect($("#divEdit"));
        initICheck($("#divEdit"));
        setupCurrency($("#divEdit"), 0);
        setupSmallQuantity($("#divEdit"));
        setupDatePicker($("#divEdit"));

        CKEDITOR.replace($("#divEdit #Description").get(0), {
            filebrowserBrowseUrl: roxyFileman,
            filebrowserImageBrowseUrl: roxyFileman + '&type=image',
            enterMode: CKEDITOR.ENTER_DIV
        });

        CKEDITOR.replace($("#divEdit #Content").get(0), {
            filebrowserBrowseUrl: roxyFileman,
            filebrowserImageBrowseUrl: roxyFileman + '&type=image',
            enterMode: CKEDITOR.ENTER_DIV
        });

        baiViet.initFileInput(res);

        $("#formInfo").submit(function () {
            return false;
        });
    };

    baiViet.initFileInput = function (res) {
        if ($("#fileImage").length == 0) return;

        var obj = {};
        obj = $.extend(obj, fileInputSettings);

        if (typeof res.imagePath == "string" && !res.imagePath.isEmpty()) {
            obj.initialPreview = [
                "<img src='" + res.imagePath + "' class='file-preview-image'/>"
            ];
        }

        $("#fileImage").fileinput(obj);

        $('#fileImage').on('filecleared', function () {
            if ($("#ImagePath").length > 0) {
                $("#ImagePath").val("");
            }
        });
    };

    baiViet.initList = function () {
        initCopyToClipboard();
        $('[data-toggle="lightbox-image"]').magnificPopup({ type: 'image', image: { titleSrc: 'title' } });
    };

    baiViet.saveClick = function() {
        for (var instanceName in CKEDITOR.instances) {
            CKEDITOR.instances[instanceName].updateElement();
        }

        if (!$("#formInfo").valid()) return;

        var data = new FormData($("#formInfo")[0]);

        ajaxFormFile(settings.saveArticeUrl, data, function (res) {
            if (res.success) {
                baiViet.hideEdit();
                baiViet.getListArtice();

                POST_BLOCK_UI = false;
                POST_SHOW_LOADING = false;
            } else {
                $("#divNotifyContainer").appendTo($("#divNotifyEdit"));
                notify.error(res.message);
            }
        });
    };

    baiViet.hideEdit = function () {
        $("#divNotifyContainer").appendTo($("#divNotifyTop"));
        notify.hide();

        for (var name in CKEDITOR.instances) {
            CKEDITOR.instances[name].destroy(true);
        }

        $("#divEdit").empty().hide();
    };

    baiViet.deleteArtice = function (id) {
        baiViet.hideEdit();
        jConfirm(CONFIRM_DELETE_MESSAGE, CONFIRM_DELETE_TITLE, function (r) {
            if (!r) return;
            post(settings.deleteArticeUrl, {
                id: id
            }, function (res) {
                if (res.success) {
                    baiViet.getListArtice();
                    
                    POST_BLOCK_UI = false;
                    POST_SHOW_LOADING = false;
                } else {
                    notify.error(res.message);
                }
                _scrollTop();
            });
        });
    };

}(window.baiViet = window.baiViet || {}, jQuery));

