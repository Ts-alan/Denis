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
    
    public partial class Side
    {
        public Side()
        {
            this.Surfaces = new HashSet<Surface>();
        }
    
        public string Name { get; set; }
        public System.Guid Billboard_Id { get; set; }
        public System.Guid Id { get; set; }
    
        public virtual Billboards1 Billboards1 { get; set; }
        public virtual ICollection<Surface> Surfaces { get; set; }
    }
}
