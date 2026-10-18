$(document).ready(function () {
    $('#botaoSalvar').click(function () {
        var codigo = $(':input[name=codigo]').val();
        var nome = $(':input[name=nome]').val();
        var razaoSocial = $(':input[name=razao]').val();
        var cpfCnpj = $(':input[name=cpf_cnpj]').val();
        var pessoa = $(':input[name=pessoa]').val();
        //var inscEstadual = $(':input[name=inscricao_Est]').val();
        var dataCadastro = $(':input[name=dataCadastro]').val();
        var credito = $(':input[name=credito]').val();
        var cep = $(':input[name=cep]').val();
        var pais = $('#comboboxpaises').val();
        var rua = $(':input[name=endereco]').val();
        var bairro = $(':input[name=bairro]').val();
        var cidade = $(':input[name=cidade]').val();
        var estado = $(':input[name=UF]').val();
        var numero = $(':input[name=numero]').val();
        var contato = $(':input[name=contato]').val();
        var ibge = $(':input[name=ibge]').val();
        //var fone1 = $(':input[name=fone1]').val();
        var fax = $(':input[name=fax]').val();
        var email = $(':input[name=email]').val();
        var site = $(':input[name=site]').val();
        var tipoForn = $(':input[name=tipoForn]').val();
        var limiteCredito = $(':input[name=limiteCredito]').val();
        var atividade = $(':input[name=atividade]').val();
        var planoPagamento = $(':input[name=planoPagamento]').val();
        var contaPagamento = $(':input[name=contaPagamento]').val();
        var tipoEntrega = $(':input[name=tipoEntrega]').val();
        var observacao = $(':input[name=observacao]').val();
        var contaDespesa = $('#contaDespesa').val();
        var tempoMedioEntrega = $(':input[name=tempoMedioEntrega]').val();

        var cargo = $('#comboboxcargos').val();
        var tipoFunc = $('#comboboxtipofunc').val();
        var camposNaoPreenchidos = "";
        var error = "";
        var abrirModal = true;


        var FaltaEndereco = false;
        var FaltaDadosPessoais = false;
        var FaltaDadosPets = false;
        var FaltaCaracteristicas = false;


        if (codigo === "") {
            camposNaoPreenchidos += "Código <br/>";
            $('#errorCodigo').html('<span style="color:red; font-size: 0.76562rem;">Código é obrigatório.</span>');
            abrirModal = false;
        } else {
            $('#errorCodigo').html('');
        }



        //DADOS PESSOAIS OBRIGATORIOS
        if (nome === "") {
            camposNaoPreenchidos += "Nome <br/>";
            $('#errorNome').html('<span style="color:red; font-size: 0.76562rem;">Nome é obrigatório.</span>');
            abrirModal = false;

            FaltaDadosPessoais = true;

        } else {
            $('#errorNome').html('');
        }

        //verifica se o radioCnpj está marcado
        var radioCnpj = document.getElementById("radioCnpj").checked;        
        if (razaoSocial === "" && radioCnpj === true) {
            camposNaoPreenchidos += "Razão Social <br/>";
            $('#errorRazaoSocial').html('<span style="color:red; font-size: 0.76562rem;">Razão social é obrigatória.</span>');
            abrirModal = false;
        } else {
            $('#errorRazaoSocial').html('');
        }


        if (cpfCnpj === "") {
            camposNaoPreenchidos += "CPF ou CNPJ <br/>";
            $('#errorCpfCnpj').html('<span style="color:red; font-size: 0.76562rem;"> CPF/CNPJ é obrigatório.</span>');
            abrirModal = false;
            
            FaltaDadosPessoais = true;

        } else {
            $('#errorCpfCnpj').html('');
        }


        if (pessoa === "") {

            $('#errorPessoa').html('<span style="color:red; font-size: 0.76562rem;">Pessoa é obrigatória.</span>');
            abrirModal = false;

            FaltaDadosPessoais = true;

        } else {
            $('#errorPessoa').html('');
        }


        //if (inscEstadual === "") {
        //    camposNaoPreenchidos += "Inscrição Estadual <br/>";
        //    $('#errorInscEstadual').html('<span style="color:red; font-size: 0.76562rem;">Inscrição estadual é obrigatória.</span>');
        //    abrirModal = false;
        //} else {
        //    $('#errorInscEstadual').html('');
        //}
        if (dataCadastro === "") {
            $('#errorDataCadastro').html('<span style="color:red; font-size: 0.76562rem;">Data cadastro é obrigatória.</span>');
            abrirModal = false;
        } else {
            $('#errorDataCadastro').html('');
        }
        if (credito === "") {
            $('#errorCredito').html('<span style="color:red; font-size: 0.76562rem;">Crédito é obrigatório.</span>');
            abrirModal = false;
        } else {
            $('#errorCredito').html('');
        }

        //DADOS DE ENDERECO
        if (cep === "") {
            camposNaoPreenchidos += "CEP <br/>";
            document.getElementById("cep").focus();
            $('#errorCep').html('<span style="color:red; font-size: 0.76562rem;">CEP é obrigatório.</span>');
            abrirModal = false;

            FaltaEndereco = true;

        } else {
            $('#errorCep').html('');
        }


        if (pais === "") {
            camposNaoPreenchidos += "País <br/>";
            $('#errorPais').html('<span style="color:red; font-size: 0.76562rem;">País é obrigatório.</span>');
            abrirModal = false;
        } else {
            $('#errorPais').html('');
        }


        if (rua === "") {
            camposNaoPreenchidos += "Rua <br/>";
            $('#errorRua').html('<span style="color:red; font-size: 0.76562rem;">Rua é obrigatória.</span>');
            abrirModal = false;

            FaltaEndereco = true;

        } else {
            $('#errorRua').html('');
        }


        if (bairro === "") {
            camposNaoPreenchidos += "Bairro <br/>";
            $('#errorBairro').html('<span style="color:red; font-size: 0.76562rem;">Bairro é obrigatório.</span>');
            abrirModal = false;

            FaltaEndereco = true;

        } else {
            $('#errorBairro').html('');
        }


        if (cidade === "") {
            camposNaoPreenchidos += "Cidade <br/>";
            $('#errorCidade').html('<span style="color:red; font-size: 0.76562rem;">Cidade é obrigatória.</span>');
            abrirModal = false;

            FaltaEndereco = true;

        } else {
            $('#errorCidade').html('');
        }


        if (estado === "XX") {
            $('#errorEstado').html('<span style="color:red; font-size: 0.76562rem;">Selecione um Estado.</span>');
            abrirModal = false;

            FaltaEndereco = true;

        } else {
            $('#errorEstado').html('');
        }



        //OUTROS DADOS

        if (ibge === "") {
            camposNaoPreenchidos += "IBGE <br/>";
            $('#errorIbge').html('<span style="color:red; font-size: 0.76562rem;">IBGE é obrigatória.</span>');
            abrirModal = false;
        } else {
            $('#errorIbge').html('');
        }

        //Aba CARACTERISTICAS
        if (limiteCredito === "") {
            camposNaoPreenchidos += "Limite de Crédito <br/>";
            $('#errorLimiteCredito').html('<span style="color:red; font-size: 0.76562rem;">Limite Crédito é obrigatório.</span>');

            FaltaCaracteristicas = true;
            abrirModal = false;
        } else {
            $('#errorLimiteCredito').html('');
        }


        if (atividade === "") {
            $('#errorAtividade').html('<span style="color:red; font-size: 0.76562rem;">Atividade é obrigatória.</span>');
            abrirModal = false;
        } else {
            $('#errorAtividade').html('');
        }
        if (planoPagamento === "") {
            $('#errorPlanoPagamento').html('<span style="color:red; font-size: 0.76562rem;">Plano pagamento é obrigatório.</span>');
            abrirModal = false;
        } else {
            $('#errorPlanoPagamento').html('');
        }
        if (contaPagamento === "") {
            $('#errorContaPagamento').html('<span style="color:red; font-size: 0.76562rem;">Conta pagamento é obrigatória.</span>');
            abrirModal = false;
        } else {
            $('#errorContaPagamento').html('');
        }
        if (tipoEntrega === "") {
            $('#errorTipoEntrega').html('<span style="color:red; font-size: 0.76562rem;">Tipo entrega é obrigatória.</span>');
            abrirModal = false;
        } else {
            $('#errorTipoEntrega').html('');
        }
        if (observacao === "") {
            $('#errorObservacao').html('<span style="color:red; font-size: 0.76562rem;">Observação é obrigatória.</span>');
            abrirModal = false;
        } else {
            $('#errorObservacao').html('');
        }
        if (contaDespesa == 0) {
            camposNaoPreenchidos += "Conta Despesa <br/>";
            $('#errorContaDespesa').html('<span style="color:red; font-size: 0.76562rem;">Conta despesa é obrigatória.</span>');
            abrirModal = false;

            FaltaCaracteristicas = true;
        } else {
            $('#errorContaDespesa').html('');
        }
        if (tempoMedioEntrega === "") {
            $('#errorTempoMedioEntrega').html('<span style="color:red; font-size: 0.76562rem;">Campo obrigatório.</span>');
            abrirModal = false;
        } else {
            $('#errorTempoMedioEntrega').html('');
        }

        //TELA DE FUNCIONARIOS (obrigatorio)
        if (cargo === "0") {
            camposNaoPreenchidos += "Cargo <br/>";
            $('#errorCargo').html('<span style="color:red; font-size: 0.76562rem;">Campo obrigatório.</span>');
            abrirModal = false;

            FaltaDadosPessoais = true;
        } else {
            $('#errorCargo').html('');
        }

        if (tipoFunc === "0") {
            camposNaoPreenchidos += "Tipo Funcionário <br/>";
            $('#errorTipoFunc').html('<span style="color:red; font-size: 0.76562rem;">Campo obrigatório.</span>');
            abrirModal = false;

            FaltaDadosPessoais = true;
        } else {
            $('#errorTipoFunc').html('');
        }



        //ABAS PRINCIPAIS (FORM)
        //Checa se falta campos das respectivas abas...
        var abasPrincipais = document.getElementById("abaPrincipal");
        var dadosTxt = abasPrincipais.getElementsByTagName("li")[0].getElementsByTagName("a")[0];
        var enderecoTxt = abasPrincipais.getElementsByTagName("li")[1].getElementsByTagName("a")[0];

        //Em alguns formularios temos 3 abas.
        var Tem3Abas = false;
        if (abasPrincipais.getElementsByTagName("li")[2] != null) {
            Tem3Abas = true;
            var terceiraAbaTxt = abasPrincipais.getElementsByTagName("li")[2].getElementsByTagName("a")[0];
        }


        if (FaltaDadosPessoais) {
            dadosTxt.setAttribute("style", "color: red");
        } else {
            dadosTxt.setAttribute("style", "color: black");
        }

        if (FaltaEndereco) {
            enderecoTxt.setAttribute("style", "color: red");
        } else {
            enderecoTxt.setAttribute("style", "color: black");
        }

        //3º aba ocasional
        if (Tem3Abas) {
            if (FaltaDadosPets || FaltaCaracteristicas) {
                terceiraAbaTxt.setAttribute("style", "color: red");
            } else {
                terceiraAbaTxt.setAttribute("style", "color: black");
            }
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


    //CEP PROCESSING
    //  Receber CEP e preencher campos do endereço
    $('#cep').keyup(function () {

        $('#errorCep').html("");
        var cep = $("#cep").val().replace(/[^0-9]/g, '');

        if (cep.length == 8) {

            //NOVO GET (CEP), com $.ajax, é possivel ter controle sobre os eventos (sucesso e erro)
            $.ajax({
                url: "http://api.postmon.com.br/v1/cep/" + cep,
                type: 'GET',
                success: function (data, status) {

                    $('#bairro').val(data.bairro);
                    $('#cidade').val(data.cidade);
                    $('#UF').val(data.estado);
                    $('#municipio').val(data.cidade_info.codigo_ibge);
                    if (data.endereco != null) {
                        $('#endereco').val(data.endereco);
                    }
                    if (data.logradouro != null) {
                        $('#endereco').val(data.logradouro);
                    }

                    if (data != null) {
                        $('#errorCep').html("");
                        $("#nRO").focus();
                    }
                },

                error: function () {
                    $('#errorCep').html('<span style="color:red; font-size: 0.76562rem;">CEP não encontrado.</span>');
                }
            });

        } else {
            $('#bairro').val("");
            $('#cidade').val("");
            $('#UF').val("");
            $('#municipio').val("");
            $('#endereco').val("");
            $('#errorCep').html('<span style="color:red; font-size: 0.76562rem;">Informe um CEP <small>(8 digitos)</small></span>');
        }
    });


});