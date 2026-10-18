$(document).ready(function () {
    //  LoginName Validation
    var loginName = $(':input[name=loginName]').val();
    if (loginName.trim().length >= 1) {
        $('#errorLoginName').html('');
    }
    $('#loginName').keyup(function () {
        var loginName = $(':input[name=loginName]').val();
        if (loginName.trim().length >= 1) {
            $('#errorLoginName').html('');
        }
    });
    $('#loginName').focusin(function () {
        var loginName = $(':input[name=loginName]').val();
        if (loginName.trim().length >= 1) {
            $('#errorLoginName').html('');
        } 
    });
    $('#loginName').focusout(function () {
        var loginName = $(':input[name=loginName]').val();
        if (loginName.trim().length >= 1) {
            $('#errorLoginName').html('');
        } 
    });

    //  LoginPassMD5 Validation
    var loginPass = $(':input[name=loginPassMD5]').val();
    if (loginPass.length >= 1) {
        $('#errorLoginPassMD5').html('');
    }
    $('#loginPassMD5').keyup(function () {
        var loginPass = $(':input[name=loginPassMD5]').val();
        if (loginPass.length >= 1) {
            $('#errorLoginPassMD5').html('');
        }
    });
    $('#loginPassMD5').focusin(function () {
        var loginPass = $(':input[name=loginPassMD5]').val();
        if (loginPass.length >= 1) {
            $('#errorLoginPassMD5').html('');
        }
    });
    $('#loginPassMD5').focusout(function () {
        var loginName = $(':input[name=loginPassMD5]').val();
        if (loginName.length >= 1) {
            $('#errorLoginPassMD5').html('');
        }
    });

    //  LoginPassMD5Confirm Validation
    var loginPassConfirm = $(':input[name=loginPassMD5Confirm]');
    if (loginPassConfirm.length >= 1) {
        $('#errorLoginPassMD5Confirm').html('');
    }
    $('#loginPassMD5Confirm').keyup(function () {
        var loginPassConfirm = $(':input[name=loginPassMD5Confirm]').val();
        if (loginPassConfirm.length >= 1) {
            $('#errorLoginPassMD5Confirm').html('');
        }
    });
    $('#loginPassMD5Confirm').focusin(function () {
        var loginPassConfirm = $(':input[name=loginPassMD5Confirm]').val();
        if (loginPassConfirm.length >= 1) {
            $('#errorLoginPassMD5Confirm').html('');
        }
    });
    $('#loginPassMD5Confirm').focusout(function () {
        var loginPassConfirm = $(':input[name=loginPassMD5Confirm]').val();
        if (loginPassConfirm.length >= 1) {
            $('#errorLoginPassMD5Confirm').html('');
        }
    });

    //  DescontoMaximo Validation
    var descontoMaximo = $(':input[name=descontoMaximo]').val();
    if (descontoMaximo.length >= 1) {
        $('#errorDescontoMaximo').html('');
    }
    $('#descontoMaximo').keyup(function () {
        var descontoMaximo = $(':input[name=descontoMaximo]').val();
        if (loginPassConfirm.length >= 1) {
            $('#errorDescontoMaximo').html('');
        }
    });
    $('#descontoMaximo').focusin(function () {
        var descontoMaximo = $(':input[name=descontoMaximo]').val();
        if (loginPassConfirm.length >= 1) {
            $('#errorDescontoMaximo').html('');
        }
    });
    $('#descontoMaximo').focusout(function () {
        var descontoMaximo = $(':input[name=descontoMaximo]').val();
        if (loginPassConfirm.length >= 1) {
            $('#errorDescontoMaximo').html('');
        }
    });

    //  Form onSubmit
    $('#botaoSalvar').click(function (form) {
        var loginName = $(':input[name=loginName]').val();
        var loginPass = $(':input[name=loginPassMD5]').val();
        var loginPassConfirm = $(':input[name=loginPassMD5Confirm]').val();
        var descontoMaximo = $(':input[name=descontoMaximo]').val();
        var funcionario = $('#funcionario').val();
        var perfil = $('#perfil').val();
        var error = "";
        var isOK = true;

        if (loginName.trim() === "") {
            $('#errorLoginName').html('<span style="color:red; font-size: 0.76562rem;">Preencha seu usuário</span>');
            isOK = false;
        } else {
            $('#errorLoginName').html('');
        }

        if (loginPass.trim() === "") {
            $('#errorLoginPassMD5').html('<span style="color:red; font-size: 0.76562rem;">Preencha sua senha</span>');
            isOK = false;
        } else {
            $('#errorLoginPassMD5').html('');
        }

        if (loginPassConfirm.trim() === "") {
            $('#errorLoginPassMD5Confirm').html('<span style="color:red; font-size: 0.76562rem;">Confirme sua senha</span>');
            isOK = false;
        } else {
            $('#errorLoginPassMD5Confirm').html('');
        }

        if (descontoMaximo.trim() === "") {
            $('#errorDescontoMaximo').html('<span style="color:red; font-size: 0.76562rem;">Preencha o valor</span>');
            isOK = false;
        } else {
            $('#errorDescontoMaximo').html('');
        }

        if (funcionario === "0") {
            $('#errorFuncionario').html('<span style="color:red; font-size: 0.76562rem;">Selecione um funcionário</span>');
            isOK = false;
        } else {
            $('#errorFuncionario').html('');
        }

        if (perfil === "0") {
            $('#errorPerfil').html('<span style="color:red; font-size: 0.76562rem;">Selecione um perfil</span>');
            isOK = false;
        } else {
            $('#errorPerfil').html('');
        }

        if (loginPass.length > 1 && loginPassConfirm.length > 1) {
            if (loginPass.trim() != loginPassConfirm.trim()) {
                $('#errorLoginPassMD5').html('<span style="color:red; font-size: 0.76562rem;">As senhas não são iguais</span>');
                isOK = false;
            } else {
                $('#errorLoginPassMD5').html('');
            }
        }
        
        if (isOK) {
            $('#infoModal').modal('toggle');
            return false;
        }
    });
});