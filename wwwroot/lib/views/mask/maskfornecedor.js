$(document).ready(function ($) {
    //mascaras dos campos de fornecedor
    $(".dataCampo").mask("99/99/9999");    
    $(".dataCampo").attr("placeholder", "__/__/____");
    $("#phone").mask("(99) 9-9999-9999");
    $("#telefone").mask("(99) 9999-9999");
    $("#telefone2").mask("(99) 9999-9999");
    $("#cep").mask("99999-999");
    $("#ssn").mask("999-99-9999");
    $("#eyescript").mask("~9.99 ~9.99 999");


    //  Set "" no campos double
    $('.zerarcampo').val("");


    $('#cpfCnpj').mask('999.999.999-99');
    $('#cpfCnpj').attr("placeholder", "___.___.___-__");
    $('#errorRazaoSocial').html('');

    $("#radioCpf").prop("checked", true);




    //mudar a mascara do CPF ou Cnpj de acordo com o click no radio
    $('#radioCpf').click(function () {
        $('#cpfCnpj').mask('999.999.999-99');
        $('#cpfCnpj').attr("placeholder", "___.___.___-__");
        $('#errorRazaoSocial').html('');
    });

    $('#radioCnpj').click(function () {
        $('#cpfCnpj').mask('99.999.999/9999-99');
        $('#cpfCnpj').attr("placeholder", "__.___.___/____-__");
    });
})