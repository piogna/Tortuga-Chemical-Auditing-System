$(function () {
    //fixing section 1 getting the appropriate classes
    $('#section-bar-1').addClass('section');

    //filling summary table
    var inputs = $('.input-summary');
    var table = $('table.table-bordered > tbody');

    $('.btn-review').on('click', function (e) {
        //absolutely prevent duplicate rows
        if ($('table.table-bordered > tbody > tr').length > 0) {
            table.find("tr").remove();
        }
        var row = "";
        for (var i = 0; i < inputs.length; i++) {
            if (inputs[i].attributes.getNamedItem('name').textContent === "Devices") {
                row += "<tr>" +
                     "<td>" + inputs[i].attributes.getNamedItem('name').textContent.replace(/([A-Z])/g, ' $1').trim() + "</td><td>";
                for (var z = 0; z < $('#device-selector').val().length; z++) {
                    //console.log($('#device-selector').val()[z]);
                    row += $('#device-selector').val()[z] + " ";
                }
                row += "</td></tr>";
            } else {
                row += "<tr>" +//drilling into each element and retrieving the value of each "name" attribute
                    "<td>" + inputs[i].attributes.getNamedItem('name').textContent.replace(/([A-Z])/g, ' $1').trim() + "</td><td>" + inputs[i].value + "</td>" +
                    "</tr>";
            }
        }
        table.append(row);
    });
});