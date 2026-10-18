
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

$('#txtabela').click(function () {
    $('#tabelaPrecoModal').modal('toggle');
});

$('#comboboxtipoconta').change(function () {
    var tipo = $("#comboboxtipoconta").val();
    //troca funcionando

    if (tipo == 2) {
        //se for banco
        $("#divbanco").removeAttr("hidden");
        $("#comboboxbanco").removeAttr("disabled");

        $("#divassociarcaixa").attr("hidden", "true");
        $("#comboboxassociarcaixausuario").attr("disabled", "disabled");

        $("#divsubconta").attr("hidden", "true");
        $("#comboboxsubcontade").attr("disabled", "disabled");

        $("#divobs").attr("hidden", "true");
        $("#observacao").attr("disabled", "disabled");

        $("#divdespesafixa").attr("hidden", "true");
        $("#despesafixa").attr("disabled", "disabled");

    } else if (tipo == 1) {
        //se for caixa

        $("#divassociarcaixa").removeAttr("hidden");
        $("#comboboxassociarcaixausuario").removeAttr("disabled");

        $("#divbanco").attr("hidden", "true");
        $("#comboboxbanco").attr("disabled", "disabled");

        $("#divsubconta").attr("hidden", "true");
        $("#comboboxsubcontade").attr("disabled", "disabled");

        $("#divobs").attr("hidden", "true");
        $("#observacao").attr("disabled", "disabled");

        $("#divdespesafixa").attr("hidden", "true");
        $("#despesafixa").attr("disabled", "disabled");
        
    } else {
        //se for os demais
        $("#divsubconta").removeAttr("hidden");
        $("#comboboxsubcontade").removeAttr("disabled");

        $("#divobs").removeAttr("hidden");
        $("#observacao").removeAttr("disabled");

        $("#divbanco").attr("hidden", "true");
        $("#comboboxbanco").attr("disabled", "disabled");

        $("#divassociarcaixa").attr("hidden", "true");
        $("#comboboxassociarcaixausuario").attr("disabled", "disabled");

        $("#divdespesafixa").attr("hidden", "true");
        $("#despesafixa").attr("disabled", "disabled");


    }
    if (tipo == 15) {
        //se for despesa fixa
        $("#divdespesafixa").removeAttr("hidden");
        $("#despesafixa").removeAttr("disabled");
    }

});

