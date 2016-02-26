using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace Sciencecom.Models
{
    public class CustomModelBinderForSurface : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var request = controllerContext.HttpContext.Request;
            List<Surface> ListSurface=new List<Surface>();

            List<string> AllKeysHeight = request.Form.AllKeys.Where(a=>a.Contains("].Height")).ToList();
            List<string> AllKeysWidth = request.Form.AllKeys.Where(a => a.Contains("].Width")).ToList();
            List<string> AllKeysSpace = request.Form.AllKeys.Where(a => a.Contains("].Space")).ToList();
            List<string> AllKeysSideOfSurface = request.Form.AllKeys.Where(a => a.Contains("].SideOfSurface")).ToList();

            double height = 0;
            double space = 0;
            double width = 0;
            var sideOfSurface = "";

            for (int i=0; i<AllKeysHeight.Count(); i++)
            {

                if (!string.IsNullOrWhiteSpace(request.Form.Get(AllKeysHeight[i])))
                {
                    height = double.Parse(request.Form.Get(AllKeysHeight[i]), CultureInfo.InvariantCulture);
                }
                if (!string.IsNullOrWhiteSpace(request.Form.Get(AllKeysSpace[i])))
                {
                    space = double.Parse(request.Form.Get(AllKeysSpace[i]), CultureInfo.InvariantCulture);
                }
                if (!string.IsNullOrWhiteSpace(request.Form.Get(AllKeysWidth[i])))
                {
                    width = double.Parse(request.Form.Get(AllKeysWidth[i]), CultureInfo.InvariantCulture);
                }
                if (!string.IsNullOrWhiteSpace(request.Form.Get(AllKeysSideOfSurface[i])))
                {
                    sideOfSurface = request.Form.Get(AllKeysSideOfSurface[i]);
                }
                ListSurface.Add(new Surface() { Height = height, 
                    Space = space, 
                    Width = width, 
                    SideOfSurface = sideOfSurface
                });

            }
            return ListSurface;
        }
    }
    public class CustomModelBinderForSide : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var request = controllerContext.HttpContext.Request;
            List<Side> ListSide = new List<Side>();

            List<string> DirectionSide_id = request.Form.AllKeys.Where(a => a.Contains("].IdentificationForDirectionSide")).ToList();
            List<string> Identification_id = request.Form.AllKeys.Where(a => a.Contains("].IdentificationForIdentificationSurface")).ToList();

            string tempIdentification_id="" ;
            for (int i = 0; i < DirectionSide_id.Count; i++)
            {
                if (Identification_id.Where(a => a.Contains(i.ToString())).Any())
                {
                    tempIdentification_id = request.Form.Get(Identification_id.Single(a => a.Contains(i.ToString())));
                }

                var test = request.Form.Get(DirectionSide_id[i]);
                if (test != "")
                {
                    ListSide.Add(new Side()
                    {
                        DirectionSide_id = new Guid(request.Form.Get(DirectionSide_id[i])),
                        Identification_id = tempIdentification_id != "" ? (Guid?)new Guid(tempIdentification_id) : null

                    });
                }

            }
            return ListSide;
        }
    }

    //public class CustomModelBinderForNormalcoordinates : DefaultModelBinder
    //{
    //    public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
    //    {
    //        var request = controllerContext.HttpContext.Request;


    //        string Height = request.Form.Get("Height");
    //        string Breadth = request.Form.Get("Breadth");
    //        request.Form.Remove("Breadth");
    //        request.Form.Remove("Height");
    //        request.Form.Set("Breadth", Breadth);
    //        request.Form.Set("Height", Height);
    //        return base.BindModel(controllerContext,bindingContext);
    //    }
    //}
}