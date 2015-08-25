﻿$(function () {
    var table = $('table');
    for (var i = 0; i < table.length; i++) {
        if (table[i].id == "myTable") {
            $('#myTable').dataTable({
                "pageLength": 20,
                "order": [[1, "asc"]],
                "aoColumnDefs": [
                    { 'bSortable': false, 'aTargets':[0] }
                ]
            });
        } else if (table[i].id == "myTable2") {
            $('#myTable2').dataTable({
                "pageLength": 20,
                "order": [[1, "asc"]],
                "aoColumnDefs": [
                    { 'bSortable': false, 'aTargets': [0] }
                ]
            });
        }
    }
});