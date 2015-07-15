$(document).ready(function() {
    $("#MapSearch").click(function() {
        var text;
        if ($("#Street1").val() != "") {
            if (myPlacemark != undefined) {

                myMap.geoObjects.remove(myPlacemark);
            }
         
            var location =$("#Locality_id").text().toString()+" "+ $("#Street1").val().toString() ;
            var myGeocoder = ymaps.geocode(location);
         
            myGeocoder.then(
                function(res) {
                    myPlacemark = new ymaps.Placemark([res.geoObjects.get(0).geometry._ec[0], res.geoObjects.get(0).geometry._ec[1]],
                    {},
                    {
                        draggable: true
                      
                    });
                    console.log(myPlacemark);
                    myMap.geoObjects.add(myPlacemark);
                });
           
            
        }
    });

});