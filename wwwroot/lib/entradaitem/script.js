$(document).ready(function () {
    //  
    $("#formModal").submit(function (event) {
        var x = $('#novoItemModal #codigoBarras').val().trim();
        console.log(x);
        $('#referencia').val(x);
        event.preventDefault();
    });

    //  Fechar modal
    $('#btnClose').click(function () {
        $('#novoItemModal').modal('hide');
    });
});


