/*
    Validation specifically for Intermediate Standards. 
    The standard validation script will not work for this form due to it being highly dynamic.
*/

$(function () {
    //recipe form
    var ChemicalType = $('#ChemicalType'), LotNumber = $('#LotNumbers'), Amount = $('#PrepAmount'), Units = $('#PrepUnits'),
    //recipe table and buttons
    Recipes = $('#build-recipe'), RecipeTable = $('#recipe-table'), AddItem = $('#another-item'), RemoveItem = $('.recipe-table-remove-item'),
    //selection options
    OptReagent = $('.opt-reagent'), OptStandard = $('.opt-standard'), OptIntStandard = $('.opt-int-standard'), OptLabel = $('.opt-label'),
    //button message
    buttonMessage = $('#button-message'),
    //miscellaneous
    ItemCount = 1, Append = "";

    if ($('#recipe-table tbody tr').length == 0) {
        RecipeTable.append("<tr class='text-center recipe-table-no-data'><td colspan='5'>Add Prep List Item</td></tr>");

        //building the appropriate list of lot numbers
        ChemicalType.on('change', function () {
            var ChemicalTypeValue;
            if (ChemicalType.val()) {

                //if a lot number is selected, wipe it to help prevent bugs
                if (LotNumber.val()) {
                    LotNumber.val("");
                }

                ChemicalTypeValue = ChemicalType.val();

                LotNumber.removeAttr("disabled");

                switch (ChemicalTypeValue) {
                    case "Reagent":
                        //display only reagent lot numbers
                        OptReagent.removeClass('opt');
                        OptLabel.text("Reagent ID Codes");
                        if (!OptStandard.hasClass('opt')) { OptStandard.addClass('opt'); }
                        if (!OptIntStandard.hasClass('opt')) { OptIntStandard.addClass('opt'); }
                        break;
                    case "Standard":
                        //display only standard lot numbers
                        OptStandard.removeClass('opt');
                        OptLabel.text("Standard ID Codes");
                        if (!OptReagent.hasClass('opt')) { OptReagent.addClass('opt'); }
                        if (!OptIntStandard.hasClass('opt')) { OptIntStandard.addClass('opt'); }
                        break;
                    case "Intermediate Standard":
                        //display only intermediate standard maxxam id's
                        OptIntStandard.removeClass('opt');
                        OptLabel.text("Intermediate Standard ID Codes");
                        if (!OptReagent.hasClass('opt')) { OptReagent.addClass('opt'); }
                        if (!OptStandard.hasClass('opt')) { OptStandard.addClass('opt'); }
                        break;
                    default:
                        //dun goofed
                        alert("Something went wrong. Refresh the page and try again. If the problem persists, contact your administrator.");
                }

            } else {
                //clear inputs and display no selection option
                ChemicalType.val("");
                LotNumber.val("");
                Amount.val("");
                Units.val("");

                OptLabel.text("Select a Chemical Type First");
                LotNumber.attr("disabled", "disabled");
            }
        });

        Recipes.on('click', '#another-item', function (e) {
            e.preventDefault();
            var listItemIsValid = "valid";
            var requiredFieldsForListItem = [ChemicalType, LotNumber, Amount, Units];
            //make sure all fields are filled
            for (var i = 0; i < requiredFieldsForListItem.length; i++) {
                if (!requiredFieldsForListItem[i].val()) {
                    listItemIsValid = "not filled";
                    break;
                }
            }

            var amountValue = Amount.val().toString() + " ";

            if (Units.find("option:selected").length > 1) {
                Units.find("option:selected").each(function () {
                    var $this = $(this);
                    if ($this.length) {
                        var selText = $this.text().trim();
                        amountValue += $this.text().trim() + "/";
                    }
                });
            } else {
                amountValue += Units.find("option:selected").text().trim();
            }

            //remove last backslash
            if (amountValue.indexOf("/") > -1) { amountValue = amountValue.substr(0, amountValue.length - 1); }

            var lotNumbersInPrepTable = $('input[name=PrepListItemLotNumbers]');

            lotNumbersInPrepTable.each(function () {
                if ($(this).val() === LotNumber.val()) {
                    listItemIsValid = "already exists";
                }
            });

            if (listItemIsValid === "valid") {
                Append = "";
                Append += "<tr><td style='width:83px'>" + ItemCount + "</td>" +
                        "<td style='width:169px'><input name='PrepListItemTypes' style='border:none;background:transparent;color:#3a87ad;width:80%' type='text' readonly='readonly' value='" + ChemicalType.val() + "'/></td>" +
                         "<td style='width:101px'><input name='PrepListItemLotNumbers' style='border:none;background:transparent;color:#3a87ad;width:80%' type='text' readonly='readonly' value='" + LotNumber.val() + "'/></td>" +
                        "<td style='width:102px'><input name='PrepListItemAmounts' style='border:none;background:transparent;color:#3a87ad;' type='text' readonly='readonly' value='" + amountValue + "'/></td>" +
                        "<td style='width:151px'><a class='recipe-table-remove-item' href='#'><i class='fa fa-close'></i>&nbsp;&nbsp;Remove</a></td></tr>";
                RecipeTable.append(Append);

                if ($('#recipe-table tbody tr').length > 0) {
                    $('#recipe-table tbody tr.recipe-table-no-data').css('display', 'none');
                    $('.btn-next').removeAttr("disabled");

                    buttonMessage.text("All required fields filled.");
                    buttonMessage.removeClass("button-message-error").addClass("button-message-success");
                }

                //clear inputs
                ChemicalType.val("");
                LotNumber.val("");
                Amount.val("");
                $('#PrepUnits').val('').trigger('chosen:updated');
                OptLabel.text("Select a Chemical Type First");
                LotNumber.attr("disabled", "disabled");

                ItemCount++;
            } else if (listItemIsValid === "not filled") {
                //form isn't valid, display error message
                var error = "Cannot create prep list item. Make sure the following fields are filled:\n\n";
                for (var i = 0; i < requiredFieldsForListItem.length; i++) {
                    if (!requiredFieldsForListItem[i].val()) {
                        error += "• " + requiredFieldsForListItem[i].attr("name").toString() + "\n";
                    }
                }
                alert(error);
            } else if (listItemIsValid === "already exists") {
                alert("The lot number " + LotNumber.val() + " already exists in the prep list table.");
                LotNumber.val("");
            }
        });

        Recipes.on('click', '.recipe-table-remove-item', function (e) {
            e.preventDefault();

            var anchor = $(this).find('.recipe-table-remove-item').index() + 1;
            $(this).parents('tr').remove();

            if ($('#recipe-table tbody tr').length == 1) {
                //one row = show no data
                $('#recipe-table tbody tr.recipe-table-no-data').css('display', 'table-row');
                $('.btn-next').attr("disabled", "disabled");

                buttonMessage.text("Fill out required fields.");
                buttonMessage.removeClass("button-message-success").addClass("button-message-error");
            } else {
                //remove the row that has been removed and fix the numbers of each item shown
                var tds = $('#recipe-table tbody tr:not(:first)').find('td:first');
                var count = 1;

                tds.each(function () {
                    $(this).text(count.toString())
                    count++;
                });
            }
            ItemCount--;
        });
    }
});