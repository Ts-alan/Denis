using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace OddBasyBY.Models
{
    public class CustomModelBinderForSegment : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var request = controllerContext.HttpContext.Request;
            List<Segment> ListSurface = new List<Segment>();

            string[] BreadthS = request.Form.Get("Street.BreadthS").Split(new Char[] { ',' });
            string[] BreadthE = request.Form.Get("Street.BreadthE").Split(new Char[] { ',' });
            string[] LengthS = request.Form.Get("Street.LengthS").Split(new Char[] { ',' });
            string[] LengthE = request.Form.Get("Street.LengthS").Split(new Char[] { ',' });
            //List<string> AllKeysWidth = request.Form.AllKeys.Where(a => a.Contains("].Width")).ToList();
            //List<string> AllKeysSpace = request.Form.AllKeys.Where(a => a.Contains("].Space")).ToList();
            //List<string> AllKeysSideOfSurface = request.Form.AllKeys.Where(a => a.Contains("].SideOfSurface")).ToList();
            for (int i = 0; i < BreadthS.Count(); i++)
            {

                ListSurface.Add(new Segment()
            {
               BreadthS = BreadthS[i],
               LengthS = LengthS[i],
               BreadthE = BreadthE[i],
               LengthE = LengthE[i]
            });

        }
            return ListSurface;
        }
    }
}