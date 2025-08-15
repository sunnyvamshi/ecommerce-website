$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    $('#tblsData').DataTable({
        "ajax": {
            "url": "/User/GetAll",
            "type": "GET",
            "datatype": "json",
            "error": function (xhr, error, thrown) {
                console.error("Error in DataTable AJAX call: ", error);
                alert("Failed to load data. Please try again later.");
            }
        },
        "columns": [
            { "data": "id", "width": "10%" },
            { "data": "company.name", "width": "20%" },
            { "data": "email", "width": "20%" },
            { "data": "phoneNumber", "width": "15%" },
            { "data": "role", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return '<div class="btn-group w-75" role="group">' +
                        '<a href="/User/Edit?Id=' + data + '" class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i> Edit</a>' +
                        '</div>';
                },
                "width": "20%"
            }
        ]
    });
}
