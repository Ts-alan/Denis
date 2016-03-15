$(document).ready(function ()
{
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
      
            $("[name$='].Width']").each(function(ind, wId) {
                console.log($(wId));
            $(wId).rules("add", {
                min: "0",
                messages :{min: "Введите положительное значение" } 
            });
            });
        
    }

    if (space.length > 0)
    {
        var sId = "";
        $("[name$='].Space']").each(function(ind, sId) {
           
            $(sId).rules("add", {
                min: "0"

            });
        });
        
    }

    if (height.length > 0)
    {
        var hId = "";

        $("[name$='].Height']").each(function (ind, hId) {
            
            $(hId).rules("add", {
                min: "0",
                messages: { min: "Введите положительное значение" }
            });
        });
    }
}