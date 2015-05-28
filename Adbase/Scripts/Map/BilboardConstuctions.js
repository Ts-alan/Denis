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
            lFinishDay: $("#lFinishDay option:selected").val(),
            lFinishMonth: $("#lFinishMonth option:selected").val(),
            lFinishYear: $("#lFinishYear").val(),
            onStatement: $("#onStatement option:selected").val(),
            isSocial: $("#isSocial option:selected").val(),
            id: parseInt(objectId)
        }
    }).success(function(data) {
        if (data.length > 0) {
            console.log(data);
            var myBilboardClusterer = new ymaps.Clusterer(
            {
                preset: 'islands#redClusterIcons',
                clusterDisableClickZoom: false
            });
            var opponentBilboardClusterer = new ymaps.Clusterer(
            {
                preset: 'islands#blueClusterIcons',
                clusterDisableClickZoom: false
            });
            for (var i = 0; i < data.length; i++) {

                
                    var placemark = new ymaps.GeoObject(
                    {
                        geometry: {
                            type: "Point",
                            coordinates: [data[i].Height, data[i].Breadth]
                        }

                    },
                    {
                        preset: 'islands#redClusterIcons'
                    });
                    console.log(data[i].Shirota);
                    console.log(data[i].Dolgota);
                    geoObjects.add(placemark);
                }
            }
        
    });
    return geoObjects;
}


     


