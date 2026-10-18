$(document).ready(function () {
    //  CNPJ Validation
    var cnpj = $('#cnpj').val().replace(/[^0-9]/g, '');
    if (cnpj.length == 14) {
        $('#errorCnpj').html('');
        $('#iconCnpj').removeClass("text-danger").addClass('text-success');
    } 
    $('#cnpj').keyup(function () {
        var cnpj = $('#cnpj').val().replace(/[^0-9]/g, '');
        if (cnpj.length != 14) {
            $('#iconCnpj').addClass("text-danger");
        } else {
            $('#errorCnpj').html('');
            $('#iconCnpj').removeClass("text-danger").addClass('text-success');
        }
    });
    $('#cnpj').focusin(function () {
        var cnpj = $('#cnpj').val().replace(/[^0-9]/g, '');
        if (cnpj.length != 14) {
            $('#iconCnpj').addClass("text-danger");
        } else {
            $('#errorCnpj').html('');
            $('#iconCnpj').removeClass("text-danger").addClass('text-success');
        }
    });
    $('#cnpj').focusout(function () {
        var cnpj = $('#cnpj').val().replace(/[^0-9]/g, '');
        if (cnpj.length != 14) {
            $('#iconCnpj').addClass("text-danger");
        } else {
            $('#errorCnpj').html('');
            $('#iconCnpj').removeClass("text-danger").addClass('text-success');
        }
    });

    //  LoginName Validation
    var loginName = $('#loginName').val();
    if (loginName.length >= 1) {
        $('#errorLoginName').html('');
        $('#iconLoginName').removeClass("text-danger").addClass('text-success');
    }
    $('#loginName').keyup(function () {
        var loginName = $('#loginName').val();
        if (loginName.length >= 1) {
            $('#errorLoginName').html('');
            $('#iconLoginName').removeClass("text-danger").addClass('text-success');
        } else {
            $('#iconLoginName').addClass("text-danger");
        }
    });
    $('#loginName').focusin(function () {
        var loginName = $('#loginName').val();
        if (loginName.length >= 1) {
            $('#errorLoginName').html('');
            $('#iconLoginName').removeClass("text-danger").addClass('text-success');
        } else {
            $('#iconLoginName').addClass("text-danger");
        }
    });
    $('#loginName').focusout(function () {
        var loginName = $('#loginName').val();
        if (loginName.length >= 1) {
            $('#errorLoginName').html('');
            $('#iconLoginName').removeClass("text-danger").addClass('text-success');
        } else {
            $('#iconLoginName').addClass("text-danger");
        }
    });

    //  LoginPassMD5 Validation
    var loginPass = $('#loginPassMD5').val();
    if (loginPass.length >= 1) {
        $('#errorLoginPass').html('');
        $('#iconLoginPass').removeClass("text-danger").addClass('text-success');
    } 
    $('#loginPassMD5').keyup(function () {
        var loginPass = $('#loginPassMD5').val();
        if (loginPass.length >= 1) {
            $('#errorLoginPass').html('');
            $('#iconLoginPass').removeClass("text-danger").addClass('text-success');
        } else {
            $('#iconLoginPass').addClass("text-danger");
        }
    });
    $('#loginPassMD5').focusin(function () {
        var loginPass = $('#loginPassMD5').val();
        if (loginPass.length >= 1) {
            $('#errorLoginPass').html('');
            $('#iconLoginPass').removeClass("text-danger").addClass('text-success');
        } else {
            $('#iconLoginPass').addClass("text-danger");
        }
    });
    $('#loginPassMD5').focusout(function () {
        var loginName = $('#loginPassMD5').val();
        if (loginName.length >= 1) {
            $('#errorLoginPass').html('');
            $('#iconLoginPass').removeClass("text-danger").addClass('text-success');
        } else {
            $('#iconLoginPass').addClass("text-danger");
        }
    });

    //  Validation Form
    $('#formLogin').submit(function (form) {
        var cnpj = $('#cnpj').val();
        var loginName = $('#loginName').val();
        var loginPass = $('#loginPassMD5').val();
        var error = "";
        var isOK = true;
        if (cnpj === "") {
            $('#errorCnpj').html('<span style="color:red; font-size: 0.76562rem;">Preencha seu CNPJ</span>');
            $('#iconCnpj').addClass("text-danger");
            isOK = false;
        } else {
            $('#errorCnpj').html('');
            $('#iconCnpj').removeClass("text-danger");
        }
        if (loginName === "") {
            $('#errorLoginName').html('<span style="color:red; font-size: 0.76562rem;">Preencha seu usuário</span>');
            $('#iconLoginName').addClass("text-danger");
            isOK = false;
        } else {
            $('#errorLoginName').html('');
        }
        if (loginPass === "") {
            $('#errorLoginPass').html('<span style="color:red; font-size: 0.76562rem;">Preencha sua senha</span>');
            $('#iconLoginPass').addClass("text-danger");
            isOK = false;
        } else {
            $('#errorLoginPass').html('');
        }
        if (!isOK) {
            return false;
        }
    });
});