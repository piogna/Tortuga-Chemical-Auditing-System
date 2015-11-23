$(function () {
    var table = $('table'), datatable = "";

    for (var i = 0; i < table.length; i++) {
        if (table[i].id == "myTable") {
            datatable = $('#myTable').dataTable({
                "pageLength": 10,
                "order": [[1, "asc"]],
                "aoColumnDefs": [
                    { 'bSortable': false, 'aTargets':[0] }
                ]
            });
        } else if (table[i].id == "myTable2") {
            datatable = $('#myTable2').dataTable({
                "pageLength": 10,
                "order": [[1, "asc"]],
                "aoColumnDefs": [
                    { 'bSortable': false, 'aTargets': [0] }
                ]
            });
        } else if (table[i].id == "home-data-table") {
            datatable = $('#home-data-table').dataTable({
                "pageLength": 10,
                "order": [1, "asc"],
                "aoColumnDefs": [
                    { 'bSortable': false, 'aTargets': [4] },
                    { 'bSearchable': false, "aTargets": [4] }
                ],
                "aLengthMenu": [[10, 25], [10, 25]],
                "iDisplayLength": 25
            });
        }
    }
});