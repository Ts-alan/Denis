
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
    
public partial class Identification
{

    public Identification()
    {

        this.Sides = new HashSet<Side>();

    }


    public System.Guid id { get; set; }

    public string IdentificationName { get; set; }



    public virtual ICollection<Side> Sides { get; set; }

}

}
