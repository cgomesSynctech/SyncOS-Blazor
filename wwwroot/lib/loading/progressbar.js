$(document).ready(function () {

    /* EXIBE O MODAL COM A PROGRESS BAR */
    function showProgress() {
        setTimeout(function () {
            var modal = $('<div />');
            modal.addClass("modal");
            $('body').append(modal);
            var loading = $(".loading");
            var popup = $('#popup');
            loading.show();
            popup.show();
            var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
            var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
            loading.css({ top: top, left: left });
        }, 200);
    }

    /* DESABILITA O CLICK NOS LINKS */
    function disableClick() {
        $('a').css({
            'pointer-events': 'none',
            'cursor': 'default',
            'text-decoration': 'none',
            'color': '#636e72'
        });
    }

    /* DEFINE OS LINKS */
    $('#rItens').click(function () {
        showProgress();
        disableClick();
    });

    $('#botaoProcurarPorData').click(function () {
        var date = $('#dataIni').val();
        if (date != "") {
            showProgress();
            disableClick();
        }
    });

    $('#btn_submit_xml').click(function () {
        showProgress();
        disableClick();
    });

    $('#formXmlOK').submit(function (form) {
        showProgress();
        disableClick();
    });

});