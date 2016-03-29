using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sciencecom.DAL;
using Sciencecom.Models;

namespace Sciencecom.Controllers
{
    public class BaseDataController : Controller
    {
       
        // GET: BaseData
        protected internal void SavePic(string structureId, string documentName, HttpPostedFileBase picture)
        {
            if (picture != null)
            {
                string src = "~/Images/" + documentName + "/" + structureId + documentName + ".jpg";
                string path = Server.MapPath(src);
                picture.SaveAs(path);
            }
        }
       

        protected internal bool LoadPic(string dataId, string photoName)
        {
            string src = "~/Images/" + photoName + "/" + dataId + photoName + ".jpg";
            string path = Server.MapPath(src);
            if (System.IO.File.Exists(path))
            {
                return true;
            }
            return false;
        }

        protected internal void DeletePic(string dataId, string photoName)
        {
            string src = "~/Images/" + photoName + "/" + dataId + photoName + ".jpg";
            string path = Server.MapPath(src);
            FileInfo info1 = new FileInfo(path);
            if (info1.Exists)
            {
                info1.Delete();
            }
        }

        protected internal void ValidatePic(HttpPostedFileBase postedPic, string picIndex, string structureId, string oldstructureId, string documentName)
        {
            if (postedPic != null & picIndex == "setImage")
            {
                SavePic(structureId, documentName, postedPic);
            }
            if (postedPic != null & picIndex == "")
            {
                SavePic(structureId, documentName, postedPic);
            }
            if (postedPic == null & picIndex == "setImage")
            {
                string src = "~/Images/" + documentName + "/" + oldstructureId + documentName + ".jpg";
                string oldPath = Server.MapPath(src);
                src = "~/Images/" + documentName + "/" + structureId + documentName + ".jpg";
                string newPath = Server.MapPath(src);
                System.IO.File.Move(oldPath, newPath);
            }
         
        }

        protected internal AdvertisingStructure ValidateCoords(AdvertisingStructure structures, string Bcoord, string Hcoord)
        {
            double q;
            if(double.TryParse(Bcoord.Replace(".", ","), out q))
            {
                structures.coordB = double.Parse(Bcoord.Replace(",", "."), CultureInfo.InvariantCulture);
            }

            double z;
            if (double.TryParse(Bcoord.Replace(".", ","), out z))
            {
                structures.coordH = double.Parse(Hcoord.Replace(",", "."), CultureInfo.InvariantCulture);
            }
            return structures;

        }

        protected internal int CountSurfaces(AdvertisingStructure adv)
        {
            return adv.Sides.Sum(side => side.Surfaces.Count);
        }

        protected double CountSquare(List<Surface> surfaces)
        {
            return surfaces.Sum(Surface => Surface.Space);
        }

    }
}