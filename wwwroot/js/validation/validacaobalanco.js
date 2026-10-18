$(document).ready(function () {
        $('#botaoSalvar').click(function () {
            var descricao = $(':input[name=descricao]').val();
            var data = $(':input[name=data]').val();
            var funcionario = $(':input[name=Funcionario]').val();
            console.log(funcionario);
            var camposNaoPreenchidos = "";
            
            if (data === "") {
                $('#errorData').html('<span style="color:red; font-size: 0.76562rem;">Data é obrigatória.</span>');
                camposNaoPreenchidos += "Data <br/>";
            }
                
            if (descricao === "") {
                $('#errorDescricao').html('<span style="color:red; font-size: 0.76562rem;">Descrição é obrigatória.</span>');
                camposNaoPreenchidos += "Descrição <br/>";
            }

            if (funcionario === "") {
                $('#errorFuncionario').html('<span style="color:red; font-size: 0.76562rem;">Funcionário é obrigatória.</span>');
                camposNaoPreenchidos += "Funcionário <br/>";
            }

            if ($('table tbody tr').html() == undefined)
                camposNaoPreenchidos += "Itens";

            if (camposNaoPreenchidos != "") {
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
                return false;
            }
            else {
                $('#infoModal').modal('toggle');
                return false;
            }
        });
    });