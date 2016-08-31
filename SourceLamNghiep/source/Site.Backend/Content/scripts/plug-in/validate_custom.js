$(document).ready(function () {
    var arr = $("input.currency");
    if (arr.length > 0) {
        arr.rules('remove', 'number');
    }
    arr = $("input.number-positive-integer");
    if (arr.length > 0) {
        arr.rules('remove', 'number');
    }
});

jQuery.extend(jQuery.validator.methods, {
    date: function (value, element) {
        return true;
    },
    number: function (value, element) {
        return true;
    }
});