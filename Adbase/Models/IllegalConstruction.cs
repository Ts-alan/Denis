//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sciencecom.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class IllegalConstruction
    {
        public int Id { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string FromStreet { get; set; }
        public Nullable<System.DateTime> DetectionDate { get; set; }
        public System.DateTime AdditionDate { get; set; }
        public string Locality { get; set; }
        public Nullable<System.DateTime> NotingDate { get; set; }
        public Nullable<System.DateTime> SolvingDate { get; set; }
        public int Status { get; set; }
        public string Note { get; set; }
        public float Shirota { get; set; }
        public float Dolgota { get; set; }
        public string WhoAdded { get; set; }
        public string WhoTakeNote { get; set; }
        public string WhoLastEdited { get; set; }
    }
}
