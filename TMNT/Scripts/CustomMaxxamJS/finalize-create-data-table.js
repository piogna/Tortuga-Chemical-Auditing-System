$(function () {
    //fixing section 1 getting the appropriate classes
    //$('#section-bar-1').addClass('section');

    //filling summary table
    var inputs = $('.input-summary');
    var table = $('table.summary-table > tbody');

    $('.btn-review').on('click', function (e) {
        //give Number of Bottles a default value if its value is 0 or empty
        if (!$('#NumberOfBottles').val()) {
            $('#NumberOfBottles').val("1");
        }

        //absolutely prevent duplicate rows
        if ($('table.summary-table > tbody > tr').length > 0) {
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
                if (inputs[i].attributes.getNamedItem('name').textContent === "MSDSNotes") {
                    row += "<tr>" +//drilling into each element and retrieving the value of each "name" attribute
                    "<td>SDS Notes</td><td>" + inputs[i].value + "</td>" +
                    "</tr>";
                //} else if (inputs[i].attributes.getNamedItem('name').textContent === "uploadMSDS") {
                //    row += "<tr>" +//drilling into each element and retrieving the value of each "name" attribute
                //    "<td>SDS File Name</td><td>" + inputs[i].value + "</td>" +
                //    "</tr>";
                } else {
                    row += "<tr>" +//drilling into each element and retrieving the value of each "name" attribute
                        "<td>" + inputs[i].attributes.getNamedItem('name').textContent.replace(/([A-Z])/g, ' $1').trim() + "</td><td>" + inputs[i].value + "</td>" +
                        "</tr>";
                }
            }
        }
        table.append(row);
    });
});