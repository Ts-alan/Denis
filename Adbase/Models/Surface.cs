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
        public double Height { get; set; }
        public double Width { get; set; }
        public double Space { get; set; }
        public int Id { get; set; }
        public Nullable<System.Guid> Side_Id { get; set; }
        public Nullable<int> NumberSurface { get; set; }
        public Nullable<System.DateTime> RentFrom { get; set; }
        public Nullable<System.DateTime> RentUntil { get; set; }
        public string Theme { get; set; }
        public string Type { get; set; }
        public Nullable<bool> isFreeOrSocial { get; set; }
    
        public virtual Side Side { get; set; }
    }
}
