(function (customer, $) {
    var dateStr = "01/01/1900";
    var subfix = "Str";
    var model = {};

    var geocoder;
    var map;
    var directionsService;
    var directionsDisplay;
    var customerMarker = null;

    var companyLocation = null;
    var customerLocation = null;

    var mapInViewMode = true;

    var settings = {};

    customer.initIndex = function (options) {
        settings = $.extend(settings, options);

        $("#formFilter #City").change(function () {
            var city = $("#formFilter #City").val();

            post(settings.getDropDownDistrictsForFilterIndexUrl, {
                City: city
            }, function (res) {
                if (res.success) {
                    $("#formFilter #divDistrictFilters").html(res.dropDownDistrict);
                } else {
                    notify.error(res.message, $("#divMainNotify"));
                }
            });
        });

        customer.initListCustomer();
        customer.initFileImport();
    };

    customer.initEdit = function (options) {
        settings = $.extend(settings, options);
        $.validator.addMethod('accept', function () { return true; });

        mapInViewMode = false;

        model = settings.model;

        customer.initVisits();
        customer.initInfoForm();
        customer.initTabs();
    };

    customer.loadCustomerLocation = function () {

        if (model.Lat != null && model.Lng != null) {
            customerLocation = new google.maps.LatLng(model.Lat, model.Lng);
            customer.initCustomerMarker();
            return;
        }

        var address = "", cityName = "", districtName = "";
        var cityId = 0;
        var districtId = 0;

        if (mapInViewMode) {
            address = model.Address == null ? "" : model.Address;
            cityId = model.City;
            districtId = model.District;
        } else {
            address = $("#formInfo #Address").val();
            cityId = $("#formInfo #City").val();
            districtId = $("#formInfo #District").val();
        }

        POST_BLOCK_UI = false;
        POST_SHOW_LOADING = false;

        post(settings.getCityNameDistrictNameUrl, {
            cityId: cityId,
            districtId: districtId
        }, function(res) {
            if (res.success) {
                cityName = res.cityName;
                districtName = res.districtName;

                geocoder.geocode({
                    'address': String.format("{0}, {1}, {2}, {3}", address, districtName, cityName, "viet nam")
                }, function (results, status) {
                    if (status == 'OK') {
                        customerLocation = results[0].geometry.location;
                        customer.initCustomerMarker();
                    }
                });
            }
        });
    };

    customer.initCustomerMarker = function () {
        if (customerMarker != null) {
            customerMarker.setMap(null);
        }

        customerMarker = new google.maps.Marker({
            map: map,
            position: customerLocation,
            draggable: true,
        });

        map.setCenter(customerLocation);

        customerMarker.addListener('dragend', function(e) {
            customerLocation = e.latLng;
        });
    };

    customer.addCustomerMarker = function() {
        customerLocation = map.getCenter();
        customer.initCustomerMarker();
    };

    customer.showDirection = function() {
        if (customerLocation == null || companyLocation == null) {
            notify.error(settings.canNotLoadDirectionMsg, $("#divNotifyMap"));
            return;
        }

        directionsService.route({
            origin: companyLocation,
            destination: customerLocation,
            travelMode: 'DRIVING'
        }, function (response, status) {
            if (status === 'OK') {
                directionsDisplay.setDirections(response);
            } else {
                notify.error(settings.canNotFindRouteMsg, $("#divNotifyMap"));
            }
        });
    };

    customer.initVisits = function() {
        $(model.Visits).each(function () {
            var item = this;
            var milli = item.DateVisit.replace(/\/Date\((-?\d+)\)\//, '$1');
            var dateVisit = new Date(parseInt(milli));
            item.DateVisit = moment(dateVisit).format(DATE_FORMAT_JS);
        });
    };

    customer.initEditImages = function (options) {
        settings = $.extend(settings, options);
        $.validator.addMethod('accept', function () { return true; });
    };

    customer.goPage = function (pageIndex) {
        $("#Pagination_CurrentPageIndex").val(pageIndex);
        customer.getListCustomer();
    };

    customer.changePageSize = function (pageSizeElement) {
        $("#Pagination_PageSize").val(pageSizeElement.value);
        $("#Pagination_CurrentPageIndex").val(1);
        customer.getListCustomer();
    };

    customer.sort = function (target, colName, direction) {
        $("#SortBy").val(colName);
        $("#SortDirection").val(direction);
        $("#EventFiredFromSortButton").val("True");

        $("#formFilter").submit();
    };

    customer.initMap = function () {
        geocoder = new google.maps.Geocoder();
        var latlng = new google.maps.LatLng(DEFAULT_LAT, DEFAULT_LNG);
        var mapOptions = {
            zoom: DEFAULT_ZOOM,
            center: latlng
        }

        map = new google.maps.Map($('#map').get(0), mapOptions);

        directionsService = new google.maps.DirectionsService;
        directionsDisplay = new google.maps.DirectionsRenderer;

        directionsDisplay.setMap(map);
    };

    customer.locateCompanyLocation = function () {
        if (companyLocation != null) {
            return;
        }

        geocoder.geocode({ 'address': settings.companyAddress }, function (results, status) {
            if (status == 'OK') {
                companyLocation = results[0].geometry.location;
            }
        });
    };

    customer.viewCustomer = function(id) {
        post(settings.viewCustomerUrl, { id: id }, function(res) {
            if (res.success) {
                model = res.model;
                customer.initVisits();

                $("#divModal").html(res.html).modal();

                initICheck($("#divTabVisit"));
                initCheckboxDeleteInList($("#divTabVisit"));
                initICheck($("#divDataLog"));
                initCheckboxDeleteInList($("#divDataLog"));

                customer.initTabs();
            } else {
                notify.error(res.message, $("#divMainNotify"));
            }
        });
    };

    customer.initTabs = function() {
        $("a[href='#divTabMap']").on('shown.bs.tab', function () {
            if ($("a[href='#divTabMap']").get(0).inited) {
                return;
            }

            $("a[href='#divTabMap']").get(0).inited = true;

            customer.initMap();

            customer.loadCustomerLocation();
            customer.locateCompanyLocation();
        });
    };

    customer.getListCustomer = function () {
        var data = $("#formFilter").serializeFormJSON();

        post(settings.getListCustomerUrl, data, function (res) {
            if (res.success) {
                $("#divList").html(res.html);
                customer.initListCustomer();
            } else {
                notify.error(res.message, $("#divMainNotify"));
            }
        }, null, true);
    };

    customer.initListCustomer = function() {
        initICheck($("#divList"));
        initCheckboxDeleteInList();
    };

    customer.transferDateVisitFormatForSave = function() {
        $(model.Visits).each(function () {
            var item = this;
            item.DateVisit = moment(item.DateVisit, DATE_FORMAT_JS).format(JS_DATE_FORMAT_TO_CSHARP_DATE_FORMAT);
        });
    };

    customer.revertDateVisitFormatWhenSaveFailed = function () {
        $(model.Visits).each(function () {
            var item = this;
            item.DateVisit = moment(item.DateVisit, JS_DATE_FORMAT_TO_CSHARP_DATE_FORMAT).format(DATE_FORMAT_JS);
        });
    };

    customer.initInfoForm = function () {
        $("#formInfo").submit(function () {
            return false;
        });

        $("#City").rules("remove", "required");
        $("#District").rules("remove", "required");

        setupSelect($("#formInfo"));

        $("#formInfo #City").change(function () {
            var city = $("#formInfo #City").val();

            post(settings.getDropDownDistrictsUrl, {
                City: city
            }, function (res) {
                if (res.success) {
                    $("#formInfo #divDistricts").html(res.dropDownDistrict);

                    setupSelect($("#formInfo #divDistricts"));
                } else {
                    notify.error(res.message, $("#divNotifyEdit"));
                    _scrollTo($("#formInfo"));
                }
            });
        });

        customer.initUsingDevice();

        if ($("#selInterestingDevice").length > 0) {
            $("#selInterestingDevice").multipleSelect({
                filter: true
            });
        }

        $("#selAssignTo").multipleSelect({
            filter: true
        });

        setupTimePicker($("#formInfo"));

        setupDatePicker($("#formInfo"));

        initICheck($("#divTabWorkingHours"));

        if ($("#fileImage1").length > 0) {
            var obj = {};
            obj = $.extend(obj, fileInputSettings);

            $("#fileImage1").fileinput(obj);
        }
    };

    customer.initUsingDevice = function() {
        $("#divUsingDevice .tree").each(function () {
            var opt = {
                'plugins': ["wholerow", "checkbox"],
                'core': {
                    'data': [],
                    "themes": {
                        "icons": false
                    }
                }
            };

            var parentId = $(this).attr("parentid");

            var cate = jlinq.from(settings.productCatesTree).equals("Id", parentId).select()[0];

            var parent = {
                id: cate.Id,
                "text": cate.Name,
                "children": [],
                "state": {
                    "selected": $("#UsingDevices").val().indexOf(cate.Id) >= 0
                }
            };

            function appendChild(p, c) {
                $(c.Childs).each(function () {
                    var child = this;

                    var item = {
                        id: child.Id,
                        "text": child.Name,
                        "children": [],
                        "state": {
                            "selected": $("#UsingDevices").val().indexOf(child.Id) >= 0
                        }
                    };

                    p.children.push(item);
                    appendChild(item, child);
                });
            }

            appendChild(parent, cate);

            opt.core.data.push(parent);

            $(this).jstree(opt);
        });
    };

    customer.saveCustomerClick = function () {
        hideValidateMessage($("#formInfo"));
        notify.hide();

        customer.buildSaveModel();

        if (!$("#formInfo").valid()) {
            _scrollTop();
            return;
        }

        if (!customer.verifyWorkingHours()) {
            _scrollTop();
            return;
        }

        var data = new FormData($("#formInfo")[0]);

        ajaxFormFile(settings.saveCustomerUrl, data, function (res) {
            if (res.success) {
                customer.backToIndex();
            } else {
                customer.revertDateVisitFormatWhenSaveFailed();
                notify.error(res.message, $("#divNotifyEdit"));
                _scrollTop();
            }
        });
    };

    customer.deleteCustomer = function (id) {
        jConfirm(CONFIRM_DELETE_MESSAGE, CONFIRM_DELETE_TITLE, function (r) {
            if (!r) return;
            post(settings.deleteCustomerUrl, {
                id: id,
            }, function (res) {
                if (res.success) {
                    notify.success(res.message, $("#divMainNotify"));
                    customer.goPage(1);
                } else {
                    notify.error(res.message, $("#divMainNotify"));
                }
                _scrollTop();
            });
        });
    };

    customer.deleteCustomers = function() {
        var arr = [];
        $("input[cbxdelete='True']:checked").each(function() {
            arr.push($(this).attr("itemid"));
        });

        if (arr.length == 0) {
            notify.warning(settings.mustSelectAtLeast1CustomerToDeleteMsg, $("#divMainNotify"));
            return;
        }

        jConfirm(CONFIRM_DELETE_MESSAGE, CONFIRM_DELETE_TITLE, function (r) {
            if (!r) return;
            post(settings.deleteCustomersUrl, {
                ids: arr.join("#"),
            }, function (res) {
                if (res.success) {
                    notify.success(res.message, $("#divMainNotify"));
                    customer.goPage(1);
                } else {
                    notify.error(res.message, $("#divMainNotify"));
                }
                _scrollTop();
            });
        });
    };

    customer.verifyWorkingHours = function () {
        for (var i = 0; i < settings.weekDays.length; ++i) {
            var item = settings.weekDays[i];
            var startTime = $("#" + item.Name + "Start" + subfix).val();
            var endTime = $("#" + item.Name + "End" + subfix).val();

            if ($("#" + item.Name + "Work").is(":checked")) {
                if (startTime.isEmpty()) {
                    notify.error(String.format(settings.workingHoursRequiredMsg, settings.startMsg, item.NameLocal), $("#divNotifyEdit"));
                    return false;
                }

                if (endTime.isEmpty()) {
                    notify.error(String.format(settings.workingHoursRequiredMsg, settings.endMsg, item.NameLocal), $("#divNotifyEdit"));
                    return false;
                }
            }

            if (!startTime.isEmpty() && !endTime.isEmpty()) {
                var format = "DD/MM/YYYY hh:mm a";
                var startDate = moment(dateStr + " " + startTime, format);
                var endDate = moment(dateStr + " " + endTime, format);

                if (startDate.isAfter(endDate) || startDate.isSame(endDate)) {
                    notify.error(item.Name + " : " + settings.workingHourInvalidMsg, $("#divNotifyEdit"));
                    return false;
                }
            }
        }
        return true;
    };

    customer.buildSaveModel = function() {
        //start - using device

        var usingDeviceIds = "";

        $("#formInfo .tree").each(function () {
            var parentId = $(this).attr("parentid");
            var selectedIds = customer.getSelectedUsingDevices(parentId);

            $(selectedIds).each(function () {
                var id = this.toString();
                usingDeviceIds = usingDeviceIds + "#" + id;
            });
        });

        $("#formInfo #UsingDevices").val(usingDeviceIds);

        //--end - using device

        if ($('#selInterestingDevice').length > 0) {
            $("#InterestingDevice").val($('#selInterestingDevice').multipleSelect('getSelects').join("#"));
        }

        $("#AssignTo").val($('#selAssignTo').multipleSelect('getSelects').join("#"));
        
        customer.transferDateVisitFormatForSave();
        $("#VisitJsonString").val(JSON.stringify(model.Visits));

        //lat, lng
        if (customerLocation != null) {
            $("#Lat").val(toCultureNumber(customerLocation.lat()));
            $("#Lng").val(toCultureNumber(customerLocation.lng()));
        }
    };

    customer.getSelectedUsingDevices = function (parentId) {
        var arrIds = [];
        $($("#divUsingDevice div.tree[parentid='" + parentId + "']").jstree("get_selected")).each(function () {
            var id = this.toString();

            if (id.indexOf("_") >= 0) {
                return;
            }

            arrIds.push(id);
        });
        return arrIds;
    };
    //start Visit
    customer.goPageVisit = function (pageIndex) {
        model.VisitIndex.Pagination.CurrentPageIndex = pageIndex;
        customer.getVisits();
    };

    customer.changePageSizeVisit = function (pageSizeElement) {
        model.VisitIndex.Pagination.PageSize = pageSizeElement.value;
        model.VisitIndex.Pagination.CurrentPageIndex = 1;
        customer.getVisits();
    };

    customer.sortVisit = function (target, colName, direction) {
        model.VisitIndex.SortBy = colName;
        model.VisitIndex.SortDirection = direction;
        model.VisitIndex.EventFiredFromSortButton = true;

        customer.getVisits();
    };

    customer.getVisits = function() {
        ajax(settings.getVisitsUrl, model, function(res) {
            if (res.success) {
                $("#divTabVisit").html(res.html);
                initICheck($("#divTabVisit"));
                initCheckboxDeleteInList($("#divTabVisit"));
            } else {
                notify.error(res.message, $("#divNotifyVisits"));
                _scrollTop();
            }
        });
    };

    customer.editVisit = function(id) {
        ajax(settings.editVisitUrl, {
            model: model,
            id: id
        }, function(res) {
            if (res.success) {
                $("#divEditVisit").html(res.html).show();
                setupDatePicker($("#divEditVisit"));
                $("#formInfoVisit").submit(function () {
                    return false;
                });

                $.validator.unobtrusive.parse($("#formInfoVisit"));
                _scrollTo($("#divEditVisit"));
            } else {
                notify.error(res.message, $("#divNotifyEdit"));
                _scrollTop();
            }
        });
    };

   

    customer.cancelEditVisit = function () {
        $("#divEditVisit").hide().empty();
    };

    customer.saveVisitClick = function() {
        if (!$("#formInfoVisit").valid()) {
            return;
        }

        var id = $("#formInfoVisit #Id").val();
        if (id != CONST_EMPTY_GUID) {
            model.Visits = jlinq.from(model.Visits).not().equals("Id", id).select();
        }

        model.Visits.push($("#formInfoVisit").serializeFormJSON());

        customer.goPageVisit(1);
        customer.cancelEditVisit();
    };

    customer.deleteVisit = function (id) {
        customer.cancelEditVisit();
        jConfirm(CONFIRM_DELETE_MESSAGE, CONFIRM_DELETE_TITLE, function (r) {
            if (!r) return;
            model.Visits = jlinq.from(model.Visits).not().equals("Id", id).select();
            customer.goPageVisit(1);
        });
    };
    //end Visit

    //start CustomerService

    customer.goPageCustomerService = function (pageIndex) {
        model.VisitIndex.Pagination.CurrentPageIndex = pageIndex;
        customer.getCustomerServices();
    };

    customer.changePageSizeCustomerService = function (pageSizeElement) {
        model.VisitIndex.Pagination.PageSize = pageSizeElement.value;
        model.VisitIndex.Pagination.CurrentPageIndex = 1;
        customer.getCustomerServices();
    };

    customer.sortCustomerService = function (target, colName, direction) {
        model.VisitIndex.SortBy = colName;
        model.VisitIndex.SortDirection = direction;
        model.VisitIndex.EventFiredFromSortButton = true;

        customer.getCustomerServices();
    };

    customer.getCustomerServices = function () {
        ajax(settings.getCustomerServicesUrl, model, function (res) {
            if (res.success) {
                $("#divTabVisit").html(res.html);
                initICheck($("#divTabVisit"));
                initCheckboxDeleteInList($("#divTabVisit"));
            } else {
                notify.error(res.message, $("#divNotifyVisits"));
                _scrollTop();
            }
        });
    };



    customer.editCustomerService = function (id) {
        ajax(settings.editCustomerServiceUrl, {
            model: model,
            id: id
        }, function (res) {
            if (res.success) {
                $("#divEditVisit").html(res.html).show();
                setupDatePicker($("#divEditVisit"));
                $("#formInfoCS").submit(function () {
                    return false;
                });

                $.validator.unobtrusive.parse($("#formInfoCS"));
                _scrollTo($("#divEditVisit"));
            } else {
                notify.error(res.message, $("#divNotifyEdit"));
                _scrollTop();
            }
        });
    };

    customer.deleteCustomerService = function (id) {
        customer.cancelEditVisit();
        jConfirm(CONFIRM_DELETE_MESSAGE, CONFIRM_DELETE_TITLE, function (r) {
            if (!r) return;
            model.Visits = jlinq.from(model.Visits).not().equals("Id", id).select();
            customer.goPageVisit(1);
        });
    };
    //end CustomerService
 
    customer.addNewImageClickOnEditCustomer = function () {
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

    customer.addNewImageClick = function (customerId) {
        post(settings.getAddNewImageHtmlUrl, {
            customerId: customerId
        }, function (res) {
            if (res.success) {
                $("#divAddNewImage").html(res.html).show();

                var obj = {};
                obj = $.extend(obj, fileInputSettings);

                $("#fileImage").fileinput(obj);
            } else {
                notify.error(res.message, $("#divNotifyEdit"));
            }
        });
    };

    customer.backToIndex = function() {
        gotoUrl(settings.indexUrl);
    };

    customer.cancelAddNewImage = function () {
        $("#divAddNewImage").hide();
        notify.hide();
    };

    customer.saveImage = function () {

        var ele = $("#fileImage").get(0);
        if (ele.value.length < 4) {
            notify.error(settings.imageIsRequiredMsg, $("#divNotifyEdit"));
            return;
        }

        var data = new FormData($("#formCustomerImage")[0]);

        ajaxFormFile(settings.saveImageUrl, data, function (res) {
            if (res.success) {
                customer.cancelAddNewImage();
                $("#divImageLists").html(res.imageList).show();
                customer.initImagesList();
            } else {
                notify.error(res.message, $("#divNotifyEdit"));
                _scrollTop();
            }
        });
    };

    customer.initImagesList = function() {
        initICheck($("#divImageLists"));
        initCheckboxDeleteInList($("#divImageLists"));
    };

    customer.deleteImage = function (id, customerId) {
        jConfirm(CONFIRM_DELETE_MESSAGE, CONFIRM_DELETE_TITLE, function (r) {
            if (!r) return;
            post(settings.deleteImageUrl, {
                id: id,
                customerId: customerId
            }, function (res) {
                if (res.success) {
                    customer.cancelAddNewImage();
                    $("#divImageLists").html(res.imageList).show();
                    customer.initImagesList();
                } else {
                    notify.error(res.message, $("#divNotifyEdit"));
                    _scrollTop();
                }
            });
        });
    };

    customer.importClick = function() {
        $("#fileImport").click();
    };

    customer.initFileImport = function () {
        $("#fileImport").change(function() {
            var data = new FormData($("#formImport")[0]);

            ajaxFormFile(settings.importCustomersUrl, data, function (res) {
                if (res.success) {
                    notify.success(res.message, $("#divMainNotify"));
                    customer.goPage(1);
                } else {
                    notify.error(res.message, $("#divMainNotify"));
                    post(settings.getImportFormUrl, null, function(response) {
                        $("#divImport").html(response.html);
                        customer.initFileImport();
                    }, null, true);
                }

                _scrollTop();
            });
        });
    };

    customer.export = function () {
        notify.hide();
        var data = $("#formFilter").serializeFormJSON();
        window.location = settings.exportCustomersUrl + "?" + $.param(data);
    };

    customer.changeCustomerTypeClick = function(id) {
        post(settings.getChangeCustomerFormUrl, { id: id }, function(res) {
            if (res.success) {
                $("#divModal").html(res.html).modal();
                $.validator.unobtrusive.parse($("#formInfo"));
            } else {
                notify.error(res.message, $("#divMainNotify"));
                _scrollTop();
            }
        });
    };

    customer.changeCustomerType = function (id) {
        if (!$("#formInfo").valid()) {
            return;
        }

        var data = $("#formInfo").serializeFormJSON();

        post(settings.updateCustomerTypeUrl, data, function (res) {
            if (res.success) {
                $("#divModal").modal("hide");
                notify.success(res.message, $("#divMainNotify"));
                customer.getListCustomer();
            } else {
                notify.error(res.message, $("#divNotifyPopup"));
                _scrollTop();
            }
        });
    };

    //--data log

    customer.goPageDataLog = function (pageIndex) {
        model.DataLogIndex.Pagination.CurrentPageIndex = pageIndex;
        customer.getDataLogs();
    };

    customer.changePageSizeDataLog = function (pageSizeElement) {
        model.DataLogIndex.Pagination.PageSize = pageSizeElement.value;
        model.DataLogIndex.Pagination.CurrentPageIndex = 1;
        customer.getDataLogs();
    };

    customer.sortDataLog = function (target, colName, direction) {
        model.DataLogIndex.SortBy = colName;
        model.DataLogIndex.SortDirection = direction;
        model.DataLogIndex.EventFiredFromSortButton = true;

        customer.getDataLogs();
    };

    customer.getDataLogs = function () {
        ajax(settings.getDataLogsUrl, model, function (res) {
            if (res.success) {
                $("#divDataLog").html(res.html);
                initICheck($("#divDataLog"));
                initCheckboxDeleteInList($("#divDataLog"));
            } else {
                notify.error(res.message, $("#divNotifyDataLog"));
                _scrollTop();
            }
        });
    };

}(window.customer = window.customer || {}, jQuery));