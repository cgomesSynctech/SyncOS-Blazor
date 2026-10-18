// Função para abrir o modal de qualquer tela
function MostrarModal(titulo, mensagem, tipo, callback) {
    $('#titulo').html(titulo);
    $("#mensagem").html(mensagem);
    switch (tipo) {
        case 1: //Confirmação Cadastro/Edição
            $("#modalConteudo").addClass("modal-success");
            $("#cancelar").addClass("btn-outline-success");
            $("#confirmar").addClass("btn-success");
            break;
        case 2: //Confirmação Cancelamento (muda de verde para Vermelho)
            $("#modalConteudo").addClass("modal-danger");
            $("#cancelar").addClass("btn-outline-danger");
            $("#confirmar").addClass("btn-danger");
            break;
    }
    $("#confirmaModal").modal("show");
    $("#confirmar").click(function () {
        // Informa a View que o usuário clicou em confirmar. A View precisa implementar a função do callback
        callback();
        $("#confirmaModal").modal("hide");
    });
}