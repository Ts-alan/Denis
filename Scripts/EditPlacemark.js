function EditPlacemark(shirota, dolgota) {
    var myMap;
    var myPlacemark;
    var havePlacemark = false;
    var shirotaValue = parseFloat(shirota.replace(",", "."));
    var dolgotaValue = parseFloat(dolgota.replace(",", "."));
    ymaps.ready(init);

    function init() {
        myMap = new ymaps.Map("map", {
            center: [53.9172, 27.5601],
            zoom: 11,
            behaviors: ['default', 'scrollZoom']
        }, {
            balloonMaxWidth: 200
        });

        myMap.controls.add('zoomControl');
        myMap.controls.add('searchControl');
        myMap.controls.add('typeSelector');

        var myPlacemark = new ymaps.Placemark([shirotaValue, dolgotaValue],
            {},
            {
                draggable: true
            });
        myMap.geoObjects.add(myPlacemark);
        myPlacemark.events.add('dragend', function (e) {
            var coords = myPlacemark.geometry.getCoordinates();
            console.log(coords);
            var str = String(coords[0].toPrecision(6));
            str = str.replace('.', ',');
            $("#Shirota").val(str);

            str = String(coords[1].toPrecision(6));
            str = str.replace('.', ',');
            $("#Dolgota").val(str);
        
        });
       
    }
}
