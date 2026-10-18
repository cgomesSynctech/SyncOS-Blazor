$(document).ready(function () {
    $('#botaoSalvar').click(function () {
        var data = $(':input[name=data]').val();

        var tipoFavorecido = $('#comboboxtipoFavorecido').val();
        var nomeCliente = $('#cliSearch').val();
        var nomeFornecedor = $('#fornecedorSearch').val();

        var numero = $(':input[name=numero]').val();
        var chaveNFE = $(':input[name=chaveNFE]').val();
        var dataNota = $(':input[name=dataNota]').val();
        var especie = $(':input[name=especie]').val();
        var modelo = $(':input[name=modelo]').val();
        var serieNota = $(':input[name=serieNota]').val();
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
        var totalItens = $(':input[name=totalItens]').val();
        var desconto = $(':input[name=desconto]').val();
        var juros = $(':input[name=juros]').val();
        var totalFinal = $(':input[name=totalFinal]').val();

        var camposNaoPreenchidos = "";
        var error = "";
        var abrirModal = true;

        
        /* NOME FAVORECIDO */
        if (tipoFavorecido === "1" && nomeCliente === "") {
            $('#errorFavorecido').html('<span style="color:red; font-size: 0.76562rem;">Selecione um favorecido</span>');
            abrirModal = false;
        } else if (tipoFavorecido === "2" && nomeFornecedor === "") {
            $('#errorFavorecido').html('<span style="color:red; font-size: 0.76562rem;">Selecione um favorecido</span>');
            abrirModal = false;
        } else {
            $('#errorFavorecido').html('');
        }


        if (data === "") {
            camposNaoPreenchidos += "Data <br/>";
            document.getElementById("data").focus();
            $('#errorData').html('<span style="color:red; font-size: 0.76562rem;">Data é obrigatório.</span>');
            abrirModal = false;

        } else {
            $('#errorData').html('');
        }
        if (numero === "") {
            camposNaoPreenchidos += "Número <br/>";
            document.getElementById("numero").focus();
            $('#errorNumero').html('<span style="color:red; font-size: 0.76562rem;">Número é obrigatório.</span>');
            abrirModal = false;
        } else {
            $('#errorNumero').html('');
        }
        if (chaveNFE === "") {
            camposNaoPreenchidos += "Chave NFE <br/>";
            document.getElementById("chaveNFE").focus();
            $('#errorChaveNFE').html('<span style="color:red; font-size: 0.76562rem;">Chave NFE é obrigatório.</span>');
            abrirModal = false;
        } else {
            $('#errorChaveNFE').html('');
        }
        if (dataNota === "") {
            camposNaoPreenchidos += "Data da Nota <br/>";
            document.getElementById("dataNota").focus();
            $('#errorDataNota').html('<span style="color:red; font-size: 0.76562rem;">Data da Nota é obrigatório.</span>');
            abrirModal = false;
        } else {
            $('#errorDataNota').html('');
        }
        if (especie === "") {
            camposNaoPreenchidos += "Espécie <br/>";
            document.getElementById("especie").focus();
            $('#errorEspecie').html('<span style="color:red; font-size: 0.76562rem;">Espécie é obrigatório.</span>');
            abrirModal = false;
        } else {
            $('#errorEspecie').html('');
        }
        if (modelo === "") {
            camposNaoPreenchidos += "Modelo <br/>";
            document.getElementById("modelo").focus();
            $('#errorModelo').html('<span style="color:red; font-size: 0.76562rem;">Modelo é obrigatório.</span>');
            abrirModal = false;
        } else {
            $('#errorModelo').html('');
        }
        if (serieNota === "") {
            camposNaoPreenchidos += "Série Nota <br/>";
            document.getElementById("serieNota").focus();
            $('#errorSerieNota').html('<span style="color:red; font-size: 0.76562rem;">Série Nota é obrigatório.</span>');
            abrirModal = false;
        } else {
            $('#errorSerieNota').html('');
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
        if (frete === "") {
            camposNaoPreenchidos += "Frete <br/>";
            document.getElementById("frete").focus();
            $('#errorFrete').html('<span style="color:red; font-size: 0.76562rem;">Frete é obrigatório.</span>');
            abrirModal = false;
        } else {
            $('#errorFrete').html('');
        }
        if (valorSeguro === "") {
            camposNaoPreenchidos += "Valor Seguro <br/>";
            document.getElementById("valorSeguro").focus();
            $('#errorValorSeguro').html('<span style="color:red; font-size: 0.76562rem;">Valor Seguro é obrigatório.</span>');
            abrirModal = false;
        } else {
            $('#errorValorSeguro').html('');
        }
        if (outrasDespesas === "") {
            camposNaoPreenchidos += "Outras Despesas <br/>";
            document.getElementById("outrasDespesas").focus();
            $('#errorOutrasDespesas').html('<span style="color:red; font-size: 0.76562rem;">Outras Despesas é obrigatório.</span>');
            abrirModal = false;
        } else {
            $('#errorOutrasDespesas').html('');
        }
        if (valorIPI === "") {
            camposNaoPreenchidos += "Valor IPI <br/>";
            document.getElementById("valorIPI").focus();
            $('#errorValorIPI').html('<span style="color:red; font-size: 0.76562rem;">Valor IPI é obrigatório.</span>');
            abrirModal = false;
        } else {
            $('#errorValorIPI').html('');
        }
        if (total === "") {
            camposNaoPreenchidos += "Total da Nota <br/>";
            document.getElementById("total").focus();
            $('#errorTotal').html('<span style="color:red; font-size: 0.76562rem;">Total da Nota é obrigatório.</span>');
            abrirModal = false;
        } else {
            $('#errorTotal').html('');
        }
        if (frete === "") {
            camposNaoPreenchidos += "Frete <br/>";
            document.getElementById("frete").focus();
            $('#errorFrete').html('<span style="color:red; font-size: 0.76562rem;">Frete é obrigatório.</span>');
            abrirModal = false;
        } else {
            $('#errorFrete').html('');
        }
        if (totalItens === "") {
            camposNaoPreenchidos += "Total Itens <br/>";
            document.getElementById("totalItens").focus();
            $('#errorTotalItens').html('<span style="color:red; font-size: 0.76562rem;">Total Itens é obrigatório.</span>');
            abrirModal = false;
        } else {
            $('#errorTotalItens').html('');
        }
        if (desconto === "") {
            camposNaoPreenchidos += "Desconto <br/>";
            document.getElementById("desconto").focus();
            $('#errorDesconto').html('<span style="color:red; font-size: 0.76562rem;">Desconto é obrigatório.</span>');
            abrirModal = false;
        } else {
            $('#errorDesconto').html('');
        }
        if (juros === "") {
            camposNaoPreenchidos += "Juros <br/>";
            document.getElementById("juros").focus();
            $('#errorJuros').html('<span style="color:red; font-size: 0.76562rem;">Juros é obrigatório.</span>');
            abrirModal = false;
        } else {
            $('#errorJuros').html('');
        }
        if (totalFinal === "") {
            camposNaoPreenchidos += "Total Final <br/>";
            document.getElementById("totalFinal").focus();
            $('#errorTotalFinal').html('<span style="color:red; font-size: 0.76562rem;">Total Final é obrigatório.</span>');
            abrirModal = false;
        } else {
            $('#errorTotalFinal').html('');
        }
        var codigoForn;
        var numeroDaLinha;
        for (var i = 0; i < indexLista; i++) {      

            numeroDaLinha = i + 1;

            idItem = $(':input[id=EntradasItens_' + i + '__Item]').val();
            if (idItem == 0 || idItem== -1) {

                camposNaoPreenchidos += "Nenhum item selecionado do item: " + numeroDaLinha + "  <br/>";
                $('#errorDescricao' + i).html('<span style="color:red; font-size: 0.76562rem;">Selecione um item.</span>');
                abrirModal = false;
            } else {
                $('#errorDescricao' + i).html('');
            }


            codigoForn = $(':input[id=EntradasItens_' + i + '__CodigoForn]').val();
            if (codigoForn === "") {

                camposNaoPreenchidos += "código forn. do item: " + numeroDaLinha + "  <br/>";
                $('#errorCodigoForn' + i).html('<span style="color:red; font-size: 0.76562rem;">Cód. forn. é obrigatório.</span>');
                abrirModal = false;
            } else {
                $('#errorCodigoForn' + i).html('');
            }

            referencia = $(':input[id=EntradasItens_' + i + '__Referencia]').val();
            if (referencia === "") {
                camposNaoPreenchidos += "Referência do item: " + numeroDaLinha + "  <br/>";
                $('#errorReferencia' + i).html('<span style="color:red; font-size: 0.76562rem;">Referência é obrigatório.</span>');
                abrirModal = false;
            } else {
                $('#errorReferencia' + i).html('');
            }

            quantidade = $(':input[id=EntradasItens_' + i + '__Quantidade]').val();
            if (quantidade === "") {
                camposNaoPreenchidos += "Quantidade do item: " + numeroDaLinha + "  <br/>";
                $('#errorQuantidade' + i).html('<span style="color:red; font-size: 0.76562rem;">Quantidade é obrigatório.</span>');
                abrirModal = false;
            } else {
                $('#errorQuantidade' + i).html('');
            }

            volume = $(':input[id=EntradasItens_' + i + '__QuantidadeVolume]').val();
            if (volume === "") {
                camposNaoPreenchidos += "Volume do item: " + numeroDaLinha + "  <br/>";
                $('#errorVolume' + i).html('<span style="color:red; font-size: 0.76562rem;">Volume é obrigatório.</span>');
                abrirModal = false;
            } else {
                $('#errorVolume' + i).html('');
            }

            custo = $(':input[id=EntradasItens_' + i + '__CustoContabil]').val();
            if (custo === "") {
                camposNaoPreenchidos += "Custo do item: " + numeroDaLinha + "  <br/>";
                $('#errorCusto' + i).html('<span style="color:red; font-size: 0.76562rem;">Custo é obrigatório.</span>');
                abrirModal = false;
            } else {
                $('#errorCusto' + i).html('');
            }

            subTotalItem = $(':input[id=EntradasItens_' + i + '__SubTotalItem]').val();
            if (subTotalItem === "") {
                camposNaoPreenchidos += "SubTotal do item: " + numeroDaLinha + "  <br/>";
                $('#errorSubTotal' + i).html('<span style="color:red; font-size: 0.76562rem;">SubTotal é obrigatório.</span>');
                abrirModal = false;
            } else {
                $('#errorSubTotal' + i).html('');
            }

            nCM = $(':input[id=EntradasItens_' + i + '__NCM]').val();
            if (nCM === "") {
                camposNaoPreenchidos += "NCM do item: " + numeroDaLinha + "  <br/>";
                $('#errorNCM' + i).html('<span style="color:red; font-size: 0.76562rem;">NCM é obrigatório.</span>');
                abrirModal = false;
            } else {
                $('#errorNCM' + i).html('');
            }

            precoVenda = $(':input[id=EntradasItens_' + i + '__PrecoVenda]').val();
            if (precoVenda === "") {
                camposNaoPreenchidos += "Preço venda do item: " + numeroDaLinha + "  <br/>";
                $('#errorPrecoVenda' + i).html('<span style="color:red; font-size: 0.76562rem;">Preço venda é obrigatório.</span>');
                abrirModal = false;
            } else {
                $('#errorPrecoVenda' + i).html('');
            }

            precoVenda = $(':input[id=EntradasItens_' + i + '__PrecoVenda]').val();
            if (precoVenda === "") {
                camposNaoPreenchidos += "Preço venda do item: " + numeroDaLinha + "  <br/>";
                $('#errorPrecoVenda' + i).html('<span style="color:red; font-size: 0.76562rem;">Preço venda é obrigatório.</span>');
                abrirModal = false;
            } else {
                $('#errorPrecoVenda' + i).html('');
            }

            numeroLote = $(':input[id=EntradasItens_' + i + '__NumeroLote]').val();
            if (numeroLote === "") {
                camposNaoPreenchidos += "Numero Lote do item: " + numeroDaLinha + "  <br/>";
                $('#errorNumeroLote' + i).html('<span style="color:red; font-size: 0.76562rem;">Numero Lote é obrigatório.</span>');
                abrirModal = false;
            } else {
                $('#errorNumeroLote' + i).html('');
            }

            validade = $(':input[id=EntradasItens_' + i + '__Validade]').val();
            if (validade === "") {
                camposNaoPreenchidos += "Validade do item: " + numeroDaLinha + "  <br/>";
                $('#errorValidade' + i).html('<span style="color:red; font-size: 0.76562rem;">Validade é obrigatório.</span>');
                abrirModal = false;
            } else {
                $('#errorValidade' + i).html('');
            }

            fabricacao = $(':input[id=EntradasItens_' + i + '__Fabricacao]').val();
            if (fabricacao === "") {
                camposNaoPreenchidos += "Fabricação do item: " + numeroDaLinha + "  <br/>";
                $('#errorFabricacao' + i).html('<span style="color:red; font-size: 0.76562rem;">Fabricação é obrigatório.</span>');
                abrirModal = false;
            } else {
                $('#errorFabricacao' + i).html('');
            }

            cfopCst = $(':input[id=EntradasItens_' + i + '__CFOPCST]').val();
            if (cfopCst === "") {
                camposNaoPreenchidos += "Cfop Cst do item: " + numeroDaLinha + "  <br/>";
                $('#errorCfopCst' + i).html('<span style="color:red; font-size: 0.76562rem;">Cfop Cst é obrigatório.</span>');
                abrirModal = false;
            } else {
                $('#errorCfopCst' + i).html('');
            }

            baseCalcICMSPROD = $(':input[id=EntradasItens_' + i + '__BaseCalcICMSPROD]').val();
            if (baseCalcICMSPROD === "") {
                camposNaoPreenchidos += "Base Calc. ICMS PROD. do item: " + numeroDaLinha + "  <br/>";
                $('#errorBaseCalcICMSPROD' + i).html('<span style="color:red; font-size: 0.76562rem;">Base Calc. ICMS PROD. é obrigatório.</span>');
                abrirModal = false;
            } else {
                $('#errorBaseCalcICMSPROD' + i).html('');
            }

            iCMS = $(':input[id=EntradasItens_' + i + '__ICMS]').val();
            if (iCMS === "") {
                camposNaoPreenchidos += "ICMS do item: " + numeroDaLinha + "  <br/>";
                $('#errorICMS' + i).html('<span style="color:red; font-size: 0.76562rem;">ICMS é obrigatório.</span>');
                abrirModal = false;
            } else {
                $('#errorICMS' + i).html('');
            }

            valorICMSProd = $(':input[id=EntradasItens_' + i + '__ValorICMSProd]').val();
            if (valorICMSProd === "") {
                camposNaoPreenchidos += "Valor ICMS Prod. do item: " + numeroDaLinha + "  <br/>";
                $('#errorValorICMSProd' + i).html('<span style="color:red; font-size: 0.76562rem;">Valor ICMS Prod. é obrigatório.</span>');
                abrirModal = false;
            } else {
                $('#errorValorICMSProd' + i).html('');
            }

            baseCalcSubstProd = $(':input[id=EntradasItens_' + i + '__BaseCalcSubstProd]').val();
            if (baseCalcSubstProd === "") {
                camposNaoPreenchidos += "Base Calc. Subst. Prod. do item: " + numeroDaLinha + "  <br/>";
                $('#errorBaseCalcSubstProd' + i).html('<span style="color:red; font-size: 0.76562rem;">Base Calc. Subst. Prod. é obrigatório.</span>');
                abrirModal = false;
            } else {
                $('#errorBaseCalcSubstProd' + i).html('');
            }

            valorICMSSubstProd = $(':input[id=EntradasItens_' + i + '__ValorICMSSubstProd]').val();
            if (valorICMSSubstProd === "") {
                camposNaoPreenchidos += "Valor ICMS Subst. Prod. do item: " + numeroDaLinha + "  <br/>";
                $('#errorValorICMSSubstProd' + i).html('<span style="color:red; font-size: 0.76562rem;">Valor ICMS Subst. Prod. é obrigatório.</span>');
                abrirModal = false;
            } else {
                $('#errorValorICMSSubstProd' + i).html('');
            }

            iPI = $(':input[id=EntradasItens_' + i + '__IPI]').val();
            if (iPI === "") {
                camposNaoPreenchidos += "IPI do item: " + numeroDaLinha + "  <br/>";
                $('#errorIPI' + i).html('<span style="color:red; font-size: 0.76562rem;">IPI é obrigatório.</span>');
                abrirModal = false;
            } else {
                $('#errorIPI' + i).html('');
            }

            valorIPIProd = $(':input[id=EntradasItens_' + i + '__ValorIPIProd]').val();
            if (valorIPIProd === "") {
                camposNaoPreenchidos += "Valor IPI Prod. do item: " + numeroDaLinha + "  <br/>";
                $('#errorValorIPIProd' + i).html('<span style="color:red; font-size: 0.76562rem;">Valor IPI Prod. é obrigatório.</span>');
                abrirModal = false;
            } else {
                $('#errorValorIPIProd' + i).html('');
            }

            aLIQPIS = $(':input[id=EntradasItens_' + i + '__ALIQPIS]').val();
            if (aLIQPIS === "") {
                camposNaoPreenchidos += "%Pis do item: " + numeroDaLinha + "  <br/>";
                $('#errorALIQPIS' + i).html('<span style="color:red; font-size: 0.76562rem;">%Pis é obrigatório.</span>');
                abrirModal = false;
            } else {
                $('#errorALIQPIS' + i).html('');
            } 

            valorPISProd = $(':input[id=EntradasItens_' + i + '__ValorPISProd]').val();
            if (valorPISProd === "") {
                camposNaoPreenchidos += "Valor PIS Prod. do item: " + numeroDaLinha + "  <br/>";
                $('#errorValorPISProd' + i).html('<span style="color:red; font-size: 0.76562rem;">Valor PIS Prod. é obrigatório.</span>');
                abrirModal = false;
            } else {
                $('#errorValorPISProd' + i).html('');
            }

            aLIQCOFINS = $(':input[id=EntradasItens_' + i + '__ALIQCOFINS]').val();
            if (aLIQCOFINS === "") {
                camposNaoPreenchidos += "%Cofins do item: " + numeroDaLinha + "  <br/>";
                $('#errorALIQCOFINS' + i).html('<span style="color:red; font-size: 0.76562rem;">%Cofins é obrigatório.</span>');
                abrirModal = false;
            } else {
                $('#errorALIQCOFINS' + i).html('');
            }

            valorCOFINSProd = $(':input[id=EntradasItens_' + i + '__ValorCOFINSProd]').val();
            if (valorCOFINSProd === "") {
                camposNaoPreenchidos += "Valor COFINS Prod. do item: " + numeroDaLinha + "  <br/>";
                $('#errorValorCOFINSProd' + i).html('<span style="color:red; font-size: 0.76562rem;">Valor COFINS Prod. é obrigatório.</span>');
                abrirModal = false;
            } else {
                $('#errorValorCOFINSProd' + i).html('');
            }

            frete = $(':input[id=EntradasItens_' + i + '__Frete]').val();
            if (frete === "") {
                camposNaoPreenchidos += "Frete do item: " + numeroDaLinha + "  <br/>";
                $('#errorFrete' + i).html('<span style="color:red; font-size: 0.76562rem;">Frete é obrigatório.</span>');
                abrirModal = false;
            } else {
                $('#errorFrete' + i).html('');
            }

            frete = $(':input[id=EntradasItens_' + i + '__Frete]').val();
            if (frete === "") {
                camposNaoPreenchidos += "Frete do item: " + numeroDaLinha + "  <br/>";
                $('#errorFrete' + i).html('<span style="color:red; font-size: 0.76562rem;">Frete é obrigatório.</span>');
                abrirModal = false;
            } else {
                $('#errorFrete' + i).html('');
            }

            seguro = $(':input[id=EntradasItens_' + i + '__Seguro]').val();
            if (seguro === "") {
                camposNaoPreenchidos += "Seguro do item: " + numeroDaLinha + "  <br/>";
                $('#errorSeguro' + i).html('<span style="color:red; font-size: 0.76562rem;">Seguro é obrigatório.</span>');
                abrirModal = false;
            } else {
                $('#errorSeguro' + i).html('');
            }
            
            outrasDespesas = $(':input[id=EntradasItens_' + i + '__OutrasDespesas]').val();
            if (outrasDespesas === "") {
                camposNaoPreenchidos += "Outras Despesas do item: " + numeroDaLinha + "  <br/>";
                $('#errorOutrasDespesas' + i).html('<span style="color:red; font-size: 0.76562rem;">Outras Despesas é obrigatório.</span>');
                abrirModal = false;
            } else {
                $('#errorOutrasDespesas' + i).html('');
            }


            console.log(codigoForn);
        }

        if (abrirModal) {
            jQuery(document).ready(function ($) {
                $('#infoModal').modal('toggle');
            });

            return false;
        } else {
            toastr.warning('Preencha todos os campos obrigatórios.', 'Campos obrigatórios: ', {
                closeButton: true,
                progressBar: true,
                debug: false,
                newestOnTop: false,
                positionClass: "toast-top-right",
                preventDuplicates: false,
                onclick: null,
                showDuration: "500",
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