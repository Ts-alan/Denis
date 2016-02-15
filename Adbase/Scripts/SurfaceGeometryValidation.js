$(document).ready(function ()
{
    $("#test1").removeAttr("novalidate");
    $("#test1").validate();
    AddRules();
    
    $("#CountSizes").change(function ()
    {
        //$("select[id^='AddButton']").change(function()
        //{
        //    AddRules();
        //});
        //var n = $("[id^='InsertPartial']");
        //console.log(n.length);
        $("[id^='InsertPartial']").bind('DOMNodeInserted DOMNodeRemoved', function ()
        {
             AddRules();
            //alert("!!!");
        });
        //$("#InsertPartial1").change(function ()
        //{
        //    AddRules();
        //});
        AddRules();
    });

    
    
});



function AddRules()
{
    var width = $("input[name$='.Width']");
    var space = $("input[name$='.Space']");
    var height = $("input[name$='.Height']");
   // console.log(width.length);
    if (width.length > 0)
    {
        var wId = "";
        for (var i = 0; i < width.length; i++)
        {
            wId = "input[name ='" + "[" + i + "].Width']";
            $(wId).rules("add", {
                min: 1
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
                min: 1
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
                min: 1
            });
        }
        
    }
}