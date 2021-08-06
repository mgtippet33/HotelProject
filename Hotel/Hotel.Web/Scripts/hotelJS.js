$(document).ready(function () {
    if ($(document).height() <= $(window).height())
        $('footer.footer').addClass('navbar-fixed-bottom');

    $('.input-daterange').datepicker({
        format: 'dd-mm-yyyy',
        autoclose: true,
        calendarWeeks: true,
        clearBtn: true,
        disableTouchKeyboard: true
    });

    $('#start').click(function () {
        $('#fa-2').hide();
    });
    $('#end').click(function () {
        $('#fa-1').hide();
    });

});