$(document).ready(function()
{
    var picID = "";

  

    ////Функция срабатывает при клике на кнопку просмотра изображения
    //$(".modalPictureTrigger").each(function(indx, element)
    //{
    //    element.click(function()
    //    {
    //    //получение значения атрибута "name" для генерации ссылки на картинку
    //    var name = $(this).parent().parent().find("input[type='file']").attr("name");
    //    console.log($(this).parent().parent().find("input[type='file']"));
    //    //получение значения атрибута "id" для генерации ссылки на картинку
    //    picID = $(this).attr("data-id");

    //    var markup = '<img src=/Images/' + name + '/' + picID + name + ".jpg" + ' height="300" width="400" style="display: block;margin-left: auto;margin-right: auto;">';
    //    //Добавление в модальное коно картинки
    //    $("#ModalPictureDiv").empty();
    //    $("#ModalPictureDiv").append(markup);
    //    });
    //});

    //Функция срабатывает при клике на кнопку загрузки файла
    $("input[type='file']").click(function()
    {
        //очистка кнопки выбора файла
        var attr = $(this).attr("name");
        var btnParent = $(this).parent();
        btnParent.parent().find("label[for='" + attr + "']").text("Файл не выбран");
        this.value = null;
       
       // btnParent.find("span").text("Выберите файл");

        //удаление кнопки открытия модального окна
        btnParent.find("button.modalPictureTrigger").remove();
        btnParent.find("label.fileSize").remove();
        btnParent.append("<label class='fileSize'>Не более 1 МБ</label>");
    });

    //Функция срабатывает изменении значения кнопки загрузки файла
    $("input[type='file']").change(function()
    {
        var attr = $(this).attr("name");
        var modalImgId = "ModalPictureimg" + attr;
        var modalId = "#ModalPictureDiv" + attr;
        var btnParent = $(this).parent();
        var fileName = this.value.split('\\').pop();
        //Изменение стиля кнопки загрузки фала при выборе загружаемого файла
        
        var markup = '<img id="' + modalImgId + '" src=""' + ' height="300" width="400" style="display: block;margin-left: auto;margin-right: auto;">';
        //добавление в модальное окно пустого тега с картинкой
        
        $(modalId).empty();
        $(modalId).append(markup);
       
        //считывание загружаемой кратинки и запись её в тег src тега img
        var fReader = new FileReader();
        fReader.readAsDataURL(this.files[0]);
        fReader.onloadend = function(event)
        {
            var imgid = "#" + modalImgId;
            //var img = document.getElementById(imgid);
             $(imgid).attr("src", event.target.result);
            //console.log($(imgid));
            //img.src = event.target.result;
            //console.log(img.src);
        }

        //добавление кнопки открывания модального окна
        var modalButton = '<button type="button" class="modalPictureTrigger" data-toggle="modal" data-target="#ViewModalPicture' + attr + '" >Открыть изображение</button>';
        btnParent.find("label.fileSize").remove();
        btnParent.append(modalButton);
    });
    
    

});
