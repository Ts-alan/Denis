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

            string[] BreadthS = request.Form.Get("Segment.BreadthS").Split(new Char[] { ',' });
            string[] BreadthE = request.Form.Get("Segment.BreadthE").Split(new Char[] { ',' });
            string[] LengthS = request.Form.Get("Segment.LengthS").Split(new Char[] { ',' });
            string[] LengthE = request.Form.Get("Segment.LengthS").Split(new Char[] { ',' });

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