using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sciencecom.Models.MapJsonModels
{
    public class BilboardConstructionJsonModel
    {
        public BilboardConstructionJsonModel(AdvertisingStructure mc, List<Surface> surfaces)
        {
            this.Height = (float)mc.Height;
            this.Breadth = (float)mc.Breadth;
            this.Street1 = mc.Street1;
            this.House1 = mc.House1;
            this.Street2 = mc.Street2;
            this.FromStreet = mc.FromStreet;
            this.StartDate = mc.StartDate.Value;
            this.EndDate = mc.EndDate.Value;
            this.Locality = mc.Locality;
         
            this.Owner_Id = mc.Owner_Id.Value;
            this.Id = mc.Id;
            this.Id_show = mc.Id_show;
            this.OwnerName = mc.OwnerName;
            this.Surfaces = surfaces.Select(a => a.Id);
        }

        public Nullable<float> Height { get; set; }
        public Nullable<float> Breadth { get; set; }
        public string Street1 { get; set; }
        public string House1 { get; set; }
        public string Street2 { get; set; }
        public string FromStreet { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public string Locality { get; set; }
       
        public int Owner_Id { get; set; }
        public System.Guid Id { get; set; }
        public int Id_show { get; set; }
        public string OwnerName { get; set; }

        public IEnumerable<int> Surfaces { get; set; }
        public IEnumerable<bool> IsSocial { get; set; }
    }
}