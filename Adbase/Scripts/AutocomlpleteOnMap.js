$(function () {
    var autocompleteUrl = '/Data/FindStreets';
    $("#mStreet1").autocomplete({
        source: autocompleteUrl,
        minLength: 2,
        select: function (event, ui) {
        }
    });
});
$(function () {
    var autocompleteUrl = '/Data/FindStreets';
    $("#mStreet2").autocomplete({
        source: autocompleteUrl,
        minLength: 2,
        select: function (event, ui) {

        }
    });
});
$(function () {
    var autocompleteUrl = '/Data/FindStreets';
    $("#mFromStreet").autocomplete({
        source: autocompleteUrl,
        minLength: 2,
        select: function (event, ui) {

        }
    });
});
$(function () {
    var autocompleteUrl = '/Data/FindStreets';
    $("#lStreet1").autocomplete({
        source: autocompleteUrl,
        minLength: 2,
        select: function (event, ui) {
            alert("Selected " + ui.item.label);
        }
    });
});
$(function () {
    var autocompleteUrl = '/Data/FindStreets';
    $("#lStreet2").autocomplete({
        source: autocompleteUrl,
        minLength: 2,
        select: function (event, ui) {

        }
    });
});
$(function () {
    var autocompleteUrl = '/Data/FindStreets';
    $("#lFromStreet").autocomplete({
        source: autocompleteUrl,
        minLength: 2,
        select: function (event, ui) {

        }
    });
});