$(document).ready(function () {
    $('#botaoSalvar').click(function () {
        var nome = $(':input[name=nome]').val();
        var nascimento = $(':input[name=dtnascimento]').val();

        var camposNaoPreenchidos = "";
        var error = "";
        var abrirModal = true;

        if (nome === "") {
            camposNaoPreenchidos += "Nome <br/>";
            $('#errorNome').html('<span style="color:red; font-size: 0.76562rem;">Nome é obrigatório.</span>');
            abrirModal = false;
        } else {
            $('#errorNome').html('');
        }

        if (nascimento === "") {
            camposNaoPreenchidos += "Data de Nascimento <br/>";
            $('#errorNascimento').html('<span style="color:red; font-size: 0.76562rem;">Data de Nascimento é obrigatório.</span>');
            abrirModal = false;
        } else {
            $('#errorNascimento').html('');
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
    })
});