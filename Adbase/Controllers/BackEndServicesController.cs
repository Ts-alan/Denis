using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sciencecom.Models;

namespace Sciencecom.Controllers
{
    public class BackEndServicesController : Controller
    {
        
        [Authorize]
        public List<SubtopicForSocialAdvertising> GetSubTopic(Guid id)
        {
         
            using (SciencecomEntities _ctx =new SciencecomEntities())
            {
                var Subtopic = _ctx.RelationshipOfAdvertisings.Where(a => a.idOfTopicForSocialAdvertising == id).Select(a=>a.idOfSubtopicForSocialAdvertising).ToList();
                List<SubtopicForSocialAdvertising> GuidSubtopic = null;
                var t = _ctx.SubtopicForSocialAdvertisings.ToList();
                foreach (var i in Subtopic)
                {
                    GuidSubtopic = _ctx.SubtopicForSocialAdvertisings.AsEnumerable().Where(a => a.id.ToString().Contains(Subtopic.ToString())).ToList();   
                }
                return GuidSubtopic;
            }
        }
    }
}