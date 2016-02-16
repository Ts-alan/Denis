using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sciencecom.Models.MapJsonModels
{
    public class JSONStructureForJQGrid
    {
        public String Собственник { get; set; }
        public String Вид_конструкции { get; set; }
        public String Населенный_пункт { get; set; }
        public String Улица { get; set; }
        public String Дом_Номер_опоры { get; set; }
        public String Количество_сторон { get; set; }
        public String Количестов_поверхностей { get; set; }
        public String Площадь_конструкции { get; set; }
        public String Разреш_по { get; set; }
        public String id { get; set; }


        public JSONStructureForJQGrid(AdvertisingStructure adv)
        {
            int SurfaceCOunt = 0;
            int SurfaceSumm = 0;
            foreach (var side in adv.Sides)
            {
                SurfaceCOunt += side.Surfaces.Count;
                foreach (var Surface in side.Surfaces)
                {
                    SurfaceSumm += Surface.Space;
                }

            }
            
            Дом_Номер_опоры = adv.Code == "BB" ? adv.House1 : adv.Support_; ;
            Площадь_конструкции = SurfaceSumm.ToString();
            Количество_сторон = adv.Sides.Count.ToString();
            Количестов_поверхностей = SurfaceCOunt.ToString();
            if (adv.Owner != null)
            {
                Собственник = adv.Owner.Name;
            }
            
            Вид_конструкции = adv.TypeOfAdvertisingStructure.Name;
            Населенный_пункт = adv.Locality.NameLocality;
            Улица = adv.Street1;
            Разреш_по = adv.EndDate.ToString();
            id = adv.Id_show.ToString();

        }
    }
}