//автозаполнение инпутов
var autocompleteUrl;

$(function () {

    formUrl();
    if ($("#Locality_id option:selected").text().toString() == "Минск")
    {
        $("#Street1").autocomplete({
            source: autocompleteUrl,
            minLength: 2,
            select: function(event, ui) {

            }
        });
        $("#street1").autocomplete({
            source: autocompleteUrl,
            minLength: 2,
            select: function(event, ui) {

            }
        });
    }
    if ($("#Locality_id option:selected").text().toString() != "Минск")
    {
        $("#Street1").autocomplete("destroy");
        $("#street1").autocomplete("destroy");
    }
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
    if (cityName != "Минск")
    {
        cityName = "Минск";
    }

    autocompleteUrl = '/Data/FindStreets?' + "cityname=" + cityName;
}

$(document).ready(function () {
    $("#Locality_id").change(function () {
        var qst = $("#Locality_id option:selected").text().toString();
        if (qst == "Минск")
        {
            formUrl();
            $("#Street1").autocomplete({
                source: autocompleteUrl,
                minLength: 2,
                select: function(event, ui) {

                }
            });
            $("#street1").autocomplete({
                source: autocompleteUrl,
                minLength: 2,
                select: function(event, ui) {

                }
            });
        }
        if ($("#Locality_id option:selected").text().toString() != "Минск") {
            $("#Street1").autocomplete("destroy");
            $("#street1").autocomplete("destroy");
        }
    });
    
})