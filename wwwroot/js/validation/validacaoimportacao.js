$(document).ready(function () {

    $("#btn_submit_download_xml").click(function () {
        var chave = $('#chaveXML').val();
        var oldUrl = $(this).attr("href");
        var newUrl = oldUrl.replace(oldUrl, "/importacao/download/?chave=" + chave);
        $(this).attr("href", newUrl);
    });

    $('#formXml').submit(function (form) {
        var isOK = true;
        var arquivoXML = $(':input[name=arquivoXML]').val();
        if (arquivoXML.trim() === "") {
            $('#errorXml').html('<span style="color:red; font-size: 0.76562rem;">Selecione um arquivo XML</span>');
            isOK = false;
        } else {
            $('#errorXml').html('');
        }
        if (!isOK) {
            return false;
        } else {
            showProgress();
            disableClick();
        }
    });

    $('#chaveXML').keypress(function () {
        var chaveXML = $('#chaveXML').val();
        if (chaveXML.length == 43) {
            console.log(chaveXML.length);
            $('#btn_submit_download_xml').removeClass("disabled");
        } else {
            console.log(chaveXML.length);
            $('#btn_submit_download_xml').addClass("disabled");
        }
    });


    $('#chaveXML').keyup(function (e) {
        switch (e.keyCode) {
            case 8:  // Backspace
                var chaveXML = $('#chaveXML').val();
                if ((chaveXML.length - 1) == 43) {
                    console.log(chaveXML.length - 1);
                    $('#btn_submit_download_xml').removeClass("disabled");
                } else {
                    console.log(chaveXML.length - 1);
                    $('#btn_submit_download_xml').addClass("disabled");
                }
                break;
            default:
        }
    });


    $("#chaveXML").focusout(function () {
        var chaveXML = $('#chaveXML').val();
        if (chaveXML.length == 44) {
            console.log(chaveXML.length);
            $('#btn_submit_download_xml').removeClass("disabled");
        } else {
            console.log(chaveXML.length);
            $('#btn_submit_download_xml').addClass("disabled");
        }
    });

});