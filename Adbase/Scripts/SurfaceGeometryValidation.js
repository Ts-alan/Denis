$(document).ready(function ()
{
    $("#test1").removeAttr("novalidate");
    var $validator = $("#test1").validate({fraction: true});

    AddRules();
    
    $("#CountSizes").change(function ()
    {
        $("[id^='InsertPartial']").bind('DOMNodeInserted DOMNodeRemoved', function ()
        {
             AddRules();
        });
        AddRules();
    });

    var widthInpts = $("input[name$='.Width']");
    widthInpts.each(function (indx, element) {
        
        $(element).change(function (event) {
                if ($(this).val() > 0) {
                    
                    $(this).parent().removeClass("has-error").addClass("has-success");
                    $(this).parent().find("span.glyphicon").addClass("glyphicon-ok").removeClass("glyphicon-remove");
                    
                } else {
                    $(this).removeClass("has-success").addClass("has-error");
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
                    $(this).removeClass("has-success").addClass("has-error");
                     $(this).parent().find("span.glyphicon").removeClass("glyphicon-ok").addClass("glyphicon-remove");
                    
                }
            });

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