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
    
    public partial class Surface
    {
        public Nullable<int> Height { get; set; }
        public Nullable<int> Width { get; set; }
        public Nullable<int> Space { get; set; }
        public int Id { get; set; }
        public Nullable<System.Guid> Side_Id { get; set; }
        public Nullable<System.Guid> Identification_id { get; set; }
        public Nullable<int> NumberSurface { get; set; }
    
        public virtual Identification Identification { get; set; }
        public virtual Side Side { get; set; }
    }
}
