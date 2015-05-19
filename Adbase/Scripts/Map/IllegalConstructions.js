function GetIllegalPoints(role, objectId) {
    var geoObjects = new ymaps.GeoObjectCollection();
    var status = 0;
    $.ajax({
        type: "POST",
        url: "/Map/GetIllegal/",
        data: {
            locality: $("#locality").val(),
            street1: $("#street1").val(),
            street2: $("#street2").val(),
            fromStreet: $("#fromStreet").val(),
            status: $("#status option:selected").val(),
            id: parseInt(objectId)
        }
    }).success(function (data) {
        if (data.length > 0) {
            var objects = new ymaps.GeoObjectCollection();
            for (var i = 0; i < data.length; i++) {
                var placemark = new ymaps.GeoObject(
                    {
                        geometry: {
                            type: "Point",
                            coordinates: [data[i].Shirota, data[i].Dolgota]
                        },
                        properties: {
                            balloonContentBody: '<b>Улица 1:&nbsp;' + data[i].Street1
                                    + '</br>Улица 2:&nbsp' + data[i].Street2
                                    + '</br>Со стороны:&nbsp;' + data[i].FromStreet
                                    + '</br>Добавил:&nbsp;' + data[i].WhoAdded + " "  +data[i].AdditionDate
                                    + '</br>Принял к сведению:&nbsp;' + data[i].WhoTakeNote + " " + data[i].NotingDate
                                    + '</br>Статус:&nbsp;<label id ="status">' + getStatus(data[i].Status) + "</label>"
                                    + '<hr>Примечание:&nbsp;</b></br>' + data[i].Note
                                    + getReferencesIllegal(role, data[i]),
                        }
                    },
                {
                    iconLayout: 'default#image',
                    iconImageHref: '/content/icons/'+data[i].Status+'.png',
                    iconImageSize: [20, 20],
                    iconImageOffset: [-10, -10]
                });
                placemark.events.add('balloonopen', function (e) {
                    $(".changeStatus").change(function (e) {
                        var currentStatus = $("label#status").text();
                        var newOption = $(".changeStatus option:selected").text();
                        console.log($("label#status")); 
                        if ((currentStatus != newOption) && (newOption != "-Выберите статус-")) {
                            $.ajax({
                                type: "POST",
                                url: "/Illegal/ChangeStatus/",
                                data: { id: parseInt($(".changeStatus").attr("id")), status: newOption }
                            }).success(function () {
                                new Messi('Статус изменен. Обновите карту для отображения изменений.',
                                    { title: 'Готово!', titleClass: 'anim success', buttons: [{ id: 0, label: 'Закрыть', val: 'X' }] });
                            }).fail(function () {
                                new Messi('При смене статуса объекта произошла ошибка. Статус не изменен.',
                                    { title: 'Ошибка', titleClass: 'anim error', buttons: [{ id: 0, label: 'Закрыть', val: 'X' }] });
                            })
                        }
                    });
                });
                objects.add(placemark); 
            }
            geoObjects.add(objects);
        }
    }).fail(function () {
        new Messi('Произошла ошибка при загрузке объектов из раздела "Неопознанные объекты".',
      { title: 'Ошибка', titleClass: 'anim error', buttons: [{ id: 0, label: 'Закрыть', val: 'X' }] });
    });
    return geoObjects;
}

function getReferencesIllegal(role, construction) {
    var result = "";
    if ((role == "Admin") || (role == "IllegalEdit")) {
        result += '<hr>Изменить статус на:&nbsp;<select id="' + construction.Id + '" class = "changeStatus">'
        + '<option value="-1">-Выберите статус-</option>'
        + '<option value="1">Рассматривается</option>'
        + '<option value="2">Нелегальный</option>'
        + '<option value="3">Легальный</option>'
        + '<option value="4">Демонтирован</option></select>'
        + '<br><a href="/Illegal/Delete/' + construction.Id + '" target="_blank">Удалить</a>'
        + '<br><a href="/Illegal/Edit/' + construction.Id + '" target="_blank">Изменить</a>'
    }
    if (construction.HasFile1) {
        result += '<br/><a href="/Images/Illegal/' + construction.Id + 'file1.jpg"><img src = "/Images/Illegal/' + construction.Id + 'file1.jpg" height = "180"></a>';
    }
    if (construction.HasFile2) {
        result += '<br/><a href="/Images/Illegal/' + construction.Id + 'file2.jpg"><img src = "/Images/Illegal/' + construction.Id + 'file2.jpg" height = "180"></a>';
    }
    return result;
}

function getStatus(statusInt)
{
    switch (statusInt) {
        case 0: return "Новый"; break;
        case 1: return "Рассматривается"; break;
        case 2: return "Нелегальный"; break;
        case 3: return "Легальный"; break;
        case 4: return "Демонтирован"; break;
        default: return "Невозможно определить статус"; break;
    }
}