$(".dataInput").mask("99/99/9999");

$('#comboboxtipoFavorecido').change(function () {
    var tipo = $("#comboboxtipoFavorecido").val();
    console.log(tipo);
    //troca funcionando
    if (tipo == 1) {
        $("#comboboxgrupotipoclientes").removeAttr("hidden");
        $("#comboboxgrupotipoclientes").removeAttr("disabled");
        $("#comboboxgrupotipoclientes").attr("name", "Favorecido");

        $("#comboboxgrupotipofornecedor").attr("hidden", "true");
        $("#comboboxgrupotipofornecedor").attr("name", "FavorecidoDesativado");
        $("#comboboxgrupotipofornecedor").attr("disabled", "disabled");
        console.log("oi, passou para o combo clientes");

    } else {
        $("#comboboxgrupotipofornecedor").removeAttr("hidden");
        $("#comboboxgrupotipofornecedor").removeAttr("disabled");
        $("#comboboxgrupotipofornecedor").attr("name", "Favorecido");

        $("#comboboxgrupotipoclientes").attr("hidden", "true");
        $("#comboboxgrupotipoclientes").attr("name", "FavorecidoDesativado");
        $("#comboboxgrupotipoclientes").attr("disabled", "disabled");
        console.log("oi, passou para o combo fornecedores");
    }

});


//popular tabela de entradaitens com atributos do item selecionado
function popularLinha(r) {
    var i = r.parentNode.parentNode.rowIndex;
    console.log("oii" + i);
    var numerodalinhapopular = i - 1;
    var idDoItem = $('#EntradasItens_' + numerodalinhapopular + '__Item').val();
    console.log("oii" + idDoItem);
    if (idDoItem == -1) {
        //console.log("oi -1 ativado");
        //    $.ajax({
        //        url: '/Item/FormModal',
        //        async: true,
        //        success: function (data) {
        //            console.log("oi -2 ativado");
        //            $("#novoItemModal").modal('toggle');
        //            console.log("oi -3 ativado");
        //            $("#novoItemModalBody").html(data);
        //            console.log("oi -4 ativado");
        //        },
        //        beforeSend: function () {
        //            $("#novoItemModalBody").html(" Carregando...");
        //        },
        //        error: function () {
        //            $("#novoItemModalBody").html("Ocorreu uma erro!");
        //        }
        //    });
        deleteRow(r);

        var win = window.open("/item/Form", '_blank');
        win.focus();

    } else {
        $.ajax({
            type: "Get",
            url: '/entradaitem/GetItem/' + idDoItem,
            contentType: "application/json; charset=utf-8",
            data: { data: "yourdata" },
            dataType: "json",
            success: function (recData) {

                $(recData).each(function (i) {
                    console.log(recData[i].Descricao + " " + recData[i].Unidade + " " + recData[i].CSTPISCOFINS + " " + recData[i].CST + " " + i)
                    $('#EntradasItens_' + numerodalinhapopular + '__Descricao').val(recData[i].Descricao);
                    $('#EntradasItens_' + numerodalinhapopular + '__NCM').val(recData[i].ClasFiscal);
                    $('#EntradasItens_' + numerodalinhapopular + '__Referencia').val(recData[i].Referencia);
                    $('#EntradasItens_' + numerodalinhapopular + '__Unidade').val(recData[i].Unidade);
                    $('#EntradasItens_' + numerodalinhapopular + '__PrecoVenda').val(recData[i].ItensPreco[0].Preco);
                    $('#EntradasItens_' + numerodalinhapopular + '__SituacaoECF').val(recData[i].SituacaoECF);
                    $('#EntradasItens_' + numerodalinhapopular + '__CFOPCST').val($('#comboboxgrupotipo').val());
                    $('#EntradasItens_' + numerodalinhapopular + '__CSTPISConfins').val(recData[i].CSTPISCOFINS);

                });
            },
            error: function () { alert('Entre em contato com synctech.'); }
        });

    }

}

function moeda(z) {
    v = z.value;
    v = v.replace(/\D/g, "") // permite digitar apenas numero
    v = v.replace(/(\d{1})(\d{1,2})$/, "$1,$2") // coloca virgula antes dos ultimos 2 digitos
    z.value = v;
}

function sonumeros(z) {
    v = z.value;
    v = v.replace(/\D/g, "") // permite digitar apenas numero
    z.value = v;
}

function moedaTresDecimal(z) {
    v = z.value;
    v = v.replace(/\D/g, "") // permite digitar apenas numero
    v = v.replace(/(\d{1})(\d{1,3})$/, "$1,$2") // coloca virgula antes dos ultimos 3 digitos
    z.value = v;
}

function moedaTresDecimalCalculoFinal(z) {
    v = z.value;
    v = v.replace(/\D/g, "") // permite digitar apenas numero
    v = v.replace(/(\d{1})(\d{1,3})$/, "$1,$2") // coloca virgula antes dos ultimos 3 digitos
    z.value = v;

    calcularCampoFinalMostrar();

}

function calcularCampoFinalMostrar() {
    var totalProdutos = $('#totalItens').val();
    if (totalProdutos == "") {
        totalProdutos = parseFloat("0.0");
    } else {
        totalProdutos = parseFloat($('#totalItens').val().replace(',', '.'));
    }
    console.log(totalProdutos);

    var frete = $('#frete').val();
    if (frete == "") {
        frete = parseFloat("0.0");
    } else {
        frete = parseFloat($('#frete').val().replace(',', '.'));
    }
    console.log(frete);

    var outrasDespesas = $('#outrasDespesas').val();
    if (outrasDespesas == "") {
        outrasDespesas = parseFloat("0.0");
    } else {
        outrasDespesas = parseFloat($('#outrasDespesas').val().replace(',', '.'));
    }
    console.log(outrasDespesas);


    var desconto = $('#desconto').val();
    if (desconto == "") {
        desconto = parseFloat("0.0");
    } else {
        desconto = parseFloat($('#desconto').val().replace(',', '.'));
    }
    console.log(desconto);

    var juros = $('#juros').val();
    if (juros == "") {
        juros = parseFloat("0.0");
    } else {
        juros = parseFloat($('#juros').val().replace(',', '.'));
    }
    console.log(juros);

    var total = totalProdutos - desconto + frete + outrasDespesas + juros;
    total = total.toFixed(3);
    console.log(total);
    $('#totalMostrar').val(total.replace('.', ','));

}

function moedaTresDecimalCalculoSubTotal(z) {
    v = z.value;
    v = v.replace(/\D/g, "") // permite digitar apenas numero
    v = v.replace(/(\d{1})(\d{1,3})$/, "$1,$2") // coloca virgula antes dos ultimos 3 digitos
    z.value = v;

    var i = z.parentNode.parentNode.rowIndex;
    var numerodalinhacalcular = i - 1;
    console.log(i);
    var quantItens = $('#EntradasItens_' + numerodalinhacalcular + '__Quantidade').val();
    if (quantItens == "") {
        quantItens = parseFloat("0.0");
    } else {
        quantItens = parseFloat($('#EntradasItens_' + numerodalinhacalcular + '__Quantidade').val().replace(',', '.'));
    }

    var custo = $('#EntradasItens_' + numerodalinhacalcular + '__CustoContabil').val();
    if (custo == "") {
        custo = parseFloat("0.0");
    } else {
        custo = parseFloat($('#EntradasItens_' + numerodalinhacalcular + '__CustoContabil').val().replace(',', '.'));
    }
    var subtotal = quantItens * custo;
    subtotal = subtotal.toFixed(3);
    $('#EntradasItens_' + numerodalinhacalcular + '__SubTotalItem').val(subtotal.replace('.', ','));
    $('#subtotaldefault_' + numerodalinhacalcular).val(subtotal.replace('.', ','));

    var totalProdutos = 0;
    for (var j = 0; j < numeroDeItens; j++) {
        subTotalSomar = $('#EntradasItens_' + j + '__SubTotalItem').val();
        if (subTotalSomar == "apagado" || subTotalSomar == "") {

        } else {
            subTotalSomar = parseFloat($('#EntradasItens_' + j + '__SubTotalItem').val().replace(',', '.'));
            totalProdutos = totalProdutos + subTotalSomar;
        }
    }
    totalProdutos = totalProdutos.toFixed(3);
    $('#totalItens').val(totalProdutos.replace('.', ','));
    $('#totalItensMostrar').val(totalProdutos.replace('.', ','));
    $('#atributo3').val(totalProdutos.replace('.', ','));

    calcularCampoFinalMostrar();
}


$('.moedaTresDecimalInput').val("");
$('.moedaTresDecimalInput').attr("placeholder", "0,000");
$('.moedaTresDecimalInput').attr("onkeyup", "moedaTresDecimal(this)");

$('.moedaTresDecimalCalculoFinalInput').attr("onkeyup", "moedaTresDecimalCalculoFinal(this)");
$('.moedaTresDecimalCalculoFinalInput').val("");
$('.moedaTresDecimalCalculoFinalInput').attr("placeholder", "0,000");

$('.moedaTresDecimalCalculoSubTotalInput').attr("onkeyup", "moedaTresDecimalCalculoSubTotal(this)");
$('.moedaTresDecimalCalculoSubTotalInput').val("");
$('.moedaTresDecimalCalculoSubTotalInput').attr("placeholder", "0,000");



$('#txtabela').click(function () {
    $('#tabelaPrecoModal').modal('toggle');
});

$(document).ready(function () {
    $(".valor").on("input", function () {
        var textoDigitado = $(this).val();
        var inputAtributo = $(this).attr("atributoIdentificacao");
        textoDigitado = textoDigitado.replace(/\D/g, "") // permite digitar apenas numero
        textoDigitado = textoDigitado.replace(/(\d{1})(\d{1,3})$/, "$1,$2") // coloca virgula antes dos ultimos 2 digitos
        console.log(textoDigitado);
        $("#" + inputAtributo).val(textoDigitado);

    });
});
var numeroDeItens = 1;
var indexLista = 1;
@{ int numteste = 0;}
function add_fields() {



    var codigo = indexLista + 1;

    for (i = 0; i < indexLista; i++) {
        @{ numteste = numteste + 1;}

    }
    console.log('@numteste');


    document.getElementById("tabelaitens").insertRow().innerHTML = '   <tr id="tr' + indexLista + '"> ' +
        '                                                   <td>  ' +
        '                                                       <div class="set-form">  ' +
        '                                                           <a id="less_field" onclick="deleteRow(this)" style="color: white; margin-top: 0;" class="btn btn-danger"><i class="fa fa-minus"></i></a>  ' +
        '                                                       </div>  ' +
        '                                                   </td>  ' +
        '                                                   <td>' + codigo + '</td>  ' +
        //'                                                   <td>  ' +
        //'                                                       <input class="form-control form-control-sm" id="EntradasItens_' + indexLista + '__CodigoForn" name="EntradasItens[' + indexLista + '].CodigoForn" type="text" value="">  ' +
        //'                                                       <div id="errorCodigoForn' + indexLista +'"></div>' +
        //'                                                   </td>  ' +
        '                                                   <td>  ' +
        '@Html.Raw(Ajax.JavaScriptStringEncode(Html.DropDownListFor(model => model.EntradasItens[0].Item, ViewBag.Itens as SelectList, new { @class = "form-control form-control-sm select2-single novaLinhaDesc", @onchange = "popularLinha(this)" }).ToHtmlString()))' +
        '                                                       <input class="form-control form-control-sm moedaTresDecimalInput" data-val="true" data-val-number="O campo Item deve ser um número." data-val-required="O campo Item é obrigatório." hidden="true" id="EntradasItens_' + indexLista + '__Descricao" name="EntradasItens[' + indexLista + '].Descricao" type="text" value="0" />' +
        '                                                       <div id="errorDescricao' + indexLista + '"></div>' +
        '                                                   </td>  ' +
        '                                                   <td>  ' +
        '                                                       <input class="form-control form-control-sm" id="EntradasItens_' + indexLista + '__Referencia" name="EntradasItens[' + indexLista + '].Referencia" type="text" value="">  ' +
        '                                                       <div id="errorReferencia' + indexLista + '"></div>' +
        '                                                   </td>  ' +
        '                                                   <td>' +
        '                                                       <select class="form-control form-control-sm select2-single" id="EntradasItens_' + indexLista + '__Unidade" name="EntradasItens[' + indexLista + '].Unidade"><option value="UN">UN</option>' +
        '                                                           <option value="MT">MT</option>' +
        '                                                           <option value="KG">KG</option>' +
        '                                                           <option value="CX">CX</option>' +
        '                                                           <option value="LT">LT</option>' +
        '                                                       </select>' +
        '                                                   </td>' +
        '                                                   <td>  ' +
        '                                                       <input class="form-control form-control-sm moedaTresDecimalCalculoSubTotalInput" data-val="true" data-val-number="O campo Quantidade deve ser um número." data-val-required="O campo Quantidade é obrigatório." id="EntradasItens_' + indexLista + '__Quantidade" name="EntradasItens[' + indexLista + '].Quantidade" type="text" value="">  ' +
        '                                                       <div id="errorQuantidade' + indexLista + '"></div>' +
        '                                                   </td>  ' +
        '                                                   <td>  ' +
        '                                                       <input class="form-control form-control-sm moedaTresDecimalInput" data-val="true" data-val-number="O campo QuantidadeVolume deve ser um número." data-val-required="O campo QuantidadeVolume é obrigatório." id="EntradasItens_' + indexLista + '__QuantidadeVolume" name="EntradasItens[' + indexLista + '].QuantidadeVolume" type="text" value="">  ' +
        '                                                       <div id="errorVolume' + indexLista + '"></div>' +
        '                                                   </td>  ' +
        '                                                   <td>  ' +
        '                                                       <input class="form-control form-control-sm moedaTresDecimalCalculoSubTotalInput" data-val="true" data-val-number="O campo CustoContabil deve ser um número." data-val-required="O campo CustoContabil é obrigatório." id="EntradasItens_' + indexLista + '__CustoContabil" name="EntradasItens[' + indexLista + '].CustoContabil" type="text" value="">  ' +
        '                                                       <div id="errorCusto' + indexLista + '"></div>' +
        '                                                   </td>  ' +
        '                                                   <td>  ' +
        '                                                       <input class="form-control form-control-sm moedaTresDecimalInput" data-val="true" data-val-number="O campo SubTotalItem deve ser um número." data-val-required="O campo SubTotalItem é obrigatório." hidden="true" id="EntradasItens_' + indexLista + '__SubTotalItem" name="EntradasItens[' + indexLista + '].SubTotalItem" type="text" value="">  ' +
        '                                                       <input class="form-control form-control-sm moedaTresDecimalInput" disabled="disabled" id="subtotaldefault_' + indexLista + '" name="EntradasItens[' + indexLista + '].SubTotalItem" type="text" value="" placeholder="0,000" onkeyup="moedaTresDecimal(this)">' +
        '                                                       <div id="errorSubTotal' + indexLista + '"></div>' +
        '                                                   </td>  ' +
        '                                                   <td>  ' +
        '                                                         ' +
        '                                                       <input class="form-control form-control-sm" id="EntradasItens_' + indexLista + '__NCM" name="EntradasItens[' + indexLista + '].NCM" type="text" value="">  ' +
        '                                                       <div id="errorNCM' + indexLista + '"></div>' +
        '                                                   </td>  ' +
        '                                                   <td>  ' +
        '                                                       <input class="form-control form-control-sm  moedaTresDecimalInput" data-val="true" data-val-number="O campo PrecoVenda deve ser um número." data-val-required="O campo PrecoVenda é obrigatório." id="EntradasItens_' + indexLista + '__PrecoVenda" name="EntradasItens[' + indexLista + '].PrecoVenda" type="text" value="">  ' +
        '                                                       <div id="errorPrecoVenda' + indexLista + '"></div>' +
        '                                                   </td>  ' +
        '                                                   <td>  ' +
        '                                                       <input class="form-control form-control-sm" id="EntradasItens_' + indexLista + '__NumeroLote" name="EntradasItens[' + indexLista + '].NumeroLote" type="text" value="">  ' +
        '                                                       <div id="errorNumeroLote' + indexLista + '"></div>' +
        '                                                   </td>  ' +
        '                                                   <td>  ' +
        '@Html.Raw(Ajax.JavaScriptStringEncode(Html.DropDownListFor(model => model.EntradasItens[0].SituacaoECF, ViewBag.TipoTribs as SelectList, new { @class = "form-control form-control-sm select2-single novaLinhaSituacaoECF" }).ToHtmlString()))' +
        '                                                   </td>  ' +
        '                                                   <td>  ' +
        '@Html.Raw(Ajax.JavaScriptStringEncode(Html.DropDownListFor(model => model.EntradasItens[0].CstICMS, ViewBag.CstICMS as SelectList, new { @class = "form-control form-control-sm select2-single novaLinhaCstICMS" }).ToHtmlString()))' +
        //'                                                       <input class="form-control form-control-sm" id="EntradasItens_' + indexLista + '__CST" name="EntradasItens[' + indexLista + '].CST" type="hidden" value="" />' +
        '                                                   </td>  ' +
        '                                                   <td>  ' +
        '                                                       <input class="form-control form-control-sm dataCampo" data-val="true" data-val-date="O campo Validade deve ser uma data." data-val-required="O campo Validade é obrigatório." id="EntradasItens_' + indexLista + '__Validade" name="EntradasItens[' + indexLista + '].Validade" type="text" value="">  ' +
        '                                                       <div id="errorValidade' + indexLista + '"></div>' +
        '                                                   </td>  ' +
        '                                                   <td>  ' +
        '                                                       <input class="form-control form-control-sm dataCampo" data-val="true" data-val-date="O campo Fabricacao deve ser uma data." data-val-required="O campo Fabricacao é obrigatório." id="EntradasItens_' + indexLista + '__Fabricacao" name="EntradasItens[' + indexLista + '].Fabricacao" type="text" value="">  ' +
        '                                                       <div id="errorFabricacao' + indexLista + '"></div>' +
        '                                                   </td>  ' +
        '                                                   <td>  ' +
        '                                                       <input class="form-control form-control-sm" id="EntradasItens_' + indexLista + '__CFOPCST" name="EntradasItens[' + indexLista + '].CFOPCST" type="text" value="">  ' +
        '                                                       <div id="errorCfopCst' + indexLista + '"></div>' +
        '                                                   </td>  ' +
        '                                                   <td>  ' +
        '                                                       <input class="form-control form-control-sm moedaTresDecimalInput" data-val="true" data-val-number="O campo BaseCalcICMSPROD deve ser um número." data-val-required="O campo BaseCalcICMSPROD é obrigatório." id="EntradasItens_' + indexLista + '__BaseCalcICMSPROD" name="EntradasItens[' + indexLista + '].BaseCalcICMSPROD" type="text" value="">  ' +
        '                                                       <div id="errorBaseCalcICMSPROD' + indexLista + '"></div>' +
        '                                                   </td>  ' +
        '                                                   <td>  ' +
        '                                                         ' +
        '                                                       <input class="form-control form-control-sm  moedaTresDecimalInput" id="EntradasItens_' + indexLista + '__ICMS" name="EntradasItens[' + indexLista + '].ICMS" type="text" value="" placeholder="0,000" onkeyup="moedaTresDecimal(this)">  ' +
        '                                                       <div id="errorICMS' + indexLista + '"></div>' +
        '                                                   </td>  ' +
        '                                                   <td>  ' +
        '                                                       <input class="form-control form-control-sm moedaTresDecimalInput" data-val="true" data-val-number="O campo ValorICMSProd deve ser um número." data-val-required="O campo ValorICMSProd é obrigatório." id="EntradasItens_' + indexLista + '__ValorICMSProd" name="EntradasItens[' + indexLista + '].ValorICMSProd" type="text" value="">  ' +
        '                                                       <div id="errorValorICMSProd' + indexLista + '"></div>' +
        '                                                   </td>  ' +
        '                                                   <td>  ' +
        '                                                       <input class="form-control form-control-sm moedaTresDecimalInput" data-val="true" data-val-number="O campo BaseCalcSubstProd deve ser um número." data-val-required="O campo BaseCalcSubstProd é obrigatório." id="EntradasItens_' + indexLista + '__BaseCalcSubstProd" name="EntradasItens[' + indexLista + '].BaseCalcSubstProd" type="text" value="">  ' +
        '                                                       <div id="errorBaseCalcSubstProd' + indexLista + '"></div>' +
        '                                                   </td>  ' +
        '                                                   <td>  ' +
        '                                                       <input class="form-control form-control-sm moedaTresDecimalInput" data-val="true" data-val-number="O campo ValorICMSSubstProd deve ser um número." data-val-required="O campo ValorICMSSubstProd é obrigatório." id="EntradasItens_' + indexLista + '__ValorICMSSubstProd" name="EntradasItens[' + indexLista + '].ValorICMSSubstProd" type="text" value="">  ' +
        '                                                       <div id="errorValorICMSSubstProd' + indexLista + '"></div>' +
        '                                                   </td>  ' +
        '                                                   <td>  ' +
        '@Html.Raw(Ajax.JavaScriptStringEncode(Html.DropDownListFor(model => model.EntradasItens[0].CSTIPI, ViewBag.CstIPIs as SelectList, new { @class = "form-control form-control-sm select2-single novaLinhaCstIpi" }).ToHtmlString()))' +
        '                                                   </td>  ' +
        '                                                   <td>  ' +
        '                                                       <input class="form-control form-control-sm moedaTresDecimalInput" data-val="true" data-val-number="O campo IPI deve ser um número." data-val-required="O campo IPI é obrigatório." id="EntradasItens_' + indexLista + '__IPI" name="EntradasItens[' + indexLista + '].IPI" type="text" value="">  ' +
        '                                                       <div id="errorIPI' + indexLista + '"></div>' +
        '                                                   </td>  ' +
        '                                                   <td>  ' +
        '                                                       <input class="form-control form-control-sm moedaTresDecimalInput" data-val="true" data-val-number="O campo ValorIPIProd deve ser um número." data-val-required="O campo ValorIPIProd é obrigatório." id="EntradasItens_' + indexLista + '__ValorIPIProd" name="EntradasItens[' + indexLista + '].ValorIPIProd" type="text" value="">  ' +
        '                                                       <div id="errorValorIPIProd' + indexLista + '"></div>' +
        '                                                   </td>  ' +
        '                                                   <td>  ' +
        '@Html.Raw(Ajax.JavaScriptStringEncode(Html.DropDownListFor(model => model.EntradasItens[0].CSTPISConfins, ViewBag.CstPisCofins as SelectList, new { @class = "form-control form-control-sm select2-single novaLinhaCstPisCofins" }).ToHtmlString()))' +
        '                                                   </td>  ' +
        '                                                   <td>  ' +
        '                                                       <input class="form-control form-control-sm moedaTresDecimalInput" data-val="true" data-val-number="O campo ALIQPIS deve ser um número." data-val-required="O campo ALIQPIS é obrigatório." id="EntradasItens_' + indexLista + '__ALIQPIS" name="EntradasItens[' + indexLista + '].ALIQPIS" type="text" value="">  ' +
        '                                                       <div id="errorALIQPIS' + indexLista + '"></div>' +
        '                                                   </td>  ' +
        '                                                   <td>  ' +
        '                                                       <input class="form-control form-control-sm moedaTresDecimalInput" data-val="true" data-val-number="O campo ValorPISProd deve ser um número." data-val-required="O campo ValorPISProd é obrigatório." id="EntradasItens_' + indexLista + '__ValorPISProd" name="EntradasItens[' + indexLista + '].ValorPISProd" type="text" value="">  ' +
        '                                                       <div id="errorValorPISProd' + indexLista + '"></div>' +
        '                                                   </td>  ' +
        '                                                   <td>  ' +
        '                                                       <input class="form-control form-control-sm moedaTresDecimalInput" data-val="true" data-val-number="O campo ALIQCOFINS deve ser um número." data-val-required="O campo ALIQCOFINS é obrigatório." id="EntradasItens_' + indexLista + '__ALIQCOFINS" name="EntradasItens[' + indexLista + '].ALIQCOFINS" type="text" value="">  ' +
        '                                                       <div id="errorALIQCOFINS' + indexLista + '"></div>' +
        '                                                   </td>  ' +
        '                                                   <td>  ' +
        '                                                       <input class="form-control form-control-sm moedaTresDecimalInput" data-val="true" data-val-number="O campo ValorCOFINSProd deve ser um número." data-val-required="O campo ValorCOFINSProd é obrigatório." id="EntradasItens_' + indexLista + '__ValorCOFINSProd" name="EntradasItens[' + indexLista + '].ValorCOFINSProd" type="text" value="">  ' +
        '                                                       <div id="errorValorCOFINSProd' + indexLista + '"></div>' +
        '                                                   </td>  ' +
        '                                                   <td>  ' +
        '                                                       <input class="form-control form-control-sm moedaTresDecimalInput" data-val="true" data-val-number="O campo Frete deve ser um número." data-val-required="O campo Frete é obrigatório." id="EntradasItens_' + indexLista + '__Frete" name="EntradasItens[' + indexLista + '].Frete" type="text" value="">  ' +
        '                                                       <div id="errorFrete' + indexLista + '"></div>' +
        '                                                   </td>  ' +
        '                                                   <td>  ' +
        '                                                       <input class="form-control form-control-sm moedaTresDecimalInput" data-val="true" data-val-number="O campo Seguro deve ser um número." data-val-required="O campo Seguro é obrigatório." id="EntradasItens_' + indexLista + '__Seguro" name="EntradasItens[' + indexLista + '].Seguro" type="text" value="">  ' +
        '                                                       <div id="errorSeguro' + indexLista + '"></div>' +
        '                                                   </td>  ' +
        '                                                   <td>  ' +
        '                                                       <input class="form-control form-control-sm moedaTresDecimalInput" data-val="true" data-val-number="O campo OutrasDespesas deve ser um número." data-val-required="O campo OutrasDespesas é obrigatório." id="EntradasItens_' + indexLista + '__OutrasDespesas" name="EntradasItens[' + indexLista + '].OutrasDespesas" type="text" value="">  ' +
        '                                                       <div id="errorOutrasDespesas' + indexLista + '"></div>' +
        '                                                   </td>  ' +
        '                                              </tr>  ';


    $j = jQuery.noConflict();
    $j(document).ready(function () {
        $j(function () {
            $(".dataCampo").mask("99/99/9999");
            $('.dataCampo').attr("placeholder", "__/__/____");
            $('.moedaTresDecimalInput').attr("placeholder", "0,000");
            $('.moedaTresDecimalInput').attr("onkeyup", "moedaTresDecimal(this)");
            $('.moedaTresDecimalCalculoSubTotalInput').attr("placeholder", "0,000");
            $('.moedaTresDecimalCalculoSubTotalInput').attr("onkeyup", "moedaTresDecimalCalculoSubTotal(this)");

            $('.novaLinhaDesc').attr("name", "EntradasItens[" + indexLista + "].Item");
            $('.novaLinhaDesc').attr("id", "EntradasItens_" + indexLista + "__Item");
            $("#EntradasItens_" + indexLista + "__Item").attr("class", "form-control form-control-sm select2-single");

            $('.novaLinhaCstIpi').attr("name", "EntradasItens[" + indexLista + "].CSTIPI");
            $('.novaLinhaCstIpi').attr("id", "EntradasItens_" + indexLista + "__CSTIPI");
            $("#EntradasItens_" + indexLista + "__CSTIPI").attr("class", "form-control form-control-sm select2-single");

            $('.novaLinhaCstPisCofins').attr("name", "EntradasItens[" + indexLista + "].CSTPISConfins");
            $('.novaLinhaCstPisCofins').attr("id", "EntradasItens_" + indexLista + "__CSTPISConfins");
            $("#EntradasItens_" + indexLista + "__CSTPISConfins").attr("class", "form-control form-control-sm select2-single");

            $('.novaLinhaSituacaoECF').attr("name", "EntradasItens[" + indexLista + "].SituacaoECF");
            $('.novaLinhaSituacaoECF').attr("id", "EntradasItens_" + indexLista + "__SituacaoECF");
            $("#EntradasItens_" + indexLista + "__SituacaoECF").attr("class", "form-control form-control-sm select2-single");


            $('.novaLinhaCstICMS').attr("name", "EntradasItens[" + indexLista + "].CstICMS");
            $('.novaLinhaCstICMS').attr("id", "EntradasItens_" + indexLista + "__CstICMS");
            $("#EntradasItens_" + indexLista + "__CstICMS").attr("class", "form-control form-control-sm select2-single");

            numeroDeItens++;
            $("#quantItensLabel").html("Quantidade de itens: " + numeroDeItens);

            //carregar novos itens
            $("#EntradasItens_" + indexLista + "__Item").empty();

            $.ajax({
                type: "Get",
                url: '/entradaitem/GetItensAtivos/',
                contentType: "application/json; charset=utf-8",
                data: { data: "yourdata" },
                dataType: "json",
                success: function (recData) {

                    $(recData).each(function (i) {

                        var indexListaDrop = indexLista - 1;
                        $('#EntradasItens_' + indexListaDrop + '__Item').append(new Option(recData[i].Descricao, recData[i].Id));

                    });
                },
                error: function () { alert('Entre em contato com synctech.'); }
            });


            console.log("passou aki" + indexLista);


            indexLista++;
        });
    });

}

function deleteRow(r) {
    console.log(r);
    var i = r.parentNode.parentNode.parentNode.rowIndex;
    $(r).closest('tr').attr('hidden', true); // Esconde a row
    //$(r).closest('tr').find(":input:not(:first)").val(""); // Apaga todos os campos menos o primeiro
    $(r).closest('tr').find(":input").val("apagado"); // Apaga todos os campos
    numeroDeItens--;
    $("#quantItensLabel").html("Quantidade de itens: " + numeroDeItens);
    console.log(i);
}

//validação

$(".dataCampo").mask("99/99/9999");
$('.dataCampo').val("");
$('.dataCampo').attr("placeholder", "__/__/____");

function moedaTresDecimalCalculoTotalProdutos(z) {
    v = z.value;
    v = v.replace(/\D/g, "") // permite digitar apenas numero
    v = v.replace(/(\d{1})(\d{1,3})$/, "$1,$2") // coloca virgula antes dos ultimos 3 digitos
    z.value = v;

    var totalProdutos = $('#totalItens').val();
    if (totalProdutos == "") {
        totalProdutos = parseFloat("0.0");
    } else {
        totalProdutos = parseFloat($('#totalItens').val().replace(',', '.'));
    }
    console.log(totalProdutos);

    var frete = $('#frete').val();
    if (frete == "") {
        frete = parseFloat("0.0");
    } else {
        frete = parseFloat($('#frete').val().replace(',', '.'));
    }
    console.log(frete);

    var outrasDespesas = $('#outrasDespesas').val();
    if (outrasDespesas == "") {
        outrasDespesas = parseFloat("0.0");
    } else {
        outrasDespesas = parseFloat($('#outrasDespesas').val().replace(',', '.'));
    }
    console.log(outrasDespesas);


    var desconto = $('#desconto').val();
    if (desconto == "") {
        desconto = parseFloat("0.0");
    } else {
        desconto = parseFloat($('#desconto').val().replace(',', '.'));
    }
    console.log(desconto);

    var juros = $('#juros').val();
    if (juros == "") {
        juros = parseFloat("0.0");
    } else {
        juros = parseFloat($('#juros').val().replace(',', '.'));
    }
    console.log(juros);

    var total = totalProdutos - desconto + frete + outrasDespesas + juros;
    total = total.toFixed(3);
    console.log(total);
    $('#totalMostrar').val(total);
}