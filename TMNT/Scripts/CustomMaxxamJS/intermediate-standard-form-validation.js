/*
    Validation specifically for Intermediate Standards. The standard validation script will not work for this form due to it being highly dynamic.
*/

$(function () {
    /* all script code below is native only to Intermediate Standard */
    var recipes = $('#build-recipe');

    //add another row
    recipes.on('click', '#another-item', function (e) {
        e.preventDefault();
        var div = document.getElementById("build-recipe");

        var addRow =
            "<div class='row' style='margin:0 !important'>" +
                "<div class='col-md-3'>" +
                    "<div class='form-group'>" +
                        "<div class='input-group'>" +
                            "<div class='input-group-addon addon-required'></div>" +
                            "<select name='Type' class='type-validation input-xlarge-fuid input-summary form-control valid no-border-radius required-field'>" +
                                   "<option value=''>No Chemical Type Selected</option>" +
                                       "<optgroup label='Item Type'>" +
                                       "<option value='Reagent'>" +
                                           "Reagent" +
                                       "</option>" +
                                       "<option value='Standard'>" +
                                           "Standard" +
                                       "</option>" +
                                       "<option value='Intermedate Standard'>" +
                                           "Intermediate Standard" +
                                       "</option>" +
                                   "</optgroup>" +
                            "</select>" +
                        "</div>" +
                    "</div>" +
                "</div>" +
            "<div class='col-md-3'>" +
                "<div class='input-group'>" +
                    "<div class='input-group-addon addon-required'></div>" +
                    "<input name='Amount' type='number' min='0' class='input-summary form-control text-box single-line no-border-radius required-field' placeholder='Amount' />" +
                "</div>" +
            "</div>" +
            "<div class='col-md-3'>" +
                "<div class='input-group'>" +
                "<div class='input-group-addon addon-required'></div>" +
                    "<input name='IdCode' type='text' class='input-summary form-control text-box single-line required-field no-border-radius' placeholder='Lot #' />" +
                "</div>" +
            "</div>" +
            "<div class='col-md-3'>" +
                "<a href='#' id='another-item' class='btn btn-default'>Add</a><a href='#' id='remove-item' class='btn btn-default' style='margin-left:5px;'>Remove</a>" +
            "</div>" +
        "</div>"
        recipes.find('div[class=col-md-3]:last').remove("div[class=col-md-3]:last");
        //recipes.find('.row:nth-last-child(1)').append("<div class='col-md-3'><a href='#' id='remove-item' class='btn btn-default'>Remove</a></div>");
        recipes.append(addRow);

        //scroll to make sure the most bottom row stays visible when adding new rows
        if (recipes.children("div[class=row]").length > 5) {
            recipes.css('overflow-y', 'scroll');
            div.scrollTop = div.scrollHeight;
        }
    });

    //remove row
    recipes.on('click', '#remove-item', function (e) {
        e.preventDefault();
        if (recipes.children("div[class=row]").length > 1) {
            //recipes.children("#remove-item").parent(".row").remove();
            recipes.children("div[class=row]:last").remove();
            recipes.children("div[class=row]:last").append("<div class='col-md-3'><a href='#' id='another-item' class='btn btn-default'>Add</a><a href='#' id='remove-item' class='btn btn-default' style='margin-left:5px;'>Remove</a></div>");
            //ensuring we can never have 0 rows
            if (recipes.children("div[class=row]").length == 1) {
                $('#remove-item').attr('disabled', 'disabled');
            }
        }
    });

    recipes.on('change', '.type-validation', function () {
        $('.type-validation').each(function () {
            if ($(this).val()) {
                $(this).css("border", "1px solid #ccc");
            }
        });
    });

    recipes.on('keyup', 'input.required-field', function () {
        $('input.required-field').each(function () {
            if ($(this).val()) {
                $(this).css("border", "1px solid #ccc");
            }
        });
    });
});