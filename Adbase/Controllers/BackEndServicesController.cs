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
        public IQueryable<SubtopicForSocialAdvertising> GetSubTopic(Guid id)
        {
         
            using (SciencecomEntities _ctx =new SciencecomEntities())
            {
                var Subtopic = _ctx.RelationshipOfAdvertisings.Where(a => a.idOfTopicForSocialAdvertising == id).Select(a=>a.idOfSubtopicForSocialAdvertising).ToList();
                var test = _ctx.SubtopicForSocialAdvertisings.ToList();
                IQueryable<SubtopicForSocialAdvertising> GuidSubtopic=null;
                foreach (var i in Subtopic)
                {
                    GuidSubtopic = _ctx.SubtopicForSocialAdvertisings.Where(a => a.id.ToString().Contains(i.ToString()));   
                }
                return GuidSubtopic;
            }
        }
    }
}