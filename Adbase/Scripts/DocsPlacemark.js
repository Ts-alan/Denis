var myMap;
var myPlacemark;
var countE = true;
ymaps.ready(init);
var geoCoords;

function init() {

    myMap = new ymaps.Map("map", {
        center: [53.9172, 27.5601],
        zoom: 11,
        behaviors: ['default', 'scrollZoom']
    }, {
        balloonMaxWidth: 200
    });

    myMap.controls.add('zoomControl');
    myMap.controls.add('typeSelector');

   

    if ($("#Breadth").val() != "" && $("#Height").val() != "") 
    {
        // нанесение обьекта на карту при загрузке
        myPlacemark = new ymaps.Placemark([$("#Breadth").val().replace(",", "."), $("#Height").val().replace(',', '.')],
        {},
        {
            draggable: true
        });
        myMap.geoObjects.add(myPlacemark);

       
    }

    MapPan();
}

function MapPan () {
    
        var loc;
        if ($("#Street1").text() == "Константиново") {
            loc = $("#Locality_NameLocality").val() + " Беларусь Минская область";
        }
        else {
            loc = $("#Locality_NameLocality").val();
        }

        var myGeocoder = ymaps.geocode(loc);
        myGeocoder.then(
            function (res) {
                geoCoords = res.geoObjects.get(0).geometry.getCoordinates();
                myMap.panTo([geoCoords[0], geoCoords[1]]);
            },
            function (err) {
                alert('Ошибка');
            }
        );
};

