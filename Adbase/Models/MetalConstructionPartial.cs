using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sciencecom.Models
{
    [MetadataType(typeof(MetalConstructionMetaData))]
    public partial class MetalConstruction
    {
    
    }

    public class MetalConstructionMetaData
    {
        public int Id { get; set; }
        public int IdOwner { get; set; }
        [Display(Name = "Опора №")]
        [Range(0, 1000000, ErrorMessage = "Значение должно быть неотрицательным")]
        [Required(ErrorMessage = "Введите значение")]
        public int Support { get; set; }
        [StringLength(50, ErrorMessage = "не более 50 символов")]
        [Required(ErrorMessage = "Введите значение")]
        [Display(Name = "Улица 1")]
        public string Street1 { get; set; }
        [StringLength(50, ErrorMessage = "не более 50 символов")]
        [Required(ErrorMessage = "Введите значение")]
        [Display(Name = "Улица 2")]
        public string Street2 { get; set; }
        [Required(ErrorMessage = "Введите значение")]
        [Display(Name = "Н.п.")]
        public string Locality { get; set; }
        [StringLength(50, ErrorMessage = "не более 50 символов")]
        [Required(ErrorMessage = "Введите значение")]
        [Display(Name = "Со стороны")]
        public string FromStreet { get; set; }
        [Required(ErrorMessage = "Введите значение")]
        [Display(Name = "Дата согласования")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Введите значение")]
        [Display(Name = "Владелец")]
        public virtual Owner Owner { get; set; }
    }
}