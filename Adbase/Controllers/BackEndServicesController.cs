using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Sciencecom.Models;

namespace Sciencecom.Controllers
{
    public class BackEndServicesController : Controller
    {
        
        //[Authorize]
        //public string GetSubTopic(Guid id)
        //{
         
        //    using (SciencecomEntities _ctx =new SciencecomEntities())
        //    {
        //        var Subtopic = _ctx.RelationshipOfAdvertisings.Where(a => a.idOfTopicForSocialAdvertising == id).Select(a=>a.idOfSubtopicForSocialAdvertising).ToList();
        //        IEnumerable GuidSubtopic = null;
        //        foreach (var i in Subtopic)
        //        {
        //            GuidSubtopic = _ctx.SubtopicForSocialAdvertisings.AsEnumerable().Where(a => Subtopic.Contains(a.id)).Select(a=>new {a.id,a.Name});   
        //        }

        //        var result = JsonConvert.SerializeObject(GuidSubtopic, Formatting.Indented, new JsonSerializerSettings
        //        {
        //            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        //        });
        //        return result;
        //    }
        //}
        [HttpPost]
        public void SaveSearch(string collectionMap)
        {
            Session["collectionMap"] = collectionMap;
          
        }
    }
}