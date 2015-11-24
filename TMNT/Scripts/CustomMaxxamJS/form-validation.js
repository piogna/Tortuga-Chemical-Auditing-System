﻿/* Maxxam Form Validator for Required Fields
   Version 2.1.0
   Created By: Tortuga
*/
$(function () {
    var requiredField = $('.required-field:not([type=hidden])'), requiredFieldSectionTwo = $('.required-field-s2'),
    btnNext = $('.btn-next'), btnReview = $('.btn-review'), buttonMessage = $('#button-message'), buttonMessageSectionTwo = $('#button-message-s2'),
    formIsValid = true,
    otherUnitWrapper = $("#other-unit-wrapper"), otherUnitExplained = $("#OtherUnitExplained");

    $('#WeightUnits').on('change', function () {
        var options = $(this).find(":selected");

        if (options.length === 0) {
            otherUnitWrapper.addClass("hide");
            otherUnitExplained.removeClass("required-field");
        }

        options.each(function () {
            if ($(this).text() === "Other") {
                otherUnitWrapper.removeClass("hide");
                otherUnitExplained.addClass("required-field");
            } else {
                otherUnitWrapper.addClass("hide");
                otherUnitExplained.removeClass("required-field");
            }
        });
    });

    var concOtherUnitWrapper = $('#conc-other-unit-wrapper'), concOtherUnitExplained = $("#ConcentrationOtherUnitExplained");

    $('#ConcentrationUnits').on('change', function () {
        var options = $(this).find(":selected");

        if (options.length === 0) {
            concOtherUnitWrapper.addClass("hide");
            concOtherUnitExplained.removeClass("required-field");
        }

        options.each(function () {
            if ($(this).text() === "Other") {
                concOtherUnitWrapper.removeClass("hide");
                concOtherUnitExplained.addClass("required-field");
            } else {
                concOtherUnitWrapper.addClass("hide");
                concOtherUnitExplained.removeClass("required-field");
            }
        });
    });

    $('#section-bar-1').on('change keyup paste', '.required-field', function () {
        formIsValid = true;
        requiredField.each(function () {
            if (!$(this).val()) {
                formIsValid = false;
            }
        });

        if (formIsValid) {
            btnNext.removeAttr("disabled");
            buttonMessage.text("All required fields filled.");
            buttonMessage.removeClass("button-message-error").addClass("button-message-success")
        } else {
            if (!btnNext.attr("disabled")) {
                btnNext.attr('disabled', 'disabled');
                buttonMessage.text("Fill out required fields.");
                buttonMessage.removeClass("button-message-success").addClass("button-message-error");
            }
        }
    });

    $('#IsExpiryDateBasedOnDays').on('change', function () {
        var expiryDays = $('#DaysUntilExpired');
        var expiryDate = $('#ExpiryDate');
        var newExpiryDate = $('#NewExpiryDate');

        if (this.checked) {
            expiryDate.attr('type', 'hidden');
            newExpiryDate.attr('type', 'hidden');
            expiryDays.attr('type', 'number');
            expiryDate.val("");
            newExpiryDate.val("");
        } else {
            expiryDate.attr('type', 'text');
            newExpiryDate.attr('type', 'text');
            expiryDays.attr('type', 'hidden');
            expiryDays.val("");
        }

        requiredField = $('.required-field:not([type=hidden])');

        formIsValid = true;
        requiredField.each(function () {
            if (!$(this).val()) {
                formIsValid = false;
            }
        });

        if (formIsValid) {
            btnNext.removeAttr("disabled");
            buttonMessage.text("All required fields filled.");
            buttonMessage.removeClass("button-message-error").addClass("button-message-success")
        } else {
            if (!btnNext.attr("disabled")) {
                btnNext.attr('disabled', 'disabled');
                buttonMessage.text("Fill out required fields.");
                buttonMessage.removeClass("button-message-success").addClass("button-message-error");
            }
        }
    });

    requiredFieldSectionTwo.on('change keyup paste', function () {
        formIsValid = true;
        requiredFieldSectionTwo.each(function () {
            if (!$(this).val()) {
                formIsValid = false;
            }
        });

        if (formIsValid) {
            btnReview.removeAttr("disabled");
            buttonMessageSectionTwo.text("All required fields filled.");
            buttonMessageSectionTwo.removeClass("button-message-error").addClass("button-message-success");
        } else {
            if (!btnReview.attr("disabled")) {
                btnReview.attr('disabled', 'disabled');
                buttonMessageSectionTwo.text("Fill out required fields.");
                buttonMessageSectionTwo.removeClass("button-message-success").addClass("button-message-error");
            }
        }
    });
});