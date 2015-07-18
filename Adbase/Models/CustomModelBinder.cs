using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

            for (int i=0;i<AllKeysHeight.Count();i++)
            {

                ListSurface.Add(new Surface() { Height = Int32.Parse(request.Form.Get(AllKeysHeight[i])), Space = Int32.Parse(request.Form.Get(AllKeysWidth[i])), Width = Int32.Parse(request.Form.Get(AllKeysSpace[i])) });

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

            string tempIdentification_id ;
            for (int i = 0; i < DirectionSide_id.Count; i++)
            {
 
                    tempIdentification_id =request.Form.Get(Identification_id.Single(a => a.Contains(i.ToString())));

                
                ListSide.Add(new Side()
                {
                    DirectionSide_id = new Guid(request.Form.Get(DirectionSide_id[i])),
                    Identification_id = tempIdentification_id!=""?(Guid?)new Guid(tempIdentification_id):null 

                });

            }
            return ListSide;
        }
    }
}