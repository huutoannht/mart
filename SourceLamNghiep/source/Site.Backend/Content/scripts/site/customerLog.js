(function (customerLog, $) {
    var settings = {};

    customerLog.initIndex = function (options) {
        settings = $.extend(settings, options);
    };

    customerLog.goPage = function (pageIndex) {
        $("#Pagination_CurrentPageIndex").val(pageIndex);
        $("#formFilter").submit();
    };

    customerLog.changePageSize = function (pageSizeElement) {
        $("#Pagination_PageSize").val(pageSizeElement.value);
        $("#Pagination_CurrentPageIndex").val(1);
        $("#formFilter").submit();
    };

    customerLog.sort = function (target, colName, direction) {
        $("#SortBy").val(colName);
        $("#SortDirection").val(direction);
        $("#EventFiredFromSortButton").val("True");
        $("#formFilter").submit();
    };

}(window.customerLog = window.customerLog || {}, jQuery));