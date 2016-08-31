jQuery.fn.extend({
    creditCardInput: function () {
        return this.each(function () {
            var tbx = $(this);

            var previewCardNumber = function() {
                var number = tbx.val();
                var display = tbx.parent().find("span");

                if (number.isEmpty()) {
                    display.html("");
                } else {
                    var a = "";
                    if (number.length <= 8) {
                        for (var i = 0; i < number.length; i++) {
                            a = a + "*";
                        }
                        display.text(a);
                    } else {
                        var last4 = number.slice(-4);
                        for (var j = 0; j < number.length - 4; j++) {
                            a = a + "*";
                        }
                        a += last4;
                        display.text(a);
                    }
                }
            };

            tbx.keyup(function () {
                previewCardNumber();
            });
            tbx.val(tbx.parent().find("input:hidden").val());
            previewCardNumber();
            tbx.parent().find("input:hidden").remove();
        });
    },
    creditCardCVVInput: function () {
        return this.each(function () {
            var tbx = $(this);

            var previewCardCVV = function () {
                var number = tbx.val();
                var display = tbx.parent().find("span");

                if (number.isEmpty()) {
                    display.html("");
                } else {
                    var a = "";
                    if (number.length <= 1) {
                        for (var i = 0; i < number.length; i++) {
                            a = a + "*";
                        }
                        display.text(a);
                    } else {
                        var last4 = number.slice(-1);
                        for (var j = 0; j < number.length - 1; j++) {
                            a = a + "*";
                        }
                        a += last4;
                        display.text(a);
                    }
                }
            };

            tbx.keyup(function () {
                previewCardCVV();
            });
            tbx.val(tbx.parent().find("input:hidden").val());
            previewCardCVV();
            tbx.parent().find("input:hidden").remove();
        });
    }
});