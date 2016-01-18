   
    var myMap;
    var myPlacemark;
    var havePlacemark = false;
    var isGreateAdvertisingDesign = true;
    var isdragend = true;
    var count = true;
    ymaps.ready(init);
   
    ////присвоение коодинат при перетаскивание метки 
    //function dragend() {

    //    myPlacemark.events.add('dragend', function (e) {

    //        var coords = myPlacemark.geometry.getCoordinates();
    //        var str = String(coords[0].toPrecision(6));
    //        str = str.replace('.', ',');
    //        //if (!isNaN(str)) {
    //        //    $("#Shirota").val(str);
    //        //} else {
    //        //    $("#Shirota").val("");
    //        //}
    //        $("#Breadth").val(str);

    //        str = String(coords[1].toPrecision(6));
    //        str = str.replace('.', ',');
    //        //if (!isNaN(str)) {
    //        //    $("#Dolgota").val(str);
    //        //} else {
    //        //    $("#Dolgota").val("");
    //        //} 
    //        $("#Height").val(str);
    //    });
    //}


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


        if ($("#Breadth").val() != "" && $("#Height").val() != "") {
            // нанесение обьекта на карту при загрузке
            myPlacemark = new ymaps.Placemark([$("#Breadth").val().replace(",", "."), $("#Height").val().replace(',', '.')],
            {},
            {
                draggable: true
            });
            myMap.geoObjects.add(myPlacemark);

            dragend();
        }
        else
        {
            //присвоение  коодинат при клике  
            myMap.events.add('click', function (e) {
                if (count) {

                    if (myPlacemark != undefined)
                    {
                        myMap.geoObjects.remove(myPlacemark);
                    }
                    var coords = e.get('coordPosition');
                    var str = String(coords[0].toPrecision(6).replace('.', ','));
                    $("#Breadth").val(str);
                    str = String(coords[1].toPrecision(6).replace('.', ','));
                    $("#Height").val(str);
                    myPlacemark = new ymaps.Placemark([coords[0], coords[1]], {}, {
                        draggable: true
                    });
                    myMap.geoObjects.add(myPlacemark);
                    dragend();
                    count = false;

                }

            });
        }

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



    $(document).ready(function () {
        var count = true;
        var streetPlacemark;
        //var myPlacemark;
        $("#MapSearchStreet").click(function () {
            var text;

            if ($("#Street1").val() != "") {
                if (myPlacemark != undefined)
                {
                    myMap.geoObjects.remove(myPlacemark);
                }

                var location = $("#Locality_id").val().toString() + " " + $("#Street1").val().toString();
                var myGeocoder = ymaps.geocode(location);

                myGeocoder.then(
                    function (res) {
                        //console.log(res.geoObjects.get(0));
                        //console.log(res.geoObjects.get(0).geometry);

                        myPlacemark = new ymaps.Placemark([res.geoObjects.get(0).geometry._Rb[0], res.geoObjects.get(0).geometry._Rb[1]],
                        {},
                        {
                            draggable: false
                        });
                        //console.log(myPlacemark);
                        myMap.geoObjects.add(myPlacemark);
                    });
            }

            if (count) {
                //присвоение  коодинат при клике  
                myMap.events.add('click', function (e) {
                    var coords = e.get('coordPosition');
                    console.log(coords);
                    if (myPlacemark != undefined) {
                        myMap.geoObjects.remove(myPlacemark);
                    }

                    myPlacemark = new ymaps.Placemark([coords[0].toPrecision(6), coords[1].toPrecision(6)],
                    {},
                    {
                        draggable: true
                    });
                    myMap.geoObjects.add(myPlacemark);

                    var str = String(coords[0].toPrecision(6).replace('.', ','));
                    $("#Breadth").val(str);
                    str = String(coords[1].toPrecision(6).replace('.', ','));
                    $("#Height").val(str);

                    dragend();
                });
                count = false;
            }
        });

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

    });

    