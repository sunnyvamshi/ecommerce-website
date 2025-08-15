$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblsData').DataTable({
        "ajax": {
            "url": 'Company/GetAll'
        },
        "columns": [
            { "data": "id", "width": "25%" },

            { "data": "name", "width": "25%" },
            { "data": "streetAdress", "width": "15%" },
            { "data": "city", "width": "10%" },
            { "data": "state", "width": "20%" },
            { "data": "postalCode", "width": "20%" },
            
            { "data": "phoneNumber", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return '<div class="btn-group w-75" role="group">' +
                        '<a href="/Company/Editss?Id=' + data + '" class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i> Edit</a>' +
                        '<a href="/Company/Delete?Id=' + data + '" class="btn btn-danger mx-2"><i class="bi bi-trash-fill"></i> Delete</a>' +
                        '</div>';
                },
                "width": "25%"
            }
        ]
    });
}
