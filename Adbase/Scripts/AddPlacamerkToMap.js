
    
    var myMap;
    var myPlacemark;
    var havePlacemark = false;
    ymaps.ready(init);

    //присвоение коодинат при перетаскивание метки 
    function dragend() {

        myPlacemark.events.add('dragend', function (e) {

            var coords = myPlacemark.geometry.getCoordinates();
            var str = String(coords[0].toPrecision(6));
            str = str.replace('.', ',');
            //if (!isNaN(str)) {
            //    $("#Shirota").val(str);
            //} else {
            //    $("#Shirota").val("");
            //}
            $("#Height").val(str);
            str = String(coords[1].toPrecision(6));
            str = str.replace('.', ',');
            //if (!isNaN(str)) {
            //    $("#Dolgota").val(str);
            //} else {
            //    $("#Dolgota").val("");
            //} 
            $("#Breadth").val(str);
        });
    }

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
        myMap.events.add('click', function(e) {
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


            var str = String(coords[0].toPrecision(6).replace('.', ','));
            $("#Breadth").val(str);
            str = String(coords[1].toPrecision(6).replace('.', ','));
            $("#Height").val(str);
            
            dragend();
        });

        //присвоение координат при задание координат в инпутах

    }

    function SetCoordinates() {

        if ($("#Height").val() != "" && $("#Breadth").val() != "") {
            //удалить обьект
            if (myPlacemark != undefined) {

                myMap.geoObjects.remove(myPlacemark);
            }
            //присвоить обьект
            myPlacemark = new ymaps.Placemark([$("#Height").val(), $("#Breadth").val()],
            {},
            {
                draggable: true
            });
        myMap.geoObjects.add(myPlacemark);
            
        }
        dragend();
    }

