(function (role, $) {
    var settings = {};

    role.init = function (options) {
        settings = $.extend(settings, options);
    };

    role.goPage = function (pageIndex) {
        $("#Pagination_CurrentPageIndex").val(pageIndex);
        $("#formFilter").submit();
    };

    role.changePageSize = function (pageSizeElement) {
        $("#Pagination_PageSize").val(pageSizeElement.value);
        $("#Pagination_CurrentPageIndex").val(1);
        $("#formFilter").submit();
    };

    role.sort = function (target, colName, direction) {
        $("#SortBy").val(colName);
        $("#SortDirection").val(direction);
        $("#EventFiredFromSortButton").val("True");
        $("#formFilter").submit();
    };

    role.deleteClick = function (id) {
        $("#divEdit").hide();
        jConfirm(CONFIRM_DELETE_MESSAGE, CONFIRM_DELETE_TITLE, function (r) {
            if (!r) return;
            post(settings.deleteUrl, { id: id }, function (res) {
                if (res.success) {
                    role.goPage(1);
                } else {
                    notify.error(res.message);
                    _scrollTop();
                }
            });
        });
    };

    role.selectAllRole = function (type) {
        $("#tbodyRolePermission input[" + type + "]").each(function() {
            this.checked = true;
        });
    };
    
    role.deSelectAllRole = function (type) {
        $("#tbodyRolePermission input[" + type + "]").each(function () {
            this.checked = false;
        });
    };
    
    role.initEdit = function (options) {
        settings = $.extend(settings, options);

    };

    role.getRolePermissionData = function () {
        var arr = [];
        $("#tbodyRolePermission tr").each(function() {
            var tr = $(this);
            var cbxView = tr.find("input[view]");
            var cbxAdd = tr.find("input[add]");
            var cbxUpdate = tr.find("input[update]");
            var cbxDelete = tr.find("input[delete]");
            var cbxPrint = tr.find("input[print]");
            var cbxExportExcel = tr.find("input[exportexcel]");
            var cbxImportExcel = tr.find("input[importexcel]");

            arr.push({
                Id: tr.attr("id"),
                RoleId: tr.attr("roleid"),
                PageId: tr.attr("pageid"),
                CanView: cbxView.is(":checked"),
                CanAdd: cbxAdd.is(":checked"),
                CanUpdate: cbxUpdate.is(":checked"),
                CanDelete: cbxDelete.is(":checked"),
                CanPrint: cbxPrint.is(":checked"),
                CanExportExcel: cbxExportExcel.is(":checked"),
                CanImportExcel: cbxImportExcel.is(":checked")
            });
        });

        return arr;
    };

    role.saveClick = function () {
        if (!$("#formInfo").valid()) return;
        var data = $("#formInfo").serializeFormJSON();
        data.Permissions = role.getRolePermissionData();

        ajax(settings.saveUrl, data, function (r) {
            if (r.success) {
                $("#divEdit").hide();
                role.goPage(1);
            } else {
                notify.error(r.message);
                _scrollTop();
            }
        });
    };

    role.addNewClick = function () {
        post(settings.addNewUrl, { organizationId: settings.organizationId }, function (res) {
            $("#divEdit").html(res.html).show();
            role.initRoleInfoForm();
            _scrollTo($("#divEdit"));
        });
    };

    role.editClick = function (id) {
        post(settings.editUrl, { id: id }, function (r) {
            if (r.success) {
                $("#divEdit").html(r.html).show();
                role.initRoleInfoForm();
                _scrollTo($("#divEdit"));
            } else {
                notify.error(r.message);
                _scrollTop();
            }
        });
    };

    role.initRoleInfoForm = function() {
        $.validator.unobtrusive.parse($("#formInfo").parent());

        $("#formInfo").submit(function () {
            return false;
        });
    };

}(window.role = window.role || {}, jQuery));