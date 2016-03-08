using System;
using System.Linq;

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
        public double? Площадь_конструкции { get; set; }
        public String Разреш_по { get; set; }
        public String id { get; set; }


        public JSONStructureForJQGrid(AdvertisingStructure adv)
        {
            double SurfaceSumm = 0;
            int SurfaceCOunt = adv.Sides.Sum(side => side.Surfaces.Count);

            Дом_Номер_опоры = adv.Code == "BB" | adv.Code == "UI"  ? adv.House1 : adv.Support_; ;
            Площадь_конструкции = adv.Area;
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