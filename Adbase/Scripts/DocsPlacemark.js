
var myMap;
var myPlacemark;
var countE = true;
ymaps.ready(init);
var geoCoords;
var changable = false;

function onCityChange()
{
   
    var loc;
    if ($("input#Locality_NameLocality").val() == "Константиново")
    {
        loc = $("input#Locality_NameLocality").val() + " Беларусь Минская область";
    }
    else
    {
        loc = $("input#Locality_NameLocality").val();
    }
    if ($("input#Locality_NameLocality").val() == "Минский район")
    {
        myMap.setZoom(10);
        myMap.panTo([53.9172, 27.5601]);
        return;
    }
    var myGeocoder = ymaps.geocode(loc);
    myGeocoder.then(
        function (res)
        {
            myMap.setZoom(11);
            geoCoords = res.geoObjects.get(0).geometry.getCoordinates();
            myMap.panTo([geoCoords[0], geoCoords[1]]);
        },
        function (err)
        {
            alert('Ошибка');
        }
    );
}

function init()
{

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

    if ($("#coordB").val() != "" && $("#coordH").val() != "")
    {
        // нанесение обьекта на карту при загрузке
        myPlacemark = new ymaps.Placemark([$("#coordB").val().replace(",", "."), $("#coordH").val().replace(',', '.')],
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
        myMap.events.add('click', function (e)
        {
            if (countE)
            {

                if (myPlacemark != undefined)
                {
                    myMap.geoObjects.remove(myPlacemark);
                }
                var coords = e.get('coordPosition');
                var str = String(coords[0].toPrecision(6).replace('.', ','));
                $("#coordB").val(str);
                str = String(coords[1].toPrecision(6).replace('.', ','));
                $("#coordH").val(str);
                myPlacemark = new ymaps.Placemark([coords[0], coords[1]], {}, {
                    draggable: true
                });
                myMap.geoObjects.add(myPlacemark);
                dragend();
                countE = false;
            }

        });
    }

    function dragend()
    {

        myPlacemark.events.add('dragend', function (e)
        {

            var coords = myPlacemark.geometry.getCoordinates();
            var str = String(coords[0].toPrecision(6));
            str = str.replace('.', ',');
            $("#coordB").val(str);

            str = String(coords[1].toPrecision(6));
            str = str.replace('.', ',');

            $("#coordH").val(str);
        });
    }
}



function SetCoordinates()
{

    if ($("#coordH").val() != "" && $("#coordB").val() != "")
    {
        //удалить обьект
        if (myPlacemark != undefined)
        {

            myMap.geoObjects.remove(myPlacemark);
        }
        //присвоить обьект
        myPlacemark = new ymaps.Placemark([$("#coordH").val(), $("#coordB").val()],
        {},
        {
            draggable: true
        });
        myMap.geoObjects.add(myPlacemark);

    }
    dragend();
}



$(document).ready(function ()
{
    var countE = true;

    $("#MapSearchStreet").click(function ()
    {

        if (!$("#Street1").val().trim())
        {
            alert("Введите название улицы!");
            return;
        }
        else
        {
            if (myPlacemark != undefined)
            {
                myMap.geoObjects.remove(myPlacemark);
            }

            var location = $("input#Locality_NameLocality").val().toString();
            var strLen;
            var h1Len;
            if ($("#Street1").val() != undefined)
            {
                strLen = $("#Street1").val().length;
            }
            if ($("input#House1").val())
            {
                h1Len = $("input#House1").val().length;
            }

            if (strLen != 0 && h1Len == 0)
            {
                location += " " + $("#Street1").val().toString() + "  ";
            }
            if (strLen != 0 && h1Len != 0)
            {
                location += $("#Street1").val() + "  " + " дом " + $("input#House1").val();
            }

            var myGeocoder = ymaps.geocode(location);

            myGeocoder.then(
                function (res)
                {

                    myPlacemark = new ymaps.Placemark([res.geoObjects.get(0).geometry._Rb[0], res.geoObjects.get(0).geometry._Rb[1]],
                    {},
                    {
                        draggable: false
                    });
                    myMap.setZoom(14);
                    myMap.geoObjects.add(myPlacemark);
                    myMap.panTo([res.geoObjects.get(0).geometry._Rb[0], res.geoObjects.get(0).geometry._Rb[1]]);

                });

        }

        if (countE)
        {
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
                $("#coordB").val(str);
                str = String(coords[1].toPrecision(6).replace('.', ','));
                $("#coordH").val(str);

                dragend();
            });
            countE = false;
        }
    });

    function dragend()
    {

        myPlacemark.events.add('dragend', function (e)
        {

            var coords = myPlacemark.geometry.getCoordinates();
            var str = String(coords[0].toPrecision(6));
            str = str.replace('.', ',');

            $("#coordB").val(str);

            str = String(coords[1].toPrecision(6));
            str = str.replace('.', ',');

            $("#coordH").val(str);
        });
    }

});

$(document).ready(function ()
{
    $("#Locality_id").change(function ()
    {
        onCityChange();
    });
});
