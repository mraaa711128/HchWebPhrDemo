$(function () {
    hideLoading();
});

function showLoading(msg) {
    var modal = $('<div id="bg-modal" class="load-modal" />');
    $('body').append(modal);

    var loading = $('<div id="pn-loading" class="loading" />');
    $('body').append(loading);
    loading.show();
    loading.append('<h4>' + msg + '</h4>');
    //var img_top = loading[0].offsetHeight / 2 - 32 / 2;
    //$("#pn-loading img").css({ 'margin-top': img_top });
    var h4_top = loading[0].offsetHeight / 2 - $("#pn-loading h4").outerHeight() / 2;
    var h4_left = loading[0].offsetWidth / 2 - $("#pn-loading h4").outerWidth() / 2;
    $("#pn-loading h4").css({ 'margin': h4_top + "px " + (h4_left - 1) + "px" });
    //$("#pn-loading").css({ "*padding-top": h4_top + "px" });

    var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
    var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
    loading.css({ top: top, left: left });

    //if ($.browser.msie && $.browser.version < 8) {
    //    var height = loading[0].offsetHeight - h4_top;
    //    loading.css({ "padding": h4_top + "px 0px 0px 0px", "height": height });
    //}
}

function hideLoading() {
    var loading = $("#pn-loading");
    if (loading.length > 0) {
        //loading[0].hide();
        //loading[0].remove();
        loading.hide();
        loading.remove();
    }
    var modal = $("#bg-modal");
    if (modal.length > 0){
        //modal[0].hide();
        //modal[0].remove();
        modal.hide();
        modal.remove();
    }
    //$("#pn-loading").hide();
    //$("#bg-modal").hide();
    //$("#pn-loading").remove();
    //$('#bg-modal').remove();
}
