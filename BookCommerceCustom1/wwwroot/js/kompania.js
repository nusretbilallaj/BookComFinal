var dataTable;

$(document).ready(function () {
    loadDataTable();
});


                //<th>Emri</th>
                //<th>Rruga</th>
                //<th>Qyteti</th>
                //<th>Shteti</th>
                //<th>KodiPostal</th>
                //<th>NumriTelefonit</th>
                //<th></th>

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Kompania/ListoTeGjitha"
        },
        "columns": [
            { "data": "emri", "width": "15%" },
            { "data": "rruga", "width": "15%" },
            { "data": "qyteti", "width": "15%" },
            { "data": "shteti", "width": "15%" },
            { "data": "kodiPostal", "width": "15%" },
            { "data": "numriTelefonit", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                        <a href="/Kompania/ShtoNdrysho?id=${data}"
                        class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> Edit</a>
                        <a onClick=Fshi('/Kompania/Fshi/${data}')
                        class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> Delete</a>
					</div>
                        `
                },
                "width": "15%"
            }
        ]
    });
}

function Fshi(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.statusi) {
                        dataTable.ajax.reload();
                        toastr.success(data.mesazhi);
                    }
                    else {
                        toastr.error(data.mesazhi);
                    }
                }
            })
        }
    })
}