using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sciencecom.Models.MapJsonModels
{
    public class LightConstructionJsonModel
    {
        public LightConstructionJsonModel(LightConstruction lc)
        {
            this.Id = lc.Id;
            this.IdOwner = lc.IdOwner;
            this.Support = lc.Support;
            this.Street1 = lc.Street1;
            this.Street2 = lc.Street2;
            this.FromStreet = lc.FromStreet;
            this.StartDate = lc.StartDate.ToShortDateString();
            this.FinishDate = lc.FinishDate.ToShortDateString();
            this.IsSocial = lc.IsSocial == true ? "Социальная" : "Не социальная";
            this.OnStatement = lc.OnStatement;
            this.Locality = lc.Locality;
            this.IdWhoAdded = lc.IdWhoAdded;
            this.DateOfSocial = lc.DateOfSocial;
            this.Shirota = lc.Shirota;
            this.Dolgota = lc.Dolgota;
            this.Owner = lc.Owner.Name.ToString();
            this.OwnerWhoAdded = lc.OwnerWhoAdded.Name.ToString();
            this.HasPhoto = System.IO.File.Exists("~/Images/Light/photo" + lc.Id + ".jpg");   
        }
        public int Id { get; set; }
        public int IdOwner { get; set; }
        public int Support { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string FromStreet { get; set; }
        public string StartDate { get; set; }
        public string FinishDate { get; set; }
        public string IsSocial { get; set; }
        public bool OnStatement { get; set; }
        public string Locality { get; set; }
        public int IdWhoAdded { get; set; }
        public string DateOfSocial { get; set; }
        public float Shirota { get; set; }
        public float Dolgota { get; set; }
        public string Owner { get; set; }
        public string OwnerWhoAdded { get; set; }
        public bool HasPhoto { get; set; }
    }
}