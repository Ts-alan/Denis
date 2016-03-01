$(document).ready(function ()
{
     $(document).on('change', '.btn-file :file', function ()
        {
            var input = $(this),
                numFiles = input.get(0).files ? input.get(0).files.length : 1,
                label = input.val().replace(/\\/g, '/').replace(/.*\//, '');
            input.trigger('fileselect', [numFiles, label]);
        });


        $('.btn-file :file').on('fileselect', function (event, numFiles, label)
        {

            var input = $(this).parents('.input-group').find(':text'),
                log = numFiles > 1 ? numFiles + ' files selected' : label;

            if (input.length)
            {
                input.val(log);
            }
            else
            {
                if (log) alert(log);
            }

        });

    AddRules();
    
    $("#CountSizes").change(function ()
    {
        $("[id^='InsertPartial']").bind('DOMNodeInserted DOMNodeRemoved', function ()
        {
             AddRules();
        });
        AddRules();
    });



    //Блокирование кнопки Enter чтобы форма не отправлялась
    $(window).keydown(function(event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            return false;
        }
    });

});

  

//Добавление правил валидации для полей
function AddRules()
{
    var width = $("input[name$='.Width']");
    var space = $("input[name$='.Space']");
    var height = $("input[name$='.Height']");

    var widthInpts = $("input[name$='.Width']");

    widthInpts.each(function (indx, element) {

        $(element).change(function (event) {
            if ($(this).val() > 0) {

                $(this).parent().removeClass("has-error").addClass("has-success");
                $(this).parent().find("span.glyphicon").addClass("glyphicon-ok").removeClass("glyphicon-remove");

            } else {
                $(this).parent().removeClass("has-success").addClass("has-error");
                $(this).parent().find("span.glyphicon").removeClass("glyphicon-ok").addClass("glyphicon-remove");

            }
        });

    });

    var hightInpts = $("input[name$='.Height']");
   
    hightInpts.each(function (indx, element) {

        $(element).change(function (event) {
            if ($(this).val() > 0) {

                $(this).parent().removeClass("has-error").addClass("has-success");
                $(this).parent().find("span.glyphicon").addClass("glyphicon-ok").removeClass("glyphicon-remove");

            } else {
                $(this).parent().removeClass("has-success").addClass("has-error");
                $(this).parent().find("span.glyphicon").removeClass("glyphicon-ok").addClass("glyphicon-remove");

            }
        });

    });
    if (width.length > 0)
    {
        var wId = "";
        for (var i = 0; i < width.length; i++)
        {
            wId = "input[name ='" + "[" + i + "].Width']";
            $(wId).rules("add", {
                min: "0,0001",
                messages :{min: "Введите положительное значение" } 
            });
            
        }
    }

    if (space.length > 0)
    {
        var sId = "";
        for (var i = 0; i < space.length; i++)
        {
            sId = "input[name ='" + "[" + i + "].Space']";
            $(sId).rules("add", {
                min: "0,00000001"

            });
        }
        
    }

    if (height.length > 0)
    {
        var hId = "";
        for (var i = 0; i < space.length; i++)
        {
            hId = "input[name ='" + "[" + i + "].Height']";
            $(hId).rules("add", {
                min: 0.0001,
                messages :{min: "Введите положительное значение" } 
            });
        }
    }
}