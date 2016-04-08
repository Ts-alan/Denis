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
            List<Surface> listSurface=new List<Surface>();

            List<string> allKeysHeight = request.Form.AllKeys.Where(a=>a.Contains("].Height")).ToList();
            List<string> allKeysWidth = request.Form.AllKeys.Where(a => a.Contains("].Width")).ToList();
            List<string> allKeysSpace = request.Form.AllKeys.Where(a => a.Contains("].Space")).ToList();
            List<string> allKeysSideOfSurface = request.Form.AllKeys.Where(a => a.Contains("].SideOfSurface")).ToList();
            List<string> types = request.Form.AllKeys.Where(a => a.Contains("].Type")).ToList();
            List<string> themes = request.Form.AllKeys.Where(a => a.Contains("].Theme")).ToList();
            List<string> rentUntils = request.Form.AllKeys.Where(a => a.Contains("].RentUntil")).ToList();
            List<string> rentFroms = request.Form.AllKeys.Where(a => a.Contains("].RentFrom")).ToList();
            List<string> freeOrEngagedChckxs = request.Form.AllKeys.Where(a => a.Contains(".FreeOrEngaged")).ToList();

            for(  )

            double height = 0;
            double space = 0;
            double width = 0;
            var sideOfSurface = "";
            string type = "";
            string theme = "";
            DateTime? rentUntil = null;
            DateTime? rentFrom = null;
            bool freeOrEngagedChckx = false;

            for (int i=0; i<allKeysHeight.Count; i++)
            {
                double z;
                if (double.TryParse(request.Form.Get(allKeysHeight[i]), out z))
                {
                    height = double.Parse(request.Form.Get(allKeysHeight[i]), CultureInfo.InvariantCulture);
                }
                double q;
                if (double.TryParse(request.Form.Get(allKeysSpace[i]), out q))
                {
                    space = double.Parse(request.Form.Get(allKeysSpace[i]), CultureInfo.InvariantCulture);
                }
                double x;
                if (double.TryParse(request.Form.Get(allKeysWidth[i]), out x))
                {
                    width = double.Parse(request.Form.Get(allKeysWidth[i]), CultureInfo.InvariantCulture);
                }
                if (!string.IsNullOrWhiteSpace(request.Form.Get(allKeysSideOfSurface[i])))
                {
                    sideOfSurface = request.Form.Get(allKeysSideOfSurface[i]);
                }
                if (!string.IsNullOrWhiteSpace(request.Form.Get(types[i])))
                {
                    type = request.Form.Get(types[i]);
                }
                if (!string.IsNullOrWhiteSpace(request.Form.Get(themes[i])))
                {
                    theme = request.Form.Get(types[i]);
                }
                DateTime u;
                if (rentUntils[i] != null && DateTime.TryParse(rentUntils[i], out u))
                {
                    rentUntil = DateTime.Parse(request.Form.Get(rentUntils[i]));
                }
                DateTime a;
                if (rentFroms[i] != null && DateTime.TryParse(rentFroms[i], out a))
                {
                    rentFrom = DateTime.Parse(rentFroms[i]);
                }
                if (!string.IsNullOrWhiteSpace(request.Form.Get(freeOrEngagedChckxs[i])))
                {
                    freeOrEngagedChckx = Boolean.Parse(request.Form.Get(freeOrEngagedChckxs[i]));
                }

                listSurface.Add(new Surface() {
                    Height = height, 
                    Space = space, 
                    Width = width, 
                    SideOfSurface = sideOfSurface,
                    Theme = theme,
                    Type = type,
                    RentFrom = rentFrom,
                    RentUntil = rentUntil,
                    FreeOrEngaged = freeOrEngagedChckx
                });

            }
            return listSurface;
        }
    }
    public class CustomModelBinderForSide : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var request = controllerContext.HttpContext.Request;
            List<Side> listSide = new List<Side>();

            List<string> directionSideId = request.Form.AllKeys.Where(a => a.Contains("].IdentificationForDirectionSide")).ToList();
            List<string> identificationId = request.Form.AllKeys.Where(a => a.Contains("].IdentificationForIdentificationSurface")).ToList();

            string tempIdentificationId="" ;
            for (int i = 0; i < directionSideId.Count; i++)
            {
                if (identificationId.Any(a => a.Contains(i.ToString())))
                {
                    tempIdentificationId = request.Form.Get(identificationId.Single(a => a.Contains(i.ToString())));
                }

                var test = request.Form.Get(directionSideId[i]);
                if (test != "")
                {
                    listSide.Add(new Side()
                    {
                        DirectionSide_id = new Guid(request.Form.Get(directionSideId[i])),
                        Identification_id = tempIdentificationId != "" ? (Guid?)new Guid(tempIdentificationId) : null

                    });
                }

            }
            return listSide;
        }
    }
    public class CustomModelBinderForSideForAd : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var request = controllerContext.HttpContext.Request;
            List<Side> listSide = new List<Side>();

            List<string> directionSideId = request.Form.AllKeys.Where(a => a.Contains("DirectionSide")).ToList();
            List<string> identificationId = request.Form.AllKeys.Where(a => a.Contains("IdentificationSurface")).ToList();
            
            for (int i = 0; i < directionSideId.Count; i++)
            {
                string tempIdentificationId = "";
                string tempDirectionSide = "";
                if (identificationId.Any(a => a.Contains(i.ToString())))
                {
                    tempIdentificationId = request.Form.Get(identificationId.Single(a => a.Contains(i.ToString())));
                    
                }
                if (directionSideId.Any(a => a.Contains(i.ToString())))
                {
                    tempDirectionSide= request.Form.Get(directionSideId.Single(a => a.Contains(i.ToString())));
                }
                    listSide.Add(new Side()
                    {
                        DirectionSide_id = tempDirectionSide != ""? (Guid?)new Guid(tempDirectionSide) :null,
                        Identification_id = tempIdentificationId != "" ? (Guid?)new Guid(tempIdentificationId) : null
                    });
            }
            return listSide;
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

    public class CustomModelBinderForPicsForAd : DefaultModelBinder
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