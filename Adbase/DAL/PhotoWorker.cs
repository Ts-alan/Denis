using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sciencecom.DAL
{
    public class PhotoWorker: Controller
    {
        internal void SavePic(string structureId, string documentName, HttpPostedFileBase picture)
        {
            if (picture != null)
            {
                string src = "~/Images/photo1/" + documentName + structureId + ".jpg";
                string path = Server.MapPath(src);
                picture.SaveAs(path);
            }
        }


        internal List<string> LoadPic(string dataId)
        {
            List<string> photoNames = Directory.GetFiles("~/Images/photo1/").Where(x => x.Contains(dataId)).ToList();
            return photoNames;
        }

        internal void DeletePic(string dataId, string photoName)
        {
            string src = "~/Images/photo1/" + dataId + photoName + ".jpg";
            string path = Server.MapPath(src);
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
                if (photo.Value != null & picIndexes.Single( x => x.Key.Contains(photo.Key)).Value == "setImage")
                {
                    SavePic(structureId, "photo" + photo.Key, photo.Value);
                }
                if (photo.Value != null & picIndexes.Single(x => x.Key.Contains(photo.Key)).Value == "")
                {
                    SavePic(structureId, "photo" + photo.Key, photo.Value);
                }
                if (photo.Value == null & picIndexes.Single(x => x.Key.Contains(photo.Key)).Value == "setImage")
                {
                    string src = "~/Images/photo1/" + "photo" + photo.Key + oldstructureId + ".jpg";
                    string oldPath = Server.MapPath(src);
                    src = "~/Images/photo1/" + "photo" + photo.Key + structureId + ".jpg";
                    string newPath = Server.MapPath(src);
                    System.IO.File.Move(oldPath, newPath);
                }
            }
            
        }
    }
}