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
        var modalButton = '<button type="button" class="modalPictureTrigger" data-toggle="modal" data-target="#ViewModalPicture' + attr + '" >Показать</button>';
        
        btnParent.append(modalButton);
        var style = css($("input[name='photo2']"));
        $(".modalPictureTrigger[data-target='#ViewModalPicture" + attr + "']").css(style);
        $(".modalPictureTrigger[data-target='#ViewModalPicture" +  attr + "']").css({ width: '122px', height: '22px', '-webkit-box-shadow': '0 0 0px 1000px white inset' });
    });
});

//добавление сохраненного в проекте изображения в модальное окно для просмотра
function addPic(id, src)
{
    $(id).append('<img id="' + id + '" src="' + src + '" height="300" width="400" style="display: block;margin-left: auto;margin-right: auto;">');
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