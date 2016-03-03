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
        public SciencecomEntities context = new SciencecomEntities();
        public JSONTableData SearchAdvertisingDesign(int page, string sidx, string sord,
            int rows, string Собственник, string Вид_конструкции, string Населенный_пункт,
            string Улица, string Дом_Номер_опоры, string Количество_сторон, string Количестов_поверхностей,
            string Площадь_конструкции, string Разреш_по)

        {

            JSONStructureForJQGrid js;
            JSONTableData jd = new JSONTableData();
            jd.Page = page.ToString();
            jd.SortColumn = sidx;
            jd.SortOrder = sord;

            jd.Data = new List<JSONStructureForJQGrid>();
            
            var adbvertisingList = context.AdvertisingStructures.AsQueryable();

            List<AdvertisingStructure> finalList = new List<AdvertisingStructure>();

            if (!string.IsNullOrWhiteSpace(Собственник))
            {
                adbvertisingList = adbvertisingList.Where(x => x.Owner.Name.Contains(Собственник));
            }
            if (!string.IsNullOrWhiteSpace(Вид_конструкции))
            {
                adbvertisingList =
                    adbvertisingList.Where(x => x.TypeOfAdvertisingStructure.Name.Contains(Вид_конструкции));
            }
            if (!string.IsNullOrWhiteSpace(Населенный_пункт))
            {
                adbvertisingList = adbvertisingList.Where(x => x.Locality.NameLocality.Contains(Населенный_пункт));
            }
            if (!string.IsNullOrWhiteSpace(Улица))
            {
                adbvertisingList = adbvertisingList.Where(x => x.Street1.Contains(Улица));
            }
            if (!string.IsNullOrWhiteSpace(Дом_Номер_опоры))
            {
                adbvertisingList =
                    adbvertisingList.Where(
                        x => x.Support_.Contains(Дом_Номер_опоры) | x.House1.Contains(Дом_Номер_опоры));
            }
            if (!string.IsNullOrWhiteSpace(Количество_сторон))
            {
                adbvertisingList = adbvertisingList.Where(x => x.Sides.Count().ToString() == Количество_сторон);
            }
            if (!string.IsNullOrWhiteSpace(Количестов_поверхностей))
            {
                adbvertisingList = adbvertisingList.Where(x => CountSurfaces(x).ToString() == Количестов_поверхностей);
            }
            if (!string.IsNullOrWhiteSpace(Разреш_по))
            {
                adbvertisingList = adbvertisingList.Where(x => x.EndDate.ToString().Contains(Разреш_по));
            }
            finalList = adbvertisingList.ToList();
            if (!string.IsNullOrWhiteSpace(Площадь_конструкции))
            {
                double sq = double.Parse(Площадь_конструкции, CultureInfo.InvariantCulture);
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
            AdvertisingStructure mc = context.AdvertisingStructures.Single(a => a.Id_show == id);
            context.ListUniqueNumbers.Add(new ListUniqueNumber() { UniqueNumber = mc.UniqueNumber, Code_id = mc.Code, TimeOpen = DateTime.Now });
            foreach (var side in mc.Sides)
            {
                if (side.Surfaces.Count > 0)
                {
                    context.Surfaces.RemoveRange(side.Surfaces);
                }
            }
            if (mc.Sides.Count > 0)
            {
                context.Sides.RemoveRange(mc.Sides);
                context.SaveChanges();
            }
            context.AdvertisingStructures.Remove(mc);
            context.SaveChanges();
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


        public IEnumerable<AdvertisingStructure> SearchAdversing(AdvertisingStructure Advertisin, string owner,
            string TypeOfAdvertisingStructure, string Locality, int? CountSize, string Backlight, string EndDate, int? AreaConstruction)
        {
           
            var owner_id = context.Owners.SingleOrDefault(a => a.Name.ToLower().Contains(owner.ToLower()));
            var typeOfAdvertisingStructure_id =
                context.TypeOfAdvertisingStructures.SingleOrDefault(a => a.Name.ToLower().Contains(TypeOfAdvertisingStructure.ToLower()));
            var locality_id = context.TypeOfAdvertisingStructures.SingleOrDefault(a => a.Name.ToLower().Contains(Locality.ToLower()));
            List<Backlight> backlights = null;
            if (Backlight != null && Backlight != "")
                backlights = context.Backlights.Where(a => a.Name.Contains(Backlight)).ToList();
            IEnumerable<AdvertisingStructure> result;
            //var Foo = context.AdvertisingStructures.ToList();
            if (CountSize != null)
            {
                result = context.AdvertisingStructures.Where(a => a.Sides.Count == CountSize && a.Breadth != null && a.Height != null).ToList();
            }
            else
            {
                result = context.AdvertisingStructures.Where(a => a.Breadth != null && a.Height != null).ToList();
            }
            if (AreaConstruction != null)
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
                        if (AreaConstruction == sum)
                        {
                            return true;
                        }
                        return false;
                    });
            }
            if (owner_id != null)
            {
                result = result.Where(m => m.Owner.Id == owner_id.Id);
            }
            if (typeOfAdvertisingStructure_id != null)
            {
                result = result.Where(m => m.Code == typeOfAdvertisingStructure_id.Code);
            }
            if (locality_id != null)
            {
                result = result.Where(m => m.Locality_id == locality_id.id);
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
            if (!string.IsNullOrEmpty(Advertisin.Street1))
            {
                result = result.Where(m => m.Street1.ToLower().Contains(Advertisin.Street1.ToLower()));
            }
            if (!string.IsNullOrEmpty(Advertisin.UniqueNumber))
            {
                result = result.Where(m => m.UniqueNumber.ToLower().Contains(Advertisin.UniqueNumber.ToLower()));
            }
            if (!string.IsNullOrEmpty(Advertisin.House1))
            {
                result = result.Where(m => m.House1.ToLower().Contains(Advertisin.House1.ToLower()));
            }
            if (!string.IsNullOrEmpty(Advertisin.C_ContractFinancialManagement))
            {
                result = result.Where(m => m.C_ContractFinancialManagement.ToLower().Contains(Advertisin.C_ContractFinancialManagement.ToLower()));
            }
            if (!string.IsNullOrEmpty(Advertisin.C_PassportAdvertising))
            {
                result = result.Where(m => m.C_PassportAdvertising.ToLower().Contains(Advertisin.C_PassportAdvertising.ToLower()));
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                result = result.Where(m => m.EndDate.ToString().ToLower().Trim().Contains(EndDate));
            }



            return result;
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