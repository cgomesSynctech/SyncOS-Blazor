$(document).ready(function () {
        $('#botaoSalvar').click(function () {
            var descricao = $(':input[name=descricao]').val();
            var error = "";
            if (descricao === "") {
                $('#error').html('<span style="color:red; font-size: 0.76562rem;">Descrição é obrigatória.</span>');
                toastr.error("Descrição ", 'Campos obrigatórios: ', {
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
                return false;
            } else {
                $('#infoModal').modal('toggle');
                return false;
            }
        });
    });