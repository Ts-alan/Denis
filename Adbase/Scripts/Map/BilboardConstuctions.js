function GetBilboardPoints(role, objectId) {
    var geoObjects = new ymaps.GeoObjectCollection();
    $.ajax({
        type: "POST",
        url: "/Map/GetBilboard",
        data: {
            owner: $("#owner option:selected").val(),
            locality: $("#locality").val(),
            street1: $("#street1").val(),
            street2: $("#street2").val(),
            fromStreet: $("#fromStreet").val(),
            startDay: $("#startDay option:selected").val(),
            startMonth: $("#startMonth option:selected").val(),
            startYear: $("#startYear").val(),
            //lBillboardFinishDay: $("#lBillboardFinishDay option:selected").val(),
            //lBillboardFinishMonth: $("#lBillboardFinishMonth option:selected").val(),
            //lBillboardFinishYear: $("#lBillboardFinishYear").val(),
            //Story: $("#Story option:selected").val(),
            //OnAgreement: $("#OnAgreement option:selected").val(),
            //IsBillboardSocial: $("#IsBillboardSocial option:selected").val(),
            id: parseInt(objectId)
        }
    }).success(function (data) {
        alert("всё гуд");
       if (data.length > 0) {
            //var myBilboardClusterer = new ymaps.Clusterer(
            //{
            //    preset: 'islands#redClusterIcons',
            //    clusterDisableClickZoom: false
            //});
            //var opponentBilboardClusterer = new ymaps.Clusterer(
            //{
            //    preset: 'islands#blueClusterIcons',
            //    clusterDisableClickZoom: false
            //});
            for (var i = 0; i < data.length; i++) {

                
                    var placemark = new ymaps.GeoObject(
                    {
                        geometry: {
                            type: "Point",
                            coordinates: [data[i].Height, data[i].Breadth]
                        },
                        properties: {
                            balloonContentBody: '<b>Владелец:&nbsp;' + data[i].OwnerName
                                    + '</br>Улица 1:&nbsp' + data[i].Street1
                                    + '</br>Улица 2:&nbsp;' + data[i].Street2
                                     + '</br>Со стороны:&nbsp;' + data[i].FromStreet
                            + getReferencesBillboard(data[i]),
                            iconContent: 'Б',
                            hintContent: 'Билборд'
                        }

                    },
                    {
                        preset: 'islands#redClusterIcons'
                    });

                    geoObjects.add(placemark);
                }
            }
        
    }).error(function () {
      
        alert("Ошибка запроса");
    });
    return geoObjects;
}

function getReferencesBillboard(construction) {
    var result = "";
    var several = "";
    for (var i = 0; i < construction.count; i++) {
        several +='<br/><img src = "/Images/Billboard/surfaces/' + construction.Id + '.jpg" height = "180">';
    };
    result += '</br><a href="/Data/EditBillboard/' + construction.Id + '" target="_blank">Изменить</a>'
        + '</br><a href="/Data/DeleteBilboard/' + construction.Id + '" target="_blank">Удалить</a></b>'
        + several;
    ;
    return result;
}

     


