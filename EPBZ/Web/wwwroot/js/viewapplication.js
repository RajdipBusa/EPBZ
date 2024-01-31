var dTable;
$(document).ready(function () {
    createDataTable();
});

function createDataTable() {
    dTable = $('#tblApplicationData').DataTable({
        order: [],
        columnDefs: [{
            targets: [9],
            orderable: false,
        }],
        fixedColumns: {
            left: 0,
            right: 1
        },
        responsive: true,
        dom: '<t<"mt-3"l>ip>',
    });

}


$('#tblCustomSearchBox').keyup(function () {
    dTable.search($(this).val()).draw();
});

function deleteApplication(aId) {
    Swal.fire({
        title: 'Delete Application',
        html:
            'Are you sure you want to delete this application?',
        showCloseButton: true,
        showCancelButton: true,
        focusConfirm: false,
        confirmButtonText:
            'Delete',
        cancelButtonText:
            'Cancel',
        customClass: {
            confirmButton: 'me-5',
            cancelButton: 'ms-5'
        },
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'get',
                url: '/ApplicationForm/DeleteApplication',
                data: {
                    appId: aId
                },
                success: function (data) {
                    toastr.success("Application deleted successfully.", "Success");
                    setTimeout(function () {
                        window.location.reload();
                    }, 1200);
                },
                error: function (error) {
                    console.log(error.responseText);
                    toastr.error(error.responseText, "Error");
                }
            });
        }
    });
}