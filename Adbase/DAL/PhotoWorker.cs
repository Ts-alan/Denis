
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Sciencecom.Controllers;


namespace Sciencecom.DAL
{
    public class PhotoWorkerController : BaseDataController
    {
        internal new void SavePic(string structureId, string documentName, HttpPostedFileBase picture)
        {
            if (picture != null)
            {
                string path = "";
                string src = "~/Images/photo1/" + documentName + structureId + ".jpg";
                path = System.Web.HttpContext.Current.Server.MapPath(src);
                picture.SaveAs(path);
            }
        }


        internal List<string> LoadPic(string dataId)
        {
            string src = "~/Images/photo1/";
            string path = System.Web.HttpContext.Current.Server.MapPath(src);
            List<string> photoNames = Directory.GetFiles(path).Where(x => x.Contains(dataId)).ToList();
            return photoNames;
        }

        internal new void DeletePic(string dataId, string photoName)
        {
            string src = "~/Images/photo1/" + dataId + photoName + ".jpg";
            string path = System.Web.HttpContext.Current.Server.MapPath(src);
            FileInfo info1 = new FileInfo(path);
            if (info1.Exists)
            {
                info1.Delete();
            }
        }

        internal void ValidatePic(Dictionary<string, HttpPostedFileBase> photos, Dictionary<string, string> picIndexes, string structureId, string oldstructureId)
        {
            foreach (KeyValuePair<string, HttpPostedFileBase> photo in photos)
            {
                //var d = ;
                if (photo.Value != null & picIndexes.SingleOrDefault( x => x.Key.Contains(photo.Key)).Value == "setImage")
                {
                    SavePic(structureId, photo.Key+"photo" , photo.Value);
                }
                if (photo.Value != null & picIndexes.SingleOrDefault(x => x.Key.Contains(photo.Key)).Value == "")
                {
                    SavePic(structureId, photo.Key + "photo", photo.Value);
                }
                if (photo.Value == null & picIndexes.SingleOrDefault(x => x.Key.Contains(photo.Key)).Value == "setImage")
                {
                    string src = "~/Images/photo1/" + photo.Key + "photo"  + oldstructureId + ".jpg";
                    string oldPath = System.Web.HttpContext.Current.Server.MapPath(src);
                    src = "~/Images/photo1/" + photo.Key + "photo"  + structureId + ".jpg";
                    string newPath = System.Web.HttpContext.Current.Server.MapPath(src);
                    System.IO.File.Move(oldPath, newPath);
                }
            }
            
        }
    }
}