using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace Sciencecom.Models
{
    //public static int i;
    public static class TableAdapterExtensions
    {
        public static ObjectCache cache ;
        private static int i= 0;
        static TableAdapterExtensions()
        {
            cache = MemoryCache.Default;
            cache.Set("increment", i, null);
        }
      
        
        public static int Increment()
        {
            int result;
            int valueInrement;
            using (SciencecomEntities context = new SciencecomEntities())
            {
                result = context.Increments.First().Counter;
                valueInrement = result;

                context.Increments.First().Counter = ++valueInrement;
                context.SaveChanges();
            }
            return result;
        }
       // формирование нулей для UnickKey
        public static string StringSymvol()
        {
            string SymbolString = "";
            int number = Increment();
            for (int s=8;s>number.ToString().Length;s--)
            {
               SymbolString=  "0" + SymbolString;
            }
            return SymbolString+number.ToString();
        }
    }

    //public static class CustomHtmlHelpers
    //{
    //    public static MvcHtmlString Side(this HtmlHelper html,int idSide)
    //    {
    //        return new MvcHtmlString("<div class=\"form-horizontal\" code>" +
    //                                    "<div class=\"form-group\">"+
    //                                    "<label class=\"control-label col-md-2\" for=\"\" style=\"margin-left: 55px\">Сторона"+idSide+" </label>"+
    //                                    "</div>"+
    //                                        "<div class=\"form-group\">"+
    //                                            "<div class=\"col-md-10\">"+
    //                                               "<a href=\"#\" id=\"AddButton" + idSide + "\" style=\"color: #428bca; font-weight: bold;\">+Добавить площадь</a>" +
    //                                            "</div>"+
    //                                    "</div>"+
    //                                    "<div id=\"InsertPartial" + idSide + "\"></div>" +
    //                                "</div>");
    //    }
    //}

}