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
    $("#test1").validate();
 
    var widthInpts = $("input[name$='.Width']");

    widthInpts.each(function (indx, element)
    {
        $(element).rules("add", {
                min: "0",
                messages :{min: "Введите положительное значение" } 
            });
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
   
    hightInpts.each(function (indx, element)
    {

        $(element).rules("add", {
                min: "0",
                messages :{min: "Введите положительное значение" } 
            });
        $(element).change(function (event)
        {
            
            if ($(this).val() >= 0) {

                $(this).parent().removeClass("has-error").addClass("has-success");
                $(this).parent().find("span.glyphicon").addClass("glyphicon-ok").removeClass("glyphicon-remove");

            } else {
                $(this).parent().removeClass("has-success").addClass("has-error");
                $(this).parent().find("span.glyphicon").removeClass("glyphicon-ok").addClass("glyphicon-remove");

            }
        });

    });
}