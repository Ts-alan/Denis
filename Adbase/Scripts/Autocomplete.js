//автозаполнение инпутов
var autocompleteUrl;
var firstCall = true;

function formUrl() {
    var cityName = $("#Locality_id option:selected").text().toString();
    if (cityName != "Минск")
    {
        cityName = "Минск";
    }

    autocompleteUrl = '/Data/FindStreets?' + "cityname=" + cityName;
}

function setAutocomplete()
{
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
             $("#street2").autocomplete({
            source: autocompleteUrl,
            minLength: 2,
            select: function(event, ui) {

            }
             });
             $("#Street2").autocomplete({
            source: autocompleteUrl,
            minLength: 2,
            select: function(event, ui) {

            }
         });

         $("#FromStreet").autocomplete({
            source: autocompleteUrl,
            minLength: 2,
            select: function(event, ui) {

            }
        });
        }
        if ($("#Locality_id option:selected").text().toString() != "Минск" && $("#Street1").hasClass("ui-autocomplete-input"))
        {
            $("#Street1").autocomplete("destroy");
        }
        if ($("#Locality_id option:selected").text().toString() != "Минск" && $("#Street2").hasClass("ui-autocomplete-input"))
        {
            $("#Street2").autocomplete("destroy");
           
        }
        if ($("#Locality_id option:selected").text().toString() != "Минск" && $("#FromStreet").hasClass("ui-autocomplete-input"))
        {
            $("#FromStreet").autocomplete("destroy");
        }
}

$(document).ready(function()
{
    setAutocomplete();
    $("#Locality_id").change(function()
    {
        setAutocomplete();
    });
});

//$(function () {

//    formUrl();
//    if ($("#Locality_id option:selected").text().toString() == "Минск")
//    {
//        $("#Street1").autocomplete({
//            source: autocompleteUrl,
//            minLength: 2,
//            select: function(event, ui) {

//            }
//        });
//        $("#street1").autocomplete({
//            source: autocompleteUrl,
//            minLength: 2,
//            select: function(event, ui) {

//            }
//        });

//         $("#street2").autocomplete({
//            source: autocompleteUrl,
//            minLength: 2,
//            select: function(event, ui) {

//            }
//         });

//         $("#FromStreet").autocomplete({
//            source: autocompleteUrl,
//            minLength: 2,
//            select: function(event, ui) {

//            }
//        });

//    }
//    if ($("#Locality_id option:selected").text().toString() != "Минск" && $("#Street1").hasClass("ui-autocomplete-input"))
//    {
//        $("#Street1").autocomplete("destroy");
//        $("#street1").autocomplete("destroy");
//        $("#FromStreet").autocomplete("destroy");
//        $("#Street2").autocomplete("destroy");
//    }
//});
//$(function () {
//    var autocompleteUrl = '/Data/FindStreets';
//    $("#Street2").autocomplete({
//        source: autocompleteUrl,
//        minLength: 2,
//        select: function (event, ui) {

//        }
//    });
//    $("#street2").autocomplete({
//        source: autocompleteUrl,
//        minLength: 2,
//        select: function (event, ui) {

//        }
//    });
//});
//$(function () {
//    var autocompleteUrl = '/Data/FindStreets';
//    $("#FromStreet").autocomplete({
//        source: autocompleteUrl,
//        minLength: 2,
//        select: function (event, ui) {

//        }
//    });
//    $("#fromStreet").autocomplete({
//        source: autocompleteUrl,
//        minLength: 2,
//        select: function (event, ui) {

//        }
//    });
//});
//$(function () {
//    var autocompleteUrl = '/Data/FindStory';
//    $("#Story").autocomplete({
//        source: autocompleteUrl,
//        minLength: 2,
//        select: function (event, ui) {

//        }
//    });
//});

