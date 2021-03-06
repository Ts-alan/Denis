﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sciencecom.Models
{
    [MetadataType(typeof(IllegalConstructionMetaData))]
    public partial class IllegalConstruction
    {

    }

    public partial class IllegalConstructionMetaData
    {
        [StringLength(50, ErrorMessage = "не более 50 символов")]
        [Required(ErrorMessage = "Введите значение Улица 1")]
        [Display(Name = "Улица 1")]
        public string Street1 { get; set; }

        [StringLength(50, ErrorMessage = "не более 50 символов")]
        [Display(Name = "Улица 2")]
        public string Street2 { get; set; }

        [StringLength(50, ErrorMessage = "Не более 50 символов")]
        [Display(Name = "Со стороны")]
        public string FromStreet { get; set; }

        [Display(Name = "Обнаружено")]
        public DateTime DetectionDate { get; set;}

        [Required(ErrorMessage = "Введите значение Добавлено")]
        [Display(Name = "Добавлено")]
        public DateTime AdditionDate { get; set; }

        [Required(ErrorMessage = "Введите значение Н. п.")]
        [Display(Name = "Н. п.")]
        public string Locality { get; set; }

        [Display(Name = "Принято к сведению")]
        public DateTime NotingDate { get; set; }

        [Display(Name = "Проблема решена")]
        public DateTime SolvingDate { get; set; }

        [Required(ErrorMessage = "Введите значение Стутус")]
        [Display(Name = "Статус")]
        public int Status { get; set; }

        [Display(Name = "Примечание")]
        public string Note { get; set; }

        [Display(Name = "Широта")]
        [Required(ErrorMessage = "Введите значение Широта")]
        public float Shirota { get; set; }

        [Display(Name = "Долгота")]
        [Required(ErrorMessage = "Введите значение Долгота")]
        public float Dolgota { get; set; }

    }
}