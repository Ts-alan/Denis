using System;
using System.ComponentModel.DataAnnotations;

namespace Sciencecom.Models
{
    [MetadataType(typeof(SurfaceConstructionMetaData))]
    public partial class Surface
    {
        public string SideOfSurface { get; set; }
    


    }

    public class SurfaceConstructionMetaData
    {
       
        public int Height { get; set; }
     
        public int Width { get; set; }
       
     
        public int Space { get; set; }
    }
  



    [MetadataType(typeof(AdvertisingStructureConstructionMetaData))]
    public partial class AdvertisingStructure
    {
        public string OwnerName { get; set; }
        public string CountSize{ get; set; }
    }



     public class AdvertisingStructureConstructionMetaData
     {
     
         [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
         [DataType(DataType.Date)]
         public Nullable<System.DateTime> TheDateOfTheContract { get; set; }
      
         public Nullable<System.Guid> Locality_id { get; set; }
   
         public string OwnerPlacements { get; set; }
       
         public Nullable<System.Guid> PropertyLocation_id { get; set; }
    
     
         public string C_ContractFinancialManagement { get; set; }

         [DataType(DataType.Date)]
         [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
         public Nullable<System.DateTime> PlannedInstallationDate { get; set; }

         
         public string C_PassportAdvertising { get; set; }

         [DataType(DataType.Date)]
         [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
         public Nullable<System.DateTime> DateOfActualInstallation { get; set; }

         [DataType(DataType.Date)]
         [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
         public Nullable<System.DateTime> DateDismantling { get; set; }
        
        public string Status { get; set; }

        
        public Nullable<float> coordH { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
         public Nullable<System.DateTime> DateOfReceiptOfTheApplication { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<System.DateTime> DateOfTakenPassport { get; set; }
              
       
        public Nullable<float> coordB { get; set; }

      
        public Nullable<int> Owner_Id { get; set; }

       
       
        [Display(Name = "Улица 1")]
        public string Street1 { get; set; }

        public string House1 { get; set; }

   
        [Display(Name = "Улица 2")]
        public string Street2 { get; set; }

        [Display(Name = "Со стороны")]
        public string FromStreet { get; set; }

        [DataType(DataType.Date)]
      
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<System.DateTime> StartDate { get; set; }
        
      
       [DataType(DataType.Date)]
       [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
       public Nullable<System.DateTime> EndDate { get; set; }

      
       public  string UniqueNumber { get; set; }

       public Nullable<System.Guid> TheElementOfTheRoadNetwork_id { get; set; }
          
      
       public Nullable<System.Guid> Status_Id { get; set; }

     
       public string Code { get; set; }
    }


}