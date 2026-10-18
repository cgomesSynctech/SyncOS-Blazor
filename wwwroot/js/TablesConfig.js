$(document).ready(function () {
    var table = $('#DATATABLE').DataTable({
        ordering: true,
        paging: true,
        filter: true, // habilita o filtro(search box)

        language: {
            "sEmptyTable": "Sem registros.",
            "sInfo": "Mostrando de <b>_START_</b> a <b>_END_</b>   <br/><sup>(Total de <b>_TOTAL_</b> registros)</sup>",
            "sInfoEmpty": "<sup>Mostrando 0 registros.</sup>",
            "sInfoFiltered": "<sup>(Filtrados de _MAX_ registros)</sup>",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "Listar _MENU_",
            "sLoadingRecords": "Carregando...",
            "sProcessing": "Processando...",
            "sZeroRecords": "Sem registros.",
            "sSearch": "<i class='fa fa-search'></i>",
            "oPaginate": {
                "sNext": "Próximo",
                "sPrevious": "Anterior",
                "sFirst": "|<<",
                "sLast": ">>|"
            },
            "oAria": {
                "sSortAscending": ": Ordenar colunas de forma ascendente",
                "sSortDescending": ": Ordenar colunas de forma descendente"
            }
        }
    });

    $("#DATATABLE_wrapper>.row:first>div").attr("class", "col-6");

});