
/*
 * VALIDAÇÃO GENERICA
 * 
 * Colocar a classe 'VALIDAR' no input que deseja validar.
 * Esse input precisa está dentro de um ELEMENTO, que contenha também um LABEL, para que o Toast funcione.
 * Ex:
 * <div>
 *    <label>Teste</label>
 *    <input class='VALIDAR CPF_CNPJ' type='text' />
 * </div> 
 * 
 * Depois chamar ValidarInfo() no botão/evento que 'Finaliza' o formulário
 */

function NomeDoCampo(elemento) {
    if ($(elemento).parent().parent().children("label").text() !== "") {
        //console.log("Pegou o texto do LABEL tio.");
        return $(elemento).parent().parent().children("label").text() + "<br/>"; //Pega o label num nivel mais acima.
    } else {
        //console.log("Texto do LABEL irmão.");
        return $(elemento).parent().children("label").text() + "<br/>"; //Pega o label que é 'irmão' do Input.
    }
}


//VALIDAÇÃO
function ValidarInfo() {

    var valido = true;
    var camposErrados = "";


    $(".VALIDAR").each(function (i, o) {

        //Neutraliza o campo
        $(this).css("border", "");

        //Validacoes especiais
        //CPF_CNPJ
        if ($(this).attr("name") === "CPF_CNPJ") {
            if (!($(this).val().length === 14 || $(this).val().length === 18)) {
                //camposErrados += $(this).parent().children("label").text() + "<br/>";
                camposErrados += NomeDoCampo($(this));
                $(this).css("border", "1px solid red");
                valido = false;
            }
        }

        //Email
        else if ($(this).attr("name") === "Email") {
            if ($(this).val().length === 0 || !$(this).val().includes("@")) {
                //camposErrados += $(this).parent().children("label").text() + "<br/>";
                camposErrados += NomeDoCampo($(this));
                $(this).css("border", "1px solid red");
                valido = false;
            }
        }

        //CEP
        else if ($(this).attr("name") === "CEP") {
            if ($(this).val().length < 9) {
                //camposErrados += $(this).parent().children("label").text() + "<br/>";
                camposErrados += NomeDoCampo($(this));
                $(this).css("border", "1px solid red");
                valido = false;
            }
        }

        //COMBOBOXes (Classe) 
        else if ($(this).hasClass("COMBOBOX")) {
            if ($(this).val() === null || $(this).val() === "" || $(this).val() === "0") {
                //camposErrados += $(this).parent().children("label").text() + "<br/>";
                camposErrados += NomeDoCampo($(this));
                $(this).css("border", "1px solid red");
                valido = false;
            }
        }

        //CONFIRMAÇÃO DE SENHA (Cadastro de Empresa)
        else if ($(this).hasClass("PASS2")) {
            if ($(this).val() !== $(".PASS1").val()) {
                camposErrados += "Senhas estão diferentes! <br/>";
                $(this).css("border", "1px solid red");
                valido = false;
            }
        }

        //Validações Genericas
        else if ($(this).val() === null || $(this).val().length === 0) {
            //camposErrados += $(this).parent().children("label").text() + "<br/>";
            camposErrados += NomeDoCampo($(this));
            $(this).css("border", "1px solid red");
            valido = false;
        }


    });

    //Tudo valido? Mostra modal de Confirmação
    if (valido)
    {
        return true;
    }
    else
    {
        //TOAST
        if (camposErrados !== "") {
            ToastMsg = "Verificar campos:<br/><b>" + camposErrados + "</b>";
            ToastType = "erro";
            MostrarToast();
        }
        return false;
    }

}