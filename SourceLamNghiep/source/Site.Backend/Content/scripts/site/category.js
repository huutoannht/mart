(function (category, $) {
    var settings = {};

    category.init = function (options) {
        settings = $.extend(settings, options);
        category.initTreeView();
        $.validator.addMethod('accept', function () { return true; });
    };

    category.goPage = function (pageIndex) {
        $("#Pagination_CurrentPageIndex").val(pageIndex);
        category.getList();
    };

    category.changePageSize = function (pageSizeElement) {
        $("#Pagination_PageSize").val(pageSizeElement.value);
        $("#Pagination_CurrentPageIndex").val(1);
        category.getList();
    };

    category.sort = function(target, colName, direction) {
        $("#SortBy").val(colName);
        $("#SortDirection").val(direction);
        $("#EventFiredFromSortButton").val("True");
        category.getList();
    };

    category.initTreeView = function () {
        $("#tblCategories").treetable({
            expandable: true,
            clickableNodeNames: true,
            onInitialized: function() {
                $("#tblCategories td span.indenter").click();
            }
        });
    };

    category.getList = function () {
        var data = $("#formFilter").serializeFormJSON();

        post(settings.getListUrl, data, function (res) {
            if (res.success) {
                $("#divList").html(res.html);
                category.initTreeView();
                initICheck($("#divList"));
                initCheckboxDeleteInList();
            } else {
                notify.error(res.message);
            }
            _scrollTop();
        });
    };

    category.addNewClick = function (parentId) {
        post(settings.addNewUrl, {
            parentId: parentId
        }, function (res) {
            if (res.success) {
                $("#divEdit").html(res.html).show();
                category.initFormInfo(res);
                _scrollTo($("#divEdit"));
            } else {
                notify.error(res.message);
                _scrollTop();
            }
        });
    };

    category.editClick = function (id) {
        post(settings.editUrl, {
            id: id
        }, function (res) {
            if (res.success) {
                $("#divEdit").html(res.html).show();
                category.initFormInfo(res);
                _scrollTo($("#divEdit"));
            } else {
                notify.error(res.message);
                _scrollTop();
            }
        });
    };

    category.initFormInfo = function (res) {
        $.validator.unobtrusive.parse($("#divEdit"));
        initICheck($("#divEdit"));
        category.initFileInput(res);
        
        setupSmallQuantity($('#divEdit'));

        if ($("#fileImage1").length > 0) {
            var obj = {};
            obj = $.extend(obj, fileInputSettings);

            $("#fileImage1").fileinput(obj);
        }

        $("#formInfo").submit(function () {
            return false;
        });
    };

    category.initFileInput = function (res) {
        if ($("#menuFileImage").length == 0) return;

        var obj = {};
        obj = $.extend(obj, fileInputSettings);

        if (typeof res.imagePath == "string" && !res.imagePath.isEmpty()) {
            obj.initialPreview = [
                "<img src='" + res.imagePath + "' class='file-preview-image'/>"
            ];
        }

        $("#menuFileImage").fileinput(obj);

        $('#menuFileImage').on('filecleared', function () {
            if ($("#ImagePath").length > 0) {
                $("#ImagePath").val("");
            }
        });
    };

    category.saveClick = function () {
        if (!$("#formInfo").valid()) return;

        var haveImage = false;

        if ($('#menuFileImage').length && $('#menuFileImage').val().length) {
            haveImage = true;
        } else if ($("#divImageList").length) {
            $("#divImageList input:file").each(function() {
                if ($(this).val().length) {
                    haveImage = true;
                }
            });
        }

        if (haveImage) {
            var data = new FormData($("#formInfo")[0]);

            ajaxFormFile(settings.saveFileUrl, data, function(res) {
                if (res.success) {
                    POST_BLOCK_UI = false;
                    POST_SHOW_LOADING = false;

                    $("#formInfo #ImageFileNames").val(res.imageFileNames);

                    category.save(res.imagePath);
                } else {
                    notify.error(res.message, $("#divEdit"));
                    _scrollTop();
                }
            });
        } else {
            category.save();
        }
    };

    category.save = function (imagePath) {
        initICheck($("#divList"));

        var model = $("#formInfo").serializeFormJSON();

        if (typeof imagePath == "string") {
            model.ImagePath = imagePath;
        }

        for (var instanceName in CKEDITOR.instances) {
            CKEDITOR.instances[instanceName].updateElement();
        }

        model.Names = [];

        $(settings.langList).each(function () {
            var item = this;
            var tbxName = $("input[name='" + item.Value + "']");

            model.Names.push({
                Name: tbxName.val(),
                Language: item.Value
            });
        });
      
        if ($('#formInfo input[name=rdoForSale]').length > 0) {
            model.TransactionType = $('#formInfo input[name=rdoForSale]:checked').attr("value");
        }

        post(settings.saveUrl, model, function (res) {
            if (res.success) {
                category.hideEdit();
                $("#divList").html(res.html);
                initICheck($("#divList"));
                initCheckboxDeleteInList();
                category.initTreeView();
                notify.success(res.message);

                //$("#sideMenuParent").html(res.leftMenu);
                //$('#side-menu').metisMenu({});

                _scrollTop();
            } else {
                $("#divNotifyContainer").appendTo($("#divNotifyTop"));
                notify.error(res.message, $("#divEdit"));
            }
        });
    };

    category.hideEdit = function () {

        for (var name in CKEDITOR.instances) {
            CKEDITOR.instances[name].destroy(true);
        }

        $("#divNotifyContainer").appendTo($("#divNotifyTop"));
        notify.hide();
        $('#divEdit').hide();
    };

    category.deleteClick = function (id) {
        category.hideEdit();

        jConfirm(CONFIRM_DELETE_MESSAGE, CONFIRM_DELETE_TITLE, function (r) {
            if (!r) return;
            post(settings.deleteUrl, { id: id }, function (res) {
                if (res.success) {
                    $("#divList").html(res.html);
                    category.initTreeView();
                    //$("#sideMenuParent").html(res.leftMenu);
                    //$('#side-menu').metisMenu({});
                    notify.success(res.message);
                } else {
                    notify.error(res.message);
                }
                _scrollTop();
            });
        });
    };

    category.collapseAll = function() {
        $("#tblCategories").treetable("collapseAll");
    };

    category.expandAll = function () {
        $("#tblCategories").treetable("expandAll");
    };

    category.addNewImageClickOnEditCategory = function () {
        var nextOrderNumber = $("#divImageList input[type='file']").length + 1;

        var marginTop = "";
        if (nextOrderNumber > 2) {
            marginTop = "mt10";
        }

        $("#divImageList").append('<div class="col-md-3 ' + marginTop + '"><input type="file" name="fileImage' + nextOrderNumber + '" id="fileImage' + nextOrderNumber + '" accept="image/*" /></div>');

        var obj = {};
        obj = $.extend(obj, fileInputSettings);
        $("#fileImage" + nextOrderNumber).fileinput(obj);
        $("#fileImage" + nextOrderNumber).click();
    };

    category.getImages = function (categoryId) {
        post(settings.getImageListUrl, {
            categoryId: categoryId
        }, function (res) {
            if (res.success) {
                $("#divEdit").html(res.imageList).show();
                initThemeCheckbox($("#divEdit"));
                _scrollTo($("#divEdit"));
            } else {
                notify.error(res.message);
            }
        });
    };

    category.addNewImageClick = function (categoryId) {
        post(settings.getAddNewImageHtmlUrl, {
            categoryId: categoryId
        }, function (res) {
            if (res.success) {
                $("#divAddNewImage").html(res.html).show();

                var obj = {};
                obj = $.extend(obj, fileInputSettings);

                $("#fileImage").fileinput(obj);

                initThemeCheckbox($("#divEdit"));

                _scrollTo($("#divEdit"));
            } else {
                notify.error(res.message);
            }
        });
    };

    category.updateImageVisible = function (target, id) {
        var visible = $(target).is(":checked");
        post(settings.updateImageVisibleUrl, {
            id: id,
            visible: visible
        }, function (res) {
            if (res.success) {
                showNotifyAlert(res.message);
            } else {
                notify.error(res.message);
            }
        });
    };

    category.deleteImage = function (id, categoryId) {
        jConfirm(CONFIRM_DELETE_MESSAGE, CONFIRM_DELETE_TITLE, function (r) {
            if (!r) return;
            post(settings.deleteImageUrl, {
                id: id,
                categoryId: categoryId
            }, function (res) {
                if (res.success) {
                    category.cancelAddNewImage();
                    $("#divList").html(res.categoryList);
                    $("#divEdit").html(res.imageList).show();

                    category.initTreeView();
                    _scrollTo($("#divEdit"));
                } else {
                    jAlert(res.message);
                }
            });
        });
    };

    category.cancelAddNewImage = function () {
        $("#divAddNewImage").empty();
    };

    category.saveImage = function () {

        var data = new FormData($("#formCategoryImage")[0]);

        ajaxFormFile(settings.saveImageUrl, data, function (res) {
            if (res.success) {
                category.cancelAddNewImage();
                $("#divList").html(res.categoryList);
                $("#divEdit").html(res.imageList).show();

                initThemeCheckbox($("#divEdit"));
                category.initTreeView();
                _scrollTo($("#divEdit"));
            } else {
                jAlert(res.message);
            }
        });
    };

}(window.category = window.category || {}, jQuery));