function GetBilboardPoints(role, userCompany ,objectId) {
    var geoObjects = new ymaps.GeoObjectCollection();
    var story = {};
    $('.storyList').each(function (i) {
        story[i]=($(this).text());
    });

    var onAgreement;
    if ($("#OnAgreement option:selected").val() == "Да") {
        onAgreement = true;
    } else {
        onAgreement = false;
    };
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
            lBillboardFinishDay: $("#lBillboardFinishDay option:selected").val(),
            lBillboardFinishMonth: $("#lBillboardFinishMonth option:selected").val(),
            lBillboardFinishYear: $("#lBillboardFinishYear").val(),
            Story: story,
            OnAgreement: onAgreement,
            IsBillboardSocial: $("#IsBillboardSocial option:selected").val(),
            id: parseInt(objectId)
        }
    }).success(function (data) {
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
                            balloonContentBody: '<b>Собственник:&nbsp;' + data[i].OwnerName
                                    + '</br>Улица 1:&nbsp' + data[i].Street1
                                    + '</br>Улица 2:&nbsp;' + data[i].Street2
                                     + '</br>Со стороны:&nbsp;' + data[i].FromStreet
                            + getReferencesBillboard(data[i]),
                            iconContent: 'Б',
                            hintContent: 'Щит'
                        }

                    },
                    {
                        preset: 'islands#redClusterIcons'
                    });

                    geoObjects.add(placemark);
                }
            }
        
    }).error(function () {
      
        alert("Ошибка запроса кукарекку");
    });
    return geoObjects;
}

function getReferencesBillboard(construction) {
    var result = "";
    console.log(construction.Surfaces);
    result += '</br><a href="/Data/EditBillboard/' + construction.Id + '" target="_blank">Изменить</a>'
        + '</br><a href="/Data/DeleteBilboard/' + construction.Id + '" target="_blank">Удалить</a></b>';
       
    for (var i = 0; i < construction.Surfaces.length; i++) {
        result += '</br><a href="/Data/Documents/' + construction.Surfaces[i] + '?type=doc" target="_blank">Документы</a>' 
         +"</br><div>Социальная</div>" 
        +"<div>" + construction.IsSocial[i]+"</div>"
        + '<br/><img src = "/Images/Billboard/surfaces/' + construction.Surfaces[i] + '.jpg" height = "180">';
    };
    return result;
}

     


