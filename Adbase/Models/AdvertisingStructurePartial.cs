using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sciencecom.Models
{
    [MetadataType(typeof(SurfaceConstructionMetaData))]
    public partial class Surface
    {
        public string SideOfSurface { get; set; }
    


    }

    public class SurfaceConstructionMetaData
    {
        [Required(ErrorMessage = "Введите значение ")]
        public int Height { get; set; }
        [Required(ErrorMessage = "Введите значение ")]
        public int Width { get; set; }
        [Required(ErrorMessage = "Введите значение ")]
        public int Space { get; set; }
    }


    [MetadataType(typeof(AdvertisingStructureConstructionMetaData))]
    public partial class AdvertisingStructure
    {
        public string OwnerName { get; set; }

    }

     public class AdvertisingStructureConstructionMetaData

    {
         [Required(ErrorMessage = "Введите значение ")]
         public string Backlight { get; set; }

         [Required(ErrorMessage = "Введите значение ")]
         public System.Guid PropertyLocation_id { get; set; }

         [DataType(DataType.Date)]
         public Nullable<System.DateTime> PlannedInstallationDate { get; set; }

         [DataType(DataType.Date)]
         public Nullable<System.DateTime> RevisionDate { get; set; }

         [Required(ErrorMessage = "Введите значение ")]
         public string C_PassportAdvertising { get; set; }

         [DataType(DataType.Date)]

         public Nullable<System.DateTime> DateOfActualInstallation { get; set; }

         [DataType(DataType.Date)]
   
         public Nullable<System.DateTime> DateDismantling { get; set; }
        
        public string Status { get; set; }

        [Required]
        public Nullable<float> Height { get; set; }

        [DataType(DataType.Date)]
         public Nullable<System.DateTime> DateOfReceiptOfTheApplication { get; set; }

        [DataType(DataType.Date)]

        public Nullable<System.DateTime> DateOfTakenPassport { get; set; }
     
        public Nullable<float> Breadth { get; set; }

        [Required(ErrorMessage = "Введите значение ")]
        public Nullable<int> Owner_Id { get; set; }
        [StringLength(50, ErrorMessage = "не более 50 символов")]
        [Required(ErrorMessage = "Введите значение")]
        [Display(Name = "Улица 1")]
        public string Street1 { get; set; }

        [Required(ErrorMessage = "Введите значение")]
        public string House1 { get; set; }

        [StringLength(50, ErrorMessage = "не более 50 символов")]
        [Required(ErrorMessage = "Введите значение")]
        [Display(Name = "Улица 2")]
        public string Street2 { get; set; }

        [StringLength(50, ErrorMessage = "Не более 50 символов")]
        [Required(ErrorMessage = "Введите значение")]
        [Display(Name = "Со стороны")]
        public string FromStreet { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Введите значение")]
        public Nullable<System.DateTime> StartDate { get; set; }
        
       [Required(ErrorMessage = "Введите значение даты")]
       [DataType(DataType.Date)]
       public Nullable<System.DateTime> EndDate { get; set; }
 
       [Required(ErrorMessage = "Введите значение ")]
       public string UniqueNumber { get; set; }

       [Required(ErrorMessage = "Выберете значение")]
       public Nullable<System.Guid> TheElementOfTheRoadNetwork_id { get; set; }
          
       [Required(ErrorMessage = "Выберете значение")]
       public Nullable<System.Guid> Status_Id { get; set; }
    }
}