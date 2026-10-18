$(document).ready(function () {

    $('#cPF_CNPJ').mask('99.999.999/9999-99');
    $('#cPF_CNPJ').attr("placeholder", "__.___.___/____-__");
    $("#cEP").mask("99999-999");
    $("#cEP").attr("placeholder", "_____-___");   
    $('#cPF_Responsavel').mask('999.999.999-99');
    $('#cPF_Responsavel').attr("placeholder", "___.___.___-__");
    $("#fone_Reponsavel").mask("(99) 9999-9999");
    $("#fone_Reponsavel").attr("placeholder", "(__) ____-____");
    $("#cEP_Responsavel").mask("99999-999");
    $("#cEP_Responsavel").attr("placeholder", "_____-___");
    $('#cPF_CNPJ_Contador').mask('999.999.999-99');
    $('#cPF_CNPJ_Contador').attr("placeholder", "___.___.___-__");
    $('#pessoaJuridicaContador').click(function () {
        if ($('#pessoaJuridicaContador').prop('checked')) {
            $('#cPF_CNPJ_Contador').mask('99.999.999/9999-99');
            $('#cPF_CNPJ_Contador').attr("placeholder", "__.___.___/____-__");
        } else {
            $('#cPF_CNPJ_Contador').mask('999.999.999-99');
            $('#cPF_CNPJ_Contador').attr("placeholder", "___.___.___-__");
        }
    });
    $("#fone_Contador").mask("(99) 9999-9999");
    $("#fone_Contador").attr("placeholder", "(__) ____-____");

});