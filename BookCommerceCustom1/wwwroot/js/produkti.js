var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Produkti/ListoTeGjitha"
        },
        "columns": [
            { "data": "emri", "width": "15%" },
            { "data": "isbn", "width": "15%" },
            { "data": "cmimi", "width": "15%" },
            { "data": "autori", "width": "15%" },
            { "data": "kategoria.emri", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                        <a href="/Produkti/ShtoNdrysho?id=${data}"
                        class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> Edit</a>
                        <a onClick=Fshi('/Produkti/Fshi/${data}')
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