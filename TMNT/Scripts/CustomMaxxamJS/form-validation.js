/* Maxxam Form Validator for Required Fields
   Version 2.0.0
   Created By: Tortuga
*/
$(function () {
    var requiredField = $('.required-field');
    var requiredFieldSectionTwo = $('.required-field-s2');
    var btnNext = $('.btn-next');
    var btnReview = $('.btn-review');
    var buttonMessage = $('#button-message');
    var buttonMessageSectionTwo = $('#button-message-s2');
    var formIsValid = true;

    requiredField.on('change keyup paste', function () {
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