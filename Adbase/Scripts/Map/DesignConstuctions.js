﻿function GetBilboardPoints(role, userCompany, objectId) {
    var geoObjects = new ymaps.GeoObjectCollection();
    console.log($("#gs_Разреш_по").val());
    $.ajax({
        type: "POST",
        url: "/Map/GetDesign",
        data: {
            owner: $("#gs_Собственник_конструкции").val(),
            UniqueNumber: $("#gs_Идентификационный_номер").val(),
            TypeOfAdvertisingStructure: $("#gs_Вид_конструкции").val(),
            Locality: $("#gs_Населенный_пункт").val(),
            Street1: $("#gs_Улица").val(),
            House1: $("#gs_Дом").val(),
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
                    colour = 'yellow';
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
                

                geoObjects.add(placemark);

            }
        }

    }).error(function () {

        alert("Ошибка запроса ТРАТАТА");
    });
    return geoObjects;
}

function getReferencesBillboard(construction) {
    var result = "";

    result += '</br><a href="/Data/Documents/' + construction.Id_show + '" >Показать данные о конструкции</a>'
        + '</br><a href="/Data/EditAdvertisingDesign/' + construction.Id_show + '" >Изменить данные конструкции</a>'
        + '</br><a href="#DeleteStructures"  role="button" data-toggle="modal" onclick="setRemove(' + construction.Id_show + ')"  >Удалить конструкцию</a></b>'

    //for (var i = 0; i < construction.Surfaces.length; i++) {
    //    result += '</br><a href="/Data/Documents/' + construction.Surfaces[i] + '?type=doc" >Документы</a>' 
    // +"</br><div>Социальная</div>" 
    //+"<div>" + construction.IsSocial[i]+"</div>"
    //    + '<br/><img src = "/Images/Billboard/surfaces/' + construction.Surfaces[i] + '.jpg" height = "180">';
    //};
    ;
    return result;
}





