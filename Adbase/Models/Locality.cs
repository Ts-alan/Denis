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
    
    public partial class Locality
    {
        public Locality()
        {
            this.Streets = new HashSet<Street>();
            this.AdvertisingStructures = new HashSet<AdvertisingStructure>();
        }
    
        public System.Guid id { get; set; }
        public string NameLocality { get; set; }
    
        public virtual ICollection<Street> Streets { get; set; }
        public virtual ICollection<AdvertisingStructure> AdvertisingStructures { get; set; }
    }
}
