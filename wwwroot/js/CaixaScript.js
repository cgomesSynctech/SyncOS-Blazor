//AO CARREGAR A PAGINA
$(document).ready(function () {
    CheckCaixaAberto();
});

function CheckCaixaAberto() {

    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: "/Caixa/CheckCaixaAberto",
        dataType: "json",
        success: function (data) {

            if (data.status === 1) { //Tem Caixas Abertos.
                $(".BtFecharCaixa").removeAttr("hidden");

                $(".quadroCaixa").removeAttr("hidden");


            } else { //Não tem caixa aberto.
                $(".BtAbrirCaixa").removeAttr("hidden");

                $(".opcoesCaixa").css("pointer-events", "none");
                $(".opcoesCaixa").css("opacity", "0.6");
                $(".opcoesCaixa").css("text-decoration", "line-through");

                $(".quadroCaixa").attr("hidden", "hidden");
            }

        },
        error: function (result) {
            console.log("Error - " + JSON.stringify(result));
        }
    });
}



function Caixa(q) {
    switch (q) {
        case "Abrir":
            $("#modalInfo").html("Deseja <b>ABRIR</b> o Caixa?");
            $("#modalConfirmBut").on("click", function () { SetCaixa(q); });
            break;

        case "Fechar":
            $("#modalInfo").html("Deseja <b>FECHAR</b> o Caixa?");
            $("#modalConfirmBut").on("click", function () { SetCaixa(q); });
            break;
    }
}

function SetCaixa(q) {

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "/Caixa/Caixa/" + q,
        dataType: "json",
        success: function (data) {
            console.log(data);
        },
        error: function (result) {
            console.log("Error - " + JSON.stringify(result));
        }
    });

}
