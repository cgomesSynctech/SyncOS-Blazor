//FUNÇÕES QUE DEVEM SER IMPLEMENTADAS NA VIEW
var ConfirmaPagamento;

//TIPO DE TEF UTILIZADA PELO CLIENTE
var tipoTef = "";

var accessToken = {
    access_token: ""
};
var orderToken = {
    order_id: ""
};

//VARIAVEIS DE TRANSACAO
var tipoCliente, cliName, itens, vlrTotal, vlrJaPago, vlrRestante, troco;
var semPagto = false;
var pagtos = []; //Lista de pagamentos.
var contasContabeis = []; // Lista de Contas Contabéis
var planoPagamento = 0;

//var pgt = { vPg, tPg, cTk };

//Especifico para Titulos (São atribuidos FORA da transação)
var idTitulo, SPedidoToken; //SPedidoToken é o OrderToken que já existia no titulo.

//Checka se a janela já foi criada nessa sessão
var PAGTOFEITO = false, FINALIZANDO = false;

//AO CARREGAR A PAGINA
$(document).ready(function (e) {

    //MOEDA INPUT
    $('.moedaInput').attr("placeholder", "0,00");
    $('.moedaInput').attr("onkeyup", "moedaDoisDecimais(this)");

    /* VALIDA SE O VALOR ESTÁ PREENCHIDO E A FORMA DE PAGAMENTO SELECIONADA */
    $('#vlrPagto').keyup(function () {
        CheckCamposValores();
    });
    $('#mFormaPag').change(function () {
        CheckCamposValores();
    });
    //Ao selecionar o PLANO DE PAGAMENTO (pagto tipo 90 - Sem Pagamento)
    //$("#planoPagto").on("change", function () {
    //    CheckCamposValores();
    //});

    /* REALIZA O PAGAMENTO */
    $('#mPagar').click(function () {
        Pagar();
    });

    //Checa os campos do Cheque
    $(".cheqField").on("input", function () {
        if ($("#CheqTitular").val().length > 0 &&
            $("#CheqNumero").val().length > 0 &&
            $("#CheqIdBanco").val().length > 0 &&
            $("#CheqValor").val().length > 0 &&
            $("#CheqVencimento").val().length > 0) {
            $('#mPagar').prop("disabled", false);
        } else {
            $('#mPagar').prop("disabled", true);
        }
    });

    //Impede o Submit com o Enter.
    $("#table_itens").on("keydown", function (e) {
        if (e.keyCode == 13) {
            e.preventDefault();
        }
    });

    //Botão de Confirmação (Fecha a VENDA)
    $("#mConfirmar").click(function () {

        ConfirmaPagamento();

        SetAviso(false);

        if (tipoTef == "S") {
            FecharPedidoStone();
        }

        //SalvarPagamentos();

        //if (rota == "contasareceber") {
        //    //$('#infoModal').modal('toggle');
        //    $('#formProcurar').attr('action', '/contasareceber/confirmTitulos');
        //    $('#formProcurar').submit();
        //    setInterval(function () {
        //        location.reload();
        //    }, 8000);
        //} else if (rota == "contasapagar") {
        //    $('#formProcurar').attr('action', '/contasapagar/confirmTitulos');
        //    $('#formProcurar').submit();
        //} else {
        //    $("#FORM_TRANS").submit(); //Submita.
        //}
        //$('#mConfirmar').attr('disabled', 'disabled');

    });

    // Botão para adicionar uma conta contábil na lista
    $("#addContaContabil").click(function () {
        AdicionarContaContabil(null, null);
    });

    //Configura o aviso ao fechar
    SetAviso(false);
});

//~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
//              SETUP & CHAMADA                 |
//~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
var rota = "";
function SetRota(rt) {
    rota = rt;
}
function SetTef(tef) {
    tipoTef = tef;
}

//~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
//          FUNÇÕES DE CONTAS CONTÁBEIS       |
//~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
function LimparContasContabeis() {
    $('#contasContabeis > tbody').html('');
    cntContaContabil = 0;
}

var cntContaContabil = 0;
function AdicionarContaContabil(conta, valor) {
    var fields =
        "<tr>" +
        "<td style='vertical-align: middle;' hidden='hidden'><input class='tituloLinha' hidden='hidden' name='TitulosContas[" + cntContaContabil + "].Titulo' value='"+ idTitulo +"'></td>" +
        "<td style='vertical-align: middle;' class='text-center selectLinha'><select class='form control contaLinha' name='TitulosContas[" + cntContaContabil + "].Conta'>" + $('#selectContasDespesas').html() + " </select></td>" +
        "<td style='vertical-align: middle;' class='text-center valorLinha'><input class='form-control form-control-sm moedaInput valorLinha' name='TitulosContas[" + cntContaContabil + "].Valor' /></td>" +
        "</tr>";

    $('#contasContabeis > tbody').append(fields);

    if (conta != null)
        $('.selectLinha:first > select').val(conta);

    if (valor != null)
        $('.valorLinha:first > input').val(valor);

    cntContaContabil++;
}

//~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
//              FUNÇÕES DO MODAL                |
//~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
function InicializarTransacao(confirmaPagamento, IDTitulo) {

    this.ConfirmaPagamento = confirmaPagamento;
    idTitulo = IDTitulo;

    $("#TransModal").modal("toggle");

    AbrirTransacao();

    SetAviso(true);
}

function FavorecidoInfo(tipo, nome) {
    if (tipo == 1)
        $('#tipoFavorecidoTransacao').html("Cliente");
    else if (tipo == 2)
        $('#tipoFavorecidoTransacao').html("Fornecedor");

    $('#favorecido').text(nome);
}

function CheckCamposValores() {
    if ($("#mFormaPag").val() != '0') { //Ao mudar a forma de pagto...

        if ($("#mFormaPag").val() == "02") { //Se for CHEQUE

            $('#mPagar').attr("disabled", true);
            MostrarCheques(true);
            MostrarCliCredito(false);
            //MostrarPlanosPagamento(false);

        } else if ($("#mFormaPag").val() == "05") { //Se for CREDITO CLIENTE

            MostrarCheques(false);
            MostrarCliCredito(true);
            //MostrarPlanosPagamento(false);

            if ($('#vlrPagto').val() != "") {//Se houver valor
                if ($('#vlrPagto').val() != "0"
                    && parseFloat($('#vlrPagto').val().replace(".", "").replace(",", "."))
                    <= parseFloat($('#cliCredito').val().replace(".", "").replace(",", "."))) {

                    $('#mPagar').attr("disabled", false);
                } else {
                    $('#mPagar').attr("disabled", true);
                }
            } else {
                $('#mPagar').attr("disabled", true);
            }

        } else { //Se for outro metodo

            MostrarCheques(false);
            MostrarCliCredito(false);
            //MostrarPlanosPagamento(false);

            if ($('#vlrPagto').val() != "") {//Se houver valor
                $('#mPagar').attr("disabled", false);
            }
        }

    } else {
        $('#mPagar').attr("disabled", true);
        MostrarCheques(false);
        MostrarCliCredito(false);
        //MostrarPlanosPagamento(false);
    }
}

function AbrirTransacao() {

    PAGTOFEITO = true;

    //Armazena o credito em loja, do cliente
    $("#cliCredito").val($("#CreditoCliente").val());

    //Pega a quantidade de Itens
    itens = $('#table_itens >tbody >tr').length;
    $('#tItens').text(itens);

    //Pega o valor total da View
    vlrTotal = totalGrl;

    if (vlrJaPago > 0) {
        vlrRestante = vlrTotal - vlrJaPago;
        $('#vPago').text('R$' + vlrJaPago.toFixed(2));
    } else {
        vlrRestante = vlrTotal;
    }

    $('#vTotal').text('R$' + vlrTotal.toFixed(2));

    if (vlrRestante <= 0) {
        if (vlrRestante < 0) {
            troco = vlrRestante * -1;
        } else {
            vlrRestante = 0;
        }

        $('#vPendente').text('R$' + troco.toFixed(2));
        Controles(false); //Desabilita os campos
    } else {

        $('#vPendente').text('R$' + vlrRestante.toFixed(2));
        Controles(true); //Habilita os campos
    }

    // STONE (se não tiver tokens)
    if (accessToken.access_token == '' && orderToken.order_id == '' && tipoTef == 'S') {

        //Gera o pedido (STONE)
        showProgress();
        $("#txtLoading").html("Um momento...");

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "/transacao/gerarpedido?total=" + vlrTotal.toFixed(2) + "&cliente=" + cliName,
            dataType: "json",
            success: function (data) {

                //Pega os TOKENS
                accessToken = data.acesso;
                orderToken = data.pedido;

                disposeProgress(); //Tira o loading
            },
            error: function (result) {
                console.log("Error - " + JSON.stringify(result));
                disposeProgress(); //Tira o loading

            }
        });
    }
}

// CONTROLAR VISUALIZAÇÃO DE CHEQUES
var chequePanel = false;
function MostrarCheques(vl) {
    if (vl == null) {
        chequePanel = !chequePanel;
    } else {
        chequePanel = vl;
    }

    if (!chequePanel) {
        $("#chequePanel").attr("hidden", "hidden");
        $("#vlrPagto").prop("disabled", false);
    } else {
        $("#chequePanel").removeAttr("hidden");
        $("#vlrPagto").prop("disabled", true);
    }
}

// CONTROLAR VISUALIZAÇÃO DE CRÉDITOS
var cliCreditoPanel = false;
function MostrarCliCredito(vl) {
    if (vl == null) {
        cliCreditoPanel = !cliCreditoPanel;
    } else {
        cliCreditoPanel = vl;
    }

    if (!cliCreditoPanel) {
        $("#cliCreditoPanel").attr("hidden", "hidden");
        //$("#vlrPagto").prop("disabled", false);
    } else {
        $("#cliCreditoPanel").removeAttr("hidden");
        //$("#vlrPagto").prop("disabled", true);
    }
}

function GetChequeInfo() {
    if ($("#mFormaPag").val() == "02") {
        return $("#CheqTitular").val() + "&" + $("#CheqNumero").val() + "&" + $("#CheqIdBanco").val()
            + "&" + $("#CheqValor").val() + "&" + $("#CheqVencimento").val();
    } else {
        return "";
    }
}

function ClearChequeFields() {
    $(".cheqField").val("");
}

//~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
//                  PAGAMENTO                   |
//~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
function Pagar() {

    var opcao = $("#mFormaPag").val();
    var vlrPago = $('#vlrPagto').val();
    //var plano = $("#planoPagto").val();

    // Executa a função na View passando o _pagto
    var token = { access: accessToken.access_token, order: orderToken.order_id };

    // Mostra indicador de progresso
    showProgress();
    $("#txtLoading").html("Um momento...");

    // Se for crédito, aguardar Stone
    if (opcao == "03")
        $("#txtLoading").html("Aguardando maquineta...");

    //~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
    //                  CONTROLER                   |
    //~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
    $.ajax({
        type: "POST"
        , contentType: "application/json; charset=utf-8"
        , url: "/transacao/Pagamento/" +
            "?vPago=" + vlrPago +
            "&tPag=" + opcao +
            "&vTotal=" + vlrTotal.toString().replace('.', ',') +
            "&vResta=" + vlrRestante.toString().replace('.', ',') +
            "&accessTk=" + token.access +
            "&cheqInfo=" + GetChequeInfo() +
            "&idTitulo=" + idTitulo
        //, data: "{'vPago': '" + vlrPago + "', " +
        //    " 'tPag': '" + opcao + "'," +
        //    " 'vTotal': '" + vlrTotal.toString().replace('.', ',') + "'," +
        //    " 'vResta':'" + vlrRestante.toString().replace('.', ',') + "'," +
        //    " 'accessTk':'" + token.access + "', 'orderTk':'" + token.order + "'," + //CARTAO (STONE)
        //    " 'cheqInfo':'" + GetChequeInfo() + "'," + //CHEQUE
        //    " 'idTitulo':'" + idTitulo + "'}" //Especifico para titulos
        , dataType: "json"
        , async: true
        , success: function (data) { //RECEBE RESPOSTA DO CONTROLE

            if (data == "ERRO") {
                MostrarToast("erro", "Erro ao efetuar pagamento!");
            } else {

                if (opcao == "02")
                    vlrPago = $("#CheqValor").val();

                //Gera um objeto _pagto e envia para a View
                var _pagto = {
                    id: pagtoRowIndex(),
                    vPg: data.vPago,
                    tPg: opcao,
                    cTk: data.cToken,
                    chq: data.cheqInfo,
                    pln: planoPagamento
                };

                //Subtrai do vlrRestante
                vlrRestante = data.vPendente;
                vlrJaPago = vlrTotal - vlrRestante;

                //Se for um pagamento que não seja cartão
                if (_pagto.tPg != "03") {
                    //Empurra na lista
                    pagtos.push(_pagto);
                }

                //(re)SETA OS CAMPOS NA VIEW
                $('#vlrPagto').val('');

                $('#vPago').text('R$' + vlrJaPago.toFixed(2));

                $('#mFormaPag').val('0');
                $('#mFormaPag').change();

                //$('#planoPagto').val('0');

                //if (opcao == "05") {

                //}

                if (vlrRestante.toFixed(2) <= 0) {
                    Controles(false); //Desabilita os campos

                    if (vlrRestante.toFixed(2) < 0) {
                        troco = vlrRestante * -1; //Armazena na variable, que será lançada no final (multiplica por -1 pra deixar o valor positivo)
                    } else {
                        troco = vlrRestante = 0;
                    }
                    $('#vPendente').text('R$' + troco.toFixed(2));

                } else {
                    $('#vPendente').text('R$' + vlrRestante.toFixed(2));
                }

                var opcaoNome;
                switch (opcao) {
                    case "01": //Dinheiro
                        opcaoNome = "Dinheiro";
                        break;
                    case "02": //Cheque
                        opcaoNome = "Cheque";
                        break;
                    case "03": //Cartão
                        opcaoNome = "Cartão";
                        break;
                    case "05": //Credito Loja
                        opcaoNome = "Crédito Loja";
                        //CALCULA A DIFERENÇA DO CREDITO DO CLIENTE
                        var creditoDif = parseFloat($("#cliCredito").val().replace(".", "").replace(",", ".")) - parseFloat(vlrPago.replace(".", "").replace(",", "."));
                        $("#cliCredito").val(creditoDif.toString().replace(".", ",")); //Atualiza o valor de credito do Cliente
                        break;
                    case "90": //Fiado
                        opcaoNome = "Sem Pagto.";
                        break;
                }

                var param = "('pagto" + pagtoRowIndex() + "', '" + data.cToken + "')";

                var output = "";
                if (opcao == "03") {
                    output = '<tr id="pagto' + pagtoRowIndex() + '">' +
                        '<td>' + opcaoNome + '</td>' +
                        '<td hidden="hidden">' + opcao + '</td>' +
                        '<td> R$ ' + data.vPago.toFixed(2) + '</td>' +
                        '<td><span style="font-size: 10px; color:orangered" title="Para estornar pagamentos em cartão, necessário ir para pagina Administrativa" data-toggle="tooltip"><i class="fa fa-exclamation-triangle"></i></span> </td>' +
                        '<td hidden="hidden" class="cToken">' + data.cToken + '</td>' +
                        '<td hidden="hidden" class="cheqInfo">' + data.cheqInfo + '</td>' + //Nao sera necessario
                        '</tr>';
                } else {
                    output = '<tr id="pagto' + pagtoRowIndex() + '">' +
                        '<td>' + opcaoNome + '</td>' +
                        '<td hidden="hidden">' + opcao + '</td>' +
                        '<td> R$ ' + data.vPago.toFixed(2) + '</td>' +
                        //'<td> R$ ' + parseFloat(vlrPago).toFixed(2) + '</td>' +
                        '<td><span class="btn btn-danger" style="font-size: 10px" onclick="EstornarPagto' + param + '">x</span></td>' +
                        '<td hidden="hidden" class="cToken">' + data.cToken + '</td>' +
                        '<td hidden="hidden" class="cheqInfo">' + data.cheqInfo + '</td>' + //Nao sera necessario
                        '</tr>';
                }

                $("#pagtoTable tbody").append(output);
            }

            disposeProgress();

            $('[data-toggle="tooltip"]').tooltip(); //Habilita os tooltips se existirem

            //Debug
            console.log(pagtos);

            ClearChequeFields();
        }
        , error: function (result) {

            console.log("Error - " + JSON.stringify(result));
            MostrarToast("erro", "Houve um erro ao efetuar Pagamento!")
            disposeProgress();
        }
    });
}

//~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
//             ESTORNO DO PAGTO.                |
//~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
function EstornarPagto(pgId, tk) {

    showProgress();
    $("#txtLoading").html("Estornando pagamento...");

    //console.log(pgId + "\n " + tk);

    if (tk != "") { //Se for cartão (se houver token)
        console.log("FOI CARTAO EM!");
    }

    //Pega o tipo desse pagamento e enumera
    var tipoPg = $("#pagtoTable tbody tr#" + pgId + " td:nth-child(2)").html();

    //Pega o valor desse pagamento (transforma pra Float)
    var vlPg = $("#pagtoTable tbody tr#" + pgId + " td:nth-child(3)").html();
    vlPg = vlPg.replace(",", ".");
    vlPg = vlPg.replace("R$", "");

    ////Pega o valor RESTANTE
    //var vlrPend = $("#vPendente").html();
    //vlrPend = vlrPend.replace(",", ".");
    //vlrPend = vlrPend.replace("R$", "");

    //REMOVE ESSE PAGAMENTO DESSE PEDIDO (Controler)
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "/transacao/estornar/" +
            "?tPag=" + tipoPg +
            "&accessTk=" + accessToken.access_token +
            "&orderTk=" + orderToken.order_id +
            "&transactTk=" + tk,
        //data: "{'tPag':'" + tipoPg + "', 'accessTk':'" + accessToken.access_token + "', 'orderTk':'" + orderToken.order_id + "', 'transactTk':'" + tk + "'}",
        dataType: "json",
        success: function (data) {

            if (data == "ERRO") {
                MostrarToast("erro", "Erro ao estornar pagamento!");
            } else {

                vlrRestante = vlrRestante + parseFloat(vlPg);
                vlrJaPago = vlrJaPago - parseFloat(vlPg);


                console.log(parseFloat($("#cliCredito").val().replace(".", "").replace(",", ".")) + "+" + parseFloat(vlPg));

                //CALCULA A DIFERENÇA DO CREDITO DO CLIENTE
                var creditoSum = parseFloat($("#cliCredito").val().replace(".", "").replace(",", ".")) + parseFloat(vlPg);
                $("#cliCredito").val(creditoSum.toString().replace(".", ",")); //Atualiza o valor de credito do Cliente

                //Atualiza na view
                $("#vPendente").html("R$" + vlrRestante.toFixed(2));
                $("#vPago").html("R$" + vlrJaPago.toFixed(2));

                //Pega o ID
                var id = parseInt(pgId.replace("pagto", ""));

                //Ve na lista qual dos itens possui esse ID e REMOVE
                for (var i = 0; i < pagtos.length; i++) {
                    if (pagtos[i].id == id) {
                        pagtos.splice(i, 1);
                    }
                }

                //Remove a linha da tabela..
                $("#pagtoTable tbody tr#" + pgId + "").remove();

                ////Em ultimo caso, esse codigo zera a lista de pagamentos, a fim de evitar bugs.
                //if ($("#pagtoTable tbody").has("tr").length == 0) {
                //    pagtos = [];
                //}

                console.log(pagtos);

                if (vlrRestante > 0) {
                    Controles(true); //Habilita os campos
                }
            }
            disposeProgress(); //Tira o loading
        },

        error: function (result) {
            MostrarToast("erro", "Erro ao estornar pagamento!");
            console.log("Error - " + JSON.stringify(result));
            disposeProgress();
        }
    });
}

/*
LIST<PAGAMENTOS>
var pgt = { vPg, tPg, cTk, chq };

PEDIDO VENDA
public Cheque Cheq { get; set; }
public Cartao Card { get; set; }
public string tPag { get; set; }
public double vPag { get; set; }

    CHEQQ
    public string Titular { get; set; }
    public string NumeroCheque { get; set; }
    public int IdBanco { get; set; }
    public double Valor { get; set; }
    public DateTime Vencimento { get; set; }

    CARTAO
    public string cAut_Opc { get; set; }
    public string CNPJ_Opc { get; set; }
    public string tBand_Opc { get; set; }
    public string tpIntegra_Opc { get; set; }
*/

/*
LIST<TITULOCONTA>
var titConta = { titulo, conta, valor, datalancamento }

TITULO CONTA
public int Titulo { get; set; }
public int Conta { get; set; }
public double Valor { get; set; }
public DateTime DataLancamento { get; set; }
*/

function FecharPedidoStone() {

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "/transacao/FecharPedidoStone/" +
            "?access=" + accessToken.access_token +
            "&order=" + orderToken.order_id,
        //data: "{ 'access':'" + accessToken.access_token + "','order':'" + orderToken.order_id + "'}",
        dataType: "json",
        success: function (data) {

        },
        error: function (result) {
            alert("Houve um erro com a resposta da STONE.");
            console.log("Error - " + JSON.stringify(result));
        }
    });

}

function ValidaContasContabeis() {

};

//Retorna o Proximo INDICE da lista de Pagamentos.
function pagtoRowIndex() {
    var index = $("#pagtoTable tbody")[0].getElementsByTagName("tr").length;
    return index;
}

//~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
//                  CONTROLES                   |
//~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
function Controles(ativar) {
    if (!ativar) {
        $('#mFormaPag').val('0');
        $('#mFormaPag').change();
        $('#mFormaPag').attr("disabled", true);
        $('#vlrPagto').attr("disabled", true);
        $("#mPagar").css("display", "none");
        $("#divPago").css("display", "none");
        //$("#divPendente").css("display", "none");
        $("#pendenteTxt").text("Troco");
        $("#divOK").css("display", "");
        $('#txtOK').text('NÃO HÁ PENDÊNCIAS');
        $("#mConfirmar").css("display", "");
    } else {
        $('#mFormaPag').val('0');
        $('#mFormaPag').change();
        $('#mFormaPag').attr("disabled", false);
        $('#vlrPagto').attr("disabled", false);
        $("#mPagar").css("display", "");
        $("#divPago").css("display", "");
        $("#divPendente").css("display", "");
        $("#pendenteTxt").text("Pendente");
        $("#divOK").css("display", "none");
        $("#mConfirmar").css("display", "none");
    }
}

//~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
//                PROGRESS BAR                  |
//~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
/* EXIBE */
function showProgress() {
    setTimeout(function () {
        var modal = $('<div />');
        modal.addClass("modal");
        $('body').append(modal);
        var loading = $(".loading");
        var popup = $('#popup');
        loading.show();
        popup.show();
        var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
        var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
        loading.css({ top: top, left: left });
        loading.css("z-index", "100001");
        popup.css("z-index", "100001");
    }, 200);
}

/*ESCONDE*/
function disposeProgress() {
    setTimeout(function () {
        var loading = $(".loading");
        var popup = $('#popup');
        loading.hide();
        popup.hide();
    }, 200);
}

/* DESABILITA O CLICK NOS LINKS */
function disableClick() {
    $('a').css({
        'pointer-events': 'none',
        'cursor': 'default',
        'text-decoration': 'none',
        'color': '#636e72'
    });
}

//Formata os campos de VALOR, para 2 digitos apos a virgula.
function moedaDoisDecimais(z) {
    v = z.value;
    v = v.replace(/\D/g, "") // permite digitar apenas numero
    v = v.replace(/(\d{1})(\d{1,2})$/, "$1,$2") // coloca virgula antes dos ultimos 3 digitos
    z.value = v;
}

//SETA O AVISO DE FECHAMENTO
function SetAviso(vl) {
    if (vl) {
        //IMPEDE O FORM DE FECHAR, precisa de Confirmação
        window.addEventListener("beforeunload", MensagemSaida);
    } else {
        window.removeEventListener("beforeunload", MensagemSaida);
    }
}
function MensagemSaida(e) {
    var message = "Atenção, os pagamentos serão perdidos! Faça os estornos antes de sair da pagina";
    e = e || window.event;
    if (e) {
        e.returnValue = message;
    }
    return message;
}

//~ ~ ~ ~ ~ ~ ~ TOASTR
function MostrarToast(tipo, msg) {

    if (tipo == "ok") {

        toastr.success(msg, 'Sucesso!', {
            closeButton: true,
            progressBar: true,
            debug: false,
            newestOnTop: false,
            positionClass: "toast-top-right",
            preventDuplicates: false,
            onclick: null,
            showDuration: "500",
            hideDuration: "1000",
            timeOut: "3000",
            extendedTimeOut: "1000",
            showEasing: "swing",
            hideEasing: "linear",
            showMethod: "fadeIn",
            hideMethod: "fadeOut",
        });

    } else {

        toastr.error(msg, 'Erro!', {
            closeButton: true,
            progressBar: true,
            debug: false,
            newestOnTop: false,
            positionClass: "toast-top-right",
            preventDuplicates: false,
            onclick: null,
            showDuration: "500",
            hideDuration: "1000",
            timeOut: "3000",
            extendedTimeOut: "1000",
            showEasing: "swing",
            hideEasing: "linear",
            showMethod: "fadeIn",
            hideMethod: "fadeOut",
        });
    }

}