using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sciencecom.Models
{
    [MetadataType(typeof(SurfaceConstructionMetaData))]
    public partial class Surface
    {
        public string SideOfSurface { get; set; }
        public HttpPostedFileBase SeveralPhoto{ get; set;}

        //public Guid Id_Bilboard { get; set; }
    }

    public  class SurfaceConstructionMetaData
    {
        [Required(ErrorMessage = "Введите значение начальной даты")]
        public int Height { get; set; }
         [Required(ErrorMessage = "Введите значение начальной даты")]
        public int Width { get; set; }
         [Required(ErrorMessage = "Введите значение начальной даты")]
        public int Space { get; set; }
         [Required(ErrorMessage = "Введите значение начальной даты")]
        public string TypeOfAdvertising { get; set; }
        public string Story { get; set; }
        [Required(ErrorMessage = "Введите значение начальной даты")]
        public System.DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Введите значение конечной даты")]
        public System.DateTime EndDate { get; set; }
        public int Id { get; set; }
      
    }

     [MetadataType(typeof(AdvertisingStructureConstructionMetaData))]
    public partial class AdvertisingStructure
    {
        public string OwnerName { get; set; }

    }

     public class AdvertisingStructureConstructionMetaData
    {
        
        public string Status { get; set; }

        [Required(ErrorMessage = "Введите значение Широта")]
        public Nullable<float> Height { get; set; }
        
        [Required(ErrorMessage = "Введите значение Долгота")]
        public Nullable<float> Breadth { get; set; }

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
      
        public string Comment { get; set; }

       [DataType(DataType.Date)]
       [Required(ErrorMessage = "Введите значение начальной даты")]
       public string StartDate { get; set; }

       [Required(ErrorMessage = "Введите значение Населенный пункт")]
       public string Locality { get; set; }
        
       [Required(ErrorMessage = "Введите значение даты")]
       [DataType(DataType.Date)]
       public Nullable<System.DateTime> EndDate { get; set; }

       [Required(ErrorMessage = "Введите значение даты")]
       [DataType(DataType.Date)]
       public System.DateTime DateOfReceiptOfTheApplication { get; set; }

       [Required(ErrorMessage = "Введите значение даты")]
       [DataType(DataType.Date)]
       public System.DateTime DateOfTakenPassport { get; set; }
      
       [Required(ErrorMessage = "Введите значение даты")]
       [DataType(DataType.Date)]
       public System.DateTime DateOfActualInstallation { get; set; }

       [Required(ErrorMessage = "Введите значение даты")]
       [DataType(DataType.Date)]
       public System.DateTime DateDismantling { get; set; }

       [Required(ErrorMessage = "Введите значение даты")]
       [DataType(DataType.Date)]
       public System.DateTime RevisionDate { get; set; }
       
       [Required(ErrorMessage = "Введите значение комментария")]
       public string Note_controller { get; set; }
    }
}