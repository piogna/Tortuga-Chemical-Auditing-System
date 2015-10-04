﻿/*
    Validation specifically for Intermediate Standards. 
    The standard validation script will not work for this form due to it being highly dynamic.
*/

$(function () {
    var ChemicalType = $('#ChemicalType');
    var IdCode = $('#IdCode');
    var Amount = $('#Amount');
    var Units = $('#Units');

    var Recipes = $('#build-recipe');
    var RecipeTable = $('#recipe-table');
    var AddItem = $('#another-item');
    var RemoveItem = $('.recipe-table-remove-item');

    var ItemCount = 1;
    var Append;

    if ($('#recipe-table tbody tr').length == 0) {
        RecipeTable.append("<tr class='text-center recipe-table-no-data'><td colspan='5'>Add Ingredients</td></tr>");
    }

    Recipes.on('click', '#another-item', function (e) {
        e.preventDefault();

        var listItemIsValid = true;
        var amountValue = Amount.val().toString() + " " + Units.find("option:selected").text().trim();
        var requiredFieldsForListItem = [ChemicalType, IdCode, Amount, Units];
        //make sure all fields are filled
        for (var i = 0; i < requiredFieldsForListItem.length; i++) {
            if (!requiredFieldsForListItem[i].val()) {
                listItemIsValid = false;
                break;
            }
        }

        //if ($('#recipe-table tbody tr:not(:first)').length > 1) {
        //    //make sure the same item isn't being used twice (declutter the view)
        //    $('input[name=PrepListItemIdCodes]').each(function () {
        //        if ($(this).val() === IdCode.val()) {
        //            alert("exists");
        //            listItemIsValid = false;
        //        }
        //    });
        //}

        if (listItemIsValid) {
            Append = "";
            Append += "<tr><td style='width:83px'>" + ItemCount + "</td>" +
                    "<td style='width:169px'><input name='PrepListItemTypes' style='border:none;background:transparent;color:#3a87ad;width:80%' type='text' readonly='readonly' value='" + ChemicalType.val() + "'/></td>" +
                     "<td style='width:101px'><input name='PrepListItemIdCodes' style='border:none;background:transparent;color:#3a87ad;width:80%' type='text' readonly='readonly' value='" + IdCode.val() + "'/></td>" +
                    "<td style='width:102px'><input name='PrepListItemAmounts' style='border:none;background:transparent;color:#3a87ad;' type='text' readonly='readonly' value='" + amountValue + "'/></td>" +
                    "<td style='width:151px'><a class='recipe-table-remove-item' href='#'><i class='fa fa-close'></i>&nbsp;&nbsp;Remove</a></td></tr>";
            RecipeTable.append(Append);

            if ($('#recipe-table tbody tr').length > 0) {
                $('#recipe-table tbody tr.recipe-table-no-data').css('display', 'none');
            }

            //clear inputs
            ChemicalType.val("");
            IdCode.val("");
            Amount.val("");
            Units.val("");

            ItemCount++;
        } else {
            //form isn't valid, display error message
            var error = "Cannot created prep list item. Make sure the following fields are filled:\n\n";
            for (var i = 0; i < requiredFieldsForListItem.length; i++) {
                if (!requiredFieldsForListItem[i].val()) {
                    error += "• " + requiredFieldsForListItem[i].attr("name").toString() + "\n";
                }
            }
            alert(error);
        }
    });

    Recipes.on('click', '.recipe-table-remove-item', function (e) {
        e.preventDefault();

        var anchor = $(this).find('.recipe-table-remove-item').index() + 1;
        $(this).parents('tr').remove();

        if ($('#recipe-table tbody tr').length == 1) {
            //one row = show no data
            $('#recipe-table tbody tr.recipe-table-no-data').css('display', 'table-row');
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
});