$(document).ready(function()
{
    //Функция срабатывает при клике на кнопку загрузки файла
    $("input[type='file']").click(function()
    {
        //очистка кнопки выбора файла
        var attr = $(this).attr("name");
        var btnParent = $(this).parent();
        this.value = null;

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
             $(imgid).attr("src", event.target.result);
        }

        //добавление кнопки открывания модального окна
        var modalButton = '<button type="button" class="modalPictureTrigger" data-toggle="modal" data-target="#ViewModalPicture' + attr + '" >Открыть изображение</button>';
        btnParent.find("label.fileSize").remove();
        btnParent.append(modalButton);
    });
});

//добавление сохраненного в проекте изображения в модальное окно для просмотра
function addPic(id, src)
{
    $(id).append('<img id="' + id + '" src="' + src + '" height="300" width="400" style="display: block;margin-left: auto;margin-right: auto;">');
}
