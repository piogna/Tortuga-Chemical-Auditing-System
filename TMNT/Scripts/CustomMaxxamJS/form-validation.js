/* Maxxam Form Validator for Required Fields
   Version 1.0.0
   Created By: Tortuga
*/
$(function () {
    //selectors
    var nextButtons = $('.next-section');
    var sectionOneInputs = $('#section-bar-1 .input-summary');
    var requiredField = $('.required-field');
    var requiredFieldSectionTwo = $('.required-field-s2');
    var dateInput = $('input[type=date]');
    var tabs = $('.tabs-style-bar > nav > ul > li');

    var units = $('#Unit');
    var devices = $('#device-selector');
    var storageLocations = $('#storage-locations');

    //section selectors
    var sectionOne = $('#section-bar-1');
    var sectionTwo = $('#section-bar-2');
    var sectionThree = $('#section-bar-3');

    //"disabling" next-section click until all required fields are clicked. this is very hacky.
    $('.btn-next').on('click', function () {
        //re-instantiating these selectors on click because the number of required fields and types in the "Build Your Intermediate Standard" section can change after document.ready
        requiredField = $('.required-field');

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

            if ($('#device-selector').val() === null) {
                $('.select2-container--default').addClass('required-field');
            }

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

    units.on('change', function () {
        units.each(function () {
            if ($(this).val()) {
                $(this).first().css("border", "1px solid #ccc");
            }
        });
    });

    storageLocations.on('change', function () {
        storageLocations.each(function () {
            if ($(this).val()) {
                $(this).first().css("border", "1px solid #ccc");
            }
        });
    });

    devices.on('change', function () {
        devices.each(function () {
            if ($(this).val()) {
                $('.select2-container--default').first().removeClass("required-field");
                $('.select2-container--default').first().css("border", "none");
            }
        });
    });

    //review button click
    $('#btn-review').on('click', function (e) {
        requiredFieldSectionTwo = $('.required-field-s2');
        sectionThree = $('#section-bar-3');
        var validForm = true;

        requiredFieldSectionTwo.each(function () {
            if (!$(this).val() || !$(this).text()) {
                validForm = false;
            }
        });

        if (!validForm) {

            $(requiredFieldSectionTwo).each(function () {
                if (!$(this).val()) {
                    $(this).css("border", "1px solid red");
                } else {
                    $(this).css("border", "1px solid #ccc");
                }
            });
            tabs.eq(1).addClass('tab-current');
            tabs.eq(2).removeClass('tab-current');
            sectionThree.removeClass("content-current");
            sectionTwo.addClass("content-current");
        } else {
            tabs.eq(1).removeClass('tab-current');
            tabs.eq(2).addClass('tab-current');
            sectionTwo.addClass("content-current");
            sectionThree.removeClass("content-current");
        }
    });
    //checking the required fields. if they have a value, the border goes back to default.
    requiredFieldSectionTwo.on('keyup', function () {
        requiredFieldSectionTwo.each(function () {
            if ($(this).is(':focus') && $(this).val()) {
                $(this).first().css("border", "1px solid #ccc");
            }
        });
    });

    $('.previous-section').on('click', function () {
        tabs.eq(0).addClass('tab-current');
        tabs.eq(1).removeClass('tab-current');
        sectionTwo.removeClass("content-current");
        sectionOne.addClass("content-current");
    });
});