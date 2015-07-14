$(document).ready(function() {
    $("#MapSearch").click(function() {

        if ($("#Street1").val() != "") {
            if (myPlacemark != undefined) {

                myMap.geoObjects.remove(myPlacemark);
            }
            var myGeocoder = ymaps.geocode($("#Street1").val());
         
            myGeocoder.then(
                function(res) {

                    myPlacemark = new ymaps.Placemark({
                        coordinates: [res.geoObjects.get(0).geometry._ec[0],
                        res.geoObjects.get(0).geometry._ec[1]]

                        },
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