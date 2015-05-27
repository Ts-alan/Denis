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
    
    public partial class Billboards1
    {
        public Billboards1()
        {
            this.Sides = new HashSet<Side>();
        }
    
        public Nullable<float> Height { get; set; }
        public Nullable<float> Breadth { get; set; }
        public string Street1 { get; set; }
        public string House1 { get; set; }
        public string Street2 { get; set; }
        public string FromStreet { get; set; }
        public string Comment { get; set; }
        public string ContractNumber { get; set; }
        public string PassportNumber { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public string Locality { get; set; }
        public bool OnAgreement { get; set; }
        public int Owner_Id { get; set; }
        public System.Guid Id { get; set; }
    
        public virtual Owner Owner { get; set; }
        public virtual ICollection<Side> Sides { get; set; }
    }
}
