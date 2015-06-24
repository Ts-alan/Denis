using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace Sciencecom.Models
{
    //public static int i;
    public static class TableAdapterExtensions
    {
        
        //public static int Increment()
        //{
        //    //Cache["increment"]=
        //}

    }

    public static class CustomHtmlHelpers
    {
        public static MvcHtmlString Side(this HtmlHelper html,int idSide)
        {
            return new MvcHtmlString("<div class=\"form-horizontal\" code>" +
                                        "<div class=\"form-group\">"+
                                        "<label class=\"control-label col-md-2\" for=\"\" style=\"margin-left: 55px\">Сторона"+idSide+" </label>"+
                                        "</div>"+
                                            "<div class=\"form-group\">"+
                                                "<div class=\"col-md-10\">"+
                                                   "<a href=\"#\" id=\"AddButton" + idSide + "\" style=\"color: #428bca; font-weight: bold;\">+Добавить площадь</a>" +
                                                "</div>"+
                                        "</div>"+
                                        "<div id=\"InsertPartial" + idSide + "\"></div>" +
                                    "</div>");
        }
    }

}