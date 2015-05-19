$(function () {
    var autocompleteUrl = '/Data/FindStreets';
    $("#Street1").autocomplete({
        source: autocompleteUrl,
        minLength: 2,
        select: function (event, ui) {

        }
    });
    $("#street1").autocomplete({
        source: autocompleteUrl,
        minLength: 2,
        select: function (event, ui) {

        }
    });
});
$(function () {
    var autocompleteUrl = '/Data/FindStreets';
    $("#Street2").autocomplete({
        source: autocompleteUrl,
        minLength: 2,
        select: function (event, ui) {

        }
    });
    $("#street2").autocomplete({
        source: autocompleteUrl,
        minLength: 2,
        select: function (event, ui) {

        }
    });
});
$(function () {
    var autocompleteUrl = '/Data/FindStreets';
    $("#FromStreet").autocomplete({
        source: autocompleteUrl,
        minLength: 2,
        select: function (event, ui) {

        }
    });
    $("#fromStreet").autocomplete({
        source: autocompleteUrl,
        minLength: 2,
        select: function (event, ui) {

        }
    });
});