//автозаполнение инпутов
var autocompleteUrl;

$(function () {

    formUrl();
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
$(function () {
    var autocompleteUrl = '/Data/FindStory';
    $("#Story").autocomplete({
        source: autocompleteUrl,
        minLength: 2,
        select: function (event, ui) {

        }
    });
});

function formUrl() {
    var cityName = $("#Locality_id option:selected").text().toString();
    autocompleteUrl = '/Data/FindStreets?' + "cityname=" + cityName;
}

$(document).ready(function () {
    $("#Locality_id").change(function () {

            formUrl();
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
    
})