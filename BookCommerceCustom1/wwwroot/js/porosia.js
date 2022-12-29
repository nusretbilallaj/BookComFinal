var dataTable;

$(document).ready(function () {
    var url = window.location.search;
    if (url.includes("NeProces")) {
        loadDataTable("NeProces");
    }
    else
    {
        if (url.includes("NePritje")) {
            loadDataTable("NePritje");
        }
        else {
            if (url.includes("TeKompletuara")) {
                loadDataTable("TeKompletuara");
            }
            else {
                if (url.includes("TeAprovuara")) {
                    loadDataTable("TeAprovuara");
                }
                else {
                    loadDataTable();
                }
            }
        }
    }
});

function loadDataTable(statusi) {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Porosia/ListoTeGjitha/?statusi="+statusi
        },
        "columns": [
            { "data": "id", "width": "15%" },
            { "data": "emri", "width": "15%" },
            { "data": "numriITelefonit", "width": "15%" },
            { "data": "perdorusi.email", "width": "15%" },
            { "data": "statusiIPorosise", "width": "15%" },
            { "data": "totali", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                        <a href="/Porosia/Detajet?porosiaId=${data}"
                        class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> Detajet</a>
					   </div>
                        `;
                },
                "width": "15%"
            }
        ]
    });
}
