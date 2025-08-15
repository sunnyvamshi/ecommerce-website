$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": 'Order/GetAll'
        },
        "columns": [
            { "data": "id", "width": "25%" },
            { "data": "name", "width": "15%" },
            { "data": "phoneNumber", "width": "10%" },
            { "data": "applicationUser.email", "width": "20%" },
            { "data": "orderTotal", "width": "15%" },
            { "data":"orderStatus", "width":"10%"},
            
            {
                "data": "id",
                "render": function (data) {
                    return '<div class="btn-group w-75" role="group">' +
                        '<a href="/Product/Editss?Id=' + data + '" class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i> Edit</a>' +
                        
                        '</div>';
                },
                "width": "25%"
            }
        ]
    });
}
