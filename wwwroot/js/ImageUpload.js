
function fotoPadraoPet() {
    if ($(".fotoUrl").val().length === 0) {
        $(".imagem").attr("src", "/img/PetMenu.jpg"); //Foto padrão PET
        console.log("aplicando imagem padrão.");
    }
}

$(document).ready(function () {

    if ($(".fotoUrl").val().length === 0) {
        $(".imagem").attr("src", "/img/icons/essential-pack/picture-2.png"); //Foto padrão
        console.log("aplicando imagem padrão.");
    }


    $("#Img").change(function () {
        var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.jpg|.jpeg|.gif|.png|.bmp)$/;
        if (regex.test($(this).val().toLowerCase())) {

            if (typeof (FileReader) !== "undefined") {

                var reader = new FileReader();
                reader.onload = function (e) {
                    $(".imagem").attr("src", e.target.result);
                    //$(".fotoBase64").attr("value", e.target.result);
                };
                reader.readAsDataURL($(this)[0].files[0]);

            } else {
                console.log("Necessário salvar para atualizar foto.");
            }

        } else {
            $(".imagem").attr("src", "");
            //$(".fotoBase64").attr("value", "");

            ToastMsg = "Escolha uma imagem válida.";
            ToastType = "erro";
            MostrarToast();
        }
    });

});