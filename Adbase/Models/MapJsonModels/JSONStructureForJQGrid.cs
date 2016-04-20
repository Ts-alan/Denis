using System;
using System.Collections.Generic;
using System.Linq;

namespace Sciencecom.Models.MapJsonModels
{
    public class JSONStructureForJQGrid
    {
        public String Собственник { get; set; }
        public String Вид_конструкции { get; set; }
        public String Населенный_пункт { get; set; }
        public String Улица { get; set; }
        public String Со_стороны { get; set; }
        public String Ближайшая_по_ходу { get; set; }
        public String Дом { get; set; }
        public String Номер_опоры { get; set; }
        public String Количество_сторон { get; set; }
        public String Количестов_поверхностей { get; set; }
        public double? Площадь_конструкции { get; set; }
        public String Разреш_по { get; set; }
        public String Разреш_с { get; set; }
        public String id { get; set; }

        public String Поверхности { get; set; }

        public JSONStructureForJQGrid(AdvertisingStructure adv)
        {
           // Поверхности = new List<SurfaceJSONModel>();
            double SurfaceSumm = 0;
            int SurfaceCOunt = adv.Sides.Sum(side => side.Surfaces.Count);

            Дом = adv.House1; 
            Номер_опоры = adv.Support_; 
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
            Со_стороны = adv.FromStreet;
            Ближайшая_по_ходу = adv.Street2;
            if (adv.EndDate != null)
            {
                Разреш_по = adv.EndDate.Value.ToShortDateString();
            }
            if (adv.StartDate != null)
            {
                Разреш_с = adv.StartDate.Value.ToShortDateString();
            }
            id = adv.Id_show.ToString();
            List<Surface> sfs = adv.Sides.SelectMany(s => s.Surfaces).ToList();
            Поверхности += "<select role='select' class='rowDropdown'>";
            for (int i = 0; i < sfs.Count; i++)
            {
                Поверхности += "<option value='" + (i + 1) + "'>" + (sfs[i].isFreeOrSocial == true ? "П. " + (i + 1) + " занята" : "П. " + (i + 1) + " свободна") + "</option>";
            }
            Поверхности += "</select>";

        }
    }
}