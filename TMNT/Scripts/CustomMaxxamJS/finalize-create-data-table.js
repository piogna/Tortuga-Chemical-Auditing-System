/*
    Finalizing All Create Forms
    Created By: Team Tortuga
*/

$(function () {
    var summaryCofa = $('#summary-cofa'), summaryMsds = $('#summary-msds'), summarySafetyNotes = $('#summary-safety-notes'), summaryDevices = $('#summary-devices'),
        inputCofa = $('#CertificateOfAnalysis'), inputMsds = $('#MSDS'), inputSafetyNotes = $('#SafetyNotes'), inputMsdsNotes = $('#MSDSNotes'), inputsPartOne = $('.input-summary-pt1'),
        inputsPartTwo = $('.input-summary-pt2'), inputDevices = $('#device-selector'), summaryInfoPartOne = $('#summary-info-pt1'), summaryInfoPartTwo = $('#summary-info-pt2'),
        numberOfBottles = $('#NumberOfBottles'), summaryAppend;

    $('.btn-review').on('click', function () {
        //reset the summary page
        summaryAppend = "";
        summaryInfoPartOne.find("dt").remove();
        summaryInfoPartOne.find("dd").remove();
        summaryInfoPartTwo.find("dt").remove();
        summaryInfoPartTwo.find("dd").remove();
        summaryDevices.find("dt").remove();
        summaryDevices.find("dd").remove();

        //give Number of Bottles a default value if its value is 0 or empty
        if (!numberOfBottles.val()) {
            numberOfBottles.val("1");
        }

        //setting the file names
        if (inputCofa.val() && inputMsds.val()) {
            summaryCofa.text(inputCofa.val().split("\\").pop());
            summaryMsds.text(inputMsds.val().split("\\").pop());
        }

        //setting safety notes
        if (inputMsdsNotes.val()) {
            //reagent or standard
            summarySafetyNotes.text(inputMsdsNotes.val());
        } else {
            //intermediate or working standard
            summarySafetyNotes.text(inputSafetyNotes.val());
        }

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
                if ($(this).attr("id") === "TotalAmount") {
                    //concatenate initial amount with 
                    summaryAppend += "<dt>" + $(this).attr("name").replace(/([A-Z])/g, ' $1').trim() + ":</dt>" +
                        "<dd>" + $(this).val() + " " + $('#Units').val() + "</dd>";
                } else {
                    summaryAppend += "<dt>" + $(this).attr("name").replace(/([A-Z])/g, ' $1').trim() + ":</dt>" +
                        "<dd>" + $(this).val() + "</dd>";
                }
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
                } else if ($(this).attr("id") === "FinalConcentration" || $(this).attr("id") === "Concentration") {
                    summaryAppend += "<dt>" + $(this).attr("name").replace(/([A-Z])/g, ' $1').trim() + ":</dt>" +
                        "<dd>" + $(this).val() + " " + $('#ConcentrationUnits').val() + "</dd>";
                } else {
                    //no special concatentation required
                    summaryAppend += "<dt>" + $(this).attr("name").replace(/([A-Z])/g, ' $1').trim() + ":</dt>" +
                        "<dd>" + $(this).val() + "</dd>";
                }
            }
        });
        summaryInfoPartTwo.append(summaryAppend);
    });

    /* special onclick handler for creating a balance */

    var locationNames = $('#location-names'), departmentNames = $('#department-names'), subDepartmentNames = $('#SubDepartmentName'),
        summaryLocation = $('#summary-location-name'), summaryDepartment = $('#summary-department-name'),
        summarySubDepartment = $('#summary-sub-department');

    $('#btn-create-balance').on("click", function () {
        summaryAppend = "";
        summaryInfoPartOne.find("dt").remove();
        summaryInfoPartOne.find("dd").remove();
        summaryInfoPartTwo.find("dt").remove();
        summaryInfoPartTwo.find("dd").remove();

        summaryLocation.text(locationNames.first().val());
        summaryDepartment.text(departmentNames.first().val());
        summarySubDepartment.text(subDepartmentNames.first().val())

        inputsPartOne.each(function () {
            if ($(this).val()) {
                if ($(this).attr("id") === "NumberOfDecimals") {
                    summaryAppend += "<dt>" + $(this).attr("name").replace(/([A-Z])/g, ' $1').trim() + ":</dt>" +
                            "<dd>" + $(this).val() + " (" + decimalExample(parseFloat($(this).val())) + ")</dd>";
                } else {
                    summaryAppend += "<dt>" + $(this).attr("name").replace(/([A-Z])/g, ' $1').trim() + ":</dt>" +
                            "<dd>" + $(this).val() + "</dd>";
                }
            }
        });

        summaryInfoPartOne.append(summaryAppend);
        //reset for second column
        summaryAppend = "";

        inputsPartTwo.each(function () {
            if ($(this).val()) {
                summaryAppend += "<dt>" + $(this).attr("name").replace(/([A-Z])/g, ' $1').trim() + ":</dt>" +
                        "<dd>" + $(this).val() + "</dd>";
            }
        });
        summaryInfoPartTwo.append(summaryAppend);
    });

    function decimalExample(value) {
        var text = "##.";
        for (var i = 0; i < value; i++) {
            text += "#";
        }
        return text;
    }
});