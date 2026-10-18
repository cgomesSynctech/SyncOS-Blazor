$(document).ready(function () {
    AplicarMascaras();
});

//Função que formata os campos
function AplicarMascaras() {

    $(".DATA").mask("99/99/9999");
    $(".DATA").attr("placeholder","__/__/____");

    $(".HORA").mask("99:99");

    $(".CEP").mask("99999-999");

    $(".CPFCNPJ").keyup(function () {
        var cpfMascara = function (val) {
            return val.replace(/\D/g, '').length > 11 ? '00.000.000/0000-00' : '000.000.000-009';
        };
        var cpfOptions = {
            onKeyPress: function (val, e, field, options) {
                field.mask(cpfMascara.apply({}, arguments), options);
            }
        };
        $(this).mask(cpfMascara, cpfOptions);
    });

    $(".DECIMAL").keyup(function () {
        DECIMAL(this);
    });

    $(".DECIMAL3").keyup(function () {
        DECIMAL3(this);
    });

    $(".NUMERO").keyup(function () {
        NUMERO(this);
    });

    $(".NUMEROALT").keyup(function () {
        NUMERO(this);
    });

    $(".TELEFONE").keyup(function () {
        TELEFONE(this);
    });


    //Dispara os eventos, para aplicar a mascara nos inputs preenchidos previamente
    $(".CPFCNPJ").keyup();
    $(".DECIMAL").keyup();
    $(".DECIMAL3").keyup();
    $(".NUMERO").keyup();
    $(".NUMEROALT").keyup();
    $(".TELEFONE").keyup();

}


function NUMERO(z) { 
    v = z.value;
    v = v.replace(/\D/g, ""); // permite digitar apenas numero
    z.value = v;
}

function DECIMAL(z) {
    v = z.value;
    v = v.replace(/\D/g, ""); // permite digitar apenas numero
    v = v.replace(/(\d{1})(\d{1,2})$/, "$1,$2"); // coloca virgula antes dos ultimos 2 digitos
    z.value = v;
}

function DECIMAL3(z) {
    v = z.value;
    v = v.replace(/\D/g, ""); // permite digitar apenas numero
    v = v.replace(/(\d{1})(\d{1,3})$/, "$1,$2");// coloca virgula antes dos ultimos 3 digitos
    z.value = v;
}

function TELEFONE(z) {
    v = z.value;
    v = v.replace(/\D/g, "");             //Remove tudo o que não é dígito
    v = v.replace(/^(\d{2})(\d)/g, "($1) $2"); //Coloca parênteses em volta dos dois primeiros dígitos
    v = v.replace(/(\d)(\d{4})$/, "$1-$2");    //Coloca hífen entre o quarto e o quinto dígitos
    z.value = v;
}

