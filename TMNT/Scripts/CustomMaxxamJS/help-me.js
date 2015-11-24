$(function () {
    var options = $('#help-options'), selectedOption = $('#help-options option:selected'), notesSection = $('#help-notes'),
    message = "";

    options.on('change', function (e) {
        var selectedOption = $('#help-options option:selected');
        notesSection.text("");
        if (selectedOption.val() === "") {
            message = "";
        } else if (selectedOption.val() === "RSCreate") {
            message = "<div style='height:40px;width:100%;background-color:#428ACA;padding:10px 0 10px 35px;margin-bottom:15px;color:#fff;font-size:16px;'>Reagent and Standard Create Help</div>" +
                "When you go to the Create Reagent or Create Standard page, you will see a form which has mutliple sections to complete. Each section's required field must be completed to advance to the next segment. You may notice it looks very similar to the paper forms you have been using. This is because it's heavily influenced from them!" +
                "Below is an image of the form to create a new Reagent or Standard along with notes to help guide you:" +
                "<img src='../../Content/images/help-images/reagent-create-part1.PNG' style='width:100%;border:1px solid #ccc;margin-top:5px;margin-bottom:5px;' alt=''/>" +
                "Some points about the form:" +
                "<ul>" +
                    "<li>Any field with a red asterisk is a required field. If it is not completed, the form will not be able to progress</li>" +
                    "<li>Please note the dropdown for 'Select Metric'. This supports both Volume and Weight, from Tera to Atto</li>" +
                    "<li>To return to the list of all Reagents, click on 'Back to List' located at the top-left corner</li>" +
                    "<li>If you ever get lost with where you are in the site, you can always refer to the breadcrumbs located at the top of the image</li>" +
                "</ul>" +
                "Once you have completed the form and clicked on 'Next', you will see the last portion of the form:" +
                "<img src='../../Content/images/help-images/reagent-create-part2.PNG' style='width:100%;border:1px solid #ccc;margin-top:5px;margin-bottom:5px;' alt=''/>" +
                "Here all you have to do is browse for the PDF copies of the Certificate of Analysis and the MSDS. This section also must be completed." +
                "Once completed, you will then be taken to a Summary page:" +
                "<img src='../../Content/images/help-images/reagent-create-part3.PNG'  style='width:100%;border:1px solid #ccc;margin-top:5px;margin-bottom:5px;' alt=''/>" +
                "Some points about the summary:" +
                "<ul>" +
                    "<li>Click on 'Cancel' to return to the previous section of the form</li>" +
                    "<li>Click on 'Save' to create the Reagent and be taken back to the Landing Page of Reagents</li>" +
                    "<li>Click on 'Save & New' to create the Reagent, and be given a new clean form to create another Reagent</li>" +
                "</ul>" +
                "Please note the Print Label checkbox currently is not functional." +
                "We hope you found this helpful! :)";
        } else {
            message = "Not yet implemented at this time. :("
        }
        notesSection.append(message);
    });
});