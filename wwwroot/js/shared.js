function s_jump(target) {
    $(location).attr('href', target)
}

var active

$(document).ready(function () {
    $("#navigation").find("li").each(function () {
        var a = $(this).find("a:first")[0];
        if ($(a).attr("href") === location.pathname) {
            $(this).addClass("active");
        } else {
            $(this).removeClass("active");
        }
    });
});

