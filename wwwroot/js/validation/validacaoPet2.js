
//VALIDAÇÃO
function ValidarFormPet() {

    var valido = true;


    //Campos..
    var nome = $(":input[name=Nome]");

    var comboEspecie = $("#comboboxespecie");
    var comboRaca = $("#comboboxraca");
    var comboPelagem = $("#comboboxpelagem");

    var nomeDono = $(":input[name=NomeDono]");

    var comboSexo = $("#comboboxsexo");
    var comboCastrado = $("#comboboxcastrado");


    //Validação...
    if (nome.val() === null || nome.val().length === 0) {
        nome.css("border", "1px solid red");
        valido = false;
    } else {
        nome.css("border", "");
    }

    if (comboEspecie.val() === null || comboEspecie.val().length === 0) {
        comboEspecie.css("border", "1px solid red");
        valido = false;
    } else {
        comboEspecie.css("border", "");
    }

    if (comboRaca.val() === null || comboRaca.val().length === 0) {
        comboRaca.css("border", "1px solid red");
        valido = false;
    } else {
        comboRaca.css("border", "");
    }

    if (comboPelagem.val() === null || comboPelagem.val().length === 0) {
        comboPelagem.css("border", "1px solid red");
        valido = false;
    } else {
        comboPelagem.css("border", "");
    }

    if (nomeDono.val() === null || nomeDono.val().length === 0) {
        nomeDono.css("border", "1px solid red");
        valido = false;
    } else {
        nomeDono.css("border", "");
    }

    if (comboSexo.val() === null || comboSexo.val().length === 0) {
        comboSexo.css("border", "1px solid red");
        valido = false;
    } else {
        comboSexo.css("border", "");
    }

    if (comboCastrado.val() === null || comboCastrado.val().length === 0) {
        comboCastrado.css("border", "1px solid red");
        valido = false;
    } else {
        comboCastrado.css("border", "");
    }


    //Tudo valido? Submite
    if (valido) {
        $("#PET_VIEW").submit();
    }


}