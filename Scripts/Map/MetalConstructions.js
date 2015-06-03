function GetMetalPoints(role, userCompany, objectId) {
    var geoObjects = new ymaps.GeoObjectCollection();
    $.ajax({
        type: "POST",
        url: "/Map/GetMetal",
        data: {
            owner: $("#owner option:selected").val(),
            locality: $("#locality").val(),
            street1: $("#street1").val(),
            street2: $("#street2").val(),
            fromStreet: $("#fromStreet").val(),
            startDay: $("#startDay option:selected").val(),
            startMonth: $("#startMonth option:selected").val(),
            startYear: $("#startYear").val(),
            id: parseInt(objectId)
        }
    }).success(function (data) {        
        if (data.length > 0) {
            var myMetalClusterer = new ymaps.Clusterer(
            {
                preset: 'islands#redClusterIcons',
                clusterDisableClickZoom: false
            });
            var opponentMetalClusterer = new ymaps.Clusterer(
            {
                preset: 'islands#blueClusterIcons',
                clusterDisableClickZoom: false
            });

            for (var i = 0; i < data.length; i++) {

                if (data[i].Owner == userCompany) {
                    var placemark = new ymaps.GeoObject(
                        {
                            geometry: {
                                type: "Point",
                                coordinates: [data[i].Shirota, data[i].Dolgota]
                            },
                            properties: {
                                clusterCaption: 'Металлический указатель',
                                balloonContentBody: '<b>Владелец:&nbsp;' + data[i].Owner
                                        + '</br>Улица 1:&nbsp;' + data[i].Street1
                                        + '</br>Улица 2:&nbsp' + data[i].Street2
                                        + '</br>Со стороны:&nbsp;' + data[i].FromStreet
                                        + '</br>Опора №:&nbsp;' + data[i].Support
                                        + '</br>Дата согласования:&nbsp' + data[i].StartDate
                                        + '</br><a href="/Data/Documents/' + data[i].Id + '?type=m" target="_blank">Документы</a></b>'
                                + getReferencesMetal(role, userCompany, data[i]),
                                iconContent: 'M',
                                hintContent: 'Металлический указатель'
                            }
                        },
                    {
                        preset: 'islands#redIcon'
                    });
                    myMetalClusterer.add(placemark);

                } else {
                    var placemark = new ymaps.Placemark(
                    [data[i].Shirota, data[i].Dolgota],
                    {
                        clusterCaption: 'Металлический указатель',
                        balloonContentBody: '<b>Владелец:&nbsp;' + data[i].Owner
                                + '</br>Улица 1:&nbsp;' + data[i].Street1
                                + '</br>Улица 2:&nbsp' + data[i].Street2
                                + '</br>Со стороны:&nbsp;' + data[i].FromStreet
                                + '</br>Опора №:&nbsp;' + data[i].Support
                                + '</br>Дата согласования:&nbsp' + data[i].StartDate
                                + '</br><a href="/Data/Documents/' + data[i].Id + '?type=m" target="_blank">Документы</a></b>'
                                + getReferencesMetal(role, userCompany, data[i]),
                        iconContent: 'M',
                        hintContent: 'Металлический указатель'
                    },
                    {
                        preset: 'islands#blueIcon'
                    });
                    opponentMetalClusterer.add(placemark);
                }
            }

            geoObjects.add(myMetalClusterer);
            geoObjects.add(opponentMetalClusterer);     
        }
    }).fail(function () {
        new Messi('Произошла ошибка при загрузке объектов из раздела "Металлические указатели".',
      { title: 'Ошибка', titleClass: 'anim error', buttons: [{ id: 0, label: 'Закрыть', val: 'X' }] });
    });
    return geoObjects;
}

function getReferencesMetal(role, userCompany, construction) {
    var result = "";
    if ((role == "Admin") || (role == "ChiefEditAll") || (role == "SupplierEditAll")) {
        result += '</br><a href="/Data/EditMetal/' + construction.Id + '" target="_blank">Изменить</a>'
              + '</br><a href="/Data/DeleteMetal/' + construction.Id + '" target="_blank">Удалить</a></b>';
    }
    if ((role == "ChiefEditOwn") || (role == "SupplierEditOwn")) {
        if (construction.Owner == userCompany) {
            result += '</br><a href="/Data/EditMetal/' + construction.Id + '" target="_blank">Изменить</a>'
                   + '</br><a href="/Data/DeleteMetal/' + construction.Id + '" target="_blank">Удалить</a></b>';
        }
    }
    if (!construction.HasPhoto) {
        result += '</br>Фото отсутствует';
    } else {
        result += '<br/><a href="/Images/Metal/p' + construction.Id + '.jpg" target="_blank"><img src = "/Images/Metal/p' + construction.Id + '.jpg" height = "180"></a>';
    }
    return result;
}
