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
    
    public partial class AdvertisingStructure
    {
        public AdvertisingStructure()
        {
            this.Sides = new HashSet<Side>();
        }
    
        public Nullable<double> Height { get; set; }
        public Nullable<double> Breadth { get; set; }
        public string Street1 { get; set; }
        public string House1 { get; set; }
        public string Street2 { get; set; }
        public string FromStreet { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<System.Guid> Locality_id { get; set; }
        public Nullable<int> Owner_Id { get; set; }
        public System.Guid Id { get; set; }
        public int Id_show { get; set; }
        public string Code { get; set; }
        public string UniqueNumber { get; set; }
        public string C_ContractFinancialManagement { get; set; }
        public string C_PassportAdvertising { get; set; }
        public Nullable<System.DateTime> PlannedInstallationDate { get; set; }
        public Nullable<System.DateTime> DateOfReceiptOfTheApplication { get; set; }
        public Nullable<System.DateTime> DateOfTakenPassport { get; set; }
        public Nullable<System.DateTime> DateOfActualInstallation { get; set; }
        public Nullable<System.DateTime> DateDismantling { get; set; }
        public Nullable<System.DateTime> RevisionDate { get; set; }
        public string Note_controller { get; set; }
        public Nullable<System.Guid> TheElementOfTheRoadNetwork_id { get; set; }
        public Nullable<System.Guid> Status_Id { get; set; }
        public string CommentOwner { get; set; }
        public string Backlight { get; set; }
        public Nullable<System.Guid> PropertyLocation_id { get; set; }
        public string OwnerPlacements { get; set; }
    
        public virtual Locality Locality { get; set; }
        public virtual Status Status { get; set; }
        public virtual TheElementOfTheRoadNetwork TheElementOfTheRoadNetwork { get; set; }
        public virtual TypeOfAdvertisingStructure TypeOfAdvertisingStructure { get; set; }
        public virtual Owner Owner { get; set; }
        public virtual ICollection<Side> Sides { get; set; }
        public virtual PropertyLocation PropertyLocation { get; set; }
    }
}
