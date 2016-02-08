var loc;
$(document).ready(function () {
    Swap();
    $("#Locality_id").change(function ()
    {
        Swap();
    });
});

function Swap()
{
    loc = $("#Locality_id option:selected").text();
    if (loc === "Смолевичский р-н" | loc === "Минский район")
    {
        $("#House1").val('');
        $("#Street1").val('');
        
        $(".HSclear").hide();
        $(".RKclear").each(function (index, element) {
            $(element).show();
        });
        
    }
    else
    {
        $("#RoadName").val('');
        $("#RoadKM").val('');
        
        $(".HSclear").each(function(index, element) {
            $(element).show();
        });
        $(".RKclear").hide();
    }
}


