
    
    var myMap;
    var myPlacemark;
    var havePlacemark = false;
    var isGreateAdvertisingDesign = true;
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
            $("#Breadth").val(str);
            
            str = String(coords[1].toPrecision(6));
            str = str.replace('.', ',');
            //if (!isNaN(str)) {
            //    $("#Dolgota").val(str);
            //} else {
            //    $("#Dolgota").val("");
            //} 
            $("#Height").val(str);
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

   

        if (isGreateAdvertisingDesign) {
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
        } else {
            // нанесение обьекта на карту при загрузке
            myPlacemark = new ymaps.Placemark([$("#Breadth").val().replace(",","."), $("#Height").val().replace(',', '.')],
           {},
           {
               draggable: true
           });
            myMap.geoObjects.add(myPlacemark);
            dragend();
        }

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

