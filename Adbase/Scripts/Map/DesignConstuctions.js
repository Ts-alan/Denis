function GetBilboardPoints(role, userCompany, objectId, myMap) {
    var StringBalun = "";
    var indexhouseSupport = "";
    var geoArray = [];
    var clusterer = new ymaps.Clusterer({ clusterDisableClickZoom: false });
    $.ajax({
        type: "POST",
        url: "/Map/GetDesign",
        data: {
            owner: $("#gs_Собственник_конструкции").val(),
            UniqueNumber: $("#gs_Идентификационный_номер").val(),
            TypeOfAdvertisingStructure: $("#gs_Вид_конструкции").val(),
            Locality: $("#gs_Населенный_пункт").val(),
            Street1: $("#gs_Улица").val(),
            house1: $("#gs_Дом").val(),
            support: $("#gs_Номер_опоры").val(),
            CountSize: $("#gs_Количество_сторон").val(),
            AreaConstruction: $("#gs_Площадь_конструкции").val(),
            CountSurface: $("#gs_Количестов_поверхностей").val(),
            Backlight: $("#gs_Подстветка").val(),
            ContractFinancialManagement: $("#gs_№_договора_с_фин_упр").val(),
            PassportAdvertising: $("#gs_№_паспорта_рекл").val(),
            startStartDate: $("#gs_beginingStartDateRange").val(),
            endStartDate: $("#gs_endingStartDateRange").val(),
            startEndDate: $("#gs_beginingEndDateRange").val(),
            endEndDate: $("#gs_endingEndDateRange").val(),
            id: parseInt(objectId)
        }
    }).success(function (data) {
        var colour, bct, bht, houseSupport;
        if (data.length > 0)
        {
            var construction;
            for (var i = 0; i < data.length; i++)
            {
                construction = data[i];

                if (construction.Breadth == null | construction.Height == null)
                {
                    continue;
                }
                if (construction.NameOfAdvertisingStructure == "Щит")
                {
                    bct = "Щ";
                    bht = "Щит";
                    houseSupport = '<b></br>Дом:&nbsp;' + construction.House1 + "</b>";
                    //colour = '#0095b6';
                    colour = "yellow";
                    indexhouseSupport = construction.House1;
                }

                if (construction.NameOfAdvertisingStructure == "Металлический указатель") {
                    bct = "У";
                    bht = "Указатель";
                    //colour = '#0095b6';
                    colour = "blue";
                    houseSupport = '<b></br>Номер опоры:&nbsp;' + construction.Support_ + "</b>";
                    indexhouseSupport = construction.Support_;
                }
                if (construction.NameOfAdvertisingStructure == "Световой короб") {
                    bct = "К";
                    bht = "Короб";
                    //colour = '#0095b6';
                    colour = "green";
                    houseSupport = '<b></br>Номер опоры:&nbsp;' + construction.Support_ + "</b>";
                    indexhouseSupport = construction.Support_;
                  
                    
                }
                if (construction.NameOfAdvertisingStructure == "Неопознанная конструкция") {
                    bct = "Н";
                    bht = "Неопознанная конструкция";
                    colour = 'red';
                    houseSupport = '<b></br>Номер дома:&nbsp;' + construction.House1+"</b>";
                    indexhouseSupport = construction.House1;
                }
           
                if (construction.OwnerName != null) {
                   
                    StringBalun += '<b>Собственник:&nbsp;' + construction.OwnerName+"<b>";
                }
                if (construction.NameOfAdvertisingStructure!=null) {
                    StringBalun += '<b></br>Вид конструкции:&nbsp;' + construction.NameOfAdvertisingStructure+"</b>";
                }
                if (construction.Street1!=null) {
                    StringBalun += '<b></br>Улица :&nbsp' + construction.Street1+"</b>";
                }
                if (indexhouseSupport != null) {
                    StringBalun += houseSupport;
                }
                StringBalun += getReferencesBillboard(construction);
               

                var placemark = new ymaps.GeoObject(
                 {
                     geometry: {
                         type: "Point",
                         coordinates: [construction.Breadth, construction.Height]
                     },
                     properties: {
                         balloonContentBody: StringBalun,

                         iconContent: bct,
                         hintContent: bht
                     }

                 },
                 {
                     preset: 'islands#redClusterIcons',
                     iconColor: colour
                 });
             

                placemark.events.add('balloonopen', function(e)
                {
                    var contentString = e.get('target').properties._data.balloonContentBody;
                    var slashIndexes = [], quoteIndexes = [], i = -1;
                    for (i = 0; i < contentString.length; i++)
                    {
                        if (contentString[i] === "/")
                        {
                            slashIndexes.push(i);
                        }
                        if (contentString[i] === "'")
                        {
                            quoteIndexes.push(i);
                        }
                    }
                    
                    var id = contentString.substring( quoteIndexes[quoteIndexes.length - 1] + 2, slashIndexes[slashIndexes.length - 1]-1);
                    
                    var n = ~~Number(id);
                    
                    if (String(n) === id && n >= 0)
                    {
                        var img = "<div id='gifContainer'><img src='/Images/ajax-loader.gif' style='display: block; margin-left: auto;margin-right: auto;'></div>";
                        
                        $.ajax({
                            type: "GET",
                            url: "/Map/FindPictures/" + id,
                            async: true,
                            timeout: 10000,
                            //beforeSend: function ()
                            //{
                            //    $("[class*='balloon__content'] > ymaps > ymaps").append(img);
                            //    e.get('target').properties._data.balloonContentBody += img;
                            //    $("[class*='balloon__content']").parent().show();
                            //},
                            error: (function()
                            {
                                //e.get('target').properties._data.balloonContentBody += '<div><h4>Не удалось загрузить изображение</h4></div>';
                                $("#gifContainer").remove();
                                e.get('target').properties._data.balloonContentBody = e.get('target').properties._data.balloonContentBody.replace(img, "");
                            })
                        }).success(function(data)
                        {
                            $("[class*='balloon__content'] > ymaps > ymaps").append(data);
                            e.get('target').properties._data.balloonContentBody += data;
                            $("#gifContainer").remove();
                            e.get('target').properties._data.balloonContentBody = e.get('target').properties._data.balloonContentBody.replace(img, '');
                             console.log("");
                        });
                        //e.get('target').properties._data.balloonContentBody += "<img src='/Images/photo1/[1](1)photo101390.jpg' height='300' style='display: block; margin-left: auto;margin-right: auto;'>";
                    }
                });
                geoArray[i] = placemark;
                StringBalun = "";
            }

            clusterer.add(geoArray);
            if (geoArray.length == 1)
            {
                myMap.panTo([[data[0].Breadth, data[0].Height]]);
            }
        }
        $("#hidden").remove();
    }).error(function () {
        alert("Ошибка запроса");
    });
    
    return clusterer;
    
}

function getReferencesBillboard(construction) {
    var result = "<div>";
    var editLink;
    var viewLink;
    if (construction.NameOfAdvertisingStructure == "Щит")
    {
        viewLink = "Documents/";
        editLink = "/Data/EditAdvertisingDesign/";
    }

    if (construction.NameOfAdvertisingStructure == "Металлический указатель")
    {
        editLink = "/Data/EditMetalPointerDesign/";
         viewLink = "Pointer/";
    }

    if (construction.NameOfAdvertisingStructure == "Световой короб")
    {
        editLink = "/Data/EditLightDuctDesign/";
         viewLink = "LightDuct/";
    }
    if (construction.NameOfAdvertisingStructure == "Неопознанная конструкция")
    {
        editLink = "/Data/EditIllegalDesign/";
        viewLink = "Illegal/";
    }
    result += '</br><a href="/Data/' + viewLink + construction.Id_show + '" >Показать данные о конструкции</a>'
        + '</br><a href="' + editLink + construction.Id_show + '" >Изменить данные конструкции</a>'
        + '</br><a href="/Data/DeleteAdvertisingDesign/' + construction.Id_show + '" >Удалить конструкцию</a></b></div>'
        + "<div id='gifContainer'><img src='/Images/ajax-loader.gif' style='display: block; margin-left: auto;margin-right: auto;'></div>"
        + "<div style='display:none;'>" + construction.Id_show + "</div>";
    
    return result;
}