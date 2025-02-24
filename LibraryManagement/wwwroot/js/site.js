$(function () {
    $(window).on('beforeunload', function () {
        $("#Loader").removeClass('d-none');
    })
})