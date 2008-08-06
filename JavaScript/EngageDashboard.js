/// <reference path="jquery-1.2.6.min.debug.js" />
function toggleElementVisibility(linkElement, elementId, showTextFieldId, hideTextFieldId) {
    var linkElement = jQuery(linkElement);
    var element = jQuery('#' + elementId);
    if (element.css("display") !== 'none') {
        linkElement.html(jQuery('#' + showTextFieldId).val());
    }
    else {
        linkElement.html(jQuery('#' + hideTextFieldId).val());
    }
    element.slideToggle('slow');
}