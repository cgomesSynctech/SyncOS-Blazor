$(document).ready(function () {
    $('#botaoSalvar').click(function () {
        var descricaoGrupo = $(':input[name=descricaoGrupo]').val();

        var camposNaoPreenchidos = "";

        var error = "";
        var abrirModal = true;
        if (descricaoGrupo === "") {
            camposNaoPreenchidos += "Descrição <br/>";
            $('#errorDescricaoGrupo').html('<span style= "color:red; font-size: 0.76562rem;"> Descrição grupo é obrigatório.</span> ');
            abrirModal = false;
        } else {
            $('#errorDescricaoGrupo').html('');
        }                
        if (abrirModal) {
            $('#infoModal').modal('toggle');
            return false;
        } else {
            toastr.error(camposNaoPreenchidos, 'Campos obrigatórios: ', {
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