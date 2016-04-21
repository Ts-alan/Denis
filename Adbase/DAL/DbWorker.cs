﻿using System;
using System.Collections.Generic;
using System.Linq;
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
            int rows, string Собственник, string Вид_конструкции, string Населенный_пункт,
            string Улица, string Со_стороны, string Ближайшая_по_ходу, string Дом, string Номер_опоры, string Количество_сторон, 
            string Количестов_поверхностей, string Площадь_конструкции, string Разреш_по, string НачалоРазреш_по, 
            string КонецРазреш_по, string Разреш_с, string НачалоРазреш_с, string КонецРазреш_с, bool? Искать_поверхности)

        {

            JSONStructureForJQGrid js;
            JSONTableData jd = new JSONTableData();
            jd.Page = page.ToString();
            jd.SortColumn = sidx;
            jd.SortOrder = sord;

            jd.Data = new List<JSONStructureForJQGrid>();

            var adbvertisingList = Context.AdvertisingStructures.AsQueryable();

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
            if (!string.IsNullOrWhiteSpace(Со_стороны))
            {
                adbvertisingList = adbvertisingList.Where(x => x.FromStreet.Contains(Со_стороны));
            }
            if (!string.IsNullOrWhiteSpace(Ближайшая_по_ходу))
            {
                adbvertisingList = adbvertisingList.Where(x => x.Street2.Contains(Ближайшая_по_ходу));
            }
            if (!string.IsNullOrWhiteSpace(Дом))
            {
                adbvertisingList = adbvertisingList.Where(x => !string.IsNullOrWhiteSpace(x.House1) && x.House1.Contains(Дом));
            }
            if (!string.IsNullOrWhiteSpace(Номер_опоры))
            {
                adbvertisingList = adbvertisingList.Where(x => x.Support_.Contains(Номер_опоры));
            }
            int w;
            if (!string.IsNullOrWhiteSpace(Количество_сторон) && Int32.TryParse(Количество_сторон, out w))
            {
                adbvertisingList = adbvertisingList.Where(x => x.Sides.Count() == w);
            }
            int v;
            if (!string.IsNullOrWhiteSpace(Количестов_поверхностей) && Int32.TryParse(Количестов_поверхностей, out v))
            {
                adbvertisingList = adbvertisingList.Where(x => x.Sides.SelectMany(side => side.Surfaces).Count() == v);
            }
            DateTime ee;
            DateTime se;
            if (DateTime.TryParse(НачалоРазреш_по, out ee) && DateTime.TryParse(КонецРазреш_по, out se) && !string.IsNullOrWhiteSpace(НачалоРазреш_по) && !string.IsNullOrWhiteSpace(КонецРазреш_по))
            {
                DateTime beginEndRange = DateTime.Parse(НачалоРазреш_по);
                DateTime endEndRange = DateTime.Parse(КонецРазреш_по);
                if (beginEndRange <= endEndRange)
                {
                    adbvertisingList = adbvertisingList.Where(x => x.EndDate >= beginEndRange && x.EndDate <= endEndRange);
                }
               
            }
            if (DateTime.TryParse(НачалоРазреш_по, out ee)  && !string.IsNullOrWhiteSpace(НачалоРазреш_по) && string.IsNullOrWhiteSpace(КонецРазреш_по))
            {
                DateTime beginEndRange = DateTime.Parse(НачалоРазреш_по);
                adbvertisingList = adbvertisingList.Where(x => x.EndDate >= beginEndRange);
            }
            if (DateTime.TryParse(КонецРазреш_по, out se) && string.IsNullOrWhiteSpace(НачалоРазреш_по) && !string.IsNullOrWhiteSpace(КонецРазреш_по))
            {
                DateTime endEndRange = DateTime.Parse(КонецРазреш_по);
                adbvertisingList = adbvertisingList.Where(x => x.EndDate <= endEndRange);
            }
            DateTime eb;
            DateTime sb;
            if (DateTime.TryParse(НачалоРазреш_с, out eb) && DateTime.TryParse(КонецРазреш_с, out sb) && !string.IsNullOrWhiteSpace(НачалоРазреш_с) && !string.IsNullOrWhiteSpace(КонецРазреш_с))
            {
                DateTime beginStartRange = DateTime.Parse(НачалоРазреш_с);
                DateTime endStartRange = DateTime.Parse(КонецРазреш_с);
                if (beginStartRange <= endStartRange)
                {
                    adbvertisingList = adbvertisingList.Where(x => x.StartDate >= beginStartRange && x.StartDate <= endStartRange);
                }
                
            }
            if (DateTime.TryParse(НачалоРазреш_с, out eb) && !string.IsNullOrWhiteSpace(НачалоРазреш_с) && string.IsNullOrWhiteSpace(КонецРазреш_с))
            {
                DateTime beginStartRange = DateTime.Parse(НачалоРазреш_с);
                adbvertisingList = adbvertisingList.Where(x => x.StartDate >= beginStartRange );
            }
            if (DateTime.TryParse(КонецРазреш_с, out sb) && string.IsNullOrWhiteSpace(НачалоРазреш_с) && !string.IsNullOrWhiteSpace(КонецРазреш_с))
            {
                DateTime endStartRange = DateTime.Parse(КонецРазреш_с);
                adbvertisingList = adbvertisingList.Where(x => x.StartDate <= endStartRange);
            }
            DateTime r;
            if (!string.IsNullOrWhiteSpace(Разреш_по) && DateTime.TryParse(Разреш_по, out r))
            {
                DateTime o = DateTime.Parse(Разреш_по);
                adbvertisingList = adbvertisingList.Where(x => x.EndDate == o);
            }
            DateTime s;
            if (!string.IsNullOrWhiteSpace(Разреш_с) && DateTime.TryParse(Разреш_с, out s))
            {
                DateTime l = DateTime.Parse(Разреш_с);
                adbvertisingList = adbvertisingList.Where(x => x.StartDate == l);
            }
            double n;
            if (!string.IsNullOrWhiteSpace(Площадь_конструкции) && double.TryParse(Площадь_конструкции, out n))
            {
                adbvertisingList = adbvertisingList.Where(x => x.Area == n);
            }
            if (Искать_поверхности == true)
            {
                adbvertisingList = adbvertisingList.Where(x => x.Sides.Any(side => side.Surfaces.Any(o => o.isFreeOrSocial == true)));
            }
            finalList = adbvertisingList.ToList();
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
            if (mc.UniqueNumber == null)
            {
                mc.UniqueNumber = TableAdapterExtensions.StringSymvol();
            }
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
                //try
                //{
                    
                //}
                //catch (DbEntityValidationException dbEx)
                //{
                //    foreach (var validationErrors in dbEx.EntityValidationErrors)
                //    {
                //        foreach (var validationError in validationErrors.ValidationErrors)
                //        {
                //            Trace.TraceInformation("Property: {0} Error: {1}",
                //                                    validationError.PropertyName,
                //                                    validationError.ErrorMessage);
                //        }
                //    }
                //}
            }
            Context.AdvertisingStructures.Remove(mc);
            Context.SaveChanges();
            //try
            //{
                
            //}
            //catch (DbEntityValidationException dbEx)
            //{
            //    foreach (var validationErrors in dbEx.EntityValidationErrors)
            //    {
            //        foreach (var validationError in validationErrors.ValidationErrors)
            //        {
            //            Trace.TraceInformation("Property: {0} Error: {1}",
            //                                    validationError.PropertyName,
            //                                    validationError.ErrorMessage);
            //        }
            //    }
            //}
           
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
            string typeOfAdvertisingStructure, string locality, int? countSize, string backlight, string startEndDate, 
            string endEndDate, string startStartDate, string endStartDate, int? areaConstruction,int? CountSurface,
            string house1, string support)
        {
           
            var ownerId = Context.Owners.SingleOrDefault(a => a.Name.ToLower().Contains(owner.ToLower()));
            
            var typeOfAdvertisingStructureId =
                Context.TypeOfAdvertisingStructures.Where(a => a.Name.ToLower().Contains(typeOfAdvertisingStructure.ToLower())).ToList();
            var localityId = Context.Localities.Where(a => a.NameLocality.ToLower().Contains(locality.ToLower())).ToList();
            List<Backlight> backlights = null;
            if (!string.IsNullOrEmpty(backlight))
                backlights = Context.Backlights.Where(a => a.Name.Contains(backlight)).ToList();
            IEnumerable<AdvertisingStructure> result;
            
            if (countSize != null)
            {
                result = Context.AdvertisingStructures.Where(a => a.Sides.Count == countSize && a.coordB != null && a.coordH != null).ToList();
            }
            else
            {
                result = Context.AdvertisingStructures.Where(a => a.coordB != null && a.coordH != null).ToList();
            }
            if (CountSurface != null)
            {
                result= result.Where(x => x.Sides.SelectMany(side => side.Surfaces).Count() == CountSurface);
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
            if (ownerId != null )
            {
                result = result.Where(m =>
                {
                    if (m.Owner != null)
                    {
                        return m.Owner.Id == ownerId.Id;
                    }
                    else
                    {
                        return false;
                    }
                });
            }
            else
            {
                if (owner != "")
                {
                    result = new List<AdvertisingStructure>();
                }
            }
            if (typeOfAdvertisingStructureId.Count != 0)
            {
                List<Sciencecom.Models.AdvertisingStructure> collList=new List<AdvertisingStructure>();
                for (int i = 0; i < typeOfAdvertisingStructureId.Count(); i++)
                {
                    collList.AddRange(result.Where(m => m.Code == typeOfAdvertisingStructureId.ElementAt(i).Code));
                }
                result = collList;
            }
 
            if (localityId.Count() != 0)
            {
                List<AdvertisingStructure> tempValue = new List<AdvertisingStructure>();
                foreach (var i in localityId)
                {
                    tempValue.AddRange(result.Where(m => m.Locality_id == i.id));
                }
                result = tempValue;
            }
            else
            {
                if (locality!="")
                {
                    result = new List<AdvertisingStructure>();
                }
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
                result = result.Where(m =>
                {
                    if (m.Street1 != null)
                    {
                        return m.Street1.ToLower().Contains(advertisin.Street1.ToLower());
                    }
                    else
                    {
                        return false;
                    }
                });
            }
            if (!string.IsNullOrEmpty(advertisin.UniqueNumber))
            {
                result = result.Where(m => m.UniqueNumber.ToLower().Contains(advertisin.UniqueNumber.ToLower()));
            }
            if (!string.IsNullOrEmpty(advertisin.House1))
            {
                List<AdvertisingStructure> tempValue = new List<AdvertisingStructure>();
                tempValue.AddRange(result.Where(m =>
                {
                    if (m.Support_ != null)
                    {
                        return m.Support_.ToLower().Contains(advertisin.House1.ToLower());
                    }
                    else
                    {
                        return false;
                    }
                }));
                tempValue.AddRange(result.Where(m =>
                {
                    if (m.House1 != null)
                    {
                        return m.House1.ToLower().Contains(advertisin.House1.ToLower());
                    }
                    else
                    {
                        return false;
                    }
                }));
                result = tempValue;
            }
            if (!string.IsNullOrEmpty(advertisin.C_ContractFinancialManagement))
            {
                result = result.Where(m => m.C_ContractFinancialManagement.ToLower().Contains(advertisin.C_ContractFinancialManagement.ToLower()));
            }
            if (!string.IsNullOrEmpty(advertisin.C_PassportAdvertising))
            {
                result = result.Where(m => m.C_PassportAdvertising.ToLower().Contains(advertisin.C_PassportAdvertising.ToLower()));
            }
            DateTime ee;
            DateTime se;
            if (DateTime.TryParse(startEndDate, out ee) && DateTime.TryParse(endEndDate, out se) && !string.IsNullOrWhiteSpace(startEndDate) && !string.IsNullOrWhiteSpace(endEndDate))
            {
                DateTime beginEndRange = DateTime.Parse(startEndDate);
                DateTime endEndRange = DateTime.Parse(endEndDate);
                if (beginEndRange < endEndRange)
                {
                    result = result.Where(x => x.EndDate >= beginEndRange && x.EndDate <= endEndRange);
                }

            }
            if (DateTime.TryParse(startEndDate, out ee) && !string.IsNullOrWhiteSpace(startEndDate) && string.IsNullOrWhiteSpace(endEndDate))
            {
                DateTime beginEndRange = DateTime.Parse(startEndDate);
                result = result.Where(x => x.EndDate >= beginEndRange);
            }
            if (DateTime.TryParse(endEndDate, out se) && string.IsNullOrWhiteSpace(startEndDate) && !string.IsNullOrWhiteSpace(endEndDate))
            {
                DateTime endEndRange = DateTime.Parse(endEndDate);
                result = result.Where(x => x.EndDate <= endEndRange);
            }
            DateTime eb;
            DateTime sb;
            if (DateTime.TryParse(startStartDate, out eb) && DateTime.TryParse(endStartDate, out sb) && !string.IsNullOrWhiteSpace(startStartDate) && !string.IsNullOrWhiteSpace(endStartDate))
            {
                DateTime beginStartRange = DateTime.Parse(startStartDate);
                DateTime endStartRange = DateTime.Parse(endStartDate);
                if (beginStartRange < endStartRange)
                {
                    result = result.Where(x => x.StartDate >= beginStartRange && x.StartDate <= endStartRange);
                }

            }
            if (DateTime.TryParse(startStartDate, out eb) && !string.IsNullOrWhiteSpace(startStartDate) && string.IsNullOrWhiteSpace(startEndDate))
            {
                DateTime beginStartRange = DateTime.Parse(startStartDate);
                result = result.Where(x => x.StartDate >= beginStartRange);
            }
            if (DateTime.TryParse(endStartDate, out sb) && string.IsNullOrWhiteSpace(startStartDate) && !string.IsNullOrWhiteSpace(endStartDate))
            {
                DateTime endStartRange = DateTime.Parse(endStartDate);
                result = result.Where(x => x.StartDate <= endStartRange);
            }
            if (!string.IsNullOrWhiteSpace(house1))
            {
                result = result.Where(x => !string.IsNullOrWhiteSpace(x.House1) && x.House1.Contains(house1));
            }
            if (!string.IsNullOrWhiteSpace(support))
            {
                result = result.Where(x => !string.IsNullOrWhiteSpace(x.Support_) && x.Support_.Contains(support));
            }
         
            return result;
        }

    
        
        protected internal int CountSurfaces(AdvertisingStructure adv)
        {
            return adv.Sides.Sum(side => side.Surfaces.Count);
        }

        private double CountSquare(List<Surface> surfaces)
        {
            return surfaces.Sum(surface => surface.Space);
        }

     



        public void CreateAdvertisingDesign(AdvertisingStructure structure, List<Side> sides, List<Surface> surfaces, int countSize = 0)
        {
            //удаление временного номера из базы данных
            DeleteTempId(structure.UniqueNumber);
            ProcessStructureSidesAndSurfaces(sides, surfaces, countSize, structure);
        }
        internal void DeleteTempId(string uniqueNumber)
        {
            if (Context.ListUniqueNumbers.Any(a => a.UniqueNumber == uniqueNumber))
            {
                Context.ListUniqueNumbers.RemoveRange(Context.ListUniqueNumbers.Where(x => x.UniqueNumber == uniqueNumber));
            }
        }

        protected void ProcessStructureSidesAndSurfaces(List<Side> sides, List<Surface> surfaces, int countSize, AdvertisingStructure structure)
        {
            if (countSize > 0)
            {
                for (int j = 0; j < countSize; j++)
                {
                    sides[j].AdvertisingStructures_Id = structure.Id;
                    sides[j].Name = (j + 1).ToString();
                    sides[j].Id = Guid.NewGuid();
                }

                Context.Sides.AddRange(sides);
                structure.Area = CountSquare(surfaces);
                Context.AdvertisingStructures.Add(structure);
                List<Surface> listSurface = new List<Surface>();
                foreach (var i in surfaces)
                {
                    i.Side_Id = sides.Single(a => a.Name == i.SideOfSurface).Id;
                    listSurface.Add(i);
                }

                Context.Surfaces.AddRange(listSurface);
                Context.SaveChanges();
            }
            else
            {
                if (surfaces != null)
                {
                    structure.Area = CountSquare(surfaces);
                }
                Context.AdvertisingStructures.Add(structure);
                Context.SaveChanges();
            }
        }

        protected void EditStructure(AdvertisingStructure oldStructure, AdvertisingStructure newStructure, int countSize, List<Side> sides, List<Surface> surfaces)
        {
            var tempId = oldStructure.Id;

            foreach (var side in oldStructure.Sides)
            {
                Context.Surfaces.RemoveRange(side.Surfaces);
            }

            Context.Sides.RemoveRange(oldStructure.Sides);
            Context.AdvertisingStructures.Remove(oldStructure);
            Context.SaveChanges();
            if (countSize > 0)
            {
                for (int j = 0; j < countSize; j++)
                {
                    try
                    {
                        sides[j].AdvertisingStructures_Id = oldStructure.Id;
                    }
                    catch (Exception e)
                    {
                        sides.Add(new Side() { DirectionSide_id = new Guid("27b8c509-8f09-4a0d-ae22-048c2611b7ea") });
                    }

                    sides[j].AdvertisingStructures_Id = oldStructure.Id;
                    sides[j].Name = (j + 1).ToString();
                    sides[j].Id = Guid.NewGuid();
                }
                newStructure.Id = tempId;
                newStructure.Area = CountSquare(surfaces);

                Context.AdvertisingStructures.Add(newStructure);

                Context.Sides.AddRange(sides);

                Context.SaveChanges();

                List<Surface> listSurface = new List<Surface>();
                foreach (var i in surfaces)
                {
                    i.Side_Id = sides.Single(a => a.Name == i.SideOfSurface).Id;
                    listSurface.Add(i);
                }
                Context.Surfaces.AddRange(listSurface);

                Context.SaveChanges();
            }
            else
            {
                newStructure.Area = CountSquare(surfaces);
                Context.AdvertisingStructures.Add(newStructure);
                Context.SaveChanges();
            }
        }

        public IQueryable<Street> FindStreets(string term)
        {
            var streets = from m in Context.Streets where m.Street1.Contains(term) select m;
            return streets;
        }
        public Owner Owner(string name)
        {
            return Context.Owners.First(m => m.Name == name);
        }
    }
}