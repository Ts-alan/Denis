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
        if (data.length > 0) {

            
            function imageExists(image_url) {

                var http = new XMLHttpRequest();

                http.open('HEAD', image_url, false);
                http.send();

                return http.status != 404;

            }
            
            for (var i = 0; i < data.length; i++)
            {
                
               
               
                if (data[i].Breadth == null | data[i].Height == null)
                {
                    continue;
                }
                if (data[i].NameOfAdvertisingStructure == "Щит")
                {
                    bct = "Щ";
                    bht = "Щит";
                    houseSupport = '<b></br>Дом:&nbsp;' + data[i].House1 + "</b>";
                    //colour = '#0095b6';
                    colour = "yellow";
                    indexhouseSupport = data[i].House1;
                }

                if (data[i].NameOfAdvertisingStructure == "Металлический указатель") {
                    bct = "У";
                    bht = "Указатель";
                    //colour = '#0095b6';
                    colour = "blue";
                    houseSupport = '<b></br>Номер опоры:&nbsp;' + data[i].Support_ + "</b>";
                    indexhouseSupport = data[i].Support_;
                }
                if (data[i].NameOfAdvertisingStructure == "Световой короб") {
                    bct = "К";
                    bht = "Короб";
                    //colour = '#0095b6';
                    colour = "green";
                    houseSupport = '<b></br>Номер опоры:&nbsp;' + data[i].Support_ + "</b>";
                    indexhouseSupport = data[i].Support_;
                  
                    
                }
                if (data[i].NameOfAdvertisingStructure == "Неопознанная конструкция") {
                    bct = "Н";
                    bht = "Неопознанная конструкция";
                    colour = 'red';
                    houseSupport = '<b></br>Номер дома:&nbsp;' + data[i].House1+"</b>";
                    indexhouseSupport = data[i].House1;
                }
           
                if (data[i].OwnerName != null) {
                   
                    StringBalun += '<b>Собственник:&nbsp;' + data[i].OwnerName+"<b>";
                }
                if (data[i].NameOfAdvertisingStructure!=null) {
                    StringBalun += '<b></br>Вид конструкции:&nbsp;' + data[i].NameOfAdvertisingStructure+"</b>";
                }
                if (data[i].Street1!=null) {
                    StringBalun += '<b></br>Улица :&nbsp' + data[i].Street1+"</b>";
                }
                if (indexhouseSupport != null) {
                    StringBalun += houseSupport;
                }
                StringBalun += getReferencesBillboard(data[i]);




                //if (data[i].NameOfAdvertisingStructure == "Световой короб") {
   

                //if (imageExists("/Images/photo1/[1](1)photo" + data[0].Id_show + ".jpg")) {
                //        StringBalun += "<br>Cторона 1";
                //        StringBalun += "<br><img  style='witdh:150px;height:150px' src='/Images/photo1/[1](1)photo" + data[0].Id_show + ".jpg' >";
                //};
                //if (imageExists("/Images/photo1/[2](1)photo" + data[0].Id_show + ".jpg")) {
                //    StringBalun += "<br>Cторона 2";
                //    StringBalun += "<br><img  style='witdh:150px;height:150px' src='/Images/photo1/[2](1)photo" + data[0].Id_show + ".jpg' >";
                //};

                //}
                //if (data[i].NameOfAdvertisingStructure == "Неопознанная конструкция") {
                //    if (imageExists("/Images/photo1/" + data[0].Id_show + "photo1.jpg")) {
                //       StringBalun += "<br>Фотография 1";
                //       StringBalun += "<br><img  style='witdh:150px;height:150px' src='/Images/photo1/" + data[0].Id_show + "photo1.jpg' >";
                //    };
                //    if (imageExists("/Images/photo2/" + data[0].Id_show + "photo2.jpg")) {
                //        StringBalun += "<br>Фотография 2";
                //        StringBalun += "<br><img  style='witdh:150px;height:150px' src='/Images/photo2/" + data[0].Id_show + "photo2.jpg' >";
                //    };
                //}
               
                //if (data[i].NameOfAdvertisingStructure == "Щит") {
                //    StringBalun += '<br>  <div id="carousel-example-generic" class="carousel slide" data-ride="carousel">' +
                //        '<ol class="carousel-indicators" ></ol>' +
                //        '<div class="carousel-inner" role="listbox">';
                //    var firstelement = true;
                //    for (var j = 1; j < 9; j++) {
                //        for (var z = 1; z < 9; z++) {

                //            if (imageExists("/Images/photo1/[" + z + "](" + j + ")photo" + data[0].Id_show + ".jpg")) {

                //                if (firstelement) {
                //                    StringBalun += '<div class="item active">' +
                //                        "<img  style='witdh:150px;height:150px' src='/Images/photo1/[" + z + "](" + j + ")photo" + data[0].Id_show + ".jpg' >" +
                //                        '</div>';
                //                    firstelement = false;
                //                } else {
                //                    StringBalun += '<div class="item ">' +
                //                        "<img  style='witdh:150px;height:150px' src='/Images/photo1/[" + z + "](" + j + ")photo" + data[0].Id_show + ".jpg' >" +
                //                        '</div>';
                //                }
                //            }
                //        }
                //    }
                  
                //    StringBalun +=
                        
                  
                //    '</div>'+
                //    '<a class="left carousel-control" href="#carousel-example-generic" role="button" data-slide="prev">'+
                //        '<span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>'+
                //        '<span class="sr-only">Previous</span>'+
                //    '</a>'+
                //    '<a class="right carousel-control" href="#carousel-example-generic" role="button" data-slide="next">'+
                //        '<span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span>'+
                //        '<span class="sr-only">Next</span>'+
                //    '</a>'+
                //    '</div>';
                //}
                //if (data[i].NameOfAdvertisingStructure == "Металлический указатель") {
                //    if (imageExists("/Images/photo1/" + data[0].Id_show + "photo1.jpg")) {
                //        StringBalun += "<br>Cторона 1";
                //        StringBalun += "<br><img  style='witdh:150px;height:150px' src='/Images/photo1/" + data[0].Id_show + "photo1.jpg' >";
                //    };
                //}

                var placemark = new ymaps.GeoObject(
                 {
                     geometry: {
                         type: "Point",
                         coordinates: [data[i].Breadth, data[i].Height]
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
                        console.log(e.get('target').properties);
                        //console.log(e.get('target'));
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
    var result = "";
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
        + '</br><a href="'+ editLink + construction.Id_show + '" >Изменить данные конструкции</a>'
        + '</br><a href="/Data/DeleteAdvertisingDesign/' + construction.Id_show + '" >Удалить конструкцию</a></b>'

 
    ;
    return result;
}





