$(document).ready(function () {
    $('#botaoSalvar').click(function () {
        var tableSize = $('#table_itens tbody tr').length;
        
        $("table tbody").find('#zero').each(function () {
            tableSize = 0;
        });

        var operacao = $('#comboboxparametro').val();
        var data = $('#data').val();
        var tipoFavorecido = $('#comboboxtipoFavorecido').val();
        var nomeCliente = $('#cliSearch').val();
        var nomeFornecedor = $('#fornecedorSearch').val();
        var baseCalcICMS = $(':input[name=baseCalcICMS]').val();
        var valorICMS = $(':input[name=valorICMS]').val();
        var baseCalcSubst = $(':input[name=baseCalcSubst]').val();
        var valorICMSSubst = $(':input[name=valorICMSSubst]').val();
        var totalItens = $(':input[name=totalItens]').val();
        var frete = $(':input[name=frete]').val();
        var valorSeguro = $(':input[name=valorSeguro]').val();
        var outrasDespesas = $(':input[name=outrasDespesas]').val();
        var valorIPI = $(':input[name=valorIPI]').val();
        var total = $(':input[name=total]').val();
        var desconto = $(':input[name=desconto]').val();
        var juros = $(':input[name=juros]').val();
        var totalFinal = $(':input[name=totalFinal]').val();
        var camposNaoPreenchidos = "";
        var error = "";
        var abrirModal = true;

        //var qtdItem = $('#table_itens .myitem').length; //QTD DE ITENS

        ///* QTD DE ITENS */
        //if (qtdItem === 0) {
        //    $('#errorQtdItens').html('<span style="color:red; font-size: 0.76562rem;">É necessário incluir 1 ou mais itens</span>');
        //    abrirModal = false;
        //} else {
        //    $('#errorQtdItens').html('');
        //}

        /* OPERAÇÃO */
        if (operacao === '0') {
            $('#errorOperacao').html('<span style="color:red; font-size: 0.76562rem;">Selecione uma operação</span>');
            abrirModal = false;
        } else {
            $('#errorOperacao').html('');
        }

        if (tableSize === 0) {
            camposNaoPreenchidos += "Selecione um item <br/>";
            abrirModal = false;
        }

        /* DATA */
        if (data === "") {
            $('#errorData').html('<span style="color:red; font-size: 0.76562rem;">Data é obrigatória</span>');
            abrirModal = false;
        } else {
            $('#errorData').html('');
        }

        /* NOME FAVORECIDO */
        if (tipoFavorecido === "1" && nomeCliente === "") {
            $('#errorNomeFavorecido').html('<span style="color:red; font-size: 0.76562rem;">Selecione um favorecido</span>');
            abrirModal = false;
        } else if (tipoFavorecido === "2" && nomeFornecedor === "") {
            $('#errorNomeFavorecido').html('<span style="color:red; font-size: 0.76562rem;">Selecione um favorecido</span>');
            abrirModal = false;
        } else {
            $('#errorNomeFavorecido').html('');
        }

        if (baseCalcICMS === "") {
            camposNaoPreenchidos += "Base Calc. ICMS <br/>";
            document.getElementById("baseCalcICMS").focus();
            $('#errorBaseCalcICMS').html('<span style="color:red; font-size: 0.76562rem;">Base Calc. ICMS é obrigatório.</span>');
            abrirModal = false;
        } else {
            $('#errorBaseCalcICMS').html('');
        }
        if (valorICMS === "") {
            camposNaoPreenchidos += "Valor ICMS <br/>";
            document.getElementById("valorICMS").focus();
            $('#errorValorICMS').html('<span style="color:red; font-size: 0.76562rem;">Valor ICMS é obrigatório.</span>');
            abrirModal = false;
        } else {
            $('#errorValorICMS').html('');
        }
        if (baseCalcSubst === "") {
            camposNaoPreenchidos += "Base Calc. Subst <br/>";
            document.getElementById("baseCalcSubst").focus();
            $('#errorBaseCalcSubst').html('<span style="color:red; font-size: 0.76562rem;">Base Calc. Subst é obrigatório.</span>');
            abrirModal = false;
        } else {
            $('#errorBaseCalcSubst').html('');
        }
        if (valorICMSSubst === "") {
            camposNaoPreenchidos += "Valor ICMS Subst. <br/>";
            document.getElementById("valorICMSSubst").focus();
            $('#errorValorICMSSubst').html('<span style="color:red; font-size: 0.76562rem;">Valor ICMS Subst. é obrigatório.</span>');
            abrirModal = false;
        } else {
            $('#errorValorICMSSubst').html('');
        }
        if (totalItens === "") {
            camposNaoPreenchidos += "Total Itens <br/>";
            document.getElementById("totalItens").focus();
            $('#errorTotalItens').html('<span style="color:red; font-size: 0.76562rem;">Total Itens é obrigatório.</span>');
            abrirModal = false;
        } else {
            $('#errorTotalItens').html('');
        }
        var codigoForn;
        var numeroDaLinha;        

        if (abrirModal) {
            jQuery(document).ready(function ($) {
                $('#infoModal').modal('toggle');
                $("#mTotalPagar").focus();
            });
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
                hideMethod: "fadeOut"
            });
        }
    });

});