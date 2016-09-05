(function (sanPham, $) {

    var settings = {};

    sanPham.init = function (options) {
        settings = $.extend(settings, options);
        $.validator.addMethod('accept', function () { return true; });
        sanPham.initList();
        sanPham.initFileImport();
    };

    sanPham.goPage = function (pageIndex) {
        $("#Pagination_CurrentPageIndex").val(pageIndex);
        sanPham.getListProduct();
    };

    sanPham.changePageSize = function (pageSizeElement) {
        $("#Pagination_PageSize").val(pageSizeElement.value);
        $("#Pagination_CurrentPageIndex").val(1);
        sanPham.getListProduct();
    };

    sanPham.sort = function (target, colName, direction) {
        $("#SortBy").val(colName);
        $("#SortDirection").val(direction);
        $("#EventFiredFromSortButton").val("True");
        sanPham.getListProduct();
    };

    sanPham.getListProduct = function () {

        sanPham.hideEdit();

        var data = $("#formFilter").serializeFormJSON();

        post(settings.getListProductUrl, data, function (res) {
            if (res.success) {
                $("#divList").html(res.html);
                sanPham.initList();
            } else {
                notify.error(res.message, $("#divNotifyTop"));
            }
            _scrollTop();
        });
    };

    sanPham.editProduct = function (id, categoryId) {
        post(settings.editProductUrl, {
            id: id,
            categoryId: categoryId
        }, function (res) {
            if (res.success) {
                $("#divEdit").html(res.html).show();
                sanPham.initInfoForm(res);
                _scrollTo($("#divEdit"));

            } else {
                notify.error(res.message, $("#divNotifyTop"));
                _scrollTop();
            }
        });
    };

    sanPham.initInfoForm = function () {
        $.validator.unobtrusive.parse($("#divEdit"));

        $("#Cost").rules("remove", "required");
        $("#Price").rules("remove", "required");
        $("#Vat").rules("remove", "required");
        setupSelect($("#divEdit"));
        initICheck($("#divEdit"));
        setupCurrency($("#divEdit"), 0);
        setupSmallQuantity($("#divEdit"));
        setupDatePicker($("#divEdit"));

        if ($("#fileImage1").length > 0) {
            var obj = {};
            obj = $.extend(obj, fileInputSettings);

            $("#fileImage1").fileinput(obj);
        }
    };

    sanPham.initList = function () {
        initICheck($("#divList"));
        initCheckboxDeleteInList($("#divList"));
    };

    sanPham.saveClick = function () {
       
        for (var instanceName in CKEDITOR.instances) {
            CKEDITOR.instances[instanceName].updateElement();
        }
        var model = $("#formInfo").serializeFormJSON();
        if (!$("#formInfo").valid()) {
            if (model.CategoryId.isEmpty()) {
                notify.error(settings.categoryRequiredMsg, $("#divNotifyEdit"));
                _scrollTo($("#divEdit"));
                return;
            }
        }
        if (model.CategoryId.isEmpty()) {
            notify.error(settings.categoryRequiredMsg, $("#divNotifyEdit"));
            _scrollTo($("#divEdit"));
            return;
        }

        $("#formInfo #VideosJsonString").val(sanPham.getVideosJsonString());

        $("#formInfo #TextSearch").val($("#formFilter #TextSearch").val());

        var arrTienNghi = [];
        $("#divTienNghi input:checkbox:checked").each(function() {
            arrTienNghi.push($(this).attr("cateid"));
        });

        $("#formInfo #TienNghi").val(arrTienNghi.join("#"));

        var arrMoiTruongXungQuanh = [];
        $("#divMoiTruongXungQuanh input:checkbox:checked").each(function () {
            arrMoiTruongXungQuanh.push($(this).attr("cateid"));
        });

        $("#formInfo #MoiTruongXungQuanh").val(arrMoiTruongXungQuanh.join("#"));

        hideValidateMessage($("#formInfo"));

        var data = new FormData($("#formInfo")[0]);

        ajaxFormFile(settings.saveProductUrl, data, function (res) {
            if (res.success) {
                sanPham.hideEdit();
                $("#divList").html(res.html);

                sanPham.initList();
                notify.success(res.message, $("#divNotifyEdit"));

                $("#sideMenuParent").html(res.leftMenu);
                $('#side-menu').metisMenu({});

                _scrollTop();
            } else {
                notify.error(res.message, $("#divNotifyEdit"));
            }
        });
    };

    sanPham.getVideosJsonString = function() {
        return JSON.stringify(sanPham.getVideosArray());
    };

    sanPham.getVideosArray = function () {
        var arr = [];
        $("#tblVideos tbody tr[data='true']").each(function() {
            var url = $(this).find("input[url='true']").val();
            var visible = $(this).find("#VideoVisible").is(":checked");
            var sortOrder = $(this).find("#VideoSortOrder").val();

            arr.push({
                Url: url,
                Visible: visible,
                SortOrder: sortOrder
            });
        });

        return arr;
    };

    sanPham.hideEdit = function () {
        notify.hide();

        for (var name in CKEDITOR.instances) {
            CKEDITOR.instances[name].destroy(true);
        }

        $("#divEdit").empty().hide();
    };

    sanPham.addNewImageClickOnEditProduct = function () {
        var nextOrderNumber = $("#divImageList input[type='file']").length + 1;

        var marginTop = "";
        if (nextOrderNumber > 2) {
            marginTop = "mt10";
        }

        $("#divImageList").append('<div class="col-md-4 ' + marginTop + '"><input type="file" name="fileImage' + nextOrderNumber + '" id="fileImage' + nextOrderNumber + '" accept="image/*" /></div>');

        var obj = {};
        obj = $.extend(obj, fileInputSettings);
        $("#fileImage" + nextOrderNumber).fileinput(obj);
        $("#fileImage" + nextOrderNumber).click();
    };

    sanPham.deleteProduct = function (id) {
        sanPham.hideEdit();
        jConfirm(CONFIRM_DELETE_MESSAGE, CONFIRM_DELETE_TITLE, function (r) {
            if (!r) return;
            post(settings.deleteProductUrl, {
                id: id,
                indexModel: $("#formFilter").serializeFormJSON()
            }, function (res) {
                if (res.success) {
                    $("#divList").html(res.html);
                    sanPham.initList();
                    notify.success(res.message, $("#divNotifyTop"));

                    $("#sideMenuParent").html(res.leftMenu);
                    $('#side-menu').metisMenu({});

                    _scrollTop();
                } else {
                    notify.error(res.message, $("#divNotifyTop"));
                }
                _scrollTop();
            });
        });
    };

    sanPham.getImages = function (productId) {
        post(settings.getImageListUrl, {
            productId: productId
        }, function (res) {
            if (res.success) {
                $("#divEdit").html(res.imageList).show();
                sanPham.initImagesList();
                initThemeCheckbox($("#divEdit"));
                _scrollTo($("#divEdit"));
            } else {
                notify.error(res.message, $("#divNotifyTop"));
            }
        });
    };

    sanPham.addNewImageClick = function (productId) {
        post(settings.getAddNewImageHtmlUrl, {
            productId: productId
        }, function (res) {
            if (res.success) {
                $("#divAddNewImage").html(res.html).show();

                var obj = {};
                obj = $.extend(obj, fileInputSettings);

                $("#fileImage").fileinput(obj);

                initThemeCheckbox($("#divEdit"));

                _scrollTo($("#divEdit"));
            } else {
                notify.error(res.message, $("#divNotifyEdit"));
            }
        });
    };

    sanPham.deleteImage = function (id, productId) {
        jConfirm(CONFIRM_DELETE_MESSAGE, CONFIRM_DELETE_TITLE, function (r) {
            if (!r) return;
            post(settings.deleteImageUrl, {
                id: id,
                productId: productId,
                indexModel: $("#formFilter").serializeFormJSON()
            }, function (res) {
                if (res.success) {
                    sanPham.cancelAddNewImage();
                    $("#divList").html(res.productList);
                    $("#divEdit").html(res.imageList).show();
                    sanPham.initImagesList();
                    sanPham.initList();
                    _scrollTo($("#divEdit"));
                } else {
                    notify.error(res.message, $("#divNotifyEdit"));
                }
            });
        });
    };

    sanPham.cancelAddNewImage = function () {
        $("#divAddNewImage").empty();
        notify.hide();
    };

    sanPham.initImagesList = function () {
        initICheck($("#divEdit"));
        initCheckboxDeleteInList($("#divEdit"));
    };

    sanPham.saveImage = function () {
        var ele = $("#fileImage").get(0);
        if (ele.value.length < 4) {
            notify.error(settings.imageIsRequiredMsg);
            return;
        }

        $("#formProductImage #TextSearch").val($("#formFilter #TextSearch").val());

        var data = new FormData($("#formProductImage")[0]);

        ajaxFormFile(settings.saveImageUrl, data, function (res) {
            if (res.success) {
                sanPham.cancelAddNewImage();
                $("#divList").html(res.productList);
                $("#divEdit").html(res.imageList).show();
                sanPham.initList();
                sanPham.initImagesList();
                _scrollTo($("#divEdit"));
            } else {
                notify.error(res.message, $("#divNotifyEdit"));
            }
        });
    };

    sanPham.addVideoLink = function () {
        notify.hide();

        var url = $("#tbxVideoLink").val();
        if (url.isEmpty()) {
            notify.error(settings.videoLinkRequiredMsg, $("#divNotifyEdit"));
            return;
        }

        $("#tbxVideoLink").val("");

        var videos = sanPham.getVideosArray();
        videos.push({
            Url: url,
            Visible: true,
            SortOrder: 0
        });

        sanPham.getVideos(videos);
    };

    sanPham.removeVideoLink = function(target) {
        $(target).parents(".form-group:first").remove();
        sanPham.getVideos(sanPham.getVideosArray());
    };

    sanPham.getVideos = function(arr) {
        post(settings.getVideosUrl, {
            videos: arr
        }, function (res) {
            if (res.success) {
                $("#divTabVideoLink").html(res.html);
                sanPham.initVideosTab();
            } else {
                notify.error(res.message, $("#divNotifyEdit"));
            }
        });
    };

    sanPham.initVideosTab = function() {
        setupSmallQuantity($("#divTabVideoLink"));
        initICheck($("#divTabVideoLink"));
    };

    sanPham.updateImageVisible = function (target, id) {
        var visible = $(target).is(":checked");
        post(settings.updateImageVisibleUrl, {
            id: id,
            visible: visible
        }, function(res) {
            if (res.success) {
                notify.error(res.message, $("#divNotifyEdit"));
            } else {
                notify.error(res.message, $("#divNotifyEdit"));
            }
        });
    };

    sanPham.export = function () {
        notify.hide();
        var data = $("#formFilter").serializeFormJSON();
        window.location = settings.getExportFormUrl + "?" + $.param(data);
    };

    sanPham.importClick = function () {
        $("#fileImport").click();
    };

    sanPham.initFileImport = function () {
        $("#fileImport").change(function () {
            var data = new FormData($("#formImport")[0]);

            ajaxFormFile(settings.importProducsUrl, data, function (res) {
                if (res.success) {
                    notify.success(res.message, $("#divNotifyTop"));
                    sanPham.goPage(1);
                } else {
                    notify.error(res.message, $("#divNotifyTop"));
                    post(settings.getImportFormUrl, null, function (response) {
                        $("#divImport").html(response.html);
                        sanPham.initFileImport();
                    }, null, true);
                }

                _scrollTop();
            });
        });
    };

}(window.sanPham = window.sanPham || {}, jQuery));

