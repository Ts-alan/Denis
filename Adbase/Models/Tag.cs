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
    
    public partial class Tag
    {
        public Tag()
        {
            this.AdvertisingStructures = new HashSet<AdvertisingStructure>();
        }
    
        public int Id { get; set; }
        public string TagText { get; set; }
    
        public virtual ICollection<AdvertisingStructure> AdvertisingStructures { get; set; }
    }
}
