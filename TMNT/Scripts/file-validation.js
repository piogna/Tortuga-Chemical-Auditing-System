/* Maxxam File Validator for File Upload Inputs
   Version 1.0.0
   Created By: Tortuga
*/

$(function () {
    //selectors
    var fileInput = $('input[type=file]');
    var tabs = $('.tabs-style-bar > nav > ul > li');
    var sectionTwo = $('#section-bar-2');
    var sectionThree = $('#section-bar-3');

    //file validator script
    $('.btn-review').on('click', function () {
        fileInput.each(function () {
            if (!$(this).val()) {
                tabs.eq(1).addClass('tab-current');
                tabs.eq(2).removeClass('tab-current');
                sectionTwo.addClass("content-current");
                sectionThree.removeClass("content-current");
                $(this).css("border", "1px solid red");
                return false;
            } else {
                tabs.eq(1).removeClass('tab-current');
                tabs.eq(2).addClass('tab-current');
                sectionTwo.removeClass("content-current");
                sectionThree.addClass("content-current");
            }
        });
    });

    fileInput.on('change', function () {
        fileInput.each(function () {
            if ($(this).val()) {
                $(this).first().css("border", "1px solid #ccc");
            }
        });
    });
});