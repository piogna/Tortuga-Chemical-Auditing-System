/* Maxxam Form Validator for Required Fields
   Version 1.0.0
   Created By: Tortuga
*/
$(function () {
    //selectors
    var nextButtons = $('.next-section');
    var sectionOneInputs = $('#section-bar-1 .input-summary');
    var requiredField = $('.required-field');
    var dateInput = $('input[type=date]');
    var tabs = $('.tabs-style-bar > nav > ul > li');

    //section selectors
    var sectionOne = $('#section-bar-1');
    var sectionTwo = $('#section-bar-2');
    var sectionThree = $('#section-bar-3');

    //"disabling" next-section click until all required fields are clicked. this is very hacky.
    $('.btn-next').on('click', function () {
        var validForm = true;
        requiredField.each(function () {
            if (!$(this).val()) {
                validForm = false;
            }
        });

        if (!validForm) {
            tabs.eq(0).addClass('tab-current');
            tabs.eq(1).removeClass('tab-current');
            sectionOne.addClass("content-current");
            sectionTwo.removeClass("content-current");

            $(requiredField).each(function () {
                if (!$(this).val()) {
                    $(this).css("border", "1px solid red");
                } else {
                    $(this).css("border", "1px solid #ccc");
                }
            });
        } else {
            tabs.eq(0).removeClass('tab-current');
            tabs.eq(1).addClass('tab-current');
            sectionOne.removeClass("content-current");
            sectionTwo.addClass("content-current");
        }
    });
    //checking the required fields. if they have a value, the border goes back to default.
    requiredField.on('keyup', function () {
        requiredField.each(function () {
            if ($(this).is(':focus') && $(this).val()) {
                $(this).first().css("border", "1px solid #ccc");
            }
        });
    });
    //checking the date inputs. if they have a value on focusout, the border goes back to default.
    dateInput.on('change', function () {
        dateInput.each(function () {
            if ($(this).val()) {
                $(this).first().css("border", "1px solid #ccc");
            }
        });
    });
});