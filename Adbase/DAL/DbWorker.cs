using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Sciencecom.Models;
using Sciencecom.Models.MapJsonModels;

namespace Sciencecom.DAL
{

    public class DbWorker
    {
        public SciencecomEntities Context = new SciencecomEntities();

        public AdvertisingStructure RetrieveStructure(int? id)
        {
            AdvertisingStructure data = new AdvertisingStructure();
            data = Context.AdvertisingStructures.SingleOrDefault(a => a.Id_show == id);
            return data;
        }

        public JSONTableData SearchAdvertisingDesign(int page, string sidx, string sord,
            int rows, string собственник, string видКонструкции, string населенныйПункт,
            string улица, string домНомерОпоры, string количествоСторон, string количестовПоверхностей,
            string площадьКонструкции, string разрешПо)

        {

            JSONStructureForJQGrid js;
            JSONTableData jd = new JSONTableData();
            jd.Page = page.ToString();
            jd.SortColumn = sidx;
            jd.SortOrder = sord;

            jd.Data = new List<JSONStructureForJQGrid>();
            
            var adbvertisingList = Context.AdvertisingStructures.AsQueryable();

            List<AdvertisingStructure> finalList = new List<AdvertisingStructure>();

            if (!string.IsNullOrWhiteSpace(собственник))
            {
                adbvertisingList = adbvertisingList.Where(x => x.Owner.Name.Contains(собственник));
            }
            if (!string.IsNullOrWhiteSpace(видКонструкции))
            {
                adbvertisingList =
                    adbvertisingList.Where(x => x.TypeOfAdvertisingStructure.Name.Contains(видКонструкции));
            }
            if (!string.IsNullOrWhiteSpace(населенныйПункт))
            {
                adbvertisingList = adbvertisingList.Where(x => x.Locality.NameLocality.Contains(населенныйПункт));
            }
            if (!string.IsNullOrWhiteSpace(улица))
            {
                adbvertisingList = adbvertisingList.Where(x => x.Street1.Contains(улица));
            }
            if (!string.IsNullOrWhiteSpace(домНомерОпоры))
            {
                adbvertisingList =
                    adbvertisingList.Where(
                        x => x.Support_.Contains(домНомерОпоры) | x.House1.Contains(домНомерОпоры));
            }
            if (!string.IsNullOrWhiteSpace(количествоСторон))
            {
                adbvertisingList = adbvertisingList.Where(x => x.Sides.Count().ToString() == количествоСторон);
            }
            if (!string.IsNullOrWhiteSpace(количестовПоверхностей))
            {
                adbvertisingList = adbvertisingList.Where(x => CountSurfaces(x).ToString() == количестовПоверхностей);
            }
            if (!string.IsNullOrWhiteSpace(разрешПо))
            {
                adbvertisingList = adbvertisingList.Where(x => x.EndDate.ToString().Contains(разрешПо));
            }
            finalList = adbvertisingList.ToList();
            if (!string.IsNullOrWhiteSpace(площадьКонструкции))
            {
                double sq = double.Parse(площадьКонструкции, CultureInfo.InvariantCulture);
                List<AdvertisingStructure> templist = new List<AdvertisingStructure>();
                foreach (AdvertisingStructure advertisingStructure in adbvertisingList)
                {
                    if (sq == CountSquare(advertisingStructure))
                    {
                        templist.Add(advertisingStructure);
                    }
                }
                finalList = finalList.Intersect(templist).ToList();

            }


            int adsCount = finalList.Count;
            double del = adsCount / rows;
            jd.Total = (int)Math.Ceiling(del) + 1;
            var filteredLifst = finalList.OrderBy(o => o.Id).Skip((page - 1) * rows).Take(rows).ToList();

            foreach (AdvertisingStructure structure in filteredLifst)
            {
                js = new JSONStructureForJQGrid(structure);
                jd.Data.Add(js);
            }

            return jd;
        }

        public void DeleteAdvertisingDesign(int? id)
        {
            AdvertisingStructure mc = Context.AdvertisingStructures.Single(a => a.Id_show == id);
            Context.ListUniqueNumbers.Add(new ListUniqueNumber() { UniqueNumber = mc.UniqueNumber, Code_id = mc.Code, TimeOpen = DateTime.Now });
            foreach (var side in mc.Sides)
            {
                if (side.Surfaces.Count > 0)
                {
                    Context.Surfaces.RemoveRange(side.Surfaces);
                }
            }
            if (mc.Sides.Count > 0)
            {
                Context.Sides.RemoveRange(mc.Sides);
                Context.SaveChanges();
            }
            Context.AdvertisingStructures.Remove(mc);
            Context.SaveChanges();
            //try
            //{
            //}
            //catch (DbEntityValidationException e)
            //{
            //    foreach (DbEntityValidationResult validationError in e.EntityValidationErrors)
            //    {
            //        Response.Write("Object: " + validationError.Entry.Entity.ToString());
            //        Response.Write("");
            //        foreach (DbValidationError err in validationError.ValidationErrors)
            //        {
            //            Response.Write(err.ErrorMessage + "");
            //        }
            //    }
            //}
        }


        public IEnumerable<AdvertisingStructure> SearchAdversing(AdvertisingStructure advertisin, string owner,
            string typeOfAdvertisingStructure, string locality, int? countSize, string backlight, string endDate, int? areaConstruction)
        {
           
            var ownerId = Context.Owners.SingleOrDefault(a => a.Name.ToLower().Contains(owner.ToLower()));
            var typeOfAdvertisingStructureId =
                Context.TypeOfAdvertisingStructures.SingleOrDefault(a => a.Name.ToLower().Contains(typeOfAdvertisingStructure.ToLower()));
            var localityId = Context.TypeOfAdvertisingStructures.SingleOrDefault(a => a.Name.ToLower().Contains(locality.ToLower()));
            List<Backlight> backlights = null;
            if (backlight != null && backlight != "")
                backlights = Context.Backlights.Where(a => a.Name.Contains(backlight)).ToList();
            IEnumerable<AdvertisingStructure> result;
            //var Foo = context.AdvertisingStructures.ToList();
            if (countSize != null)
            {
                result = Context.AdvertisingStructures.Where(a => a.Sides.Count == countSize && a.Breadth != null && a.Height != null).ToList();
            }
            else
            {
                result = Context.AdvertisingStructures.Where(a => a.Breadth != null && a.Height != null).ToList();
            }
            if (areaConstruction != null)
            {
                double sum;
                result = result.Where(a =>
                    {
                        sum = 0;
                        foreach (var side in a.Sides)
                        {
                            foreach (var surface in side.Surfaces)
                            {
                                sum += surface.Space;
                            }

                        }
                        if (areaConstruction == sum)
                        {
                            return true;
                        }
                        return false;
                    });
            }
            if (ownerId != null)
            {
                result = result.Where(m => m.Owner.Id == ownerId.Id);
            }
            if (typeOfAdvertisingStructureId != null)
            {
                result = result.Where(m => m.Code == typeOfAdvertisingStructureId.Code);
            }
            if (localityId != null)
            {
                result = result.Where(m => m.Locality_id == localityId.id);
            }
            if (backlights != null)
            {
                result = result.Where(m =>
                       {
                           if (m.Backlight_Id.HasValue)
                           {
                               if (backlights.Select(a => a.id.ToString()).Contains(m.Backlight_Id.ToString()))
                                   return true;
                           }
                           return false;

                       }).ToList();
            }
            if (!string.IsNullOrEmpty(advertisin.Street1))
            {
                result = result.Where(m => m.Street1.ToLower().Contains(advertisin.Street1.ToLower()));
            }
            if (!string.IsNullOrEmpty(advertisin.UniqueNumber))
            {
                result = result.Where(m => m.UniqueNumber.ToLower().Contains(advertisin.UniqueNumber.ToLower()));
            }
            if (!string.IsNullOrEmpty(advertisin.House1))
            {
                result = result.Where(m => m.House1.ToLower().Contains(advertisin.House1.ToLower()));
            }
            if (!string.IsNullOrEmpty(advertisin.C_ContractFinancialManagement))
            {
                result = result.Where(m => m.C_ContractFinancialManagement.ToLower().Contains(advertisin.C_ContractFinancialManagement.ToLower()));
            }
            if (!string.IsNullOrEmpty(advertisin.C_PassportAdvertising))
            {
                result = result.Where(m => m.C_PassportAdvertising.ToLower().Contains(advertisin.C_PassportAdvertising.ToLower()));
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                result = result.Where(m => m.EndDate.ToString().ToLower().Trim().Contains(endDate));
            }



            return result;
        }

        public void AddSurfaces(List<Surface> listSurface)
        {
            Context.Surfaces.AddRange(listSurface);
        }

        protected internal int CountSurfaces(AdvertisingStructure adv)
        {
            return adv.Sides.Sum(side => side.Surfaces.Count);
        }

        private double CountSquare(AdvertisingStructure adv)
        {
            return adv.Sides.SelectMany(side => side.Surfaces).Sum(surface => surface.Space);
        }
   
    }
}