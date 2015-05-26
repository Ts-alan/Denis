using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sciencecom.Models.MapJsonModels
{
    public class BillBoardJsonModel
    {
        public BillBoardJsonModel(Billboard bb)
        {
            this.Id = bb.Id;
            this.Street1 = bb.Street1;
            this.House1 = bb.House1;
            this.Street2 = bb.Street2;
            this.Comment = bb.Comment;
            this.ContractNumber = bb.ContractNumber;
            this.PassportNumber = bb.PassportNumber;
            this.StartDate = bb.StartDate;
            this.EndDate = bb.EndDate;
            this.OnAgreement = bb.OnAgreement;
            this.Latitude = bb.Latitude;
            this.Longitude = bb.Longitude;
            this.FromStreet = bb.FromStreet;
            this.Sides = bb.Sides;
            this.Owner = bb.Owner.Name.ToString();

        }
        public int Id { get; set; }

        public string Street1 { get; set; }

        public string House1 { get; set; }

        public string Street2 { get; set; }

        public string FromStreet { get; set; }

        public string Comment { get; set; }

        public string ContractNumber { get; set; }

        public string PassportNumber { get; set; }

        public System.DateTime StartDate { get; set; }

        public System.DateTime EndDate { get; set; }

        public bool OnAgreement { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }



        public string Owner { get; set; }

        public ICollection<Side> Sides { get; set; } //ToDo implement Icollection and AdType

        public AdType AdType { get; set; }
    }
}