   
    var myMap;
    var myPlacemark;
    var countE = true;
    ymaps.ready(init);
    var geoCoords;
    var changable = false;

    function onCityChange() {
        
        var myGeocoder = ymaps.geocode($("#Locality_id option:selected").text());
        myGeocoder.then(
            function (res)
            {
                geoCoords = res.geoObjects.get(0).geometry.getCoordinates();
                myMap.panTo([geoCoords[0], geoCoords[1]]);
            },
            function (err)
            {
                alert('Ошибка');
            }
        );
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
        myMap.controls.add('typeSelector');
        
        onCityChange();

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
                if (countE) {
                    
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
                    countE = false;
                }

            });
        }

        function dragend() {

            myPlacemark.events.add('dragend', function (e) {

                var coords = myPlacemark.geometry.getCoordinates();
                var str = String(coords[0].toPrecision(6));
                str = str.replace('.', ',');
                $("#Breadth").val(str);

                str = String(coords[1].toPrecision(6));
                str = str.replace('.', ',');
                
                $("#Height").val(str);
            });
        }

    }

   

    function SetCoordinates() {

        if ($("#Height").val() != "" && $("#Breadth").val() != "")
        {
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
        var countE = true;
        var streetPlacemark;
        
        $("#MapSearchStreet").click(function () {
            
            if ($("#Street1").val() != "") {
                if (myPlacemark != undefined)
                {
                    myMap.geoObjects.remove(myPlacemark);
                }
                
                var location = $("#Locality_id option:selected").text().toString();
                if ($("#Street1").val().toString() != "" && $("#House1").val().toString() == "");
                {
                    location += " " + $("#Street1").val().toString();
                }
                if ($("#House1").val().toString() != '' && $("#Street1").val().toString() != "");
                {
                    location += " " + $("#Street1").val().toString() + " " + $("House1").val();
                }
                var myGeocoder = ymaps.geocode(location);

                myGeocoder.then(
                    function (res) {

                        myPlacemark = new ymaps.Placemark([res.geoObjects.get(0).geometry._Rb[0], res.geoObjects.get(0).geometry._Rb[1]],
                        {},
                        {
                            draggable: false
                        });
                        
                        myMap.geoObjects.add(myPlacemark);
                    });
            }

            if (countE) {
                //присвоение  коодинат при клике  
                myMap.events.add('click', function (e)
                {
                    var coords = e.get('coordPosition');
                   
                    if (myPlacemark != undefined)
                    {
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
                countE = false;
            }
        });

        function dragend() {

            myPlacemark.events.add('dragend', function (e) {

                var coords = myPlacemark.geometry.getCoordinates();
                var str = String(coords[0].toPrecision(6));
                str = str.replace('.', ',');
                
                $("#Breadth").val(str);

                str = String(coords[1].toPrecision(6));
                str = str.replace('.', ',');
                
                $("#Height").val(str);
            });
        }

    });

    $(document).ready(function () {
        $("#Locality_id").change(function () {
            onCityChange();
        });
    });
 