function GetBilboardPoints(role, userCompany, objectId) {
    var geoArray = [];
    var clusterer = new ymaps.Clusterer({ clusterDisableClickZoom: true });
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
        var colour, bct, bht;
        if (data.length > 0) {

            for (var i = 0; i < data.length; i++) {
                if (data[i].NameOfAdvertisingStructure == "Щит")
                {
                    bct = "Щ";
                    bht = "Щит";
                    colour = '#0095b6';
                }

                if (data[i].NameOfAdvertisingStructure == "Металлический указатель") {
                    bct = "У";
                    bht = "Указатель";
                    colour = '#0095b6';
                }
                if (data[i].NameOfAdvertisingStructure == "Световой короб") {
                    bct = "К";
                    bht = "Короб";
                    colour = '#0095b6';
                }
                if (data[i].NameOfAdvertisingStructure == "Неопознанная конструкция") {
                    bct = "Н";
                    bht = "Неопознанная конструкция";
                    colour = '#0095b6';
                }
                    var placemark = new ymaps.GeoObject(
                 {
                     geometry: {
                         type: "Point",
                         coordinates: [data[i].Breadth, data[i].Height]
                     },
                     properties: {
                         balloonContentBody: '<b>Собственник:&nbsp;' + data[i].OwnerName
                                    + '</br>Вид конструкции:&nbsp;' + data[i].NameOfAdvertisingStructure
                                 + '</br>Улица 1:&nbsp' + data[i].Street1
                                 + '</br>Дом:&nbsp;' + data[i].House1

                         + getReferencesBillboard(data[i]),
                         iconContent: bct,
                         hintContent: bht
                     }

                 },
                 {
                     preset: 'islands#redClusterIcons',
                     iconColor: colour
                 });
                
                geoArray[i] = placemark;
                
            }

            clusterer.add(geoArray);

        }

    }).error(function () {
        alert("Ошибка запроса ТРАТАТА");
    });
    
    return clusterer;
    
}

function getReferencesBillboard(construction) {
    var result = "";
    var editLink;
    if (construction.NameOfAdvertisingStructure == "Щит") {


        editLink = "/Data/EditAdvertisingDesign/";
    }

    if (construction.NameOfAdvertisingStructure == "Металлический указатель")
    {
        editLink = "/Data/EditMetalPointerDesign/";
    }

    if (construction.NameOfAdvertisingStructure == "Световой короб")
    {
        editLink = "/Data/EditLightDuctDesign/";
    }
    console.log(construction);
    result += '</br><a href="/Data/Documents/' + construction.Id_show + '" >Показать данные о конструкции</a>'
        + '</br><a href="'+ editLink + construction.Id_show + '" >Изменить данные конструкции</a>'
        + '</br><a href="#DeleteStructures"  role="button" data-toggle="modal" onclick="setRemove(' + construction.Id_show + ')"  >Удалить конструкцию</a></b>'

 
    ;
    return result;
}





