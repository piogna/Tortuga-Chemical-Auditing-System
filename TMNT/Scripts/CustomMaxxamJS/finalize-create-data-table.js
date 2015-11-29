$(function () {
    //fixing section 1 getting the appropriate classes
    //$('#section-bar-1').addClass('section');

    //filling summary table
    var inputs = $('.input-summary'), table = $('table.summary-table > tbody');

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
                    row += $('#device-selector').val()[z] + " ";
                }
                row += "</td></tr>";
            } else {
                if (inputs[i].attributes.getNamedItem('name').textContent === "MSDSNotes") {
                    row += "<tr>" +//drilling into each element and retrieving the value of each "name" attribute
                    "<td>SDS Notes</td><td>" + inputs[i].value + "</td>" +
                    "</tr>";
                } else {
                    row += "<tr>" +//drilling into each element and retrieving the value of each "name" attribute
                        "<td>" + inputs[i].attributes.getNamedItem('name').textContent.replace(/([A-Z])/g, ' $1').trim() + "</td><td>" + inputs[i].value + "</td>" +
                        "</tr>";
                }
            }
        }
        table.append(row);
    });

    var summaryCofa = $('#summary-cofa'), summaryMsds = $('#summary-msds'), summaryDevices = $('#summary-devices'), inputCofa = $('#CertificateOfAnalysis'),
            inputMsds = $('#MSDS'), inputsPartOne = $('.input-summary-pt1'), inputsPartTwo = $('.input-summary-pt2'), inputDevices = $('#device-selector'),
            summaryInfoPartOne = $('#summary-info-pt1'), summaryInfoPartTwo = $('#summary-info-pt2'),
            summaryAppend;

    $('.btn-review').on('click', function (e) {
        //give Number of Bottles a default value if its value is 0 or empty
        if (!$('#NumberOfBottles').val()) {
            $('#NumberOfBottles').val("1");
        }

        summaryAppend = "";
        summaryInfoPartOne.find("dt").remove();
        summaryInfoPartOne.find("dd").remove();
        summaryInfoPartTwo.find("dt").remove();
        summaryInfoPartTwo.find("dd").remove();
        summaryDevices.find("dt").remove();
        summaryDevices.find("dd").remove();

        summaryCofa.text(inputCofa.val().split("\\").pop());
        summaryMsds.text(inputMsds.val().split("\\").pop());

        $('#device-selector').each(function () {
            if ($(this).val()) {
                summaryAppend += "<dt>Device Code:</dt>" +
                    "<dd>" + $(this).val() + "</dd>";
            }
        });
        summaryDevices.append(summaryAppend);

        //reset for reagent info
        summaryAppend = "";

        inputsPartOne.each(function () {
            if ($(this).val()) {
                summaryAppend += "<dt>" + $(this).attr("name").replace(/([A-Z])/g, ' $1').trim() + ":</dt>" +
                    "<dd>" + $(this).val() + "</dd>";
            }
        });
        summaryInfoPartOne.append(summaryAppend);
        //reset for second column
        summaryAppend = "";

        inputsPartTwo.each(function () {
            if ($(this).val()) {
                if ($(this).attr("id") === "Amount") {
                    //concatenate initial amount with 
                    summaryAppend += "<dt>" + $(this).attr("name").replace(/([A-Z])/g, ' $1').trim() + ":</dt>" +
                        "<dd>" + $(this).val() + " " + $('#Units').val() + "</dd>";
                } else {
                    //no special concatentation required
                    summaryAppend += "<dt>" + $(this).attr("name").replace(/([A-Z])/g, ' $1').trim() + ":</dt>" +
                        "<dd>" + $(this).val() + "</dd>";
                }
            }
        });
        summaryInfoPartTwo.append(summaryAppend);
    });
});