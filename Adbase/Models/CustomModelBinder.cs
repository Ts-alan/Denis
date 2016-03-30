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

            for (int i=0; i<AllKeysHeight.Count; i++)
            {
                double z;
                if (double.TryParse(request.Form.Get(AllKeysHeight[i]), out z))
                {
                    height = double.Parse(request.Form.Get(AllKeysHeight[i]), CultureInfo.InvariantCulture);
                }
                double q;
                if (double.TryParse(request.Form.Get(AllKeysSpace[i]), out q))
                {
                    space = double.Parse(request.Form.Get(AllKeysSpace[i]), CultureInfo.InvariantCulture);
                }
                double x;
                if (double.TryParse(request.Form.Get(AllKeysWidth[i]), out x))
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
            
            Dictionary<string, string> picIndexes = new Dictionary<string, string>();
            var tempPhotoInd = request.Form.AllKeys.Where(a => a.Contains("PhotoInd["));
            for (int i = 0; i < request.Form.AllKeys.Count(a => a.Contains("PhotoInd[")); i++ )
            {

                picIndexes.Add(tempPhotoInd.ElementAt(i),request.Form.Get(tempPhotoInd.ElementAt(i)));
            }
            
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
            
            return photos.ToDictionary(photo => photo.Substring(5), photo =>
            {
                if (files.Get(photo).ContentLength != 0)
                    return files.Get(photo);
                else
                {
                    return null;
                }
            });
        }
    }

}