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

        protected internal int CountSurfaces(AdvertisingStructure adv)
        {
            return adv.Sides.Sum(side => side.Surfaces.Count);
        }

        private double CountSquare(AdvertisingStructure adv)
        {
            return adv.Sides.SelectMany(side => side.Surfaces).Sum(Surface => Surface.Space);
        }

    }
}