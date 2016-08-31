﻿(function (site, $) {

    var settings = {};

    site.init = function (options) {
        settings = $.extend(settings, options);
        site.initTimeZone();
    };

    site.initTimeZone = function () {
        var offset = this.calculateTimeZone();
        if ($.cookie(settings.browserTimeZone) == null || $.cookie(settings.browserTimeZone) != offset) {
            $.cookie(settings.browserTimeZone, offset, { path: '/' });
        }
    };

    site.calculateTimeZone = function() {
        var rightNow = new Date();
        var jan1 = new Date(rightNow.getFullYear(), 0, 1, 0, 0, 0, 0);  // jan 1st
        var june1 = new Date(rightNow.getFullYear(), 6, 1, 0, 0, 0, 0); // june 1st
        var temp = jan1.toGMTString();
        var jan2 = new Date(temp.substring(0, temp.lastIndexOf(" ") - 1));
        temp = june1.toGMTString();
        var june2 = new Date(temp.substring(0, temp.lastIndexOf(" ") - 1));
        var std_time_offset = (jan1 - jan2) / (1000 * 60 * 60);
        var daylight_time_offset = (june1 - june2) / (1000 * 60 * 60);
        var dst;
        if (std_time_offset == daylight_time_offset) {
            dst = "0"; // daylight savings time is NOT observed
        } else {
            // positive is southern, negative is northern hemisphere
            var hemisphere = std_time_offset - daylight_time_offset;
            if (hemisphere >= 0)
                std_time_offset = daylight_time_offset;
            dst = "1"; // daylight savings time is observed
        }
        // Here set the value of hidden field to the ClientTimeZone.
        return site.convertTimezoneOffsetToStandardFormat(std_time_offset);
    };

    site.convertTimezoneOffsetToStandardFormat = function (value) {
        var hours = parseInt(value);
        value -= parseInt(value);
        value *= 60;
        var mins = parseInt(value);
        value -= parseInt(value);
        value *= 60;
        var secs = parseInt(value);
        var display_hours = hours;
        // handle GMT case (00:00)
        if (hours == 0) {
            display_hours = "00";
        } else if (hours > 0) {
            // add a plus sign and perhaps an extra 0
            display_hours = (hours < 10) ? "+0" + hours : "+" + hours;
        } else {
            // add an extra 0 if needed
            display_hours = (hours > -10) ? "-0" + Math.abs(hours) : hours;
        }
        mins = (mins < 10) ? "0" + mins : mins;
        return display_hours + ":" + mins;
    };

    site.changeLanguage = function (code) {
        $.cookie(settings.languageCode, code, { path: '/', expires: 365 });
        window.location = window.location.href.replace('#', '');
    };

}(window.site = window.site || {}, jQuery));

