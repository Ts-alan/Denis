function GetBilboardPoints(role, userCompany, objectId, myMap) {
    var StringBalun = "";
    var indexhouseSupport = "";
    var geoArray = [];
    var clusterer = new ymaps.Clusterer({ clusterDisableClickZoom: false });
    $.ajax({
        type: "POST",
        url: "/Map/GetDesign",
        data: {
            owner: $("#gs_Собственник").val(),
            UniqueNumber: $("#gs_Идентификационный_номер").val(),
            TypeOfAdvertisingStructure: $("#gs_Вид_конструкции").val(),
            Locality: $("#gs_Населенный_пункт").val(),
            Street1: $("#gs_Улица").val(),
            House1: $("#gs_Дом\\/Номер_опоры").val(),
            CountSize: $("#gs_Количество_сторон").val(),
            AreaConstruction: $("#gs_Площадь_конструкции").val(),
            CountSurface: $("#gs_Количестов_поверхностей").val(),
            Backlight: $("#gs_Подстветка").val(),
            ContractFinancialManagement: $("#gs_№_договора_с_фин_упр").val(),
            PassportAdvertising: $("#gs_№_паспорта_рекл").val(),
            EndDate: $("#gs_Разреш_по").val(),
            id: parseInt(objectId)
        }
    }).success(function (data) {
        var colour, bct, bht, houseSupport;
        if (data.length > 0)
        {

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
                    colour = '#0095b6';
                    indexhouseSupport = data[i].House1;
                }

                if (data[i].NameOfAdvertisingStructure == "Металлический указатель") {
                    bct = "У";
                    bht = "Указатель";
                    colour = '#0095b6';
                    houseSupport = '<b></br>Номер опоры:&nbsp;' + data[i].Support_ + "</b>";
                    indexhouseSupport = data[i].Support_;
                }
                if (data[i].NameOfAdvertisingStructure == "Световой короб") {
                    bct = "К";
                    bht = "Короб";
                    colour = '#0095b6';
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
        alert("Ошибка запроса ТРАТАТА");
    });
    
    return clusterer;
    
}

function getReferencesBillboard(construction) {
    var result = "";
    var editLink;
    var viewLink;
    if (construction.NameOfAdvertisingStructure == "Щит")
    {
        viewLink = "AdvertisingDesign/";
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





