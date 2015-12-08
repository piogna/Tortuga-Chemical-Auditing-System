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
                    "<li>To return to the list of all Reagents/Standards, click on 'Back to List' located at the top-left corner. Please note that you will lose the data that you have filled out currently</li>" +
                    "<li>If you ever get lost with where you are in the site, you can always refer to the stage indicator just below the \"Back to List\" button</li>" +
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
                "We hope you found this helpful! :)";
        } else if (selectedOption.val() === "RSDetails") {
            message = "<div style='height:40px;width:100%;background-color:#428ACA;padding:10px 0 10px 35px;margin-bottom:15px;color:#fff;font-size:16px;'>Reagent and Standard Details Help</div>" +
                "When you go to the Reagent or Standard Details page, you will see a form which has mutliple sections to of information. In order the sections are Location Information, Reagent or Standard Information, Device Used, and All Certificates of Analysis for \" Reagent or Standard ID Code \"" +
                "Below is an image of the Details form of a Reagent or Standard:" +
                "<img src='../../Content/images/help-images/reagent-details.PNG' style='width:100%;border:1px solid #ccc;margin-top:5px;margin-bottom:5px;' alt=''/>" +
                "Some points about the Details Page:" +
                "<ul>" +
                    "<li>Location Information: Contains information abou the location, address, phone number, fax number, department name, and sub department</li>" +
                    "<li>Reagent Information: Everything from Reagent/Standard Lot # to who created the Reagent/Standard is contained in this section</li>" +
                    "<li>Device Used: Will contain information on the device that was used when the Reagent/Standard was created</li>" +
                    "<li>All Certificates of Analysis for \" Reagent or Standard ID Code \": Shows all the CofA's that have been paired to the Reagent/Standard</li>" +
                "</ul>" +
                "We hope you found this helpful! :)";
        } else if (selectedOption.val() === "RSEdit") {
            message = "<div style='height:40px;width:100%;background-color:#428ACA;padding:10px 0 10px 35px;margin-bottom:15px;color:#fff;font-size:16px;'>Reagent and Standard Edit Help</div>" +
                "When you go to the Edit Reagent or Edit Standard page, you will see a form which has mutliple fields that you can edit." +
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
                "When you click on Reagent or Standard, you will be greeted by the dashboard that has a table containing all the current Reagents/Standards that are available. " +
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
        } else if (selectedOption.val() === "ISCreate") {
            message = "<div style='height:40px;width:100%;background-color:#428ACA;padding:10px 0 10px 35px;margin-bottom:15px;color:#fff;font-size:16px;'>Intermediate Standard or Working Standard Create Help</div>" +
                "When you go to the Create Intermediate Standard or Working Standard page, you will see a form which has mutliple sections to complete. Each section's required field must be completed to advance to the next segment. You may notice it looks very similar to the paper forms you have been using. This is because it's heavily influenced from them!" +
                " Below is an image of the form to create a new Intermediate Standard or Working Standard along with notes to help guide you:" +
                "<img src='../../Content/images/help-images/intermediate-standard-create-part1.PNG' style='width:100%;border:1px solid #ccc;margin-top:5px;margin-bottom:5px;' alt=''/>" +
                "Some points about the form:" +
                "<ul>" +
                    "<li>Any field with a red asterisk is a required field. If it is not completed, the form will not be able to progress</li>" +
                    "<li>Please note the dropdown for 'Units'. This supports both Volume and Weight, from Tera to Atto</li>" +
                    "<li>To return to the list of all Intermediate/Working Standard, click on 'Back to List' located at the top-left corner. Please note that you will lose the data that you have filled out currently</li>" +
                    "<li>If you ever get lost with where you are in the site, you can always refer to the stage indicator just below the \"Back to List\" button</li>" +
                "</ul>" +
                "Once you have completed the form and clicked on 'Next', you will see the last portion of the form:" +
                "<img src='../../Content/images/help-images/intermediate-standard-create-part2.PNG' style='width:100%;border:1px solid #ccc;margin-top:5px;margin-bottom:5px;' alt=''/>" +
               "Some points about this portion of the form:" +
                "<ul>" +
                    "<li>Similar to the first portion of this procedure, any field with a red asterisk is a required field. If it is not completed, the form will not be able to progress</li>" +
                    "<li>Also, please note the dropdown for 'Units'. This supports both Volume and Weight, from Tera to Atto</li>" +
                    "<li>To return to the list of all Intermediate/Working Standard, click on 'Back to List' located at the top-left corner. Please note that you will lose the data that you have filled out currently</li>" +
                    "<li>If you ever get lost with where you are in the site, you can always refer to the stage indicator just below the \"Back to List\" button</li>" +
                "</ul>" +
                "Once completed, you will then be taken to a Summary page:" +
                "<img src='../../Content/images/help-images/intermediate-standard-create-part3.PNG'  style='width:100%;border:1px solid #ccc;margin-top:5px;margin-bottom:5px;' alt=''/>" +
                "Some points about the summary:" +
                "<ul>" +
                    "<li>Click on 'Cancel' to return to the previous section of the form</li>" +
                    "<li>Click on 'Save' to create the Reagent and be taken back to the Landing Page of Reagents</li>" +
                    "<li>Click on 'Save & New' to create the Reagent, and be given a new clean form to create another Reagent</li>" +
                "</ul>" +
                "We hope you found this helpful! :)";
        } else if (selectedOption.val() === "ISDetails") {
            message = "<div style='height:40px;width:100%;background-color:#428ACA;padding:10px 0 10px 35px;margin-bottom:15px;color:#fff;font-size:16px;'>Intermediate Standard or Working Standard Details Help</div>" +
                "When you go to the Intermediate Standard or Working Standard Details page, you will see a form which has mutliple sections to of information. In order the sections are Location Information, Intermediate/Working Standard Information, Device Used, and Prep List Table " +
                "Below is an image of the Details form of a Intermediate/Working Standard:" +
                "<img src='../../Content/images/help-images/intermediate-standard-details.PNG' style='width:100%;border:1px solid #ccc;margin-top:5px;margin-bottom:5px;' alt=''/>" +
                "Some points about the Details Page:" +
                "<ul>" +
                    "<li>Location Information: Contains information abou the location, address, phone number, fax number, department name, and sub department</li>" +
                    "<li>Intermediate/Working Standard Information: Everything from Intermediate/Working Standard Lot # to who created the Intermediate/Working Standard is contained in this section</li>" +
                    "<li>Prep List Table: All items that were use to create this Intermediate/Working Standard will be listed here. You will have the ability to view all of their details aswell</li>" +
                "</ul>" +
                "We hope you found this helpful! :)";
        } else if (selectedOption.val() === "ISEdit") {
            message = "<div style='height:40px;width:100%;background-color:#428ACA;padding:10px 0 10px 35px;margin-bottom:15px;color:#fff;font-size:16px;'>Intermediate Standard or Working Standard Edit Help</div>" +
                "When you go to the Edit Intermediate/Working Standard page, you will see a form which has mutliple fields that you can edit. " +
                "Below is an image of the form to Edit a Intermediate/Working Standard along with notes to help guide you:" +
                "<img src='../../Content/images/help-images/intermediate-standard-edit.PNG' style='width:100%;border:1px solid #ccc;margin-top:5px;margin-bottom:5px;' alt=''/>" +
                "Some points about the form:" +
                "<ul>" +
                    "<li>Intermediate/Working Standard Information: You will be able to update/edit the ID Code, Maxxam Id, and Expiry Date for the Intermediate/Working Standard</li>" +
                "</ul>" +
                "We hope you found this helpful! :)";
        } else if (selectedOption.val() === "ISDashboard") {
            message = "<div style='height:40px;width:100%;background-color:#428ACA;padding:10px 0 10px 35px;margin-bottom:15px;color:#fff;font-size:16px;'>Intermediate Standard or Working Standard Dashboard Help</div>" +
                "When you click on Intermediate Standard or Working Standard, you will be greeted by the dashboard that has a table containing all the current Intermediate/Working Standards that are available. " +
                "Below is an image of the Dashboard for a Intermediate/Working Standard:" +
                "<img src='../../Content/images/help-images/intermediate-standard-dashboard.PNG' style='width:100%;border:1px solid #ccc;margin-top:5px;margin-bottom:5px;' alt=''/>" +
                "Some points about the form:" +
                "<ul>" +
                    "<li>Each Intermediate/Working Standard has an Edit and Details option, both have thier own help section. (Click the drop down list and select Edit or Details to view their help pages)</li>" +
                    "<li>The table has mutliple rows that display important details regarding the Intermediate/Working Standard</li>" +
                    "<li>You have the ability to sort by each row in ascending or descending order</li>" +
                    "<li>As the screen size changes the fewer rows are displayed</li>" +
                    "<li>You are able to change the amount of Intermediate/Working Standards shown per page</li>" +
                    "<li>Also, You can search for a specific Intermediate/Working Standard by anyof the Rows shown</li>" +
                "</ul>" +
                "We hope you found this helpful! :)";
        } else if (selectedOption.val() === "BVCreate") {
            message = "<div style='height:40px;width:100%;background-color:#428ACA;padding:10px 0 10px 35px;margin-bottom:15px;color:#fff;font-size:16px;'>Balance Device Create Help</div>" +
                "When you go to the Create Balance Device page, you will see a form which has mutliple fields to fill-out. Each section's required field must be completed to advance to the next segment." +
                " Below is an image of the form to create a new Balance Device along with notes to help guide you:" +
                "<img src='../../Content/images/help-images/balance-create-part1.PNG' style='width:100%;border:1px solid #ccc;margin-top:5px;margin-bottom:5px;' alt=''/>" +
                "Some points about the form:" +
                "<ul>" +
                    "<li>Any field with a red asterisk is a required field. If it is not completed, the form will not be able to progress</li>" +
                    "<li>The number of decimals field will determine what decimal place to round to</li>" +
                    "<li>To return to the list of all Balance Devices, click on 'Back to List' located at the top-left corner. Please note that you will lose the data that you have filled out currently</li>" +
                    "<li>If you ever get lost with where you are in the form, you can always refer to the stage indicator just below the \"Back to List\" button</li>" +
                "</ul>" +
                "Once completed, you will then be taken to a Summary page:" +
                "<img src='../../Content/images/help-images/balance-create-part2.PNG'  style='width:100%;border:1px solid #ccc;margin-top:5px;margin-bottom:5px;' alt=''/>" +
                "Some points about the summary:" +
                "<ul>" +
                    "<li>Click on 'Cancel' to return to the previous section of the form</li>" +
                    "<li>Click on 'Save' to create the Balance Device and be taken back to the Landing Page of Balance Devices</li>" +
                    "<li>Click on 'Save & New' to create the Balance Device, and be given a new clean form to create another Balance Device</li>" +
                "</ul>" +
                "We hope you found this helpful! :)";
        } else if (selectedOption.val() === "BVDetails") {
            message = "<div style='height:40px;width:100%;background-color:#428ACA;padding:10px 0 10px 35px;margin-bottom:15px;color:#fff;font-size:16px;'>Balance Device Details Help</div>" +
                "When you go to the Balance Device Details page, you will see a form which has mutliple sections to of information. In order the sections are Location Information, Balance Information, Device Used, and Device Balance Verification History Table " +
                "Below is an image of the Details form of a Balance Device:" +
                "<img src='../../Content/images/help-images/balance-details.PNG' style='width:100%;border:1px solid #ccc;margin-top:5px;margin-bottom:5px;' alt=''/>" +
                "Some points about the Details Page:" +
                "<ul>" +
                    "<li>Location Information: Contains information about the location, address, phone number, fax number, department name, and sub department</li>" +
                    "<li>Balance Information: Everything from Balance Device ID to the Balance Type is contained in this section</li>" +
                    "<li>Device Balance Verification History Table: All verifications for the device will be listed here</li>" +
                "</ul>" +
                "We hope you found this helpful! :)";
        } else if (selectedOption.val() === "BVDashboard") {
            message = "<div style='height:40px;width:100%;background-color:#428ACA;padding:10px 0 10px 35px;margin-bottom:15px;color:#fff;font-size:16px;'>Balance Device Dashboard Help</div>" +
                "When you click on Balance Device, you will be greeted by the dashboard that has a table containing all the current Balance Device that are available. " +
                "Below is an image of the Dashboard for a Balance Device:" +
                "<img src='../../Content/images/help-images/balance-dashboard.PNG' style='width:100%;border:1px solid #ccc;margin-top:5px;margin-bottom:5px;' alt=''/>" +
                "Some points about the form:" +
                "<ul>" +
                    "<li>Each Balance Device has an Archive and Details option, only Details has its own help section. (Click the drop down list and  Details to view its help page)</li>" +
                    "<li>The table has mutliple rows that display important details regarding the Balance Devices</li>" +
                    "<li>You have the ability to sort by each row in ascending or descending order</li>" +
                    "<li>As the screen size changes the fewer rows are displayed</li>" +
                    "<li>You are able to change the amount of Balance Devices shown per page</li>" +
                    "<li>Also, You can search for a specific Balance Device by anyof the Rows shown</li>" +
                "</ul>" +
                "We hope you found this helpful! :)";
        } else if (selectedOption.val() === "BVVerification") {
            message = "<div style='height:40px;width:100%;background-color:#428ACA;padding:10px 0 10px 35px;margin-bottom:15px;color:#fff;font-size:16px;'>Balance Device Verification Help</div>" +
                "When you click on Verify Balance Device, you will be greeted by the verification page." +
                "Below is an image of the Verifications for a Balance Device:" +
                "<img src='../../Content/images/help-images/balance-verification.PNG' style='width:100%;border:1px solid #ccc;margin-top:5px;margin-bottom:5px;' alt=''/>" +
                "Some points about the form:" +
                "<ul>" +
                    "<li>Device ID, location, type, and verification standard weights for the device will be automatically filled in</li>" +
                    "<li>There will be instructions helping you through the verification</li>" +
                    "<li>The table shown has the acceptable ranges for the standard weights</li>" +
                    "<li>You will only be able to verify the next weights if the previous one passed</li>" +
                    "<li>There is a section for comments if applicable</li>" +
                    "<li>If you click on the Verificaion results table at the bottom it will show you the results from your two to three test.</li>" +
                    "<li>If the required tests passed the you will be able to save the device verification</li>" +
                    "<li>You will now be able to use the device to create standards & reagents</li>" +
                "</ul>" +
                "We hope you found this helpful! :)";
        } else {
            message = "Not yet implemented at this time. :("
        }
        notesSection.append(message);
    });
});