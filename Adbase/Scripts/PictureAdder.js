$(document).ready(function()
{
    var picID = "";
    $(".modalPictureTrigger").click(function()
    {
        var name = $(this).parent().parent().find("input[type='file']").attr("name");
        //console.log($(this).parent().parent().find("input[type='file']").attr("data-id"));
        picID = $(this).attr("data-id");

        var markup = '<img src=/Images/' + name + '/' + picID + name + ".jpg" + ' height="300" width="400" style="display: block;margin-left: auto;margin-right: auto;">';
        //console.log(markup);
        $("#ModalPictureDiv").empty();
        $("#ModalPictureDiv").append(markup);
    });

    $("input[type='file']").click(function()
    {
        //очистка кнопки выбора файла
        var attr = $(this).attr("name");
        var btnParent = $(this).parent();
        btnParent.parent().find("label[for='" + attr + "']").text("Файл не выбран");
        this.value = null;
        $(this).parent().removeClass("btn-warning");
        $(this).parent().addClass("btn-default");
        btnParent.find("span").text("Выберите файл");
        

        //удаление кнопки открытия модального окна
        btnParent.parent().find("button.modalPictureTrigger").remove();
        btnParent.parent().find("label.fileSize").remove();
        btnParent.parent().append("<label class='fileSize'>Не более 1 МБ</label>");
    });

    $("input[type='file']").change(function()
    {
        var attr = $(this).attr("name");
        var btnParent = $(this).parent();
        var fileName = this.value.split('\\').pop();
        //Изменение стиля кнопки загрузки фала при выборе загружаемого файла
        btnParent.addClass("btn-warning");
        btnParent.parent().find("label[for='" + attr + "']").text(fileName);

        var markup = '<img id="ModalPictureimg" src=""' + ' height="300" width="400" style="display: block;margin-left: auto;margin-right: auto;">';
        //добавление в модальное окно пустого тега с картинкой
        $("#ModalPictureDiv").empty();
        $("#ModalPictureDiv").append(markup);

        //считывание загружаемой кратинки и запись её в тег src тега img
        var fReader = new FileReader();
        fReader.readAsDataURL(this.files[0]);
        fReader.onloadend = function(event)
        {
            var img = document.getElementById("ModalPictureimg");
            img.src = event.target.result;
            //console.log(event.target.result);
        }

        //добавление кнопки открывания модального окна
        var modalButton = '<button type="button" class="btn btn-info input-sm modalPictureTrigger" data-toggle="modal" data-id ="@ViewBag.Id" data-target="#ViewModalPicture">Открыть изображение</button>';
        btnParent.parent().find("label.fileSize").remove();
        btnParent.parent().append(modalButton);
    });
    
    

});
