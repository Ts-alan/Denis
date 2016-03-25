using System;
using System.Collections.Generic;
using System.Globalization;
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
            List<string> AllKeysSideOfSurface = request.Form.AllKeys.Where(a => a.Contains("].SideOfSurface")).ToList();
            List<string> AllKeysGuids = request.Form.AllKeys.Where(a => a.Contains("Guid")).ToList();

            double height = 0;
            double space = 0;
            double width = 0;
            var sideOfSurface = "";
            int guid = 0;

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
                if (!string.IsNullOrWhiteSpace(request.Form.Get(AllKeysGuids[i])))
                {
                    guid = int.Parse(request.Form.Get(AllKeysGuids[i]));
                }

                ListSurface.Add(new Surface() { Height = height, 
                    Space = space, 
                    Width = width, 
                    SideOfSurface = sideOfSurface,
                    NumberSurface = guid
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
    public class CustomModelBinderForSideForAD : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var request = controllerContext.HttpContext.Request;
            List<Side> ListSide = new List<Side>();

            List<string> DirectionSide_id = request.Form.AllKeys.Where(a => a.Contains("DirectionSide")).ToList();
            List<string> Identification_id = request.Form.AllKeys.Where(a => a.Contains("IdentificationSurface")).ToList();
            
            for (int i = 0; i < DirectionSide_id.Count; i++)
            {
                string tempIdentification_id = "";
                string tempDirectionSide = "";
                if (Identification_id.Where(a => a.Contains(i.ToString())).Any())
                {
                    tempIdentification_id = request.Form.Get(Identification_id.Single(a => a.Contains(i.ToString())));
                    
                }
                if (DirectionSide_id.Where(a => a.Contains(i.ToString())).Any())
                {
                    tempDirectionSide= request.Form.Get(DirectionSide_id.Single(a => a.Contains(i.ToString())));
                }
                    ListSide.Add(new Side()
                    {
                        DirectionSide_id = tempDirectionSide != ""? (Guid?)new Guid(tempDirectionSide) :null,
                        Identification_id = tempIdentification_id != "" ? (Guid?)new Guid(tempIdentification_id) : null

                    });
            
            }
            return ListSide;
        }
    }

    public class CustomModelBinderForPicInd : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var request = controllerContext.HttpContext.Request;
            List<string> picIndexes = request.Form.AllKeys.Where(a => a.Contains("PhotoInd[")).ToList();
            return picIndexes;
        }
    }

    public class CustomModelBinderForPicsForAD : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var request = controllerContext.HttpContext.Request;

            var files = request.Files;
            var photos = files.AllKeys.Where(x => x.Contains("photo[")).ToList();
            Dictionary<int, HttpPostedFileBase> photosDic = new Dictionary<int, HttpPostedFileBase>();
            List<string> AllKeysGuids = request.Form.AllKeys.Where(a => a.Contains("Guid")).ToList();
            foreach (var photo in photos)
            {
                photosDic.Add(int.Parse(photo.Split('[', ']')[1]), files.Get(photo));
            }

            return photosDic;
        }
    }

}