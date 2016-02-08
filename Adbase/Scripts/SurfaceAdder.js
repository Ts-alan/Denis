
    //кол-во сторон
    function SetCountSize(DirectionSide, IdentificationSurface) {
        EndCountForSurface = 0;
        var CountSizes = $('#CountSizes').val();
        $("#CountSize").val(CountSizes);
        if (CountSizes != "") {
            $("[code]").each(function (indx, element) {
                element.remove();
            });

            for (var i = 1; i <= CountSizes; i++) {

                $("#InsertSide").before("<div code class=\"form-horizontal\" >" +
                    "<div class=\"form-group\" >" +
                    "<h3 class=\"control-label col-md-2\" for=\"\" style=\"margin-left: 55px\">Сторона  " + i + " </h3>" +
                    "</div>" +
                    "<div class=\"form-group\">" +
                    "<div class=\"col-md-10\">" +
                    "<b>Количество поверхностей</b>" +
                    " <select class=\"text-box single-line\" id=\"AddButton" + i + "\" onchange=AddSurface(" + i + ") name=\"CountSurfuce" + i + "\" IdentificationForCountSide=\"\" type=\"text\" data-val=\"true\" data-val-required=\"Укажите кол-во сторон\">" +
                    "<option></option>" +
                    "<option>1</option>" +
                    "<option>2</option>" +
                    "<option>3</option>" +
                    "<option>4</option>" +
                    "<option>5</option>" +
                    "<option>6</option>" +
                    "<option>7</option>" +
                    "<option>8</option>" +
                    "</select> &nbsp;&nbsp;&nbsp; <b>Направление стороны</b>" + DirectionSide +
                    "</div>" +
                    "</div> <div style=\"margin-left: -15px;display: none\" id=\"" + i + "HiddenIdentification\"> &nbsp;&nbsp;&nbsp;<b>Идентификация поверхностей</b>" + IdentificationSurface + ' </div>' +
                    "<div  id=\"InsertPartial" + i + "\"></div>" +
                    "</div>");

                $("#SizeOfCount").val(CountSizes);

            }
        }



    }


    //Добавить поверхность+           
    function AddSurface(i) {

        var count = $("#AddButton" + i + "").val();
        StartCountForSurface = EndCountForSurface;
        EndCountForSurface = parseInt(count) + parseInt(EndCountForSurface);

        if (count == 1) {
            $("#" + i + "HiddenIdentification").hide();
        } else {
            $("#" + i + "HiddenIdentification").show();
            //валидация

        }
        $("[side=" + i + "]").remove();


    $.ajax('/Data/AddSurface', { data: { side: i, StartCountForSurface: StartCountForSurface, EndCountForSurface: EndCountForSurface } })
            .success(function (e) {
                var live_str = $('<div>', { html: e });

                var res = live_str.find('.body-content');

                $("#InsertPartial" + i + "").prepend(res);
            });

    }