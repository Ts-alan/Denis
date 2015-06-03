$(function () {
    ymaps.ready(init);
    var myMap;
    var myPlacemark;
    var havePlacemark = false;
    function init() {
        myMap = new ymaps.Map("map", {
            center: [53.901, 27.5601], 
            zoom: 11,
            behaviors: ['default', 'scrollZoom']
        }, {
            balloonMaxWidth: 200
        });

        myMap.controls.add('zoomControl');
        myMap.controls.add('searchControl');
        myMap.controls.add('typeSelector');


        myMap.events.add('click', function (e) {
            if (!havePlacemark) {
                var coords = e.get('coordPosition');
                myPlacemark = new ymaps.Placemark([coords[0].toPrecision(6), coords[1].toPrecision(6)],
                    {},
                    {
                        draggable: true
                    });
                myMap.geoObjects.add(myPlacemark);
                havePlacemark = true;
            }

            var str = String(coords[0].toPrecision(6));
            str = str.replace('.', ',');
            $("#Shirota").val(str);
            str = String(coords[1].toPrecision(6));
            str = str.replace('.', ',');
            $("#Dolgota").val(str);

            myPlacemark.events.add('dragend', function (e) {
                var coords = myPlacemark.geometry.getCoordinates();
                var str = String(coords[0].toPrecision(6));
                str = str.replace('.', ',');
                $("#Shirota").val(str);

                str = String(coords[1].toPrecision(6));
                str = str.replace('.', ',');
                $("#Dolgota").val(str);
            });
        });

    }
});