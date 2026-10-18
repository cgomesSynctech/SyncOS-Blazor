$(document).ready(function () {
    
    //  Form validation
    $('#botaoSalvar').click(function () {
        var nome_Responsavel = $(':input[name=nome_Responsavel]').val();
        var cPF_Responsavel = $(':input[name=cPF_Responsavel]').val();
        var fone_Reponsavel = $(':input[name=fone_Reponsavel]').val();
        var email_Responsavel = $(':input[name=email_Responsavel]').val();
        var cPF_CNPJ = $(":input[name='favorecido.CPF_CNPJ']").val();
        var nome = $(":input[name='favorecido.Nome']").val();
        var cnae = $("#cnae").val();
        var razao = $(":input[name='favorecido.Razao']").val();
        var cep = $(":input[name='favorecido.CEP']").val();
        var endereco = $(":input[name='favorecido.Endereco']").val();
        var nRO = $(":input[name='favorecido.NRO']").val();
        var cidade = $(":input[name='favorecido.Cidade']").val();
        var bairro = $(":input[name='favorecido.Bairro']").val();
        var cEP_Responsavel = $(':input[name=cEP_Responsavel]').val();
        var endereco_Responsavel = $(':input[name=endereco_Responsavel]').val();
        var cidade_Responsavel = $(':input[name=cidade_Responsavel]').val();
        var bairro_Responsavel = $(':input[name=bairro_Responsavel]').val();
        var numero = $(':input[name=numero_Responsavel]').val();
        var tipoEmpresa = $('#tipoEmpresa').val();
        var uf = $('#estados').val();
        var uf_responsavel = $('#estados_responsavel').val();

        var userLogin = $("#userLogin").val();
        var userPass = $("#userPass").val();
        var userPassConfirm = $("#userPassConfirm").val();
        
        //var fileCertificado = $(':input[name=fileCertificado]').val();
        //var senha = $(':input[name=senha]').val();
        //var cSC = $(':input[name=cSC]').val();
        //var idToken = $(':input[name=idToken]').val();
        //var flexDocs = $(':input[name=flexDocs]').val();

        var camposNaoPreenchidos = "";

        var error = "";
        var abrirModal = true;

        //  Input certificado
        //if (fileCertificado.trim() === "") {
        //    $('#errorFileCertificado').html('<span style="color:red; font-size: 0.76562rem;">Selecione um certificado</span>');
        //    isOK = false;
        //} else {
        //    $('#errorFileCertificado').html('');
        //}

        ////  Input senha
        //if (senha.trim() === "") {
        //    $('#errorSenha').html('<span style="color:red; font-size: 0.76562rem;">Preencha a senha</span>');
        //    isOK = false;
        //} else {
        //    $('#errorSenha').html('');
        //}

        ////  Input cSC
        //if (cSC.trim() === "") {
        //    $('#errorCSC').html('<span style="color:red; font-size: 0.76562rem;">Preencha seu CSC</span>');
        //    isOK = false;
        //} else {
        //    $('#errorCSC').html('');
        //}

        ////  Input idToken
        //if (idToken.trim() === "") {
        //    $('#errorIdToken').html('<span style="color:red; font-size: 0.76562rem;">Preencha seu ID Token</span>');
        //    isOK = false;
        //} else {
        //    $('#errorIdToken').html('');
        //}

        ////  Input flexDocs
        //if (flexDocs.trim() === "") {
        //    $('#errorFlexDocs').html('<span style="color:red; font-size: 0.76562rem;">Preencha sua FlexDocs</span>');
        //    isOK = false;
        //} else {
        //    $('#errorFlexDocs').html('');
        //}

        if (!telaEdicao) {

            //Credenciais de Login
            if (userLogin.trim() === "") {
                camposNaoPreenchidos += "Login <br/>";
                $('#errorUserLogin').html('<span style="color:red; font-size: 0.76562rem;">Informe um login</span>');
                abrirModal = false;
            } else {
                $('#errorUserLogin').html('');
            }

            if (userPass.trim() === "") {
                camposNaoPreenchidos += "Senha <br/>";
                $('#errorUserPass').html('<span style="color:red; font-size: 0.76562rem;">Informe uma senha</span>');
                abrirModal = false;
            } else {
                $('#errorUserPass').html('');
            }

            if (userPassConfirm.trim() === "") {
                camposNaoPreenchidos += "Confirmar Senha <br/>";
                $('#errorUserPassConfirm').html('<span style="color:red; font-size: 0.76562rem;">Confirme sua senha</span>');
                abrirModal = false;
            } else {

                //Compara as senhas...
                if (userPass !== userPassConfirm) {

                    camposNaoPreenchidos += "Senhas não conferem<br/>";
                    $('#errorUserPassConfirm').html('<span style="color:red; font-size: 0.76562rem;">Senhas não conferem!</span>');
                    $('#userPassConfirm').select(); //Foca no controle.
                    abrirModal = false;
                } else {
                    $('#errorUserPassConfirm').html('');
                }

            }

        }

        
        

        //  Input tipoEmpresa
        if (tipoEmpresa === "0") {
            camposNaoPreenchidos += "Tipo Empresa <br/>";
            $('#errorTipoEmpresa').html('<span style="color:red; font-size: 0.76562rem;">Escolha o tipo da sua empresa</span>');
            abrirModal = false;
        } else {
            $('#errorTipoEmpresa').html('');
        }

        //  Input nome
        if (nome_Responsavel.trim() === "") {
            camposNaoPreenchidos += "Nome do Responsável <br/>";
            $('#errorNome_Responsavel').html('<span style="color:red; font-size: 0.76562rem;">Preencha o nome</span>');
            abrirModal = false;
        } else {
            $('#errorNome_Responsavel').html('');
        }

        //  Input cpf/cnpj
        if (cPF_Responsavel.trim() === "") {
            camposNaoPreenchidos += "CPF do Responsável <br/>";
            $('#errorCPF_Responsavel').html('<span style="color:red; font-size: 0.76562rem;">Preencha seu CPF</span>');
            abrirModal = false;
        } else {
            $('#errorCPF_Responsavel').html('');
        }
        if (cPF_CNPJ.trim() === "") {
            camposNaoPreenchidos += "CPF/CNPJ <br/>";
            $('#errorCPF_CNPJ').html('<span style="color:red; font-size: 0.76562rem;">Preencha seu CNPJ</span>');
            abrirModal = false;
        } else {
            $('#errorCPF_CNPJ').html('');
        }

        //  Input nome fantasia
        if (nome.trim() === "") {
            camposNaoPreenchidos += "Nome Fantasia <br/>";
            $('#errorNomeFantasia').html('<span style="color:red; font-size: 0.76562rem;">Preencha o nome fantasia</span>');
            abrirModal = false;
        } else {
            $('#errorNomeFantasia').html('');
        }

        //  Input CNAE
        if (cnae === "0") {
            camposNaoPreenchidos += "CNAE <br/>";
            $('#errorCNAE').html('<span style="color:red; font-size: 0.76562rem;">Selecione uma opção</span>');
            abrirModal = false;
        } else {
            $('#errorCNAE').html('');
        }

        //  Input razao social
        if (razao.trim() === "") {
            camposNaoPreenchidos += "Razão Social <br/>";
            $('#errorRazaoSocial').html('<span style="color:red; font-size: 0.76562rem;">Preencha a razão social</span>');
            abrirModal = false;
        } else {
            $('#errorRazaoSocial').html('');
        }

        //  Input telefone
        if (fone_Reponsavel.trim() === "") {
            camposNaoPreenchidos += "Telefone <br/>";
            $('#errorFone_Reponsavel').html('<span style="color:red; font-size: 0.76562rem;">Preencha o telefone</span>');
            abrirModal = false;
        } else {
            $('#errorFone_Reponsavel').html('');
        }

        //  Input email
        if (email_Responsavel.trim() === "") {
            camposNaoPreenchidos += "E-mail <br/>";
            $('#errorEmail_Responsavel').html('<span style="color:red; font-size: 0.76562rem;">Preencha o email</span>');
            abrirModal = false;
        } else {
            $('#errorEmail_Responsavel').html('');
        }
        //  Email validation
        if (email_Responsavel.includes('@') == false) {
            camposNaoPreenchidos += "E-mail <br/>";
            $('#errorEmail_Responsavel').html('<span style="color:red; font-size: 0.76562rem;">Formate o e-mail (falta o @)</span>');
            abrirModal = false;
        } else {
            $('#errorEmail_Responsavel').html('');
        }
        //  Input cep
        if (cEP_Responsavel.trim() === "") {
            camposNaoPreenchidos += "CEP <br/>";
            $('#errorCEP_Responsavel').html('<span style="color:red; font-size: 0.76562rem;">Preencha o CEP</span>');
            abrirModal = false;
        } else {
            $('#errorCEP_Responsavel').html('');
        }
        if (cep.trim() === "") {
            camposNaoPreenchidos += "CEP do Responsável <br/>";
            $('#errorCEP').html('<span style="color:red; font-size: 0.76562rem;">Preencha o CEP</span>');
            abrirModal = false;
        } else {
            $('#errorCEP').html('');
        }

        //  Input rua
        if (endereco_Responsavel.trim() === "") {
            camposNaoPreenchidos += "Endereço do Responsável <br/>";
            $('#errorEndereco_Responsavel').html('<span style="color:red; font-size: 0.76562rem;">Preencha a Rua</span>');
            abrirModal = false;
        } else {
            $('#errorEndereco_Responsavel').html('');
        }
        if (endereco.trim() === "") {
            camposNaoPreenchidos += "Endereco <br/>";
            $('#errorEndereco').html('<span style="color:red; font-size: 0.76562rem;">Preencha a Rua</span>');
            abrirModal = false;
        } else {
            $('#errorEndereco').html('');
        }

        //  Input cidade
        if (cidade_Responsavel.trim() === "") {
            camposNaoPreenchidos += "Cidade do Responsável <br/>"
            $('#errorCidade_Responsavel').html('<span style="color:red; font-size: 0.76562rem;">Preencha a cidade</span>');
            abrirModal = false;
        } else {
            $('#errorCidade_Responsavel').html('');
        }
        if (cidade.trim() === "") {
            camposNaoPreenchidos += "Cidade <br/>";
            $('#errorCidade').html('<span style="color:red; font-size: 0.76562rem;">Preencha a cidade</span>');
            abrirModal = false;
        } else {
            $('#errorCidade').html('');
        }

        //  Input estado
        if (uf === "XX") {
            camposNaoPreenchidos += "Estado <br/>";
            $('#errorUF').html('<span style="color:red; font-size: 0.76562rem;">Selecione o estado</span>');
            abrirModal = false;
        } else {
            $('#errorUF').html('');
        }
        if (uf_responsavel === "XX") {
            camposNaoPreenchidos += "Estado do Responsável <br/>";
            $('#errorUF_Responsavel').html('<span style="color:red; font-size: 0.76562rem;">Selecione o estado</span>');
            abrirModal = false;
        } else {
            $('#errorUF_Responsavel').html('');
        }

        //  Input bairro
        if (bairro_Responsavel.trim() === "") {
            camposNaoPreenchidos += "Bairro do Responsável <br/>";
            $('#errorBairro_Responsavel').html('<span style="color:red; font-size: 0.76562rem;">Preencha o bairro</span>');
            abrirModal = false;
        } else {
            $('#errorBairro_Responsavel').html('');
        }
        if (bairro.trim() === "") {
            camposNaoPreenchidos += "Bairro <br/>";
            $('#errorBairro').html('<span style="color:red; font-size: 0.76562rem;">Preencha o bairro</span>');
            abrirModal = false;
        } else {
            $('#errorBairro').html('');
        }

        //  Input numero
        if (numero.trim() === "") {
            camposNaoPreenchidos += "Número do Responsável <br/>";
            $('#errorNumero').html('<span style="color:red; font-size: 0.76562rem;">Preencha o número</span>');
            abrirModal = false;
        } else {
            $('#errorNumero').html('');
        }
        if (nRO.trim() === "") {
            camposNaoPreenchidos += "Número <br/>";
            $('#errorNRO').html('<span style="color:red; font-size: 0.76562rem;">Preencha o número</span>');
            abrirModal = false;
        } else {
            $('#errorNRO').html('');
        }

        if (abrirModal) {
            $('#infoModal').modal('toggle');
            return false;
        } else {
            toastr.error(camposNaoPreenchidos, 'Campos obrigatórios: ', {
                closeButton: true,
                progressBar: true,
                debug: false,
                newestOnTop: false,
                positionClass: "toast-top-right",
                preventDuplicates: false,
                onclick: null,
                showDuration: "300",
                hideDuration: "1000",
                timeOut: "5000",
                extendedTimeOut: "1000",
                showEasing: "swing",
                hideEasing: "linear",
                showMethod: "fadeIn",
                hideMethod: "fadeOut",
            });
        }
    });

    //  Permite apenas numeros
    //$('#numero_Responsavel').keyup(function (e) {
    //    if (/\D/g.test(this.value)) {
    //        this.value = this.value.replace(/\D/g, '');
    //    }
    //});
    //$('#nRO').keyup(function (e) {
    //    if (/\D/g.test(this.value)) {
    //        this.value = this.value.replace(/\D/g, '');
    //    }
    //});
    $('#inscricao_Mun').keyup(function (e) {
        if (/\D/g.test(this.value)) {
            this.value = this.value.replace(/\D/g, '');
        }
    });
    $('#inscricao_Est').keyup(function (e) {
        if (/\D/g.test(this.value)) {
            this.value = this.value.replace(/\D/g, '');
        }
    });

    //  Nome Validation
    var nome_Responsavel = $(':input[name=nome_Responsavel]').val();
    if (nome_Responsavel.length >= 1) {
        $('#errorNome_Responsavel').html('');
    }
    $('#nome_Responsavel').keyup(function () {
        var nome_Responsavel = $(':input[name=nome_Responsavel]').val();
        if (nome_Responsavel.length >= 1) {
            $('#errorNome_Responsavel').html('');
        }
    });

    //  CPF/CNPJ Validation
    var cPF_Responsavel = $(':input[name=cPF_Responsavel]').val().replace(/[^0-9]/g, '');
    if (!$('#pessoaFisica').prop('checked')) {
        if (cPF_Responsavel.length == 14) {
            $('#errorCPF_Responsavel').html('');
        }
    } else {
        if (cPF_Responsavel.length == 11) {
            $('#errorCPF_Responsavel').html('');
        }
    }
    $('#cPF_Responsavel').keyup(function () {
        var cPF_Responsavel = $(':input[name=cPF_Responsavel]').val().replace(/[^0-9]/g, '');
        if (!$('#pessoaFisica').prop('checked')) {
            if (cPF_Responsavel.length == 14) {
                $('#errorCPF_Responsavel').html('');
            }
        } else {
            if (cPF_Responsavel.length == 11) {
                $('#errorCPF_Responsavel').html('');
            }
        }
    });

    //  CNPJ Validation
    var cPF_CNPJ = $(":input[name='favorecido.CPF_CNPJ']").val().replace(/[^0-9]/g, '');
    if (cPF_CNPJ.length == 14) {
        $('#errorCPF_CNPJ').html('');
    }
    $('#cPF_CNPJ').keyup(function () {
        var cPF_CNPJ = $(":input[name='favorecido.CPF_CNPJ']").val().replace(/[^0-9]/g, '');
        if (cPF_CNPJ.length == 14) {
            $('#errorCPF_CNPJ').html('');
        }
    });

    //  Nome Fantasia Validation
    var nome = $(":input[name='favorecido.Nome']").val();
    if (nome.length >= 1) {
        $('#errorNomeFantasia').html('');
    }
    $('#nome').keyup(function () {
        var nome = $(":input[name='favorecido.Nome']").val();
        if (nome.length >= 1) {
            $('#errorNomeFantasia').html('');
        }
    });

    //  Razao Social Validation
    var razao = $(":input[name='favorecido.Razao']").val();
    if (razao.length >= 1) {
        $('#errorRazaoSocial').html('');
    }
    $('#razao').keyup(function () {
        var razao = $(":input[name='favorecido.Razao']").val();
        if (razao.length >= 1) {
            $('#errorRazaoSocial').html('');
        }
    });

    //  CEP Empresa Validation
    var cep = $(":input[name='favorecido.CEP']").val().replace(/[^0-9]/g, '');
    if (cep.length == 8) {
        $('#errorCEP').html('');
    }
    $('#cEP').keyup(function () {
        var cep = $(":input[name='favorecido.CEP']").val().replace(/[^0-9]/g, '');
        if (cep.length == 8) {
            $('#errorCEP').html('');
            $('#errorEndereco').html('');
            $('#errorCidade').html('');
            $('#errorBairro').html('');
        }
    });

    //  Rua Empresa Validation
    var endereco = $(":input[name='favorecido.Endereco']").val();
    if (endereco.length >= 1) {
        $('#errorEndereco').html('');
    }
    $('#endereco').keyup(function () {
        var endereco = $(":input[name='favorecido.Endereco']").val();
        if (endereco.length >= 1) {
            $('#errorEndereco').html('');
        }
    });

    //  Cidade Empresa Validation
    var cidade = $(":input[name='favorecido.Cidade']").val();
    if (cidade.length >= 1) {
        $('#errorCidade').html('');
    }
    $('#cidade').keyup(function () {
        var cidade = $(":input[name='favorecido.Cidade']").val();
        if (cidade.length >= 1) {
            $('#errorCidade').html('');
        }
    });

    //  Bairro Empresa Validation
    var bairro = $(":input[name='favorecido.Bairro']").val();
    if (bairro.length >= 1) {
        $('#errorBairro').html('');
    }
    $('#bairro').keyup(function () {
        var bairro = $(":input[name='favorecido.Bairro']").val();
        if (bairro.length >= 1) {
            $('#errorBairro').html('');
        }
    });

    //  Numero Empresa Validation
    var nRO = $(":input[name='favorecido.NRO']").val();
    if (nRO.length >= 1) {
        $('#errorNRO').html('');
    }
    $('#nRO').keyup(function () {
        var nRO = $(":input[name='favorecido.NRO']").val();
        if (nRO.length >= 1) {
            $('#errorNRO').html('');
        }
    });

    //  Telefone Validation
    var fone_Reponsavel = $(':input[name=fone_Reponsavel]').val().replace(/[^0-9]/g, '');
    if (fone_Reponsavel.length == 10) {
        $('#errorFone_Reponsavel').html('');
    }
    $('#fone_Reponsavel').keyup(function () {
        var fone_Reponsavel = $(':input[name=fone_Reponsavel]').val().replace(/[^0-9]/g, '');
        if (fone_Reponsavel.length == 10) {
            $('#errorFone_Reponsavel').html('');
        }
    });

    //  Email Validation
    var email_Responsavel = $(':input[name=email_Responsavel]').val();
    if (email_Responsavel.length >= 1) {
        $('#errorEmail_Responsavel').html('');
    }
    $('#email_Responsavel').keyup(function () {
        var email_Responsavel = $(':input[name=email_Responsavel]').val();
        if (email_Responsavel.length >= 1) {
            $('#errorEmail_Responsavel').html('');
        }
    });

    //  CEP Validation
    var cEP_Responsavel = $(':input[name=cEP_Responsavel]').val().replace(/[^0-9]/g, '');
    if (cEP_Responsavel.length == 8) {
        $('#errorCEP_Responsavel').html('');
    }
    $('#cEP_Responsavel').keyup(function () {
        var cEP_Responsavel = $(':input[name=cEP_Responsavel]').val().replace(/[^0-9]/g, '');
        if (cEP_Responsavel.length == 8) {
            $('#errorCEP_Responsavel').html('');
            $('#errorEndereco_Responsavel').html('');
            $('#errorCidade_Responsavel').html('');
            $('#errorBairro_Responsavel').html('');
        }
    });

    //  Rua Validation
    var endereco_Responsavel = $(':input[name=endereco_Responsavel]').val();
    if (endereco_Responsavel.length >= 1) {
        $('#errorEndereco_Responsavel').html('');
    }
    $('#endereco_Responsavel').keyup(function () {
        var endereco_Responsavel = $(':input[name=endereco_Responsavel]').val();
        if (endereco_Responsavel.length >= 1) {
            $('#errorEndereco_Responsavel').html('');
        }
    });

    //  Numero Validation
    var numero = $(':input[name=numero_Responsavel]').val();
    if (numero.length >= 1) {
        $('#errorNumero').html('');
    }
    $('#numero_Responsavel').keyup(function () {
        var numero = $(':input[name=numero_Responsavel]').val();
        if (numero.length >= 1) {
            $('#errorNumero').html('');
        }
    });

    //  Cidade Validation
    var cidade_Responsavel = $(':input[name=cidade_Responsavel]').val();
    if (cidade_Responsavel.length >= 1) {
        $('#errorCidade_Responsavel').html('');
    }
    $('#cidade_Responsavel').keyup(function () {
        var cidade_Responsavel = $(':input[name=cidade_Responsavel]').val();
        if (cidade_Responsavel.length >= 1) {
            $('#errorCidade_Responsavel').html('');
        }
    });

    //  Bairro Validation
    var bairro_Responsavel = $(':input[name=bairro_Responsavel]').val();
    if (bairro_Responsavel.length >= 1) {
        $('#errorBairro_Responsavel').html('');
    }
    $('#bairro_Responsavel').keyup(function () {
        var bairro_Responsavel = $(':input[name=bairro_Responsavel]').val();
        if (bairro_Responsavel.length >= 1) {
            $('#errorBairro_Responsavel').html('');
        }
    });
});