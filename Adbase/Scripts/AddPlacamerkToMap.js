$(function () {
    ymaps.ready(init);
    var myMap;
    var myPlacemark;
    var havePlacemark = false;
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

            //присвоение  коодинат при клике  
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

            //присвоение коодинат при перетаскивание метки  
            myPlacemark.events.add('dragend', function (e) {
                var coords = myPlacemark.geometry.getCoordinates();
                var str = String(coords[0].toPrecision(6));
                str = str.replace('.', ',');
                //if (!isNaN(str)) {
                //    $("#Shirota").val(str);
                //} else {
                //    $("#Shirota").val("");
                //}
                $("#Shirota").val(str);
                str = String(coords[1].toPrecision(6));
                str = str.replace('.', ',');
                //if (!isNaN(str)) {
                //    $("#Dolgota").val(str);
                //} else {
                //    $("#Dolgota").val("");
                //} 
                $("#Dolgota").val(str);
            });


                //присвоение координат при задание координат в инпутах
            $("#Shirota").change(function () {
                    SetCoordinates();
                }
              );
            
            $("#Dolgota").change(function () {
                    SetCoordinates();
                }
             );
            function SetCoordinates() {
                console.log(myPlacemark);
                //console.log(myPlacemark.geometry._ec[0]);
                //console.log(myPlacemark.geometry._ec[1]);
                myMap.geoObjects.remove(myPlacemark);
                //console.log($("#Shirota").val());
                //console.log($("#Dolgota").val());
                if ($("#Shirota").val() != undefined && $("#Dolgota").val() != undefined) {
                    myPlacemark = new ymaps.Placemark([$("#Shirota").val(), $("#Dolgota").val()],
                    {},
                    {
                        draggable: true
                    });
                    console.log(myPlacemark);
                    //console.log(myPlacemark.geometry._ec[0]);
                    //console.log(myPlacemark.geometry._ec[1]);
                    myMap.geoObjects.add(myPlacemark);
                }
            }
        });
    }
});