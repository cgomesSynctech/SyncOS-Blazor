// Funções de validação para transações

function ValidarTransacao(tabela, operacao, data, nomeFavorecido, baseCalcICMS, valorICMS, baseCalcSubst, valorICMSSubst, totalItens, transacaoModal) {

    var tabelaQtdLinhas = tabela.children('tbody').length;
    var tabelaQtdItens = tabela.find('.myitem').length;

    var operacaoVal = operacao.val();
    var dataVal = data.val();
    var nomeFavorecidoVal = nomeFavorecido.val();
    var baseCalcICMSVal = baseCalcICMS.val();
    var valorICMSVal = valorICMS.val();
    var baseCalcSubstVal = baseCalcSubst.val();
    var valorICMSSubstVal = baseCalcSubst.val();
    var totalItensVal = totalItens.val();

    var camposNaoPreenchidos = "";
    var abrirModal = true;

    // QTD DE LINHAS
    if (tabelaQtdLinhas === 0 && tabela != null) {
        camposNaoPreenchidos += "Selecione um item <br/>";
        abrirModal = false;
    }

    /* QTD DE ITENS */
    if (tabelaQtdItens === 0 && tabela != null) {
        camposNaoPreenchidos += "Itens <br/>";
        $('#error' + tabela.attr('id')).html('<span style="color:red; font-size: 0.76562rem;">É necessário incluir 1 ou mais itens</span>');
        abrirModal = false;
    } else {
        $('#error' + tabela.attr('id')).html('');
    }

    /* OPERAÇÃO */
    if (operacaoVal === '0' && operacao != null) {
        $('#error' + operacao.attr('id')).html('<span style="color:red; font-size: 0.76562rem;">Selecione uma operação</span>');
        abrirModal = false;
    } else {
        $('#error' + operacao.attr('id')).html('');
    }

    /* DATA */
    if (dataVal === "" && data != null) {
        camposNaoPreenchidos += "Data <br/>";
        $('#error' + data.attr('id')).html('<span style="color:red; font-size: 0.76562rem;">Data é obrigatória</span>');
        abrirModal = false;
    } else {
        $('#error' + data.attr('id')).html('');
    }

    /* NOME FAVORECIDO */
    if (nomeFavorecidoVal === "" && nomeFavorecido != null) {
        camposNaoPreenchidos += "Favorecido <br/>";
        $('#error' + nomeFavorecido.attr('id')).html('<span style="color:red; font-size: 0.76562rem;">Selecione um favorecido</span>');
        abrirModal = false;
    } else {
        $('#error' + nomeFavorecido.attr('id')).html('');
    }

    // BASECALCICMS
    if (baseCalcICMSVal === "" && baseCalcICMS != null) {
        camposNaoPreenchidos += "Base Calc. ICMS <br/>";
        document.getElementById(baseCalcICMS.attr('id')).focus();
        $('#error' + baseCalcICMS.attr('id')).html('<span style="color:red; font-size: 0.76562rem;">Base Calc. ICMS é obrigatório.</span>');
        abrirModal = false;
    } else {
        $('#error' + baseCalcICMS.attr('id')).html('');
    }

    // VALORICMS
    if (valorICMSVal === "" && valorICMS != null) {
        camposNaoPreenchidos += "Valor ICMS <br/>";
        document.getElementById(valorICMS.attr('id')).focus();
        $('#error' + valorICMS.attr('id')).html('<span style="color:red; font-size: 0.76562rem;">Valor ICMS é obrigatório.</span>');
        abrirModal = false;
    } else {
        $('#error' + valorICMS.attr('id')).html('');
    }

    // BASECALCSUBST
    if (baseCalcSubstVal === "" && baseCalcSubst != null) {
        camposNaoPreenchidos += "Base Calc. Subst <br/>";
        document.getElementById(baseCalcICMS.attr('id')).focus();
        $('#error' + baseCalcICMS.attr('id')).html('<span style="color:red; font-size: 0.76562rem;">Base Calc. Subst é obrigatório.</span>');
        abrirModal = false;
    } else {
        $('#error' + baseCalcICMS.attr('id')).html('');
    }

    // VALORICMSSUBST
    if (valorICMSSubstVal === "" && valorICMSSubst != null) {
        camposNaoPreenchidos += "Valor ICMS Subst. <br/>";
        document.getElementById(valorICMSSubst.attr('id')).focus();
        $('#error' + valorICMSSubst.attr('id')).html('<span style="color:red; font-size: 0.76562rem;">Valor ICMS Subst. é obrigatório.</span>');
        abrirModal = false;
    } else {
        $('#error' + valorICMSSubst.attr('id')).html('');
    }

    // Total de itens da tabela
    if (totalItensVal === "" && totalItens != null) {
        camposNaoPreenchidos += "Total Itens <br/>";
        document.getElementById(totalItens.attr('id')).focus();
        $('#error' + totalItens.attr('id')).html('<span style="color:red; font-size: 0.76562rem;">Total Itens é obrigatório.</span>');
        abrirModal = false;
    } else {
        $('#error' + totalItens.attr('id')).html('');
    }

    // Se a validação deu errado, exibir os campos
    if (!abrirModal) {
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

    return abrirModal;

    // Se a validação tiver sido bem sucedida
    if (abrirModal) {
        jQuery(document).ready(function ($) {
            if (tipoMovGeraFinanceiro !== null) {

                if (tipoMovGeraFinanceiro === "S") {
                    console.log("TRANSACAO...");

                    InicializarTransacao(); //ABRE O MODAL DE TRANSACAO
                } else {
                    console.log("NORMAL...");

                    transacaoModal.modal('toggle'); //ABRE O MODAL DE CONFIRMACAO
                }

            } else {

                InicializarTransacao(); //ABRE O MODAL DE TRANSACAO

            }
        });

        return true;
    }
} 