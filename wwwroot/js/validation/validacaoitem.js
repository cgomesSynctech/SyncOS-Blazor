$(document).ready(function () {
    $('#botaoSalvar').click(function () {
        var descricao = $(':input[name=descricao]').val();
        var clasFiscal = $(':input[name=clasFiscal]').val();
        var aLIQICMS = $(':input[name=aLIQICMS]').val();
        var preco = $(':input[id=Precos_0__Preco]').val();
        

        
        var camposNaoPreenchidos = "";
       
        var error = "";
        var abrirModal = true;

        if (aLIQICMS === "") {
            camposNaoPreenchidos += "ALIQ. ICMS <br/>";
            document.getElementById("aLIQICMS").focus();
            $('#errorALIQICMS').html('<span style="color:red; font-size: 0.76562rem;">Campo é obrigatório.</span>');
            abrirModal = false;
        } else {
            $('#errorALIQICMS').html('');
        }

        if (clasFiscal === "") {
            camposNaoPreenchidos += "NCM <br/>";
            document.getElementById("clasFiscal").focus();
            $('#errorClasFiscal').html('<span style="color:red; font-size: 0.76562rem;">NCM é obrigatório.</span>');
            abrirModal = false;
        } else {
            if (!NCMValido) {
                camposNaoPreenchidos += "NCM Inválido <br/>";
                document.getElementById("clasFiscal").focus();
                $('#errorClasFiscal').html('<span style="color:red; font-size: 0.76562rem;">Informe um NCM válido!</span>');
                abrirModal = false;

            } else {
                $('#errorClasFiscal').html('');
            }
        }

        if (descricao === "") {
            camposNaoPreenchidos += "Descrição <br/>";
            document.getElementById("descricao").focus();
            $('#errorDescricao').html('<span style="color:red; font-size: 0.76562rem;">Descrição é obrigatório.</span>');
            abrirModal = false;
        } else {
            $('#errorDescricao').html('');
        }
        if (preco === "" || preco === null || preco === "0") {
            camposNaoPreenchidos += "Preço <br/>";
            $('#tabelaPrecoModal').modal('toggle');
            document.getElementById("ItensPreco_0__Preco").focus();
            $('#errorPreco').html('<span style="color:red; font-size: 0.76562rem;">Preço é obrigatório.</span>');
            $('#errorPreco2').html('<span style="color:red; font-size: 0.76562rem;">Preço é obrigatório.</span>');

            abrirModal = false;
        } else {
            $('#errorPreco').html('');
            $('#errorPreco2').html('');
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