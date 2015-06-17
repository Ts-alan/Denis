function GetLightPoints(role, userCompany, objectId) {
    var geoObjects = new ymaps.GeoObjectCollection();
    $.ajax({
        type: "POST",
        url: "/Map/GetLights",
        data: {
            owner: $("#owner option:selected").val(),
            locality: $("#locality").val(),
            street1: $("#street1").val(),
            street2: $("#street2").val(),
            fromStreet: $("#fromStreet").val(),
            startDay: $("#startDay option:selected").val(),
            startMonth: $("#startMonth option:selected").val(),
            startYear: $("#startYear").val(),
            lFinishDay: $("#lFinishDay option:selected").val(),
            lFinishMonth: $("#lFinishMonth option:selected").val(),
            lFinishYear: $("#lFinishYear").val(),
            onStatement: $("#onStatement option:selected").val(),
            isSocial: $("#isSocial option:selected").val(),
            id: parseInt(objectId)
        }
    }).success(function (data) {
        if (data.length > 0) {
            var myLightClusterer = new ymaps.Clusterer(
            {
                preset: 'islands#invertedRedClusterIcons',
                clusterDisableClickZoom: false
            });
            var opponentLightClusterer = new ymaps.Clusterer(
            {
                preset: 'islands#invertedBlueClusterIcons',
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
                                clusterCaption: 'Световой короб',
                                balloonContentBody: data[i].OnStatement ? getBaloonForOnStatement(role, userCompany, data[i]) : getBaloon(role, userCompany, data[i]),
                                iconContent: data[i].OnStatement ? "З" : "C",
                                hintContent: 'Световой короб'
                            }
                        },
                    {
                        preset: 'islands#redIcon'
                    });
                    myLightClusterer.add(placemark);

                } else {
                    var placemark = new ymaps.Placemark(
                    [data[i].Shirota, data[i].Dolgota],
                    {
                        clusterCaption: 'Световой короб',
                        balloonContentBody: data[i].OnStatement ? getBaloonForOnStatement(role, userCompany, data[i]) : getBaloon(role, userCompany, data[i]),
                        iconContent: data[i].OnStatement ? "З" : "C",
                        hintContent: 'Световой короб'
                    },
                    {
                        preset: 'islands#blueIcon'
                    });
                    opponentLightClusterer.add(placemark);
                }
            }
            geoObjects.add(myLightClusterer);
            geoObjects.add(opponentLightClusterer);      
        }
    }).fail(function () {
        new Messi('Произошла ошибка при загрузке объектов из раздела "Световые указатели".',
      { title: 'Ошибка', titleClass: 'anim error', buttons: [{ id: 0, label: 'Закрыть', val: 'X' }] });
    });
    return geoObjects;
}

function getReferencesLight(role, userCompany, construction) {
    var result = "";
    if ((role == "Admin") || (role == "ChiefEditAll") || (role == "SupplierEditAll")) {
        result += '</br><a href="/Data/EditLight/' + construction.Id + '" target="_blank">Изменить</a>'
               + '</br><a href="/Data/DeleteLight/' + construction.Id + '" target="_blank">Удалить</a></b>';
    }
    if ((role == "ChiefEditOwn") || (role == "SupplierEdotOwn")) {
        if (construction.Owner == userCompany) {
            result += '</br><a href="/Data/EditLight/' + construction.Id + '" target="_blank">Изменить</a>'
                  + '</br><a href="/Data/DeleteLight/' + construction.Id + '" target="_blank">Удалить</a></b>';
        }
    }
    if (construction.HasPhoto) {
        result += '<br/><a href="/Images/Light/photo' + construction.Id + '.jpg"><img src = "/Images/Light/photo' + informationLight[i][9] + '.jpg" height = "180"></a>';
    }
    else {
        result += '</br>Фото отсутствует';
    }
    return result;
}

function getReferencesLightOnStatement(role, userCompany, construction)
{
    if ((role == "Admin") || (role == "ChiefEditAll") || (role == "SupplierEditAll")) {
        return '</br><a href="/Data/AddPassport/' + construction.Id + '" target="_blank">В согласованные</a>'
               + '</br><a href="/Data/EditLight/' + construction.Id + '" target="_blank">Изменить</a>'
               + '</br><a href="/Data/DeleteLight/' + construction.Id + '" target="_blank">Удалить</a></b>';
    }
    if ((role == "ChiefEditOwn") || (role == "SupplierEdotOwn")) {
        if (construction.Owner == userCompany) {
            return '</br><a href="../Data/AddPassport/' + construction.Id + '" target="_blank">В согласованные</a>'
                + '</br><a href="/Data/EditLight/' + construction.Id + '" target="_blank">Изменить</a>'
                + '</br><a href="/Data/DeleteLight/' + construction.Id + '" target="_blank">Удалить</a></b>';
        }
        else return '';
    }
    if (role == "ReadOnly") {
        return '';
    }
}

function getBaloon(role, userCompany, construction) {
    return '<b>Собственник:&nbsp;' + construction.Owner
           + '</br>Улица 1:&nbsp;' + construction.Street1
           + '</br>Улица 2:&nbsp;' + construction.Street2
           + '</br>Со стороны:&nbsp;' + construction.FromStreet
           + '</br>Опора №:&nbsp;' + construction.Support
           + '</br>Разрешено с:&nbsp;' + construction.StartDate
           + '</br>Разрешено до:&nbsp;' + construction.FinishDate
           + '</br>' + construction.IsSocial
           + '</br><a href="/Data/Documents/' + construction.Id + '?type=l" target="_blank">Документы</a>'
    + getReferencesLight(role, userCompany, construction)
}

function getBaloonForOnStatement(role, userCompany, construction) {
    return '<b>Собственник:&nbsp;' + construction.Owner
           + '</br>Улица 1:&nbsp;' + construction.Street1
           + '</br>Улица 2:&nbsp;' + construction.Street2
           + '</br>Со стороны:&nbsp;' + construction.FromStreet
           + '</br>Опора №:&nbsp;' + construction.Support
           + '</br>Заявление подано:&nbsp;' + construction.StartDate
           + '</br><a href="/Data/Documents/' + construction.Id + '?type=ls" target="_blank">Документы</a>'
    + getReferencesLightOnStatement(role, userCompany, construction)
}