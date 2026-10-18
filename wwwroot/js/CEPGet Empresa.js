
$(document).ready(function () {
    
    //  Requisisao VIACEP - CEP Responsavel
    $("#cEP_Responsavel").on("input", function () {
        if ($(this).val().length === 9) {
            $.ajax({
                type: "GET",
                contentType: "application/json",
                url: "https://viacep.com.br/ws/" + $(this).val() + "/json/",
                dataType: "json",

                success: function (data) {

                    //console.log(data);
                    $('#endereco_Responsavel').val(data.logradouro);
                    $('#bairro_Responsavel').val(data.bairro);
                    $('#cidade_Responsavel').val(data.localidade);
                    $('#estados_responsavel').val(data.uf);

                    $("#numero_Responsavel").focus(); //Foca nesse Controle
                },

                error: function (result) {
                    console.log("Erro CEP\n" + JSON.stringify(result));
                }
            });
        }
    });


    //  Requisisao VIACEP - CEP EMPRESA
    $("#cEP").on("input", function () {
        if ($(this).val().length === 9) {
            $.ajax({
                type: "GET",
                contentType: "application/json",
                url: "https://viacep.com.br/ws/" + $(this).val() + "/json/",
                dataType: "json",

                success: function (data) {

                    //console.log(data);
                    $('#endereco').val(data.logradouro);
                    $('#bairro').val(data.bairro);
                    $('#cidade').val(data.localidade);
                    $('#estados').val(data.uf);
                    $('#municipio').val(data.ibge);

                    $("#nRO").focus(); //Foca nesse Controle
                },

                error: function (result) {
                    console.log("Erro CEP\n" + JSON.stringify(result));
                }
            });
        }
    });

});
