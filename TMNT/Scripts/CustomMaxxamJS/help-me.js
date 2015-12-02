$(function () {
    var options = $('#help-options');
    var selectedOption = $('#help-options option:selected');
    var notesSection = $('#help-notes');
    var message = "";

    options.on('change', function (e) {
        var selectedOption = $('#help-options option:selected');
        notesSection.text("");
        if (selectedOption.val() === "") {
            message = "";
        } else if (selectedOption.val() === "RSCreate") {
            message = "<div style='height:40px;width:100%;background-color:#428ACA;padding:10px 0 10px 35px;margin-bottom:15px;color:#fff;font-size:16px;'>Reagent and Standard Create Help</div>" +
                "When you go to the Create Reagent or Create Standard page, you will see a form which has mutliple sections to complete. Each section's required field must be completed to advance to the next segment. You may notice it looks very similar to the paper forms you have been using. This is because it's heavily influenced from them!" +
                " Below is an image of the form to create a new Reagent or Standard along with notes to help guide you:" +
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
                    "<li>Click on 'Save' to create the Reagent and be taken back to the Dashboard of Reagents</li>" +
                    "<li>Click on 'Save & New' to create the Reagent, and be given a new clean form to create another Reagent</li>" +
                "</ul>" +
                "We hope you found this helpful! :)";
        } else if (selectedOption.val() === "RSDetails") {
            message = "<div style='height:40px;width:100%;background-color:#428ACA;padding:10px 0 10px 35px;margin-bottom:15px;color:#fff;font-size:16px;'>Reagent and Standard Details Help</div>" +
                "When you go to the Reagent or Standard Details page, you will see a form which has mutliple sections to of information. In order the sections are Location Information, Reagent or Standard Information, Device Used, and All Certificates of Analysis for \" Reagent or Standard ID Code \"" +
                "Below is an image of the Details form of a Reagent or Standard:" +
                "<img src='../../Content/images/help-images/reagent-details.PNG' style='width:100%;border:1px solid #ccc;margin-top:5px;margin-bottom:5px;' alt=''/>" +
                "Some points about the Details Page:" +
                "<ul>" +
                    "<li>Location Information: Contains information abou the location, address, phone number, fax number, department name, and sub department<li>" +
                    "<li>Reagent Information: Everything from Reagent/Standard Lot # to who created the Reagent/Standard is contained in this section<li>" +
                    "<li>Device Used: Will contain information on the device that was used when the Reagent/Standard was created<li>" +
                    "<li>All Certificates of Analysis for \" Reagent or Standard ID Code \": Shows all the CofA's that have been paired to the Reagent/Standard<li>" +
                "</ul>" +
                "We hope you found this helpful! :)";
        } else if (selectedOption.val() === "RSEdit") {
            message = "<div style='height:40px;width:100%;background-color:#428ACA;padding:10px 0 10px 35px;margin-bottom:15px;color:#fff;font-size:16px;'>Reagent and Standard Edit Help</div>" +
                "When you go to the Edit Reagent or Edit Standard page, you will see a form which has mutliple sections that you can edit." +
                "Below is an image of the form to Edit a Reagent or Standard along with notes to help guide you:" +
                "<img src='../../Content/images/help-images/reagent-edit.PNG' style='width:100%;border:1px solid #ccc;margin-top:5px;margin-bottom:5px;' alt=''/>" +
                "Some points about the form:" +
                "<ul>" +
                    "<li>Reagent/Standard Information: In this section you can update the ID code, Lot #, Supplier, Expiry Date, Reagent Name, Grade(Reagent only), Grade Notes (Reagent only)</li>" +
                    "<li>Reagent/Standard Certificate of Analysis and SDS: HEre you can update the CofA and SDS by uploading the new/updated versions</li>" +
                    "<li>Finaly when you have finnished updating the Reagent/Standard you can click save and it will save the changes</li>" +
                "</ul>" +
                "We hope you found this helpful! :)";
        } else if (selectedOption.val() === "RSDashboard") {
            message = "<div style='height:40px;width:100%;background-color:#428ACA;padding:10px 0 10px 35px;margin-bottom:15px;color:#fff;font-size:16px;'>Reagent and Standard Details Help</div>" +
                "When you click on Reagent or Standard page, you will be greeted by a table containing all the current Reagents/Standards that are available. " +
                "Below is an image of the Dashboard for a Reagent/Standard:" +
                "<img src='../../Content/images/help-images/reagent-dashboard.PNG' style='width:100%;border:1px solid #ccc;margin-top:5px;margin-bottom:5px;' alt=''/>" +
                "Some points about the form:" +
                "<ul>" +
                    "<li>Each Reagent/Standard has an Edit and Details option, both have thier own help section. (Click the drop down list and select Edit or Details to view their help pages)</li>" +
                    "<li>The table has mutliple rows that display important details regarding the Reagent/Standard</li>" +
                    "<li>You have the ability to sort by each row in ascending or descending order</li>" +
                    "<li>As the screen size changes the fewer rows are displayed</li>" +
                    "<li>You are able to change the amount of Reagents/Standards shown per page</li>" +
                    "<li>Also, You can search for a specific Reagent/Standard by anyof the Rows shown</li>" +
                "</ul>" +
                "We hope you found this helpful! :)";
        } else {
            message = "Not yet implemented at this time. :("
        }
        notesSection.append(message);
    });
});

