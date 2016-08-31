

String.prototype.isEmpty = function () {
    return $.trim(this) == "";
};

jQuery.fn.tagName = function () {
    return this.prop("tagName").toLowerCase();
};

String.prototype.parseServerDateTime = function () {
    return parseFloat(this.replace("/Date(", "").replace(")/", ""));
};

String.prototype.format = function () {
    var s = this;
    for (var i = 0; i < arguments.length; i++) {
        var reg = new RegExp("\\{" + i + "\\}", "gm");
        s = s.replace(reg, arguments[i]);
    }
    return s;
}

String.prototype.replaceAll = function (search, replacement) {
    var target = this;
    return target.split(search).join(replacement);
};