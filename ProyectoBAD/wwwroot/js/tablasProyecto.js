

let tblUsuarios, tblRoles, tblEncuestas, tblTipoPregunta;
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
            "first": '<i class="fas fa-angle-double-left"></i>',
            "last": '<i class="fas fa-angle-double-right"></i>',
            "next": '<i class="fa fa-solid fa-angle-right"></i>',
            "previous": '<i class="fa fa-solid fa-angle-left"></i>'
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
        language,
        dom: "<'row'<'col-sm-12 py-4 text-right'B>>" +
            "<'row'<'col-12 col-md-4 col-xl-8 text-left'l><'col-12 col-md-8 col-xl-4 text-right'f>>" +
            "<'p-2'>"+
            "<'row'<'col-12'tr>>" +
            "<'p-2'>" +
            "<'row'<'col-sm-5'i><'col-sm-7 d-flex justify-content-end'p>>",
        buttons: [
            {
                text: '<a class="btn btn-dark"><i class="fas fa-user-tag mr-2" ></i> Crear Roles</a>',
                className: "btn-sm btn-dark p-0 mx-2",
                action: function (e, dt, button, config) {
                    var url = $('#urlCreateRol').val();
                    window.location.href = url;
                }
            },
            {
                text: '<a class="btn btn-success"><i class="fas fa-plus-circle mr-2" ></i> Crear Usuario</a>',
                className: "btn-sm btn-success p-0 mx-2",
                action: function (e, dt, button, config) {
                    var url = $('#urlCreateUsuarios').val();
                    window.location.href = url;
                }
            },

        ],
    });
    tblRoles = new DataTable('#tblRoles', {
        fixedHeader: true,
        responsive: true,
        language,
        dom: "<'row'<'col-sm-12 py-4 text-right'B>>" +
            "<'row'<'col-12 col-md-4 col-xl-8 text-left'l><'col-12 col-md-8 col-xl-4 text-right'f>>" +
            "<'p-2'>" +
            "<'row'<'col-12'tr>>" +
            "<'p-2'>" +
            "<'row'<'col-sm-5'i><'col-sm-7 d-flex justify-content-end'p>>",
        buttons: [
            {
                text: '<a class="btn btn-success"><i class="fas fa-plus-circle mr-2" ></i> Crear Rol</a>',
                className: "btn-sm btn-success p-0 mx-2",
                action: function (e, dt, button, config) {
                    var url = $('#urlCreateRol').val();
                    window.location.href = url;
                }
            },

        ],
    });
    tblEncuestas = new DataTable('#tblEncuestas', {
        fixedHeader: true,
        responsive: true,
        language,
        dom: "<'row'<'col-sm-12 py-4 text-right'B>>" +
            "<'row'<'col-12 col-md-4 col-xl-8 text-left'l><'col-12 col-md-8 col-xl-4 text-right'f>>" +
            "<'p-2'>" +
            "<'row'<'col-12'tr>>" +
            "<'p-2'>" +
            "<'row'<'col-sm-5'i><'col-sm-7 d-flex justify-content-end'p>>",
        buttons: [
        ],
    });
    tblTipoPregunta = new DataTable('#tblTipoPregunta', {
        fixedHeader: true,
        responsive: true,
        language,
        dom: "<'row'<'col-sm-12 py-4 text-right'B>>" +
            "<'row'<'col-12 col-md-4 col-xl-8 text-left'l><'col-12 col-md-8 col-xl-4 text-right'f>>" +
            "<'p-2'>" +
            "<'row'<'col-12'tr>>" +
            "<'p-2'>" +
            "<'row'<'col-sm-5'i><'col-sm-7 d-flex justify-content-end'p>>",
        buttons: [
        ],
    });
})