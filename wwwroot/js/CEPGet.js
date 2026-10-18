
//IMPLEMENTAÇÃO PADRÃO
$(document).ready(function () {
    $(".CEP").on("input", function () {
        if ($(this).val().length === 9) {
            GetCEPInfo($(this).val());
        }
    });
});



function GetCEPInfo(cep) {
    //AJAX..
    $.ajax({
        type: "GET",
        contentType: "application/json",
        url: "https://viacep.com.br/ws/"+cep+"/json/",
        dataType: "json",

        success: function (data) {

            //console.log(data);
            $('#endereco').val(data.logradouro);
            $('#bairro').val(data.bairro);
            $('#cidade').val(data.localidade);
            $('#estados').val(data.uf);
            $('#municipio').val(data.ibge);

            $("#nRO").focus(); //Foca nesse Controle

            ////console.log(data);
            //$("input[name='Logradouro']").val(data.logradouro);
            //$("input[name='Bairro']").val(data.bairro);
            //$("input[name='Cidade']").val(data.localidade);
            //$("input[name='UF']").val(data.uf);

            //$("input[name='Complemento']").select(); //Foca nesse Controle
        },

        error: function (result) {
            console.log("Erro CEP\n" + JSON.stringify(result));
        }
    });

}