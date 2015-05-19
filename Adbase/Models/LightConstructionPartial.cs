using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sciencecom.Models
{
    [MetadataType(typeof(LightConstructionMetaData))]
    public partial class LightConstruction
    {

    }
    public partial class LightConstructionMetaData 
    {
        public int Id { get; set; }
        public int IdOwner { get; set; }

        [Range(0, 1000000, ErrorMessage = "Значение должно быть неотрицательным")]
        [Display(Name = "Опора №")]
        public int Support { get; set; }

        [StringLength(50, ErrorMessage = "не более 50 символов")]
        [Required(ErrorMessage = "Введите значение Улица 1")]
        [Display(Name = "Улица 1")]
        public string Street1 { get; set; }

        [StringLength(50, ErrorMessage = "не более 50 символов")]
        [Required(ErrorMessage = "Введите значение Улица 2")]
        [Display(Name = "Улица 2")]
        public string Street2 { get; set; }

        [Required(ErrorMessage = "Введите значение Н. п.")]
        [Display(Name = "Н.п.")]
        public string Locality { get; set; }

        [StringLength(50, ErrorMessage = "Не более 50 символов")]
        [Required(ErrorMessage = "Введите значение Со стороны")]
        [Display(Name = "Со стороны")]
        public string FromStreet { get; set; }

        [Required(ErrorMessage = "Введите значение Разрешено с")]
        [Display(Name = "Разреш. с")]
        public System.DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Введите значение Разрешено до")]
        [Display(Name = "Разреш. до")]
        public System.DateTime FinishDate { get; set; }

        [Display(Name = "Соц-ная")]
        public bool IsSocial { get; set; }

        [Display(Name = "Заявление")]
        public bool OnStatement { get; set; }

        [Required(ErrorMessage = "Введите значение Владелец")]
        [Display(Name = "Владелец")]
        public virtual Owner Owner { get; set; }

        [Display(Name = "Широта")]
        [Required(ErrorMessage = "Введите значение Широта")]
        public float Shirota { get; set; }

        [Display(Name = "Долгота")]
        [Required(ErrorMessage = "Введите значение Долгота")]
        public float Dolgota { get; set; }
    }
}