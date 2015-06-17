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
        public int Height { get; set; }
        public int Width { get; set; }
        public int Space { get; set; }
        public string TypeOfAdvertising { get; set; }
        public string Story { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public int Id { get; set; }
        public Nullable<System.Guid> Side_Id { get; set; }
        public Nullable<System.Guid> Topic { get; set; }
        public Nullable<System.Guid> SubTopic { get; set; }
    
        public virtual Side Side { get; set; }
        public virtual SubtopicForSocialAdvertising SubtopicForSocialAdvertising { get; set; }
        public virtual TopicForSocialAdvertising TopicForSocialAdvertising { get; set; }
    }
}
