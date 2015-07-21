using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Sciencecom.Controllers
{
    using Models;
    using Models.MapJsonModels;
    using System.Net;
    public class MapController : Controller
    {
        

        [Authorize]
        public ActionResult Index(string type = null, int? id = null)
        {
            if (!string.IsNullOrEmpty(type) && id.HasValue)
            {
                ViewBag.Type = type;
                ViewBag.Id = id;
            }
            return View();
        }
     
        //[HttpPost]
        //[Authorize]
        //public JsonResult GetMetal(string owner, string locality, string street1, string street2, string fromStreet, string startDay, string startMonth, string startYear, int? id = null)
        //{
        //    SciencecomEntities context = new SciencecomEntities();
        //    List<MetalConstructionJsonModel> objectsForJson = new List<MetalConstructionJsonModel>();
        //    if (id.HasValue)
        //    {
        //        var construction = context.MetalConstructions.Find(id);            //используется для отображения едининого объекта при прееходе по ссылке "Показать на карте"
        //        if (construction != null)
        //        {
        //            objectsForJson.Add(new MetalConstructionJsonModel(construction));
        //        }
        //        return Json(objectsForJson, JsonRequestBehavior.AllowGet);
        //    }
        //    DataController dataController = new DataController();
        //    int idOwner;
        //    if (owner != "")
        //    {
        //        idOwner = context.Owners.Where(m => m.Name == owner).First().Id;
        //    }
        //    else
        //    {
        //        idOwner = 0;
        //    }
        //    MetalConstruction mc = new MetalConstruction()
        //    {
        //        IdOwner = idOwner,
        //        Street1 = street1,
        //        Street2 = street2,
        //        FromStreet = fromStreet,
        //        Locality = locality
        //    };
        //    List<MetalConstruction> result = dataController.SearchMetal(mc, startDay, startMonth, startYear).ToList();
        //    for (int i = 0; i < result.Count; i++)
        //    {
        //        objectsForJson.Add(new MetalConstructionJsonModel(result[i]));
        //    }
        //    return Json(objectsForJson, JsonRequestBehavior.AllowGet);
        //}
        //[HttpPost]
        //[Authorize]
        public JsonResult GetDesign(string owner, string locality, string street1, string street2,
            string fromStreet, string startDay, string startMonth, string startYear,
            string lBillboardFinishDay, string lBillboardFinishMonth, string lBillboardFinishYear, string[] Story, bool OnAgreement, string IsBillboardSocial, int? id = null)
        {
            SciencecomEntities context = new SciencecomEntities();
            List<BilboardConstructionJsonModel> objectsForJson = new List<BilboardConstructionJsonModel>();
            List<Surface> Surfaces = new List<Surface>();
            if (id.HasValue)
            {
                //используется для отображения едининого объекта при прееходе по ссылке "Показать на карте"

                var construction = context.AdvertisingStructures.Single(a => a.Id_show == id);
                var Owner = context.Owners.Single(a => a.Id == construction.Owner_Id).Name;
                construction.OwnerName = Owner;
                var enumeratorSide = context.AdvertisingStructures.Single(a => a.Id_show == id).Sides;

                for (int i = 0; i < enumeratorSide.Count; i++)
                {
                    for (int j = 0;
                        j < context.AdvertisingStructures.Single(a => a.Id_show == id).Sides.ElementAt(i).Surfaces.Count;
                        j++)
                    {
                        Surfaces.Add(
                            context.AdvertisingStructures.Single(a => a.Id_show == id)
                                .Sides.ElementAt(i)
                                .Surfaces.ElementAt(j));
                    }
                }
                if (construction != null)
                {
                    objectsForJson.Add(new BilboardConstructionJsonModel(construction, Surfaces));
                }

                return Json(objectsForJson, JsonRequestBehavior.AllowGet);
            }
            DataController dataController = new DataController();

            AdvertisingStructure mc = new AdvertisingStructure()
            {
                //Locality = locality,
                Street1 = street1,
                Street2 = street2,
                FromStreet = fromStreet,
            
            };
            IEnumerable<AdvertisingStructure> result =
            dataController.SearchBillbord(mc, startDay, startMonth, startYear, lBillboardFinishDay,
                lBillboardFinishMonth, lBillboardFinishYear, Story, IsBillboardSocial, owner).ToList();

            foreach (var biboard in result.AsEnumerable())
            {


                foreach (var side in biboard.Sides)
                {
                    foreach (var Surface in side.Surfaces)
                    {
                        Surfaces.Add(Surface);
                    }

                }
                objectsForJson.Add(new BilboardConstructionJsonModel(biboard, Surfaces));
                Surfaces.Clear();
            }


            return Json(objectsForJson, JsonRequestBehavior.AllowGet);
        }
 

        //[HttpPost]
        //[Authorize]
        //public JsonResult GetLights(string owner, string locality, string street1, string street2,
        //    string fromStreet, string startDay, string startMonth, string startYear,
        //    string lFinishDay, string lFinishMonth, string lFinishYear, string onStatement, string isSocial, int? id = null)
        //{
        //    SciencecomEntities context = new SciencecomEntities();
        //    List<LightConstructionJsonModel> objectsForJson = new List<LightConstructionJsonModel>();
        //    if (id.HasValue)
        //    {
        //        var construction = context.LightConstructions.Find(id);         //используется для отображения едининого объекта при прееходе по ссылке "Показать на карте"
        //        if (construction != null)
        //        objectsForJson.Add(new LightConstructionJsonModel(construction));
        //        return Json(objectsForJson, JsonRequestBehavior.AllowGet);
        //    }
        //    DataController dataController = new DataController();
        //    int idOwner;
        //    if (owner != "")
        //    {
        //        idOwner = context.Owners.Where(m => m.Name == owner).First().Id;
        //    }
        //    else
        //    {
        //        idOwner = 0;
        //    }
        //    LightConstruction lc = new LightConstruction()
        //    {
        //        IdOwner = idOwner,
        //        Street1 = street1,
        //        Street2 = street2,
        //        FromStreet = fromStreet,
        //        Locality = locality
        //    };
        //    List<LightConstruction> result = dataController.SearchLight(lc, startDay, startMonth,
        //        startYear, lFinishDay, lFinishMonth, lFinishYear, onStatement, isSocial).ToList();
            
        //    for (int i = 0; i < result.Count; i++)
        //    {
        //        objectsForJson.Add(new LightConstructionJsonModel(result[i]));
        //    }
        //    return Json(objectsForJson, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //[Authorize]
        //public JsonResult GetIllegal(string locality, string street1, string street2, string fromStreet, string status, int? id = null)
        //{
        //    SciencecomEntities context = new SciencecomEntities();
        //    List<IllegalConstructionJsonModel> objectsForJson = new List<IllegalConstructionJsonModel>();
        //    if (id.HasValue)
        //    {
        //        var construction = context.IllegalConstructions.Find(id);           //используется для отображения едининого объекта при прееходе по ссылке "Показать на карте"
        //        if (construction != null)
        //        objectsForJson.Add(new IllegalConstructionJsonModel(construction));
        //        return Json(objectsForJson, JsonRequestBehavior.AllowGet);
        //    }
        //    DataController dataController = new DataController();
        //    int statusForSearch = -1;
        //    if (status == "") statusForSearch = -1;
        //    if (status == "Новый") statusForSearch = 0;
        //    if (status == "Рассматривается") statusForSearch = 1;
        //    if (status == "Нелегальный") statusForSearch = 2;
        //    if (status == "Легальный") statusForSearch = 3;
        //    if (status == "Демонтирован") statusForSearch = 4;
        //    IllegalController illegalController = new IllegalController();
        //    IEnumerable<IllegalConstruction> result = ((IEnumerable<IllegalConstruction>)illegalController.Index().ViewData.Model);
        //    if (statusForSearch != -1)
        //    {
        //        result = result.Where(x => x.Status == statusForSearch);
        //    }
        //    if (locality != "")
        //    {
        //        result = result.Where(x => x.Locality == locality);
        //    }
        //    if (street1 != "")
        //    {
        //        result = result.Where(x => x.Street1 == street1);
        //    }
        //    if (street2 != "")
        //    {
        //        result = result.Where(x => x.Street2 == street2);
        //    }
        //    if (fromStreet != "")
        //    {
        //        result = result.Where(x => x.FromStreet == fromStreet);
        //    }
        //    List<IllegalConstruction> result2 = new List<IllegalConstruction>();
        //    result2 = result.ToList();
        //    for (int i = 0; i < result2.Count; i++)
        //    {
        //        objectsForJson.Add(new IllegalConstructionJsonModel(result2[i]));
        //    }
        //    return Json(objectsForJson, JsonRequestBehavior.AllowGet);
        //}
        

        //[Authorize]
        //public ActionResult ShowMetal(int id)
        //{
        //    SciencecomEntities context = new SciencecomEntities();
        //    MetalConstruction result = context.MetalConstructions.Where(m => m.Id == id).Single();
        //    Constructions constructions = new Constructions()
        //        {
        //            MetalConstructions = context.MetalConstructions.ToList(),
        //            LightConstructions = context.LightConstructions.ToList()
        //        };
        //    constructions.MetalConstructions.Clear();
        //    constructions.LightConstructions.Clear();
        //    constructions.MetalConstructions.Add(result);
        //    return View("Index", constructions);
        //}

        //[Authorize]
        //public ActionResult ShowLight(int id)
        //{
        //    SciencecomEntities context = new SciencecomEntities();
        //    LightConstruction result = context.LightConstructions.Where(m => m.Id == id).Single();
        //    Constructions constructions = new Constructions()
        //    {
        //        MetalConstructions = context.MetalConstructions.ToList(),
        //        LightConstructions = context.LightConstructions.ToList()
        //    };
        //    constructions.MetalConstructions.Clear();
        //    constructions.LightConstructions.Clear();
        //    constructions.LightConstructions.Add(result);
        //    return View("Index", constructions);
        //}
    }
}