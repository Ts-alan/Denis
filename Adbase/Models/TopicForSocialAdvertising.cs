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
    
    public partial class TopicForSocialAdvertising
    {
        public TopicForSocialAdvertising()
        {
            this.RelationshipOfAdvertisings = new HashSet<RelationshipOfAdvertising>();
            this.RelationshipOfAdvertisings1 = new HashSet<RelationshipOfAdvertising>();
        }
    
        public System.Guid id { get; set; }
        public string Name { get; set; }
        public bool Visible { get; set; }
    
        public virtual ICollection<RelationshipOfAdvertising> RelationshipOfAdvertisings { get; set; }
        public virtual ICollection<RelationshipOfAdvertising> RelationshipOfAdvertisings1 { get; set; }
    }
}
