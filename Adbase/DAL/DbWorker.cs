using System;
using System.Collections.Generic;
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

        //public JSONTableData SearchAdvertisingDesign(int page, string sidx, string sord,
        //    int rows, string Собственник, string Вид_конструкции, string Населенный_пункт,
        //    string Улица, string Дом_Номер_опоры, string Количество_сторон, string Количестов_поверхностей,
        //    string Площадь_конструкции, string Разреш_по)

        //{

        //    JSONStructureForJQGrid js;
        //    JSONTableData jd = new JSONTableData();
        //    jd.Page = page.ToString();
        //    jd.SortColumn = sidx;
        //    jd.SortOrder = sord;

        //    jd.Data = new List<JSONStructureForJQGrid>();
        //    //var adbvertisingList = context.AdvertisingStructures.OrderBy(o => o.Id).Skip((page - 1)*rows).Take(rows).ToList();
        //    var adbvertisingList = context.AdvertisingStructures.AsQueryable();

        //    List<AdvertisingStructure> finalList = new List<AdvertisingStructure>();

        //    if (!string.IsNullOrWhiteSpace(Собственник))
        //    {
        //        adbvertisingList = adbvertisingList.Where(x => x.Owner.Name.Contains(Собственник));
        //    }
        //    if (!string.IsNullOrWhiteSpace(Вид_конструкции))
        //    {
        //        adbvertisingList =
        //            adbvertisingList.Where(x => x.TypeOfAdvertisingStructure.Name.Contains(Вид_конструкции));
        //    }
        //    if (!string.IsNullOrWhiteSpace(Населенный_пункт))
        //    {
        //        adbvertisingList = adbvertisingList.Where(x => x.Locality.NameLocality.Contains(Населенный_пункт));
        //    }
        //    if (!string.IsNullOrWhiteSpace(Улица))
        //    {
        //        adbvertisingList = adbvertisingList.Where(x => x.Street1.Contains(Улица));
        //    }
        //    if (!string.IsNullOrWhiteSpace(Дом_Номер_опоры))
        //    {
        //        adbvertisingList =
        //            adbvertisingList.Where(
        //                x => x.Support_.Contains(Дом_Номер_опоры) | x.House1.Contains(Дом_Номер_опоры));
        //    }
        //    if (!string.IsNullOrWhiteSpace(Количество_сторон))
        //    {
        //        adbvertisingList = adbvertisingList.Where(x => x.Sides.Count().ToString() == Количество_сторон);
        //    }
        //    if (!string.IsNullOrWhiteSpace(Количестов_поверхностей))
        //    {
        //        adbvertisingList = adbvertisingList.Where(x => CountSurfaces(x).ToString() == Количестов_поверхностей);
        //    }
        //    if (!string.IsNullOrWhiteSpace(Разреш_по))
        //    {
        //        adbvertisingList = adbvertisingList.Where(x => x.EndDate.ToString().Contains(Разреш_по));
        //    }
        //    finalList = adbvertisingList.ToList();
        //    if (!string.IsNullOrWhiteSpace(Площадь_конструкции))
        //    {
        //        double sq = double.Parse(Площадь_конструкции, CultureInfo.InvariantCulture);
        //        List<AdvertisingStructure> templist = new List<AdvertisingStructure>();
        //        foreach (AdvertisingStructure advertisingStructure in adbvertisingList)
        //        {
        //            if (sq == CountSquare(advertisingStructure))
        //            {
        //                templist.Add(advertisingStructure);
        //            }
        //        }
        //        finalList = finalList.Intersect(templist).ToList();

        //    }


        //    int adsCount = finalList.Count;
        //    double del = adsCount/rows;
        //    jd.Total = (int) Math.Ceiling(del) + 1;
        //    var filteredLifst = finalList.OrderBy(o => o.Id).Skip((page - 1)*rows).Take(rows).ToList();

        //    foreach (AdvertisingStructure structure in filteredLifst)
        //    {
        //        js = new JSONStructureForJQGrid(structure);
        //        jd.Data.Add(js);
        //    }

        //    return jd;
        //}
    }
}