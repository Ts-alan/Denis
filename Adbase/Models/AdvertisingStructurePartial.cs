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
    public partial class Side
    {
        public HttpPostedFileBase SeveralPhoto { get; set; }


    }


    public class SurfaceConstructionMetaData
    {
        [Required(ErrorMessage = "Введите значение начальной даты")]
        public int Height { get; set; }
        [Required(ErrorMessage = "Введите значение начальной даты")]
        public int Width { get; set; }
        [Required(ErrorMessage = "Введите значение начальной даты")]
        public int Space { get; set; }
    }

     [MetadataType(typeof(AdvertisingStructureConstructionMetaData))]
    public partial class AdvertisingStructure
    {
        public string OwnerName { get; set; }

    }

     public class AdvertisingStructureConstructionMetaData
    {
        
        public string Status { get; set; }

        [Required]
        public Nullable<float> Height { get; set; }


     
        public Nullable<float> Breadth { get; set; }

        [Required(ErrorMessage = "Введите значение ")]
        public Nullable<int> Owner_Id { get; set; }
        [StringLength(50, ErrorMessage = "не более 50 символов")]
        [Required(ErrorMessage = "Введите значение Улица 1")]
        [Display(Name = "Улица 1")]
        public string Street1 { get; set; }

        [Required(ErrorMessage = "Введите значение Дома")]
        public string House1 { get; set; }

        [StringLength(50, ErrorMessage = "не более 50 символов")]
        [Required(ErrorMessage = "Введите значение Улица 2")]
        [Display(Name = "Улица 2")]
        public string Street2 { get; set; }

        [StringLength(50, ErrorMessage = "Не более 50 символов")]
        [Required(ErrorMessage = "Введите значение Со стороны")]
        [Display(Name = "Со стороны")]
        public string FromStreet { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Введите значение начальной даты")]
        public Nullable<System.DateTime> StartDate { get; set; }
        
       [Required(ErrorMessage = "Введите значение даты")]
       [DataType(DataType.Date)]
       public Nullable<System.DateTime> EndDate { get; set; }
 
       [Required(ErrorMessage = "Введите значение уникального ключа")]
       public string UniqueNumber { get; set; }

       [Required(ErrorMessage = "Выберете значение")]
       public Nullable<System.Guid> TheElementOfTheRoadNetwork_id { get; set; }
          
       [Required(ErrorMessage = "Выберете значение")]
       public Nullable<System.Guid> Status_Id { get; set; }
    }
}