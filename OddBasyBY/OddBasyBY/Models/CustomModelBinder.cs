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

    public class CustomModelBinderForModels: DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var request = controllerContext.HttpContext.Request;
            List<SpecificationofRM> ListSpecificationofRM = new List<SpecificationofRM>();
            
            List<string> ModelsL  = request.Form.AllKeys.Where(a => a.Contains("ModalsL")).ToList();
            List<string> ModalsA = request.Form.AllKeys.Where(a => a.Contains("ModalsA")).ToList();
            for (int i = 0; i < ModelsL.Count; i++)
            {
                string tempValueL = request.Form.Get(ModelsL.ElementAt(i));
                string tempValueA= request.Form.Get(ModalsA.ElementAt(i));

                if (tempValueL!= ""&& tempValueA!="")
                {
                 
                    ListSpecificationofRM.Add(new SpecificationofRM() {length = tempValueL,area = tempValueA,Mackup = ModalsA.ElementAt(i).Substring(7) });
                }
            }
            return ListSpecificationofRM;
        }
    }
}