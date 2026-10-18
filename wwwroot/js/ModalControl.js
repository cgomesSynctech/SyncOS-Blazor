function MostrarModal(tipo, mensagem, target, rota) {

    var corModal = "";

    $("#modalMensagem").html(mensagem);

    switch (tipo) {
        case 1: //Confirmação Cadastro/Edição
            //$("#btModalCancel").attr("hidden", "hidden");
            //$("#btModalConfirm").removeAttr("hidden");
            $(".modal-danger").switchClass("modal-danger", "modal-success");
            $("#btModalConfirm").switchClass("btn-danger", "btn-success");
            break;

        case 2: //Confirmação Cancelamento (muda de verde para Vermelho)
            //$("#btModalCancel").removeAttr("hidden");
            //$("#btModalConfirm").attr("hidden", "hidden");
            $(".modal-success").switchClass("modal-success", "modal-danger");
            $("#btModalConfirm").switchClass("btn-success", "btn-danger");
            break;
    }

    $("#ConfirmModal").modal("show");

    $("#btModalConfirm").click(function () {
        //Atenção, aqui a confirmação precisa ser '=='.
        //Geralmente o VISUAL STUDIO reclama, mas a sintaxe está correta.
        if (target == null) {
            window.location.href = rota;
        } else {
            target();
        }
    });


}