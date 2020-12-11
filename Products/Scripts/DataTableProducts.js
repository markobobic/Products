$(document).ready(function () {
    dataTable = $("#productsTable").DataTable({
        "processing": true,
        "ajax": {
            "url": "/Product/GetData/",
            "type": "GET",
            "datatype": "json",
            "dataSrc": ""
        },
        "columns": [
            { 'data': 'id', defaultContent: '' },
            { "data": "Name", "searchable": false },
            { "data": "CategoryName", "searchable": false },
            { "data": "ManufacturerName", "searchable": false },
            { "data": "SupplierName", "searchable": false },
            { "data": "Price", "searchable": false },
            { "data": "Description", "searchable": false },
            {
                "data": "Id", "width": "22%", sortable: false, render: function (data) {

                    return "<a class='btn btn-outline-info btn-block btn-sm'" +
                        "data-toggle='modal' data-target='#modalAdd'onclick=PopupForm('/Product/Update/" +
                            data + "')><i class='far fa-edit'></i>Update</a>";
                }
            }
        ],
        "lengthMenu": [[10, 50, 100, 200, 300], [10, 50, 100, 200, 300]
        ],
        "responsive": true,
        "dom": 'Bfrtlip',
        initComplete: function () {
            let btns = $('.paginate_button');
            btns.addClass('btn btn-info btn-sm');
            btns.removeClass('paginate_button');
            let searchButton = $("input[type='search']");
            searchButton.addClass('form-control');
            searchButton.attr("placeholder", "Search..");
        },
        "columnDefs": [
            {
                "searchable": false,
                "orderable": false,
                "targets": 0
            },
        ],
        "language": {
            search: "",
            "emptyTable": "No data found"
        },
        "responsive": "true"
    });

    dataTable.on('order.dt search.dt', function () {
        dataTable.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();

    dataTable.columns().every(function () {
        var datatableColumn = this;

        $(this.footer()).find('input').on('keyup change', function () {
            datatableColumn.search(this.value).draw();
        });
    })
});