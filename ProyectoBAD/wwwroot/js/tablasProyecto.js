

let tblUsuarios;
document.addEventListener("DOMContentLoaded", function () {
    const language = {
        "decimal": "",
        "emptyTable": "No hay información",
        "info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
        "infoEmpty": "Mostrando 0 to 0 of 0 Entradas",
        "infoFiltered": "(Filtrado de _MAX_ total entradas)",
        "infoPostFix": "",
        "thousands": ",",
        "lengthMenu": "Mostrar _MENU_ Entradas",
        "loadingRecords": "Cargando...",
        "processing": "Procesando...",
        "search": "Buscar:",
        "zeroRecords": "Sin resultados encontrados",
        "paginate": {
            "first": "Primero",
            "last": "Ultimo",
            "next": "Siguiente",
            "previous": "Anterior"
        },
        "buttons": {
            'copy': 'Copy',
            'copyTitle': '<i class="fa-solid fa-copy"></i> Copiado',
            'copySuccess': {
                '1': "Una fila en el porta papeles",
                '_': "%d filas en el porta papeles"
            },
        },
        "select": {
            "rows": {
                _: '%d filas seleccionadas',
                0: 'Seleccione una fila',
                1: 'Una fila seleccionada'
            }
        }

    }
    tblUsuarios = new DataTable('#tblUsuarios', {
        fixedHeader: true,
        responsive: true,
        select: true,
        language,
        dom: "<'row'<'col-sm-12 py-4'B>>" +
            "<'row'<'col-12 col-md-4 col-xl-8'l><'col-12 col-md-8 col-xl-4'f>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-5'i><'col-sm-7'p>>",
    });
})