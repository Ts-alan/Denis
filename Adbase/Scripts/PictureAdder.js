$(document).ready(function()
{
    ////Функция срабатывает при клике на кнопку загрузки файла
    //$("input[type='file']").click(function()
    //{
    //    //очистка кнопки выбора файла
    //    var attr = $(this).attr("name");
    //    var btnParent = $(this).parent();
    //    this.value = null;

    //    //удаление кнопки открытия модального окна
    //    btnParent.find("button.modalPictureTrigger").remove();
       
       
    //});

    ////Функция срабатывает изменении значения кнопки загрузки файла
    //$("input[type='file']").change(function()
    //{
    //    var attr = $(this).attr("name");
    //    var modalImgId = "ModalPictureimg" + attr;
    //    var modalId = "#ModalPictureDiv" + attr;
    //    var btnParent = $(this).parent();
    //    var markup = '<img id="' + modalImgId + '" src=""' + ' height="300" width="400" style="display: block;margin-left: auto;margin-right: auto;">';

    //    //добавление в модальное окно пустого тега с картинкой
    //    $(modalId).empty();
    //    $(modalId).append(markup);
       
    //    //считывание загружаемой кратинки и запись её в тег src тега img
    //    var fReader = new FileReader();
    //    fReader.readAsDataURL(this.files[0]);
    //    fReader.onloadend = function(event)
    //    {
    //        var imgid = "#" + modalImgId;
    //         $(imgid).attr("src", event.target.result);
    //    }

    //    //добавление кнопки открывания модального окна
    //    var modalButton = '<button type="button" class="modalPictureTrigger" data-toggle="modal" data-target="#ViewModalPicture' + attr + '" >Показать</button>';
        
    //    btnParent.append(modalButton);
    //    var style = css($("input[name='photo2']"));
    //    $(".modalPictureTrigger[data-target='#ViewModalPicture" + attr + "']").css(style);
    //    $(".modalPictureTrigger[data-target='#ViewModalPicture" +  attr + "']").css({ width: '122px', height: '22px', '-webkit-box-shadow': '0 0 0px 1000px white inset' });
    //});
    $(".closeimg").click(function () {
        //$(this).prev().attr("value", "");
        $(this).next().find("input[type='file']")[0].value = null;
        $(this).next().next().text("Файл не выбран");
        $(this).next().removeClass("btn-warning");
        $(this).next().addClass("btn-default");
        //$(this).next().find("label[for^='Scan']").text("Файл не выбран");
        $(this).closest("[class^='col-md']").find("button.modalPictureTrigger").remove();
    });


     //Функция срабатывает при клике на кнопку загрузки файла
    $("input[type='file']").click(function() {
        $(this).prev().attr("value", "");
       
        //очистка кнопки выбора файла
        //var attr = $(this).attr("name");
        var btnParent = $(this).parent();
        //btnParent.parent().find("label[for='" + attr + "']").text("Файл не выбран");
       
       
        //btnParent.parent().find("label[for^='photo']").text("Файл не выбран");
        //btnParent.parent().find("label[for^='Scan']").text("Файл не выбран");
        //btnParent.removeClass("btn-warning");
        //btnParent.addClass("btn-default");
        //удаление кнопки открытия модального окна
        //btnParent.parent().find("button.modalPictureTrigger").remove();
       
    });

    //Функция срабатывает изменении значения кнопки загрузки файла
    $("input[type='file']").change(function()
    {
        //$(this).parent().prev().prev().attr("value", "setImage");
      
        var attr = $(this).attr("name");
        var modalImgId = "ModalPictureimg" + attr;
        var modalId = "#ModalPictureDiv" + attr;
        var btnParent = $(this).parent();
        var fileName = this.value.split('\\').pop();
        if (fileName != "") {
            btnParent.parent().find("label[for^='photo']").text(fileName);
            btnParent.parent().find("label[for^='Scan']").text(fileName);
            //Изменение стиля кнопки загрузки фала при выборе загружаемого файла

            var markup = '<img id="' + modalImgId + '" src=""' + ' height="300" style="display: block;margin-left: auto;margin-right: auto;">';
            //добавление в модальное окно пустого тега с картинкой

            $(modalId).empty();
            $(modalId).append(markup);
        }

       
        btnParent.addClass("btn-warning");
        btnParent.removeClass("btn-default");

        //считывание загружаемой кратинки и запись её в тег src тега img
        var fReader = new FileReader();
        fReader.readAsDataURL(this.files[0]);
        fReader.onloadend = function(event)
        {
           var imgid = "#" + modalImgId;
           $(imgid).attr("src", event.target.result);
        }

        //добавление кнопки открывания модального окна

        var modalButton = '<button type="button" style="margin-left:25px" class="modalPictureTrigger btn btn-default" data-toggle="modal" data-target="#ViewModalPicture' + attr + '" >Показать</button>';

       

        if (btnParent.closest(".fileUpload").next().next().next().next().length != 0)
        {
            btnParent.closest(".fileUpload").next().next().next().next().remove();
        }
        btnParent.parent().append(modalButton);
     
    });





});

function addButton(parameters)
{

     var modalButton = '<button type="button" style="margin-left:25px" class="modalPictureTrigger btn btn-default" data-toggle="modal" data-target="#ViewModalPicture' + attr + '" >Показать</button>';
}

//добавление сохраненного в проекте изображения в модальное окно для просмотра
function addPic(id, src)
{
    $(id).empty();
    $(id).append('<img id="' + id + '" src="' + src + '" height="300"  style="display: block;margin-left: auto;margin-right: auto;">');
    //var style = css($("input[name='photo2']"));
    //$(id).css(style);
    //$(id).css({ width: '122px', height: '22px', '-webkit-box-shadow': '0 0 0px 1000px white inset' });
}

function css(a) {
    var sheets = document.styleSheets, o = {};
    for (var i in sheets) {
        var rules = sheets[i].rules || sheets[i].cssRules;
        for (var r in rules) {
            if (a.is(rules[r].selectorText)) {
                o = $.extend(o, css2json(rules[r].style), css2json(a.attr('style')));
            }
        }
    }
    return o;
}

function css2json(css) {
    var s = {};
    if (!css) return s;
    if (css instanceof CSSStyleDeclaration) {
        for (var i in css) {
            if ((css[i]).toLowerCase) {
                s[(css[i]).toLowerCase()] = (css[css[i]]);
            }
        }
    } else if (typeof css == "string") {
        css = css.split("; ");
        for (var i in css) {
            var l = css[i].split(": ");
            s[l[0].toLowerCase()] = (l[1]);
        }
    }
    return s;
}