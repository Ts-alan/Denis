using System.Web.Mvc;
using Enums.Selectors;

namespace Sciencecom.Controllers
{

    public class HomeController : Controller
    {
  
        public ActionResult Index()
        {
            Session["collectionMap"] = null;
            if (RolesList.IsInRole(RolesEnum.OnlyBillboards, User))
            {
                return RedirectToAction("BillboardsIndex");
            }
            return View();
        }

        public ActionResult BillboardsIndex()
        {
            if (!RolesList.IsInRole(RolesEnum.OnlyBillboards, User))
            {
                return RedirectToAction("Index");
            }
            return View();
        }

    //    public void FilldataBase()
    //    {
            
    //        SciencecomEntities context = new SciencecomEntities();
    //        Street maxIdSteet = context.Streets.OrderByDescending(x => x.id).FirstOrDefault();
    //        int maxId = maxIdSteet.id;
    //        HtmlDocument hFile = new HtmlDocument();
    //        hFile.Load(@"d:\Projects\AdBaseBy\adbaseby\Cities\Novopolotsk.html");
    //        var root = hFile.DocumentNode;
    //        var citiesTable = hFile.DocumentNode.SelectSingleNode("//div[@id='streets']/*[1]");
    //        var a = citiesTable.SelectNodes(".//tr/td/a");
    //        String streetName;
    //        String streetType;
    //        byte[] bytes;
    //        Street street;

    //        foreach (var ancor in a)
    //        {
    //            streetType = "";
    //            bytes = Encoding.Default.GetBytes(ancor.InnerText);
    //            streetName = Encoding.UTF8.GetString(bytes);
                
    //            if (streetName.EndsWith(" Ул.") | streetName.EndsWith(" ул."))
    //            {
    //                streetType = "ул.";
    //                streetName = streetName.Remove(streetName.Length - 4);
    //            }
    //            if (streetName.EndsWith(" Пер.") | streetName.EndsWith(" пер."))
    //            {
    //                streetType = "пер.";
    //                streetName = streetName.Remove(streetName.Length - 5);
    //            }
    //            if (streetName.EndsWith(" Пл.") | streetName.EndsWith(" пл."))
    //            {
    //                streetType = "пл.";
    //                streetName = streetName.Remove(streetName.Length - 4);
    //            }
    //            maxId ++;
    //            street = new Street()
    //            {
                   
    //                id = maxId,
    //                Street1 = streetName,
    //                Type = streetType,
    //                CityId = new Guid("e55059d4-74f7-4c2a-ab0a-bb5dd36c8de0")
    //            };

    //            //Response.Write(streetName + " +  " + streetType + " +  ");
    //            context.Streets.Add(street);
    //            context.SaveChanges();
    //        }
    //        Response.Write("Успешно внесено!");

    //    }


    }
}