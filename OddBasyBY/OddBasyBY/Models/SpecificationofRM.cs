//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OddBasyBY.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SpecificationofRM
    {
        public int id { get; set; }
        public string length { get; set; }
        public string area { get; set; }
        public int Street_id { get; set; }
        public string Mackup { get; set; }
    
        public virtual Street Street { get; set; }
    }
}
