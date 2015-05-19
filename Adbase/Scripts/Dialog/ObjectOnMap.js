function ShowMap(shirota, dolgota)
{
    ymaps.ready(init);
    var myMap, myPlacemark;

    function init() {
        shirota = shirota.replace(',', '.');
        dolgota = dolgota.replace(',', '.');
        shirota = parseFloat(shirota);
        dolgota = parseFloat(dolgota);
        myMap = new ymaps.Map("map", {
            center: [shirota, dolgota],
            zoom: 11
        });
        myPlacemark = new ymaps.Placemark([shirota, dolgota]);
        myMap.geoObjects.add(myPlacemark);
    }
}



