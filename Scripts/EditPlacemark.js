function EditPlacemark(shirota, dolgota) {
    ymaps.ready(init);
    var myMap;
    var myPlacemark;
    var havePlacemark = false;
    shirota = parseFloat(shirota.replace(",", "."));
    dolgota = parseFloat(dolgota.replace(",", "."));
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
        var myPlacemark = new ymaps.Placemark([shirota, dolgota],
            {},
            {
                draggable: true
            });
        myPlacemark.events.add('dragend', function (e) {
            var coords = myPlacemark.geometry.getCoordinates();
            var str = String(coords[0].toPrecision(6));
            str = str.replace('.', ',');
            $("#Shirota").val(str);

            str = String(coords[1].toPrecision(6));
            str = str.replace('.', ',');
            $("#Dolgota").val(str);
        
        });
        myMap.geoObjects.add(myPlacemark);
    }
}
