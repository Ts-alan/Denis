$(document).ready(function ()
{
    AddRules();
    
    $("#CountSizes").change(function ()
    {
         //$("input[name$='.Width']").rules( "remove" );
         //      $("input[name$='.Space']").rules( "remove" );
         //      $("input[name$='.Height']").rules( "remove" );
        $("[id^='InsertPartial']").bind('DOMNodeInserted DOMNodeRemoved', function ()
        {
               //$("input[name$='.Width']").rules( "remove" );
               //$("input[name$='.Space']").rules( "remove" );
               //$("input[name$='.Height']").rules( "remove" );
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
    //var width = $("input[name$='.Width']");
    //var space = $("input[name$='.Space']");
    //var height = $("input[name$='.Height']");
    
    var widthInpts = $("input[name$='.Width']");

    widthInpts.each(function (indx, element)
    {
         //console.log($(element)[0]);
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
        //$(element).rules("remove");
       // console.log($(element));
        $(element).rules("add", {
                min: "0",
                messages :{min: "Введите положительное значение" } 
            });
        $(element).change(function (event)
        {
            
            if ($(this).val() > 0) {

                $(this).parent().removeClass("has-error").addClass("has-success");
                $(this).parent().find("span.glyphicon").addClass("glyphicon-ok").removeClass("glyphicon-remove");

            } else {
                $(this).parent().removeClass("has-success").addClass("has-error");
                $(this).parent().find("span.glyphicon").removeClass("glyphicon-ok").addClass("glyphicon-remove");

            }
        });

    });
    //if (width.length > 0)
    //{
    //    var wId = "";
        
    //    for (var i = 0; i < width.length; i++)
    //    {
    //        wId = "input[name ='" + "[" + i + "].Width']";
    //        $(wId).rules("add", {
    //            min: "0",
    //            messages :{min: "Введите положительное значение" } 
    //        });
            
    //    }
    //}

    //if (space.length > 0)
    //{
    //    var sId = "";
    //    for (var i = 0; i < space.length; i++)
    //    {
    //        sId = "input[name ='" + "[" + i + "].Space']";
    //        $(sId).rules("add", {
    //            min: "0"

    //        });
    //    }
        
    //}

    //if (height.length > 0)
    //{
    //    var hId = "";
    //    for (var i = 0; i < space.length; i++)
    //    {
    //        hId = "input[name ='" + "[" + i + "].Height']";
    //        $(hId).rules("add", {
    //            min: "0",
    //            messages :{min: "Введите положительное значение" } 
    //        });
    //    }
    //}
}