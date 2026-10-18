$(document).ready(function () {
    $('#btn-salvar').click(function (e) {
        $('#errorDataInicial').html('');
        $('#errorDataFinal').html('');
        $('#errorVersaoCotepe').html('');
        $('#errorPerfil').html('');

        var camposNaoPreenchidos = "";
        if ($("#dataInicial").val() == "") {
            camposNaoPreenchidos = "Data inicial <br />";
            $('#errorDataInicial').html('<span>Data inicial é obrigatória</span>');
        }
        if ($("#dataFinal").val() == "") {
            camposNaoPreenchidos += "Data final <br />";
            $('#errorDataFinal').html('<span>Data final é obrigatória</span>');
        }
        if ($("#versaoCotepe").val() == "") {
            camposNaoPreenchidos += "Versão Cotepe <br />";
            $('#errorVersaoCotepe').html('<span>Versão cotepe é obrigatória</span>');
        }
            
        if ($("#perfil").val() == "") {
            camposNaoPreenchidos += "Perfil <br />";
            $('#errorPerfil').html('<span>Perfil é obrigatório</span>');
        }

        if (camposNaoPreenchidos != "") {
            console.log(camposNaoPreenchidos);
            toastr.error(camposNaoPreenchidos, 'Campos obrigatórios', {
                closeButton: true,
                progressBar: true,
                debug: false,
                newestOnTop: false,
                positionClass: "toast-top-right",
                preventDuplicates: false,
                onclick: null,
                showDuration: "300",
                hideDuration: "1000",
                timeOut: "5000",
                extendedTimeOut: "1000",
                showEasing: "swing",
                hideEasing: "linear",
                showMethod: "fadeIn",
                hideMethod: "fadeOut",
            });
        }
    });
});